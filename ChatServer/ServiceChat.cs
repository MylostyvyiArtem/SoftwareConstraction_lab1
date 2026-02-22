using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ChatLibrary;

namespace ChatServer
{
    // Реалізація сервісу. InstanceContextMode.Single означає, що всі юзери працюють з одним об'єктом сервісу
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;
            users.Add(user);

            // Повідомляємо всім про нового
            SendMsg($": {user.Name} підключився до чату!", 0);
            UpdateAllUsersList();
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg($": {user.Name} покинув чат.", 0);
                UpdateAllUsersList();
            }
        }

        public void SendMsg(string msg, int id)
        {
            string senderName = (id == 0) ? "System" : users.FirstOrDefault(x => x.ID == id)?.Name;
            string time = DateTime.Now.ToShortTimeString();
            string formattedMsg = $"[{time}] {senderName}: {msg}";

            foreach (var item in users)
            {
                try
                {
                    item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(formattedMsg);
                }
                catch { /* Юзер відвалився */ }
            }
        }

        public void SendPrivateMsg(string targetUserName, string msg, int senderId)
        {
            var target = users.FirstOrDefault(u => u.Name == targetUserName);
            var sender = users.FirstOrDefault(u => u.ID == senderId);

            if (target != null && sender != null)
            {
                string time = DateTime.Now.ToShortTimeString();
                string format = $"[ПРИВАТ] {sender.Name} -> Вам: {msg}";

                // Відправляємо отримувачу
                target.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(format);

                // Показуємо відправнику, що він відправив
                string formatMe = $"[ПРИВАТ] Ви -> {target.Name}: {msg}";
                sender.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(formatMe);
            }
        }

        private void UpdateAllUsersList()
        {
            var names = users.Select(u => u.Name).ToList();
            foreach (var user in users)
            {
                try
                {
                    user.operationContext.GetCallbackChannel<IServerChatCallback>().RefreshClients(names);
                }
                catch { }
            }
        }
    }

    public class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}