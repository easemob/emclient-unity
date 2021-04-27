using System.Collections.Generic;

namespace ChatSDK
{
    public class IMessageBody
    {
        public MessageBodyType Type;
        public virtual void ToJson() { }
    }

    namespace MessageBody
    {

        public class TextBody : IMessageBody
        {

            public string Text;

            public TextBody(string text)
            {
                Text = text;
                Type = MessageBodyType.TXT;
            }
        }

        public class LocationBody : IMessageBody
        {
            public string Latitude, Longitude, Address;

            public LocationBody(string latitude, string longitude, string address = "")
            {
                Latitude = latitude;
                Longitude = longitude;
                Address = address;
                Type = MessageBodyType.LOCATION;
            }
        }

        public class FileBody : IMessageBody
        {

            public string LocalPath, DisplayName, Secret, RemotePath;
            public long FileSize;
            public DownLoadStatus DownStatus = DownLoadStatus.PENDING;

            public FileBody(string localPath, string displayName = "", long fileSize = 0)
            {
                LocalPath = localPath;
                DisplayName = displayName;
                FileSize = fileSize;
                Type = MessageBodyType.FILE;
            }
        }

        public class ImageBody : FileBody
        {

            public string ThumbnaiLocationPath, ThumbnaiRemotePath, ThumbnaiSecret;
            public double Height, Wdith;
            public bool Original;
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;

            public ImageBody(string localPath, string displayName, long fileSize = 0, bool original = false) : base(localPath, displayName, fileSize)
            {
                Original = original;
                Type = MessageBodyType.IMAGE;
            }
        }

        public class VoiceBody : FileBody
        {
            public int Duration;
            public VoiceBody(string localPath, string displayName, int duration, long fileSize = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Type = MessageBodyType.VOICE;
            }
        }

        public class VideoBody : FileBody
        {
            public string ThumbnaiLocationPath, ThumbnaiRemotePath, ThumbnaiSecret;
            public double Height, Wdith;
            public int Duration;

            public VideoBody(string localPath, string displayName, int duration, long fileSize = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Type = MessageBodyType.VIDEO;
            }
        }

        public class CmdBody : IMessageBody
        {
            public string Action;
            public bool DeliverOnlineOnly;

            public CmdBody(string action, bool deliverOnlineOnly = false)
            {
                Action = action;
                DeliverOnlineOnly = deliverOnlineOnly;
                Type = MessageBodyType.CMD;
            }
        }

        public class CustomBody : IMessageBody
        {

            public string CustomEvent;
            public Dictionary<string, string> CustomParams;

            public CustomBody(string customEvent)
            {
                CustomEvent = customEvent;
                Type = MessageBodyType.CUSTOM;
            }
        }
    }
}