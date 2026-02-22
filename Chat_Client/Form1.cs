using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;
using Chat_Library;

namespace Chat_Client
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class Form1 : Form, IServerChatCallback
    {
        bool isConnected = false;
        IServiceChat server;
        int ID;

        public Form1()
        {
            InitializeComponent();
            btnSend.Enabled = false;
            btnPrivate.Enabled = false;
        }

        // --- Логіка підключення ---
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        void ConnectUser()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text)) return;

            try
            {
                // Налаштування з'єднання в коді (без app.config)
                InstanceContext site = new InstanceContext(this);
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                EndpointAddress endpoint = new EndpointAddress("net.tcp://localhost:8302/");

                DuplexChannelFactory<IServiceChat> factory = new DuplexChannelFactory<IServiceChat>(site, binding, endpoint);
                server = factory.CreateChannel();

                ID = server.Connect(txtUserName.Text);

                btnConnect.Text = "Вихід";
                isConnected = true;
                btnSend.Enabled = true;
                btnPrivate.Enabled = true;
                txtUserName.Enabled = false;
                lbChat.Items.Add("--- Ви увійшли в чат ---");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення: " + ex.Message);
            }
        }

        void DisconnectUser()
        {
            if (server != null)
            {
                server.Disconnect(ID);
                server = null;
            }
            btnConnect.Text = "Вхід";
            isConnected = false;
            btnSend.Enabled = false;
            btnPrivate.Enabled = false;
            txtUserName.Enabled = true;
            lbUsers.DataSource = null;
            lbChat.Items.Add("--- Ви покинули чат ---");
        }

        // --- Відправка повідомлень ---
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (isConnected && !string.IsNullOrEmpty(txtMessage.Text))
            {
                server.SendMsg(txtMessage.Text, ID);
                txtMessage.Text = string.Empty;
            }
        }

        private void btnPrivate_Click(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {
                string target = lbUsers.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    server.SendPrivateMsg(target, txtMessage.Text, ID);
                    txtMessage.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Виберіть користувача зі списку!");
            }
        }

        // --- Методи Callback (викликає сервер) ---
        public void MsgCallback(string msg)
        {
            // Оскільки це приходить з іншого потоку, треба Invoke
            this.Invoke(new Action(() => {
                lbChat.Items.Add(msg);
                lbChat.TopIndex = lbChat.Items.Count - 1; // Автоскрол
            }));
        }

        public void RefreshClients(List<string> users)
        {
            this.Invoke(new Action(() => {
                lbUsers.DataSource = users;
            }));
        }

        // При закритті форми
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectUser();
        }
    }
}