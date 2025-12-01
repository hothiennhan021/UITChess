using System.Drawing;
using System.Windows.Forms;

namespace AccountUI
{
    partial class Resetpassword
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtPass2;
        private Button btnXacNhan;
        private Button btnQuayLai;
        private Button btnShow1;
        private Button btnShow2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resetpassword));
            txtPass2 = new TextBox();
            btnXacNhan = new Button();
            btnQuayLai = new Button();
            btnShow1 = new Button();
            btnShow2 = new Button();
            txtPass1 = new TextBox();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // txtPass2
            // 
            txtPass2.Font = new Font("Times New Roman", 16.2F);
            txtPass2.Location = new Point(279, 219);
            txtPass2.Name = "txtPass2";
            txtPass2.Size = new Size(360, 39);
            txtPass2.TabIndex = 1;
            txtPass2.UseSystemPasswordChar = true;
            // 
            // btnXacNhan
            // 
            btnXacNhan.AutoSize = true;
            btnXacNhan.BackColor = Color.DarkSlateBlue;
            btnXacNhan.Font = new Font("Times New Roman", 13.8F);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(357, 286);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(224, 54);
            btnXacNhan.TabIndex = 4;
            btnXacNhan.Text = "Xác Nhận";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // btnQuayLai
            // 
            btnQuayLai.AutoSize = true;
            btnQuayLai.BackColor = Color.DarkSlateBlue;
            btnQuayLai.Font = new Font("Times New Roman", 13.8F);
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.Location = new Point(357, 367);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(224, 54);
            btnQuayLai.TabIndex = 5;
            btnQuayLai.Text = "Quay Lại";
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += btnQuayLai_Click;
            // 
            // btnShow1
            // 
            btnShow1.Location = new Point(645, 140);
            btnShow1.Name = "btnShow1";
            btnShow1.Size = new Size(40, 39);
            btnShow1.TabIndex = 2;
            btnShow1.Text = "👁";
            btnShow1.UseVisualStyleBackColor = true;
            // 
            // btnShow2
            // 
            btnShow2.Location = new Point(645, 218);
            btnShow2.Name = "btnShow2";
            btnShow2.Size = new Size(40, 39);
            btnShow2.TabIndex = 3;
            btnShow2.Text = "👁";
            btnShow2.UseVisualStyleBackColor = true;
            // 
            // txtPass1
            // 
            txtPass1.Font = new Font("Times New Roman", 16.2F);
            txtPass1.Location = new Point(279, 137);
            txtPass1.Name = "txtPass1";
            txtPass1.Size = new Size(360, 39);
            txtPass1.TabIndex = 6;
            txtPass1.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(247, 38);
            label4.Name = "label4";
            label4.Size = new Size(0, 42);
            label4.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(306, 26);
            label1.Name = "label1";
            label1.Size = new Size(296, 42);
            label1.TabIndex = 9;
            label1.Text = "CHESS ONLINE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 137);
            label2.Name = "label2";
            label2.Size = new Size(206, 35);
            label2.TabIndex = 10;
            label2.Text = "Mật Khẩu Mới";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(-6, 218);
            label3.Name = "label3";
            label3.Size = new Size(278, 35);
            label3.TabIndex = 11;
            label3.Text = "Xác Nhận Mật Khẩu";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label5.ForeColor = Color.White;
            label5.Location = new Point(318, 85);
            label5.Name = "label5";
            label5.Size = new Size(272, 25);
            label5.TabIndex = 33;
            label5.Text = "Hãy Nhập Mật Khẩu Mới";
            // 
            // Resetpassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(966, 488);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(txtPass1);
            Controls.Add(btnQuayLai);
            Controls.Add(btnXacNhan);
            Controls.Add(btnShow2);
            Controls.Add(btnShow1);
            Controls.Add(txtPass2);
            Name = "Resetpassword";
            Text = "Resetpassword";
            ResumeLayout(false);
            PerformLayout();
        }
        private TextBox txtPass1;
        private Label label4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
    }
}
