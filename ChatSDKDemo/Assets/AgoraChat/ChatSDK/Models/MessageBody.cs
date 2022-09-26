using System.Collections.Generic;
using SimpleJSON;

namespace AgoraChat
{
    /**
      * \~chinese
      * 消息体抽象类。
      * 
      * \~english
      * The message body abstract class.
      * 
      */
    public abstract class IMessageBody
    {

        public MessageBodyType Type;
        internal abstract JSONObject ToJson();
        internal abstract string TypeString();

        /**
          * \~chinese
          * IMessageBody 构造方法。
          * 
          * @param jsonString   JSON 格式的消息内容。
          * @param type         消息类型。
          * @return             IMessageBody 实例。
          * 
          * \~english
          * The IMessageBody constructor.
          * 
          * @param jsonString   The message content in the JSON format.
          * @param type         The message type.
          * @return             The IMessageBody instance.
          */
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
                    ((MessageBody.TextBody)body).TargetLanguages = TransformTool.JsonStringToStringList(jo["targetLanguages"]);
                    ((MessageBody.TextBody)body).Translations = TransformTool.JsonStringToDictionary(jo["translations"]);
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

    /**
     * \~chinese
     * 消息体。
     * 
     * \~english
     * The message body.
     * 
     */
    namespace MessageBody
    {
        /**
         * \~chinese
         * 文本消息体。
         *
         * \~english
         * The text message body.
         */
        public class TextBody : IMessageBody
        {
            /**
             * \~chinese
             * 文本消息内容。
             *
             * \~english
             * The text message content.
             */
            public string Text;

            /**
              * \~chinese
              * 翻译目标语种。
              *
              * \~english
              * Target languages.
              * 
              */
            public List<string> TargetLanguages;

            /**
              * \~chinese
              * 翻译语种与翻译结果。
              *
              * \~english
              * Target languages and tranlations.
              * 
              */
            public Dictionary<string, string> Translations;

            /**
             * \~chinese
             * 文本消息构造方法。
             * 
             * @param text  文本消息体。
             *
             * \~english
             * The text message constructor.
             * 
             * @param text  The text message body.
             */
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
                    jo.Add("targetLanguages", TransformTool.JsonStringFromStringList(TargetLanguages));
                    jo.Add("translations", TransformTool.JsonStringFromDictionary(Translations));
                }
                
                return jo;
            }

            internal override string TypeString()
            {
                return "txt";
            }
        }

        /**
         * \~chinese
         * 位置消息体。
         *
         * \~english
         * The location message body.
         */
        public class LocationBody : IMessageBody
        {
            /**
             * \~chinese
             * 经纬度坐标。
             *
             * \~english
             * The latitue and longitude.
             */
            public double Latitude, Longitude;

            /**
             * \~chinese
             * 地址信息。
             *
             * \~english
             * The address.
             */
            public string Address;

            /**
             * \~chinese
             * 建筑物名称。
             *
             * \~english
             * The building name.
             */
            public string BuildingName;

            /**
             * \~chinese
             * 位置消息体构造方法。
             * 
             * @param latitue 纬度。
             * @param longitude 经度。
             * @param address 地址信息。
             * @param buildName 建筑物名称。
             *
             * \~english
             * The location message body constructor.
             * 
             * @param latitue The latitue.
             * @param longitude The longitude.
             * @param address The address.
             * @param buildName The building name.
             */
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

        /**
         * \~chinese
         * 文件消息体。
         *
         * \~english
         * The file message body.
         */
        public class FileBody : IMessageBody
        {
            /**
             * \~chinese
             * 文件的本地路径。
             *
             * \~english
             * The local path of the file.
             */
            public string LocalPath;

            /**
             * \~chinese
             * 文件显示名称。
             *
             * \~english
             * The display name of the file.
             */
            public string DisplayName;

            /**
             * \~chinese
             * 文件下载密钥。
             *
             * \~english
             * The secret for downloading the file.
             */
            public string Secret;

