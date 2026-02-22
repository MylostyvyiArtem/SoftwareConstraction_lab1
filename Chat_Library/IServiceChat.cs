using System.Collections.Generic;
using System.ServiceModel;

namespace Chat_Library
{
    // Контракт сервісу (що вміє робити сервер)
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);

        [OperationContract(IsOneWay = true)]
        void SendPrivateMsg(string targetUserName, string msg, int senderId);
    }

    // Контракт зворотного зв'язку (що сервер може сказати клієнту)
    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);

        [OperationContract(IsOneWay = true)]
        void RefreshClients(List<string> users);
    }
}