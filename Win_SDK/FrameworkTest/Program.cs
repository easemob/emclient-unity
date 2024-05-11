using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgoraChat;

namespace WinSDKTest
{

    class Program : IChatManagerDelegate
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Please input appkey:");
            string appkey = Console.ReadLine();

            Testor testor = new Testor();            
            testor.InitAll(appkey);

            //SimpleCase();
            testor.ShowLevel1Menu();

            Console.WriteLine("Please press any key to end.");
            Console.ReadLine();

            return;
        }
        static void SimpleCase()
        {
            Options options = new Options("easemob-demo#easeim");
            options.AutoLogin = false;
            options.UsingHttpsOnly = true;
            options.DebugMode = true;
            SDKClient.Instance.InitWithOptions(options);

            Program pro = new Program();
            SDKClient.Instance.ChatManager.AddChatManagerDelegate(pro);

            SDKClient.Instance.Login("yqtest", "yqtest",
            callback: new CallBack(

                onSuccess: () =>
                {
                    Console.WriteLine("Login succeed");

                    Message msg = Message.CreateTextSendMessage("yqtest1", "this is message from winsdk.");
                    SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                        onSuccess: () => {
                            Console.WriteLine($"发送成功, {msg.MsgId}");
                        },
                        onProgress: (progress) => {
                            Console.WriteLine($"发送进度, {progress.ToString()}");
                        },
                        onError: (code, desc) => {
                            Console.WriteLine($"发送错误, code:{code}, msgid:{msg.MsgId}");
                        }
                    ));

                    /*Message msg = Message.CreateVideoSendMessage("yqtest1", "d:\\test5.mp4");
                    SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                        onSuccess: () => {
                            Console.WriteLine($"发送成功, {msg.MsgId}");
                        },
                        onProgress: (progress) => {
                            Console.WriteLine($"发送进度, {progress.ToString()}");
                        },
                        onError: (code, desc) => {
                            Console.WriteLine($"发送错误, code:{code}, msgid:{msg.MsgId}");
                        }
                    ));*/
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        Console.WriteLine("Already login");
                    }
                    else
                    {
                        Console.WriteLine("Login failed");
                    }
                }
            )
        );
        }

        public void OnMessageContentChanged(Message msg, string operatorId, long operationTime)
        {
            throw new NotImplementedException();
        }

        public void OnMessagePinChanged(string messageId, string conversationId, bool isPinned, string operatorId, long operationTime)
        {
            throw new NotImplementedException();
        }

        public void MessageReactionDidChange(List<MessageReactionChange> list)
        {
            throw new NotImplementedException();
        }

        public void OnCmdMessagesReceived(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public void OnConversationRead(string from, string to)
        {
            throw new NotImplementedException();
        }

        public void OnConversationsUpdate()
        {
            throw new NotImplementedException();
        }

        public void OnGroupMessageRead(List<GroupReadAck> list)
        {
            throw new NotImplementedException();
        }

        public void OnMessagesDelivered(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public void OnMessagesRead(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public void OnMessagesRecalled(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public void OnMessagesReceived(List<Message> messages)
        {
            List<string> list = new List<string>();

            foreach (var msg in messages)
            {
                //list.Add(msg.MsgId);
                AgoraChat.MessageBody.TextBody tb = (AgoraChat.MessageBody.TextBody)msg.Body;
                list.Add(tb.Text);
            }

            string str = string.Join(",", list.ToArray());
            Console.WriteLine($"Received messages, ids: {str}");
        }

        public void OnReadAckForGroupMessageUpdated()
        {
            throw new NotImplementedException();
        }
    }
}