            /**
             * \~chinese
             * 文件在服务器上的保存路径。
             *
             * \~english
             * The URL where the file is located on the server.
             */
            public string RemotePath;

            /**
             * \~chinese
             * 文件大小，单位为字节。
             *
             * \~english
             * The file size in bytes.
             */
            public long FileSize;

            /**
             * \~chinese
             * 文件下载状态。
             *
             * \~english
             * The file download status.
             */
            public DownLoadStatus DownStatus = DownLoadStatus.PENDING;

            /**
             * \~chinese
             * 文件消息体构造方法。
             * 
             * @param localPath     文件的本地路径。
             * @param displayName   文件的显示名称。
             * @param fileSize      文件大小，单位为字节。
             *
             * \~english
             * The file message body constructor.
             * 
             * @param localPath     The local path of the file.
             * @param displayName   The display name of the file.
             * @param fileSize      The file size in bytes.
             */
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

        /**
         * \~chinese
         * 图片消息体。
         *
         * \~english
         * The image message body.
         */
        public class ImageBody : FileBody
        {
            /**
             * \~chinese
             * 缩略图的本地路径。
             *
             * \~english
             * The local path of the thumbnail.
             */
            public string ThumbnailLocalPath;

            /**
             * \~chinese
             * 缩略图在服务器上的保存路径。
             *
             * \~english
             * The URL where the thumbnail is located on the server.
             */
            public string ThumbnaiRemotePath;

            /**
             * \~chinese
             * 缩略图的下载密钥。
             *
             * \~english
             * The secret for downloading the thumbnail.
             */
            public string ThumbnaiSecret;

            /**
             * \~chinese
             * 图片文件的宽度和高度，单位为像素。
             *
             * \~english
             * The width and height of the image file, in pixels.
             */
            public double Height, Width;

            /**
             * \~chinese
             * 是否发送原图。
             * - `true`: 发送原图和缩略图。
             * - (默认） `false`: 若图片小于 100 KB，发送原图和缩略图；若图片大于等于 100 KB, 发送压缩后的图片和压缩后图片的缩略图。
             *
             * \~english
             * Whether to send the original image.
             * - `true`: Yes. 
             * - (Default) `false`: No. If the image is smaller than 100 KB, the SDK sends the original image and its thumbnail. If the image is equal to or greater than 100 KB, the SDK will compress it before sending the compressed image and the thumbnail of the compressed image.
             */
            public bool Original;

            /**
             * \~chinese
             * 图片的下载状态。
             *
             * \~english
             * The image download status.
             */
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;

            /**
             * \~chinese
             * 图片消息体构造方法。
             * 
             * @param localPath     图片的本地路径。
             * @param displayName   图片的显示名称。
             * @param fileSize      图片大小，单位为字节。
             * @param original      是否发送原图。
             * @param width         图片宽度，单位为像素。 
             * @param height        图片高度，单位为像素。
             *
             * \~english
             * The image message body constructor.
             * 
             * @param localPath     The local path of the image.
             * @param displayName   The display name of the image.
             * @param fileSize      The image size in bytes.
             * @param original      Whether to send the original image.
             * @param width         The image width in pixels.
             * @param Height        The image height in pixels.
             * 
             */
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

        /**
         * \~chinese
         * 音频消息体。
         *
         * \~english
         * The voice message body.
         */
        public class VoiceBody : FileBody
        {
            /**
             * \~chinese
             * 音频时长，单位为秒。
             *
             * \~english
             * The voice duration in seconds.
             */
            public int Duration;
            /**
             * \~chinese
             * 音频消息体构造方法。
             * 
             * @param localPath     音频消息的本地路径。
             * @param displayName   音频消息的显示名称。
             * @param duration      音频时长，单位为秒。
             * @param fileSize      音频文件大小，单位为字节。
             *
             * \~english
             * The voice message body constructor.
             * 
             * @param localPath     The local path of the voice message.
             * @param displayName   The display name of the voice message.
             * @param duration      The voice duration in seconds.
             * @param fileSize      The size of the voice file, in bytes.
             * 
             */
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

