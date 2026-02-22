namespace Chat_Client
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbChat = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrivate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // txtUserName
            this.txtUserName.Location = new System.Drawing.Point(12, 25);
            this.txtUserName.Size = new System.Drawing.Size(150, 20);
            this.txtUserName.Text = "Student";
            // btnConnect
            this.btnConnect.Location = new System.Drawing.Point(170, 23);
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.Text = "Вхід";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // lbChat
            this.lbChat.Location = new System.Drawing.Point(12, 60);
            this.lbChat.Size = new System.Drawing.Size(400, 300);
            // lbUsers
            this.lbUsers.Location = new System.Drawing.Point(420, 60);
            this.lbUsers.Size = new System.Drawing.Size(150, 300);
            // txtMessage
            this.txtMessage.Location = new System.Drawing.Point(12, 380);
            this.txtMessage.Size = new System.Drawing.Size(310, 20);
            // btnSend
            this.btnSend.Location = new System.Drawing.Point(330, 378);
            this.btnSend.Text = "Надіслати";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // btnPrivate
            this.btnPrivate.Location = new System.Drawing.Point(420, 378);
            this.btnPrivate.Size = new System.Drawing.Size(150, 23);
            this.btnPrivate.Text = "Приватне повід.";
            this.btnPrivate.Click += new System.EventHandler(this.btnPrivate_Click);
            // labels
            this.label1.Text = "Нікнейм:"; this.label1.Location = new System.Drawing.Point(12, 9);
            this.label2.Text = "Користувачі онлайн:"; this.label2.Location = new System.Drawing.Point(420, 40);

            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.btnPrivate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lbChat);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtUserName);
            this.Text = "WCF Chat Lab 5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lbChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrivate;
    }
}