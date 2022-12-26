using AgoraChat.SimpleJSON;
using System.Collections.Generic;


namespace AgoraChat
{
    public abstract class IMessageBody : BaseModel
    {
        public IMessageBody() { }

        internal IMessageBody(string jsonString) : base(jsonString) { }
        internal IMessageBody(JSONObject jsonObject) : base(jsonObject) { }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("type", Type.ToInt());

            JSONObject jo_body = new JSONObject();
            jo.AddWithoutNull("body", jo_body);

            return jo;
        }
        public MessageBodyType Type;
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

            internal TextBody()
            {
                Type = MessageBodyType.TXT;
            }


            internal TextBody(JSONObject jsonObject) : base(jsonObject)
            {
                Type = MessageBodyType.TXT;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                Text = jo["content"];
                TargetLanguages = List.StringListFromJsonArray(jo["targetLanguages"]);
                Translations = Dictionary.StringDictionaryFromJsonObject(jo["translations"]);
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("content", Text);
                jo_body.AddWithoutNull("targetLanguages", JsonObject.JsonArrayFromStringList(TargetLanguages));
                jo_body.AddWithoutNull("translations", JsonObject.JsonObjectFromDictionary(Translations));

                return jo;
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

            internal LocationBody()
            {
                Type = MessageBodyType.LOCATION;
            }

            internal LocationBody(string jsonString) : base(jsonString)
            {
                Type = MessageBodyType.LOCATION;
            }

            internal LocationBody(JSONObject jsonObject) : base(jsonObject)
            {
                Type = MessageBodyType.LOCATION;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                Latitude = jo["latitude"].AsDouble;
                Longitude = jo["longitude"].AsDouble;
                Address = jo["address"].Value;
                BuildingName = jo["buildingName"].Value;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("latitude", Latitude);
                jo_body.AddWithoutNull("longitude", Longitude);
                jo_body.AddWithoutNull("address", Address ?? "");
                jo_body.AddWithoutNull("buildingName", BuildingName ?? "");

                return jo;
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

            internal FileBody(string json) : base(json)
            {
                Type = MessageBodyType.FILE;
            }
            internal FileBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.FILE;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("localPath", LocalPath ?? "");
                jo_body.AddWithoutNull("displayName", DisplayName ?? "");
                jo_body.AddWithoutNull("fileSize", FileSize);
                jo_body.AddWithoutNull("remotePath", RemotePath ?? "");
                jo_body.AddWithoutNull("secret", Secret ?? "");
                jo_body.AddWithoutNull("fileStatus", DownStatus.ToInt());

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                LocalPath = jo["localPath"];
                FileSize = jo["fileSize"];
                DisplayName = jo["displayName"];
                RemotePath = jo["remotePath"];
                Secret = jo["secret"];
                DownStatus = jo["fileStatus"].AsInt.ToDownLoadStatus();
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
             * 缩略图的下载状态。
             *
             * \~english
             * The status for downloading the thumbnail.
             */
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;
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


            internal ImageBody()
            {
                Type = MessageBodyType.IMAGE;
            }

            internal ImageBody(string json) : base(json)
            {
                Type = MessageBodyType.IMAGE;
            }
            internal ImageBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.IMAGE;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("thumbnailLocalPath", ThumbnailLocalPath ?? "");
                jo_body.AddWithoutNull("thumbnailRemotePath", ThumbnaiRemotePath ?? "");
                jo_body.AddWithoutNull("thumbnailSecret", ThumbnaiSecret ?? "");
                jo_body.AddWithoutNull("thumbnailStatus", ThumbnaiDownStatus.ToInt());
                jo_body.AddWithoutNull("height", Height);
                jo_body.AddWithoutNull("width", Width);
                jo_body.AddWithoutNull("sendOriginalImage", Original);

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                base.FromJsonObject(jo);
                ThumbnailLocalPath = jo["thumbnailLocalPath"].Value;
                ThumbnaiRemotePath = jo["thumbnailRemotePath"].Value;
                ThumbnaiSecret = jo["thumbnailSecret"].Value;
                ThumbnaiDownStatus = jo["thumbnailStatus"].AsInt.ToDownLoadStatus();
                Height = jo["height"].AsDouble;
                Width = jo["width"].AsDouble;
                Original = jo["sendOriginalImage"].AsBool;
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

            internal VoiceBody()
            {
                Type = MessageBodyType.VOICE;
            }

            internal VoiceBody(string json) : base(json)
            {
                Type = MessageBodyType.VOICE;
            }
            internal VoiceBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.VOICE;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("duration", Duration);

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                base.FromJsonObject(jo);
                Duration = jo["duration"].AsInt;
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
             * 缩略图下载状态。
             *
             * \~english
             * The status for downloading the thumbnail.
             */
            public DownLoadStatus ThumbnaiDownStatus = DownLoadStatus.PENDING;
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

            internal VideoBody(string json) : base(json)
            {
                Type = MessageBodyType.VIDEO;
            }
            internal VideoBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.VIDEO;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("thumbnailRemotePath", ThumbnaiRemotePath ?? "");
                jo_body.AddWithoutNull("thumbnailSecret", ThumbnaiSecret ?? "");
                jo_body.AddWithoutNull("thumbnailLocalPath", ThumbnaiLocationPath ?? "");
                jo_body.AddWithoutNull("thumbnailStatus", ThumbnaiDownStatus.ToInt());
                jo_body.AddWithoutNull("height", Height);
                jo_body.AddWithoutNull("width", Width);
                jo_body.AddWithoutNull("duration", Duration);

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                base.FromJsonObject(jo);
                ThumbnaiRemotePath = jo["thumbnailRemotePath"];
                ThumbnaiSecret = jo["thumbnailSecret"];
                ThumbnaiLocationPath = jo["thumbnailLocalPath"];
                ThumbnaiDownStatus = jo["thumbnailStatus"].AsInt.ToDownLoadStatus();
                Height = jo["height"].AsDouble;
                Width = jo["width"].AsDouble;
                Duration = jo["duration"].AsInt;
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

            internal CmdBody(string json) : base(json)
            {
                Type = MessageBodyType.CMD;
            }
            internal CmdBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.CMD;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("deliverOnlineOnly", DeliverOnlineOnly);
                jo_body.AddWithoutNull("action", Action ?? "");

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                Action = jo["action"].Value;
                DeliverOnlineOnly = jo["deliverOnlineOnly"].AsBool;
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

            internal CustomBody(string json) : base(json)
            {
                Type = MessageBodyType.CUSTOM;
            }
            internal CustomBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.CUSTOM;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("event", CustomEvent ?? "");
                jo_body.AddWithoutNull("params", JsonObject.JsonObjectFromDictionary(CustomParams));

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                CustomEvent = jo["event"].Value;
                CustomParams = Dictionary.StringDictionaryFromJsonObject(jo["params"]);
            }
        }
    }
}