        /**
         * \~chinese
         * 视频消息体。
         *
         * \~english
         * The video message body.
         */
        public class VideoBody : FileBody
        {
            /**
             * \~chinese
             * 视频缩略图的本地路径。
             *
             * \~english
             * The local path of the video thumbnail.
             */
            public string ThumbnaiLocationPath;

            /**
             * \~chinese
             * 视频缩略图在服务器端的存储路径。
             *
             * \~english
             * The URL of the video thumbnail.
             */
            public string ThumbnaiRemotePath;

            /**
             * \~chinese
             * 缩略图下载密钥。
             *
             * \~english
             * The secret for downloading the thumbnail.
             */
            public string ThumbnaiSecret;

            /**
             * \~chinese
             * 视频的宽度和高度，单位为像素。
             *
             * \~english
             * The height and width of the video, in pixels.
             */
            public double Height, Width;

            /**
             * \~chinese
             * 视频时长，单位为秒。
             *
             * \~english
             * The video duration in seconds.
             */
            public int Duration;

            /**
             * \~chinese
             * 视频消息体构造方法。
             * 
             * @param localPath     视频消息的本地路径。
             * @param displayName   视频文件的显示名称。
             * @param duration      视频时长，单位为秒。
             * @param fileSize      视频的文件大小，单位为字节。
             * @param thumbnailLocalPath 视频缩略图的本地路径。
             * @param width         视频宽度，单位为像素。 
             * @param height        视频高度，单位为像素。
             *
             * \~english
             * The video message body constructor.
             * 
             * @param localPath     The local path of the video message.
             * @param displayName   The display name of the video file.
             * @param duration      The video duration in seconds.
             * @param fileSize      The size of the video file, in bytes.
             * @param thumbnailLocalPath The local path of the video thumbnail.
             * @param width         The video width in pixels.
             * @param Height        The video height in pixels.
             * 
             */
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

        /**
         * \~chinese
         * 命令消息体。
         *
         * \~english
         * The command message body.
         */
        public class CmdBody : IMessageBody
        {
            /**
             * \~chinese
             * 命令内容。
             *
             * \~english
             * The command action.
             */
            public string Action;

            /**
             * \~chinese
             * 当前命令消息是否只投递在线用户。
             *
             * \~english
             * Whether this command message is delivered only to online users.
             */
            public bool DeliverOnlineOnly;

            /** 
             * \~chinese
             * 命令消息体构造方法。
             * 
             * @param action                命令内容。
             * @param deliverOnlineOnly     当前命令消息是否只投递在线用户。
             *                              - `true`：只投在线用户。
             *                              - （默认） `false`：不管用户是否在线均投递。
             *
             * \~english
             * The command message body constructor.
             * 
             * @param action                The command action.
             * @param deliverOnlineOnly     Whether this command message is delivered only to online users.
             *                              - `true`: Yes.
             *                              - (Default) `false`: No. The command message is delivered to users, regardless of their online or offline status.
             * 
             */
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

        /**
         * \~chinese
         * 自定义消息体。
         *
         * \~english
         * The custom message body.
         */
        public class CustomBody : IMessageBody
        {
            /**
             * \~chinese
             * 自定义事件。
             *
             * \~english
             * The custom event.
             */
            public string CustomEvent;
            //TODO: Dictionary<string,string> -> string[][] in marshalling

            /**
             * \~chinese
             * 自定义消息的键值对。
             *
             * \~english
             * The custom params map.
             */
            public Dictionary<string, string> CustomParams;

            /**
             * \~chinese
             * 自定义消息体构造方法。
             * 
             * @param customEvent      自定义事件。
             * @param customParams     自定义消息的键值对。
             *
             * \~english
             * The constructor for the custom message body.
             * 
             * @param customEvent      The custom event.
             * @param customParams     The custom params map.
             * 
             */
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