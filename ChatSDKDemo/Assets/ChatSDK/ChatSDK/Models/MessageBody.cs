using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public abstract class IMessageBody
    {
        public MessageBodyType Type;
        internal abstract string ToJsonString();

        static public IMessageBody FromJsonString(string jsonString) {
            JSONNode jn = JSON.Parse(jsonString);
            //if (!jn.IsNull && jn.IsObject)
            //{
            //    JSONObject jo = jn.AsObject;
            //    string type = jo["type"];
            //    if(type == "txt")
            //}
            return null;
        }

        internal int DownLoadStatusToInt(MessageBody.DownLoadStatus status)
        {
            int ret = 0;
            switch (status)
            {
                case MessageBody.DownLoadStatus.DOWNLOADING:
                    ret = 0;
                    break;
                case MessageBody.DownLoadStatus.SUCCESS:
                    ret = 1;
                    break;
                case MessageBody.DownLoadStatus.FAILED:
                    ret = 2;
                    break;
                case MessageBody.DownLoadStatus.PENDING:
                    ret = 3;
                    break;
            }

            return ret;
        }

        internal MessageBody.DownLoadStatus DownLoadStatusFromInt(int intStatus)
        {
            MessageBody.DownLoadStatus ret = 0;
            switch (intStatus)
            {
                case 0:
                    ret = MessageBody.DownLoadStatus.DOWNLOADING;
                    break;
                case 1:
                    ret = MessageBody.DownLoadStatus.SUCCESS;
                    break;
                case 2:
                    ret = MessageBody.DownLoadStatus.FAILED;
                    break;
                case 3:
                    ret = MessageBody.DownLoadStatus.PENDING;
                    break;
            }

            return ret;
        }
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

            internal TextBody(string jsonString, object obj)
            {
                Type = MessageBodyType.TXT;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject) {
                    JSONObject jo = jn.AsObject;
                    Text = jo["content"];
                }
            }

            internal override string ToJsonString()
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

            internal LocationBody(string jsonString, object obj)
            {
                Type = MessageBodyType.LOCATION;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Latitude = jo["latitude"].AsDouble;
                    Longitude = jo["longitude"].AsDouble;
                    Address = jo["address"].Value;
                }
            }

            internal override string ToJsonString()
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

            internal FileBody(string jsonString, object obj)
            {
                Type = MessageBodyType.FILE;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    LocalPath = jo["latitude"].Value;
                    FileSize = jo["fileSize"].AsInt;
                    DisplayName = jo["displayName"].Value;
                    RemotePath = jo["remotePath"].Value;
                    Secret = jo["secret"].Value;
                    FileSize = jo["displayName"].AsInt;
                    DownStatus = DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                }
            }

            internal FileBody() { }

            internal override string ToJsonString()
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

            internal ImageBody(string jsonString, object obj) {
                Type = MessageBodyType.IMAGE;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject) {
                    JSONObject jo = jn.AsObject;
                    LocalPath = jo["localPath"].Value;
                    FileSize = jo["fileSize"].AsInt;
                    DisplayName = jo["displayName"].Value;
                    RemotePath = jo["remotePath"].Value;
                    Secret = jo["secret"].Value;
                    DownStatus = DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    ThumbnailLocalPath = jo["thumbnailLocalPath"].Value;
                    ThumbnaiRemotePath = jo["thumbnailRemotePath"].Value;
                    ThumbnaiSecret = jo["thumbnailSecret"].Value;
                    Height = jo["height"].AsDouble;
                    Width = jo["width"].AsDouble;
                    Original = jo["sendOriginalImage"].AsBool;
                }
            }

            internal override string ToJsonString()
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

            internal VoiceBody(string jsonString, object obj) {
                Type = MessageBodyType.VOICE;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    LocalPath = jo["localPath"].Value;
                    FileSize = jo["fileSize"].AsInt;
                    DisplayName = jo["displayName"].Value;
                    RemotePath = jo["remotePath"].Value;
                    Secret = jo["secret"].Value;
                    DownStatus = DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    Duration = jo["duration"].AsInt;
                }
            }

            internal override string ToJsonString()
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

            internal VideoBody(string jsonString, object obj)
            {
                Type = MessageBodyType.VIDEO;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    LocalPath = jo["localPath"].Value;
                    FileSize = jo["fileSize"].AsInt;
                    DisplayName = jo["displayName"].Value;
                    RemotePath = jo["remotePath"].Value;
                    Secret = jo["secret"].Value;
                    DownStatus = DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    ThumbnaiRemotePath = jo["thumbnailRemotePath"].Value;
                    ThumbnaiSecret = jo["thumbnailSecret"].Value;
                    ThumbnaiLocationPath = jo["thumbnailLocalPath"].Value;
                    Height = jo["height"].AsDouble;
                    Width = jo["width"].AsDouble;
                    Duration = jo["duration"].AsInt;
                }
            }

            internal override string ToJsonString()
            {
                JSONObject jo = new JSONObject();
                jo.Add("localPath", LocalPath);
                jo.Add("displayName", DisplayName);
                jo.Add("remotePath", RemotePath);
                jo.Add("secret", Secret);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("thumbnailRemotePath", ThumbnaiRemotePath);
                jo.Add("thumbnailSecret", ThumbnaiSecret);
                jo.Add("thumbnailLocalPath", ThumbnaiLocationPath);
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

            internal CmdBody(string jsonString, object obj)
            {
                Type = MessageBodyType.CMD;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Action = jo["action"].Value;
                    DeliverOnlineOnly = jo["deliverOnlineOnly"].AsBool;
                }
            }

            internal override string ToJsonString()
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

            internal CustomBody(string jsonString, object obj)
            {
                Type = MessageBodyType.CUSTOM;
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    CustomEvent = jo["event"].Value;
                    CustomParams = TransformTool.JsonStringToDictionary(jo["params"].Value);
                }
            }

            internal override string ToJsonString()
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