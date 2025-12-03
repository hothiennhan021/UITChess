namespace AccountUI
{
    partial class Signup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Signup));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            txtEmail = new TextBox();
            textBox1 = new TextBox();
            pictureBox3 = new PictureBox();
            textBox3 = new TextBox();
            pictureBox4 = new PictureBox();
            textBox4 = new TextBox();
            btnSignup = new Button();
            button2 = new Button();
            label4 = new Label();
            button_passwordhide = new Button();
            button_passwordhide2 = new Button();
            button_passwordshow = new Button();
            button_passwordshow2 = new Button();
            txtFullName = new TextBox();
            dtpBirthday = new DateTimePicker();
            txtOtp = new TextBox();
            btnVerifyOtp = new Button();
            btnSendOtp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(520, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(39, 39);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = Properties.Resources.Password;
            pictureBox2.Location = new Point(520, 308);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(39, 39);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Times New Roman", 16.2F);
            txtEmail.ForeColor = SystemColors.GrayText;
            txtEmail.Location = new Point(566, 57);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(395, 39);
            txtEmail.TabIndex = 1;
            txtEmail.Text = "Email";
            txtEmail.Enter += textBox2_Enter;
            txtEmail.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Times New Roman", 16.2F);
            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Location = new Point(566, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(395, 39);
            textBox1.TabIndex = 0;
            textBox1.Text = "Tên Đăng Nhập";
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.White;
            pictureBox3.Image = Properties.Resources.Password;
            pictureBox3.Location = new Point(520, 364);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(39, 39);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 22;
            pictureBox3.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Times New Roman", 16.2F);
            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Location = new Point(566, 308);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(395, 39);
            textBox3.TabIndex = 4;
            textBox3.Text = "Mật Khẩu";
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.White;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(520, 12);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(39, 39);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 25;
            pictureBox4.TabStop = false;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Times New Roman", 16.2F);
            textBox4.ForeColor = SystemColors.GrayText;
            textBox4.Location = new Point(566, 364);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(395, 39);
            textBox4.TabIndex = 5;
            textBox4.Text = "Xác Nhận Mật Khẩu";
            textBox4.Enter += textBox4_Enter;
            textBox4.Leave += textBox4_Leave;
            // 
            // btnSignup
            // 
            btnSignup.AutoSize = true;
            btnSignup.BackColor = Color.DarkSlateBlue;
            btnSignup.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnSignup.ForeColor = Color.White;
            btnSignup.Location = new Point(648, 424);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(224, 54);
            btnSignup.TabIndex = 6;
            btnSignup.Text = "Đăng Ký";
            btnSignup.UseVisualStyleBackColor = false;
            btnSignup.Click += button1_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.DarkSlateBlue;
            button2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button2.ForeColor = Color.White;
            button2.Location = new Point(648, 488);
            button2.Name = "button2";
            button2.Size = new Size(224, 54);
            button2.TabIndex = 7;
            button2.Text = "Quay Lại";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.White;
            label4.Location = new Point(40, 200);
            label4.Name = "label4";
            label4.Size = new Size(476, 68);
            label4.TabIndex = 28;
            label4.Text = "CHESS ONLINE";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button_passwordhide
            // 
            button_passwordhide.Image = (Image)resources.GetObject("button_passwordhide.Image");
            button_passwordhide.Location = new Point(964, 308);
            button_passwordhide.Name = "button_passwordhide";
            button_passwordhide.Size = new Size(40, 40);
            button_passwordhide.TabIndex = 29;
            button_passwordhide.UseVisualStyleBackColor = true;
            button_passwordhide.Click += button_passwordhide_Click;
            // 
            // button_passwordhide2
            // 
            button_passwordhide2.Image = (Image)resources.GetObject("button_passwordhide2.Image");
            button_passwordhide2.Location = new Point(964, 363);
            button_passwordhide2.Name = "button_passwordhide2";
            button_passwordhide2.Size = new Size(40, 40);
            button_passwordhide2.TabIndex = 30;
            button_passwordhide2.UseVisualStyleBackColor = true;
            button_passwordhide2.Click += button_passwordhide2_Click;
            // 
            // button_passwordshow
            // 
            button_passwordshow.Image = (Image)resources.GetObject("button_passwordshow.Image");
            button_passwordshow.Location = new Point(964, 307);
            button_passwordshow.Name = "button_passwordshow";
            button_passwordshow.Size = new Size(40, 40);
            button_passwordshow.TabIndex = 31;
            button_passwordshow.UseVisualStyleBackColor = true;
            button_passwordshow.Click += button_passwordshow_Click;
            // 
            // button_passwordshow2
            // 
            button_passwordshow2.Image = (Image)resources.GetObject("button_passwordshow2.Image");
            button_passwordshow2.Location = new Point(964, 363);
            button_passwordshow2.Name = "button_passwordshow2";
            button_passwordshow2.Size = new Size(40, 40);
            button_passwordshow2.TabIndex = 32;
            button_passwordshow2.UseVisualStyleBackColor = true;
            button_passwordshow2.Click += button_passwordshow2_Click;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Times New Roman", 16.2F);
            txtFullName.ForeColor = SystemColors.GrayText;
            txtFullName.Location = new Point(566, 196);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(395, 39);
            txtFullName.TabIndex = 2;
            txtFullName.Text = "Họ và Tên";
            txtFullName.Enter += TxtFullName_Enter;
            txtFullName.Leave += TxtFullName_Leave;
            // 
            // dtpBirthday
            // 
            dtpBirthday.CalendarFont = new Font("Times New Roman", 16.2F);
            dtpBirthday.Font = new Font("Times New Roman", 16.2F);
            dtpBirthday.Location = new Point(566, 252);
            dtpBirthday.Margin = new Padding(2);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(395, 39);
            dtpBirthday.TabIndex = 3;
            // 
            // txtOtp
            // 
            txtOtp.Font = new Font("Times New Roman", 16.2F);
            txtOtp.ForeColor = SystemColors.GrayText;
            txtOtp.Location = new Point(566, 153);
            txtOtp.Name = "txtOtp";
            txtOtp.Size = new Size(164, 39);
            txtOtp.TabIndex = 33;
            txtOtp.Text = "Nhập mã OTP";
            txtOtp.Enter += txtOtp_Enter;
            txtOtp.Leave += txtOtp_Leave;
            // 
            // btnVerifyOtp
            // 
            btnVerifyOtp.AutoSize = true;
            btnVerifyOtp.BackColor = Color.DarkSlateBlue;
            btnVerifyOtp.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnVerifyOtp.ForeColor = Color.White;
            btnVerifyOtp.Location = new Point(797, 146);
            btnVerifyOtp.Name = "btnVerifyOtp";
            btnVerifyOtp.Size = new Size(164, 44);
            btnVerifyOtp.TabIndex = 34;
            btnVerifyOtp.Text = "Xác Nhận OTP";
            btnVerifyOtp.UseVisualStyleBackColor = false;
            // 
            // btnSendOtp
            // 
            btnSendOtp.AutoSize = true;
            btnSendOtp.BackColor = Color.DarkSlateBlue;
            btnSendOtp.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnSendOtp.ForeColor = Color.White;
            btnSendOtp.Location = new Point(566, 102);
            btnSendOtp.Name = "btnSendOtp";
            btnSendOtp.Size = new Size(203, 43);
            btnSendOtp.TabIndex = 35;
            btnSendOtp.Text = "Gửi OTP Xác Nhận";
            btnSendOtp.UseVisualStyleBackColor = false;
            // 
            // Signup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1040, 600);
            Controls.Add(btnSendOtp);
            Controls.Add(btnVerifyOtp);
            Controls.Add(txtOtp);
            Controls.Add(label4);
            Controls.Add(pictureBox4);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(txtEmail);
            Controls.Add(txtFullName);
            Controls.Add(dtpBirthday);
            Controls.Add(pictureBox2);
            Controls.Add(textBox3);
            Controls.Add(pictureBox3);
            Controls.Add(textBox4);
            Controls.Add(button_passwordshow2);
            Controls.Add(button_passwordshow);
            Controls.Add(button_passwordhide2);
            Controls.Add(button_passwordhide);
            Controls.Add(button2);
            Controls.Add(btnSignup);
            Name = "Signup";
            Text = "Signup";
            Load += Signup_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox txtEmail;
        private TextBox textBox1;
        private PictureBox pictureBox3;
        private TextBox textBox3;
        private PictureBox pictureBox4;
        private TextBox textBox4;
        private Button btnSignup;
        private Button button2;
        private Label label4;
        private Button button_passwordhide;
        private Button button_passwordhide2;
        private Button button_passwordshow;
        private Button button_passwordshow2;
        private TextBox txtFullName;
        private DateTimePicker dtpBirthday;
        private TextBox txtOtp;
        private Button btnVerifyOtp;
        private Button btnSendOtp;
    }
}