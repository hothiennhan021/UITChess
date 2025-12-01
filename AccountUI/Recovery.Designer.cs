using System.Drawing;
using System.Windows.Forms;

namespace AccountUI
{
    partial class Recovery
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnQuayLai;
        private PictureBox pictureBox1;
        private TextBox txtEmail;
        private TextBox txtOTP;
        private Button btnGui;
        private Label label4;
        private Label label2;
        private Button btnXacNhan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(Recovery));
            btnQuayLai = new Button();
            pictureBox1 = new PictureBox();
            txtEmail = new TextBox();
            txtOTP = new TextBox();
            btnGui = new Button();
            label4 = new Label();
            label2 = new Label();
            btnXacNhan = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnQuayLai
            // 
            btnQuayLai.AutoSize = true;
            btnQuayLai.BackColor = Color.DarkSlateBlue;
            btnQuayLai.Font = new Font("Times New Roman", 13.8F);
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.Location = new Point(175, 409);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(224, 54);
            btnQuayLai.TabIndex = 4;
            btnQuayLai.Text = "Quay Lại";
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += btnQuayLai_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(56, 136);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(39, 39);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabStop = false;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Times New Roman", 16.2F);
            txtEmail.ForeColor = SystemColors.WindowText;
            txtEmail.Location = new Point(98, 136);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(395, 39);
            txtEmail.TabIndex = 1;
            // 
            // txtOTP
            // 
            txtOTP.Font = new Font("Times New Roman", 16.2F);
            txtOTP.ForeColor = SystemColors.WindowText;
            txtOTP.Location = new Point(98, 275);
            txtOTP.Name = "txtOTP";
            txtOTP.Size = new Size(395, 39);
            txtOTP.TabIndex = 2;
            // 
            // btnGui
            // 
            btnGui.AutoSize = true;
            btnGui.BackColor = Color.DarkSlateBlue;
            btnGui.Font = new Font("Times New Roman", 13.8F);
            btnGui.ForeColor = Color.White;
            btnGui.Location = new Point(175, 199);
            btnGui.Name = "btnGui";
            btnGui.Size = new Size(224, 54);
            btnGui.TabIndex = 3;
            btnGui.Text = "Gửi";
            btnGui.UseVisualStyleBackColor = false;
            btnGui.Click += btnGui_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(134, 24);
            label4.Name = "label4";
            label4.Size = new Size(296, 42);
            label4.Text = "CHESS ONLINE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(36, 85);
            label2.Name = "label2";
            label2.Size = new Size(488, 25);
            label2.Text = "Nhập Email Để Nhận Mã Khôi Phục Mật Khẩu";
            // 
            // btnXacNhan
            // 
            btnXacNhan.AutoSize = true;
            btnXacNhan.BackColor = Color.DarkSlateBlue;
            btnXacNhan.Font = new Font("Times New Roman", 13.8F);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(175, 336);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(224, 54);
            btnXacNhan.TabIndex = 5;
            btnXacNhan.Text = "Xác Nhận";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // Recovery
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(966, 488);
            Controls.Add(btnXacNhan);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(btnGui);
            Controls.Add(txtOTP);
            Controls.Add(pictureBox1);
            Controls.Add(txtEmail);
            Controls.Add(btnQuayLai);
            Name = "Recovery";
            Text = "Recovery";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
