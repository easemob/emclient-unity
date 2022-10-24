using System;
using AgoraChat;
using AgoraChat.InternalSpace;
using AgoraChat.MessageBody;

namespace AgoraChatDemo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sdk = SDKClient.Instance;

            //TO-DO: need to remove, just for testing
            Message img_msg1 = Message.CreateImageSendMessage("user", "\\d\\workspace", "image displayname", 2022, true, 125, 250);
            string img_json = img_msg1.ToJson();
            Message img_msg12 = MyJson.FromJson<Message>(img_json);

            Message txt_msg1 = Message.CreateTextSendMessage("send_user", "send_content");
            string txt_json = txt_msg1.ToJson();
            Message txt_msg2 = MyJson.FromJson<Message>(txt_json);

            TextBody txt_bd1 = new TextBody("this is a text message");
            string txt_bd_json = txt_bd1.ToJson();
            TextBody txt_bd2 = MyJson.FromJson<TextBody>(txt_bd_json);

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }
    }
}
