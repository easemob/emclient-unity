using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public abstract class IMessageBody
    {
        public MessageBodyType Type;
        public abstract string ToJsonString();
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

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("content", Text);
                jo.Add("type", "txt");
                return jo.ToString();
            }
        }

        public class LocationBody : IMessageBody
        {
            public double Latitude, Longitude;
            public string Address;

            public LocationBody(double latitude, double longitude, string address = "")
            {
                Latitude = latitude;
                Longitude = longitude;
                Address = address;
                Type = MessageBodyType.LOCATION;
            }

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("latitude", Latitude);
                jo.Add("longitude", Longitude);
                jo.Add("address", Address);
                jo.Add("type", "loc");
                return jo.ToString();
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

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("localPath", LocalPath);
                jo.Add("fileSize", FileSize);
                jo.Add("displayName", DisplayName);
                jo.Add("remotePath", RemotePath);
                jo.Add("secret", Secret);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("type", "file");
                return jo.ToString();
            }

            public int DownLoadStatusToInt(DownLoadStatus status) {
                int ret = 0;
                switch (status) { 
                    case DownLoadStatus.DOWNLOADING:
                        ret = 0;
                        break;
                    case DownLoadStatus.SUCCESS:
                        ret = 1;
                        break;
                    case DownLoadStatus.FAILED:
                        ret = 2;
                        break;
                    case DownLoadStatus.PENDING:
                        ret = 3;
                        break;
                }

                return ret;
            }
        }

        public class ImageBody : FileBody
        {

            public string ThumbnailLocalPath, ThumbnaiRemotePath, ThumbnaiSecret;
            public double Height, Width;
            public bool Original;
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;

            public ImageBody(string localPath, string displayName, long fileSize = 0, bool original = false, double width = 0, double height = 0) : base(localPath, displayName, fileSize)
            {
                Original = original;
                Height = height;
                Width = width;
                Type = MessageBodyType.IMAGE;
            }

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("localPath", LocalPath);
                jo.Add("fileSize", FileSize);
                jo.Add("displayName", DisplayName);
                jo.Add("remotePath", RemotePath);
                jo.Add("secret", Secret);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("thumbnailLocalPath", ThumbnailLocalPath);
                jo.Add("thumbnailRemotePath", ThumbnaiRemotePath);
                jo.Add("thumbnailSecret", ThumbnaiSecret);
                jo.Add("height", Height);
                jo.Add("width", Width);
                jo.Add("sendOriginalImage", Original);
                jo.Add("type", "img");
                return jo.ToString();
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

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("localPath", LocalPath);
                jo.Add("fileSize", FileSize);
                jo.Add("displayName", DisplayName);
                jo.Add("remotePath", RemotePath);
                jo.Add("secret", Secret);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("duration", Duration);
                jo.Add("type", "voice");
                return jo.ToString();
            }
        }

        public class VideoBody : FileBody
        {
            public string ThumbnaiLocationPath, ThumbnaiRemotePath, ThumbnaiSecret;
            public double Height, Width;
            public int Duration;

            public VideoBody(string localPath, string displayName, int duration, long fileSize = 0, string thumbnailLocalPath = "", double width = 0, double height = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Height = height;
                Width = width;
                ThumbnaiLocationPath = thumbnailLocalPath;
                Type = MessageBodyType.VIDEO;
            }

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("localPath", LocalPath);
                jo.Add("fileSize", FileSize);
                jo.Add("displayName", DisplayName);
                jo.Add("remotePath", RemotePath);
                jo.Add("secret", Secret);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("thumbnailRemotePath", ThumbnaiRemotePath);
                jo.Add("thumbnailSecret", ThumbnaiSecret);
                jo.Add("height", Height);
                jo.Add("width", Width);
                jo.Add("duration", Duration);
                jo.Add("type", "video");
                return jo.ToString();
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

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("deliverOnlineOnly", DeliverOnlineOnly);
                jo.Add("action", Action);
                jo.Add("type", "cmd");
                return jo.ToString();
            }
        }

        public class CustomBody : IMessageBody
        {

            public string CustomEvent;
            public Dictionary<string, string> CustomParams;

            public CustomBody(string customEvent, Dictionary<string, string> customParams = null)
            {
                CustomEvent = customEvent;
                CustomParams = customParams;
                Type = MessageBodyType.CUSTOM;
            }

            public override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("event", CustomEvent);
                jo.Add("params", TransformTool.JsonStringFromDictionary(CustomParams));
                jo.Add("type", "custom");
                return jo.ToString();
            }
        }
    }
}