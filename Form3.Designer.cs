namespace SideNavSample
{
    partial class Form3
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
            this.txt_pass_customer = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_user_customer = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX26 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // txt_pass_customer
            // 
            this.txt_pass_customer.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txt_pass_customer.Border.Class = "TextBoxBorder";
            this.txt_pass_customer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_pass_customer.DisabledBackColor = System.Drawing.Color.White;
            this.txt_pass_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pass_customer.ForeColor = System.Drawing.Color.Black;
            this.txt_pass_customer.Location = new System.Drawing.Point(103, 151);
            this.txt_pass_customer.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txt_pass_customer.Name = "txt_pass_customer";
            this.txt_pass_customer.PasswordChar = '*';
            this.txt_pass_customer.PreventEnterBeep = true;
            this.txt_pass_customer.Size = new System.Drawing.Size(611, 34);
            this.txt_pass_customer.TabIndex = 114;
            this.txt_pass_customer.WatermarkText = "Şifre";
            // 
            // txt_user_customer
            // 
            this.txt_user_customer.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txt_user_customer.Border.Class = "TextBoxBorder";
            this.txt_user_customer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_user_customer.DisabledBackColor = System.Drawing.Color.White;
            this.txt_user_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user_customer.ForeColor = System.Drawing.Color.Black;
            this.txt_user_customer.Location = new System.Drawing.Point(103, 85);
            this.txt_user_customer.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txt_user_customer.Name = "txt_user_customer";
            this.txt_user_customer.PreventEnterBeep = true;
            this.txt_user_customer.Size = new System.Drawing.Size(611, 34);
            this.txt_user_customer.TabIndex = 113;
            this.txt_user_customer.WatermarkText = "Kulanıcı Adı";
            // 
            // buttonX26
            // 
            this.buttonX26.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX26.BackColor = System.Drawing.SystemColors.HighlightText;
            this.buttonX26.Location = new System.Drawing.Point(176, 207);
            this.buttonX26.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.buttonX26.Name = "buttonX26";
            this.buttonX26.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5);
            this.buttonX26.Size = new System.Drawing.Size(200, 46);
            this.buttonX26.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.buttonX26.TabIndex = 115;
            this.buttonX26.Text = "Giriş";
            this.buttonX26.Click += new System.EventHandler(this.buttonX26_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.buttonX1.Location = new System.Drawing.Point(454, 207);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(7);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(5);
            this.buttonX1.Size = new System.Drawing.Size(200, 46);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.buttonX1.TabIndex = 116;
            this.buttonX1.Text = "Kapat";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 318);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.buttonX26);
            this.Controls.Add(this.txt_pass_customer);
            this.Controls.Add(this.txt_user_customer);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şifre";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX txt_pass_customer;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_user_customer;
        private DevComponents.DotNetBar.ButtonX buttonX26;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}