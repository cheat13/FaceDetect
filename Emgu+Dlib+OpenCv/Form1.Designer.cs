namespace Emgu_Dlib_OpenCv
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.camera = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.SuccessMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.camera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // camera
            // 
            this.camera.Location = new System.Drawing.Point(0, 0);
            this.camera.Margin = new System.Windows.Forms.Padding(6);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(508, 625);
            this.camera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camera.TabIndex = 0;
            this.camera.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 563);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 49);
            this.button1.TabIndex = 7;
            this.button1.Text = "🙈";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // picture
            // 
            this.picture.BackColor = System.Drawing.Color.Transparent;
            this.picture.Location = new System.Drawing.Point(379, 12);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(117, 146);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture.TabIndex = 16;
            this.picture.TabStop = false;
            // 
            // checkedListBox
            // 
            this.checkedListBox.BackColor = System.Drawing.SystemColors.Window;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(297, 504);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(199, 108);
            this.checkedListBox.TabIndex = 17;
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.ErrorMsg.Location = new System.Drawing.Point(300, 476);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(144, 25);
            this.ErrorMsg.TabIndex = 18;
            this.ErrorMsg.Text = "☹ ไม่พบใบหน้า";
            this.ErrorMsg.Visible = false;
            // 
            // SuccessMsg
            // 
            this.SuccessMsg.AutoSize = true;
            this.SuccessMsg.BackColor = System.Drawing.Color.Transparent;
            this.SuccessMsg.ForeColor = System.Drawing.Color.Green;
            this.SuccessMsg.Location = new System.Drawing.Point(386, 161);
            this.SuccessMsg.Name = "SuccessMsg";
            this.SuccessMsg.Size = new System.Drawing.Size(110, 25);
            this.SuccessMsg.TabIndex = 19;
            this.SuccessMsg.Text = "😉 สำเร็จ !!!";
            this.SuccessMsg.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 624);
            this.Controls.Add(this.SuccessMsg);
            this.Controls.Add(this.ErrorMsg);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.camera);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.camera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox camera;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.Label SuccessMsg;
    }
}

