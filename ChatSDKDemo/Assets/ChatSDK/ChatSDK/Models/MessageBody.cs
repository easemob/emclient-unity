using System.Runtime.InteropServices;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public abstract class IMessageBody
    {

        public MessageBodyType Type;
        internal abstract JSONObject ToJson();
        internal abstract string TypeString();

        public static IMessageBody Constructor(string jsonString, string type)
        {
            IMessageBody body = null;
            if (type == "txt")
            {
                body = new MessageBody.TextBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.TextBody)body).Text = jo["content"];
                }
            }
            else if (type == "img")
            {
                body = new MessageBody.ImageBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.ImageBody)body).LocalPath = jo["localPath"].Value;
                    ((MessageBody.ImageBody)body).FileSize = jo["fileSize"].AsInt;
                    ((MessageBody.ImageBody)body).DisplayName = jo["displayName"].Value;
                    ((MessageBody.ImageBody)body).RemotePath = jo["remotePath"].Value;
                    ((MessageBody.ImageBody)body).Secret = jo["secret"].Value;
                    ((MessageBody.ImageBody)body).DownStatus = body.DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    ((MessageBody.ImageBody)body).ThumbnailLocalPath = jo["thumbnailLocalPath"].Value;
                    ((MessageBody.ImageBody)body).ThumbnaiRemotePath = jo["thumbnailRemotePath"].Value;
                    ((MessageBody.ImageBody)body).ThumbnaiSecret = jo["thumbnailSecret"].Value;
                    ((MessageBody.ImageBody)body).Height = jo["height"].AsDouble;
                    ((MessageBody.ImageBody)body).Width = jo["width"].AsDouble;
                    ((MessageBody.ImageBody)body).Original = jo["sendOriginalImage"].AsBool;
                }
            }
            else if (type == "loc")
            {
                body = new MessageBody.LocationBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.LocationBody)body).Latitude = jo["latitude"].AsDouble;
                    ((MessageBody.LocationBody)body).Longitude = jo["longitude"].AsDouble;
                    ((MessageBody.LocationBody)body).Address = jo["address"].Value;
                    ((MessageBody.LocationBody)body).BuildingName = jo["buildingName"].Value;
                }
            }
            else if (type == "cmd")
            {
                body = new MessageBody.CmdBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.CmdBody)body).Action = jo["action"].Value;
                    ((MessageBody.CmdBody)body).DeliverOnlineOnly = jo["deliverOnlineOnly"].AsBool;
                }
            }
            else if (type == "custom")
            {
                body = new MessageBody.CustomBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.CustomBody)body).CustomEvent = jo["event"].Value;
                    ((MessageBody.CustomBody)body).CustomParams = TransformTool.JsonStringToDictionary(jo["params"].Value);
                }
            }
            else if (type == "file")
            {
                body = new MessageBody.FileBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.FileBody)body).LocalPath = jo["localPath"].Value;
                    ((MessageBody.FileBody)body).FileSize = jo["fileSize"].AsInt;
                    ((MessageBody.FileBody)body).DisplayName = jo["displayName"].Value;
                    ((MessageBody.FileBody)body).RemotePath = jo["remotePath"].Value;
                    ((MessageBody.FileBody)body).Secret = jo["secret"].Value;
                    ((MessageBody.FileBody)body).FileSize = jo["displayName"].AsInt;
                    ((MessageBody.FileBody)body).DownStatus = body.DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                }
            }
            else if (type == "video")
            {
                body = new MessageBody.VideoBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.VideoBody)body).LocalPath = jo["localPath"].Value;
                    ((MessageBody.VideoBody)body).FileSize = jo["fileSize"].AsInt;
                    ((MessageBody.VideoBody)body).DisplayName = jo["displayName"].Value;
                    ((MessageBody.VideoBody)body).RemotePath = jo["remotePath"].Value;
                    ((MessageBody.VideoBody)body).Secret = jo["secret"].Value;
                    ((MessageBody.VideoBody)body).DownStatus = body.DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    ((MessageBody.VideoBody)body).ThumbnaiRemotePath = jo["thumbnailRemotePath"].Value;
                    ((MessageBody.VideoBody)body).ThumbnaiSecret = jo["thumbnailSecret"].Value;
                    ((MessageBody.VideoBody)body).ThumbnaiLocationPath = jo["thumbnailLocalPath"].Value;
                    ((MessageBody.VideoBody)body).Height = jo["height"].AsDouble;
                    ((MessageBody.VideoBody)body).Width = jo["width"].AsDouble;
                    ((MessageBody.VideoBody)body).Duration = jo["duration"].AsInt;
                }
            }
            else if (type == "voice")
            {
                body = new MessageBody.VoiceBody();
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    ((MessageBody.VoiceBody)body).LocalPath = jo["localPath"].Value;
                    ((MessageBody.VoiceBody)body).FileSize = jo["fileSize"].AsInt;
                    ((MessageBody.VoiceBody)body).DisplayName = jo["displayName"].Value;
                    ((MessageBody.VoiceBody)body).RemotePath = jo["remotePath"].Value;
                    ((MessageBody.VoiceBody)body).Secret = jo["secret"].Value;
                    ((MessageBody.VoiceBody)body).DownStatus = body.DownLoadStatusFromInt(jo["fileStatus"].AsInt);
                    ((MessageBody.VoiceBody)body).Duration = jo["duration"].AsInt;
                }
            }
            return body;
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

    /// <summary>
    /// 消息体
    /// </summary>
    namespace MessageBody
    {
        /// <summary>
        /// 文字消息
        /// </summary>
        public class TextBody : IMessageBody
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            public string Text;

            public TextBody(string text)
            {
                Text = text;
                Type = MessageBodyType.TXT;
            }

            internal TextBody() {
                Type = MessageBodyType.TXT;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (Text != null) {
                    jo.Add("content", Text);
                }
                
                return jo;
            }

            internal override string TypeString()
            {
                return "txt";
            }
        }

        /// <summary>
        /// 位置消息
        /// </summary>
        public class LocationBody : IMessageBody
        {
            /// <summary>
            /// 经纬度坐标
            /// </summary>
            public double Latitude, Longitude;

            /// <summary>
            /// 地址信息
            /// </summary>
            public string Address;

            /// <summary>
            /// 建筑物信息
            /// </summary>
            public string BuildingName;

            public LocationBody(double latitude, double longitude, string address = "", string buildName = "")
            {
                Latitude = latitude;
                Longitude = longitude;
                Address = address;
                BuildingName = buildName;
                Type = MessageBodyType.LOCATION;
            }

            internal LocationBody() {
                Type = MessageBodyType.LOCATION;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                jo.Add("latitude", Latitude);
                jo.Add("longitude", Longitude);
                if (Address != null) {
                    jo.Add("address", Address);
                }
                if (BuildingName != null)
                {
                    jo.Add("buildingName", BuildingName);
                }

                return jo;
            }

            internal override string TypeString()
            {
                return "loc";
            }
        }

        /// <summary>
        /// 文件消息
        /// </summary>
        public class FileBody : IMessageBody
        {
            /// <summary>
            /// 本地路径
            /// </summary>
            public string LocalPath;

            /// <summary>
            /// 文件名称
            /// </summary>
            public string DisplayName;

            /// <summary>
            /// 环信验证信息
            /// </summary>
            public string Secret;

            /// <summary>
            /// 文件URL
            /// </summary>
            public string RemotePath;

            /// <summary>
            /// 文件大小
            /// </summary>
            public long FileSize;

            /// <summary>
            /// 下载状态
            /// </summary>
            public DownLoadStatus DownStatus = DownLoadStatus.PENDING;

            public FileBody(string localPath, string displayName = "", long fileSize = 0)
            {
                LocalPath = localPath;
                DisplayName = displayName;
                FileSize = fileSize;
                Type = MessageBodyType.FILE;
            }

            internal FileBody()
            {
                Type = MessageBodyType.FILE;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (LocalPath != null) {
                    jo.Add("localPath", LocalPath);
                }
                
                jo.Add("fileSize", FileSize);

                if (DisplayName != null) {
                    jo.Add("displayName", DisplayName);
                }
                
                if (RemotePath != null) {
                    jo.Add("remotePath", RemotePath);
                }
                if (Secret != null) {
                    jo.Add("secret", Secret);
                }

                jo.Add("fileSize", FileSize);

                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));

                return jo;

            }

            internal override string TypeString()
            {
                return "file";
            }

        }

        /// <summary>
        /// 图片消息
        /// </summary>
        public class ImageBody : FileBody
        {

            /// <summary>
            /// 缩略图本地路径，只有接收方才有
            /// </summary>
            public string ThumbnailLocalPath;

            /// <summary>
            /// 缩略图URL,只有接收方才有
            /// </summary>
            public string ThumbnaiRemotePath;

            /// <summary>
            /// 缩略图验证信息，只有接收方才有
            /// </summary>
            public string ThumbnaiSecret;

            /// <summary>
            /// 文件宽高
            /// </summary>
            public double Height, Width;

            /// <summary>
            /// 是否发送原图
            /// </summary>
            public bool Original;

            /// <summary>
            /// 下载状态
            /// </summary>
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;

            public ImageBody(string localPath, string displayName, long fileSize = 0, bool original = false, double width = 0, double height = 0) : base(localPath, displayName, fileSize)
            {
                Original = original;
                Height = height;
                Width = width;
                Type = MessageBodyType.IMAGE;
            }

            internal ImageBody() {
                Type = MessageBodyType.IMAGE;
            }


            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (LocalPath != null) {
                    jo.Add("localPath", LocalPath);
                }

                if (DisplayName != null)
                {
                    jo.Add("displayName", DisplayName);
                }

                if (RemotePath != null)
                {
                    jo.Add("remotePath", RemotePath);
                }

                if (Secret != null)
                {
                    jo.Add("secret", Secret);
                }

                if (ThumbnailLocalPath != null)
                {
                    jo.Add("thumbnailLocalPath", ThumbnailLocalPath);
                }

                if (ThumbnaiRemotePath != null)
                {
                    jo.Add("thumbnailRemotePath", ThumbnaiRemotePath);
                }

                if (ThumbnaiSecret != null)
                {
                    jo.Add("thumbnailSecret", ThumbnaiSecret);
                }

                jo.Add("height", Height);
                jo.Add("width", Width);
                jo.Add("sendOriginalImage", Original);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                
                return jo;
            }

            internal override string TypeString()
            {
                return "img";
            }
        }

        /// <summary>
        /// 音频消息
        /// </summary>
        public class VoiceBody : FileBody
        {
            /// <summary>
            /// 消息时长
            /// </summary>
            public int Duration;
            public VoiceBody(string localPath, string displayName, int duration, long fileSize = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Type = MessageBodyType.VOICE;
            }

            internal VoiceBody() {
                Type = MessageBodyType.VOICE;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (LocalPath != null) {
                    jo.Add("localPath", LocalPath);
                }

                if (DisplayName != null)
                {
                    jo.Add("displayName", DisplayName);
                }

                if (RemotePath != null)
                {
                    jo.Add("remotePath", RemotePath);
                }

                if (Secret != null)
                {
                    jo.Add("secret", Secret);
                }

                
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                jo.Add("duration", Duration);
                
                return jo;
            }

            internal override string TypeString()
            {
                return "voice";
            }
        }

        /// <summary>
        /// 视频消息
        /// </summary>
        public class VideoBody : FileBody
        {
            /// <summary>
            /// 缩略图本地路径，只有接收方有
            /// </summary>
            public string ThumbnaiLocationPath;

            /// <summary>
            /// 缩略图URL，只有接收方有
            /// </summary>
            public string ThumbnaiRemotePath;

            /// <summary>
            /// 缩略图验证信息，只有接收方有
            /// </summary>
            public string ThumbnaiSecret;

            /// <summary>
            /// 视频宽高
            /// </summary>
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

            internal VideoBody()
            {
                Type = MessageBodyType.VIDEO;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (LocalPath != null) {
                    jo.Add("localPath", LocalPath);
                }

                if (DisplayName != null) {
                    jo.Add("displayName", DisplayName);
                }

                if (RemotePath != null) {
                    jo.Add("remotePath", RemotePath);
                }

                if (Secret != null) {
                    jo.Add("secret", Secret);
                }

                if (ThumbnaiRemotePath != null) {
                    jo.Add("thumbnailRemotePath", ThumbnaiRemotePath);
                }

                if (ThumbnaiSecret != null) {
                    jo.Add("thumbnailSecret", ThumbnaiSecret);
                }

                if (ThumbnaiLocationPath != null) {
                    jo.Add("thumbnailLocalPath", ThumbnaiLocationPath);
                }
                
                
                jo.Add("height", Height);
                jo.Add("width", Width);
                jo.Add("duration", Duration);
                jo.Add("fileSize", FileSize);
                jo.Add("fileStatus", DownLoadStatusToInt(DownStatus));
                //jo.Add("bodyType", "video");
                return jo;
            }

            internal override string TypeString()
            {
                return "video";
            }
        }

        /// <summary>
        /// Cmd消息
        /// </summary>
        public class CmdBody : IMessageBody
        {
            /// <summary>
            /// 消息Action信息
            /// </summary>
            public string Action;

            /// <summary>
            /// 是否只发在线
            /// </summary>
            public bool DeliverOnlineOnly;

            public CmdBody(string action, bool deliverOnlineOnly = false)
            {
                Action = action;
                DeliverOnlineOnly = deliverOnlineOnly;
                Type = MessageBodyType.CMD;
            }

            internal CmdBody()
            {
                Type = MessageBodyType.CMD;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                jo.Add("deliverOnlineOnly", DeliverOnlineOnly);
                if (Action != null) {
                    jo.Add("action", Action);
                }
                
                
                return jo;
            }

            internal override string TypeString()
            {
                return "cmd";
            }
        }

        /// <summary>
        /// 自定义消息
        /// </summary>
        public class CustomBody : IMessageBody
        {
            /// <summary>
            /// 自定义事件
            /// </summary>
            public string CustomEvent;
            //TODO: Dictionary<string,string> -> string[][] in marshalling

            /// <summary>
            /// 自定义Params
            /// </summary>
            public Dictionary<string, string> CustomParams;

            public CustomBody(string customEvent, Dictionary<string, string> customParams = null)
            {
                CustomEvent = customEvent;
                CustomParams = customParams;
                Type = MessageBodyType.CUSTOM;
            }

            internal CustomBody()
            {
                Type = MessageBodyType.CUSTOM;
            }

            internal override JSONObject ToJson()
            {
                JSONObject jo = new JSONObject();
                if (CustomEvent != null) {
                    jo.Add("event", CustomEvent);
                }
                
                if (CustomParams != null) {
                    jo.Add("params", TransformTool.JsonStringFromDictionary(CustomParams));
                }

                return jo;
            }

            internal override string TypeString()
            {
                return "custom";
            }
        }
    }
}