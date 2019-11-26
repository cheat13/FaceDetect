using DlibDotNet;
using OpenCvSharp;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using DlibDotNet.Extensions;
using System.Runtime.InteropServices;
using HeadPose;
using Rectangle = DlibDotNet.Rectangle;
using Point = OpenCvSharp.Point;
using System.Diagnostics;

namespace Emgu_Dlib_OpenCv
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private FrontalFaceDetector fd;
        private ShapePredictor sp;
        private MatOfPoint3d model;
        private MatOfDouble coeffs;
        private MatOfPoint3d poseModel;
        private MatOfPoint2d poseProjection;
        private bool start;

        private int[] checker;
        private int step;
        private string[] text;

        private Stopwatch stopwatch;
        private int timeset;
        private int countdown;

        private Size size;
        private Point point;
        private Rect rect;

        public Form1()
        {
            InitializeComponent();
            this.capture = new VideoCapture(0);
            this.frame = new Mat();
            this.fd = Dlib.GetFrontalFaceDetector();
            this.sp = ShapePredictor.Deserialize(@"C:\Users\trago\OneDrive\Desktop\OpenCV\shape_predictor_68_face_landmarks.dat");
            this.model = Utility.GetFaceModel();
            this.coeffs = new MatOfDouble(4, 1);
            this.coeffs.SetTo(0);
            this.poseModel = new MatOfPoint3d(1, 1, new Point3d(0, 0, 1000));
            this.poseProjection = new MatOfPoint2d();
            this.checker = new int[4] { 100, -10, 10, 0 };
            this.text = new string[4] { "1. เอาหน้าใส่กรอบ", "2. ก้มหน้าเล็กน้อย", "3. เงยหน้าเล็กน้อย", "4. มองตรง" };
            this.timeset = 3;
            this.size = new Size(250, 300);
            SetStart();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            capture.Read(frame);

            this.point = new Point((frame.Width - size.Width) / 2, (frame.Height - size.Height) / 2);
            this.rect = new Rect(point, size);

            Cv2.Flip(frame, frame, FlipMode.Y);

            if (!frame.Empty() && start)
            {
                var img = ConvertToArray2D(frame);

                var faces = fd.Operator(img);

                if (faces.Any(face => IsFaceInFrame(face)))
                {
                    foreach (var face in faces)
                    {
                        if (IsFaceInFrame(face))
                        {
                            //Dlib.DrawRectangle(img, face, color: new RgbPixel(0, 255, 255), thickness: 4);
                            var shape = sp.Detect(img, face);

                            var landmarks = new MatOfPoint2d(1, 6,
                                (from i in new int[] { 30, 8, 36, 45, 48, 54 }
                                 let pt = shape.GetPart((uint)i)
                                 select new OpenCvSharp.Point2d(pt.X, pt.Y)).ToArray());

                            var cameraMatrix = Utility.GetCameraMatrix((int)img.Rect.Width, (int)img.Rect.Height);

                            Mat rotation = new MatOfDouble();
                            Mat translation = new MatOfDouble();
                            Cv2.SolvePnP(model, landmarks, cameraMatrix, coeffs, rotation, translation);

                            var euler = Utility.GetEulerMatrix(rotation);

                            var yaw = 180 * euler.At<double>(0, 2) / Math.PI;
                            var pitch = 180 * euler.At<double>(0, 1) / Math.PI;
                            pitch = Math.Sign(pitch) * 180 - pitch;

                            Cv2.ProjectPoints(poseModel, rotation, translation, cameraMatrix, coeffs, poseProjection);

                            //var landmark = landmarks.At<Point2d>(0);
                            //var p = poseProjection.At<Point2d>(0);
                            //Dlib.DrawLine(
                            //    img,
                            //    new DlibDotNet.Point((int)landmark.X, (int)landmark.Y),
                            //    new DlibDotNet.Point((int)p.X, (int)p.Y),
                            //    color: new RgbPixel(0, 255, 255));

                            //foreach (var i in new int[] { 30, 8, 36, 45, 48, 54 })
                            //{
                            //    var point = shape.GetPart((uint)i);
                            //    var rect = new Rectangle(point);
                            //    Dlib.DrawRectangle(img, rect, color: new RgbPixel(255, 255, 0), thickness: 4);
                            //}
                            for (var i = 0; i < shape.Parts; i++)
                            {
                                var point = shape.GetPart((uint)i);
                                var rect = new Rectangle(point);
                                Dlib.DrawRectangle(img, rect, color: new RgbPixel(0, 255, 255), thickness: 4);
                            }

                            CheckFace(pitch, frame, face, yaw, pitch);
                            frame = img.ToBitmap().ToMat();
                        }
                    }
                }
                else if (this.step > 0)
                {
                    SetStart();
                    this.ErrorMsg.Visible = true;
                }
            }

            Cv2.Rectangle(frame, rect, Scalar.Yellow, thickness: 2);
            camera.Image = frame.ToBitmap();
        }

        private Array2D<RgbPixel> ConvertToArray2D(Mat frame)
        {
            var array = new byte[frame.Width * frame.Height * frame.ElemSize()];
            Marshal.Copy(frame.Data, array, 0, array.Length);
            var img = Dlib.LoadImageData<RgbPixel>(array, (uint)frame.Height, (uint)frame.Width, (uint)(frame.Width * frame.ElemSize()));

            return img;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.start == true)
            {
                SetStart();
            }
            else
            {
                button1.Text = "🙉";
                this.start = true;
                this.ErrorMsg.Visible = false;
                this.SuccessMsg.Visible = false;
            }
        }

        private void SetStart()
        {
            this.button1.Text = "🙈";
            this.start = false;
            this.step = 0;
            this.countdown = 3;
            this.stopwatch = new Stopwatch();
            this.stopwatch.Stop();
            this.checkedListBox.Items.Clear();
            this.checkedListBox.Items.AddRange(this.text);
        }

        private bool IsFaceInFrame(Rectangle face)
        {

            var percent = (double)(face.Area) / (this.size.Width * this.size.Height);
            return this.rect.Left < face.Center.X && face.Center.X < this.rect.Right &&
            this.rect.Top < face.Center.Y && face.Center.Y < this.rect.Bottom &&
            Math.Abs(1 - percent) < 0.5;
        }

        private void CheckFace(double picth, Mat frame, Rectangle face, double yaw, double pitch)
        {
            if (this.countdown == 0)
            {
                this.picture.Image = frame.ToBitmap();
                this.SuccessMsg.Visible = true;
                SetStart();
            }
            else if (this.step == this.checker.Length)
            {
                this.stopwatch.Start();
                CountDown(face);
                if (!IsForntFace(yaw, pitch)) SetStart();
            }
            else if (this.step == 0 && IsFaceInFrame(face) || Math.Abs(this.checker[this.step] - picth) <= 5)
            {
                checkedListBox.SetItemChecked(this.step, true);
                this.step++;
            }
        }

        private void CountDown(Rectangle face)
        {
            var time = this.stopwatch.Elapsed.TotalSeconds;
            this.countdown = this.timeset - (int)this.stopwatch.Elapsed.TotalSeconds;
            checkedListBox.Items.RemoveAt(3);
            checkedListBox.Items.Insert(3, $"4. มองตรง ({this.countdown} วินาที)");
            checkedListBox.SetItemChecked(3, true);
        }

        private bool IsForntFace(double yaw, double pitch)
        {
            return Math.Abs(yaw) <= 15 && Math.Abs(pitch) <= 15;
        }
    }
}
