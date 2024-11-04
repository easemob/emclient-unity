using AgoraChat.SimpleJSON;
using System.Collections.Generic;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
    public abstract class IMessageBody : BaseModel
    {
        [Preserve]
        public IMessageBody() { }

        [Preserve]
        internal IMessageBody(string jsonString) : base(jsonString) { }

        [Preserve]
        internal IMessageBody(JSONObject jsonObject) : base(jsonObject) { }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("type", Type.ToInt());

            JSONObject jo_body = new JSONObject();
            jo.AddWithoutNull("body", jo_body);

            return jo;
        }

        internal void FromJsonObjectToIMessageBody(JSONObject jo)
        {
            OperationTime = (long)jo["operationTime"].AsDouble;
            OperatorId = jo["operatorId"];
            OperationCount = (long)jo["operationCount"].AsDouble;
        }

        public MessageBodyType Type;

        /**
         * \~chinese
         * 最后一次消息修改的 UNIX 时间戳，单位为毫秒（只读）。
         *
         * \~english
         * The UNIX timestamp of the last message modification, in milliseconds (readonly).
         *
         */
        public long OperationTime { get; internal set; }

        /**
         * \~chinese
         * 最后一次消息修改的操作者的用户 ID（只读）。
         *
         * \~english
         * The user ID of the operator that modified the message last time (readonly).
         *
         */
        public string OperatorId { get; internal set; }

        /**
         * \~chinese
         * 消息发送后的修改次数（只读）。
         *
         * \~english
         * The number of times a message is modified (readonly).
         *
         */
        public long OperationCount { get; internal set; }
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
        [Preserve]
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
              * Target languages and translations.
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
            [Preserve]
            public TextBody(string text)
            {
                Text = text;
                Type = MessageBodyType.TXT;
            }

            [Preserve]
            internal TextBody()
            {
                Type = MessageBodyType.TXT;
            }

            [Preserve]
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
        [Preserve]
        public class LocationBody : IMessageBody
        {
            /**
             * \~chinese
             * 经纬度坐标。
             *
             * \~english
             * The latitude and longitude.
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
             * @param latitude 纬度。
             * @param longitude 经度。
             * @param address 地址信息。
             * @param buildName 建筑物名称。
             *
             * \~english
             * The location message body constructor.
             * 
             * @param latitude The latitude.
             * @param longitude The longitude.
             * @param address The address.
             * @param buildName The building name.
             */
            [Preserve]
            public LocationBody(double latitude, double longitude, string address = "", string buildName = "")
            {
                Latitude = latitude;
                Longitude = longitude;
                Address = address;
                BuildingName = buildName;
                Type = MessageBodyType.LOCATION;
            }

            [Preserve]
            internal LocationBody()
            {
                Type = MessageBodyType.LOCATION;
            }

            [Preserve]
            internal LocationBody(string jsonString) : base(jsonString)
            {
                Type = MessageBodyType.LOCATION;
            }

            [Preserve]
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
        [Preserve]
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
            [Preserve]
            public FileBody(string localPath, string displayName = "", long fileSize = 0)
            {
                LocalPath = localPath;
                DisplayName = displayName;
                FileSize = fileSize;
                Type = MessageBodyType.FILE;
            }

            [Preserve]
            internal FileBody()
            {
                Type = MessageBodyType.FILE;
            }

            [Preserve]
            internal FileBody(string json) : base(json)
            {
                Type = MessageBodyType.FILE;
            }

            [Preserve]
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
        [Preserve]
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
            public string ThumbnailRemotePath;

            /**
             * \~chinese
             * 缩略图的下载密钥。
             *
             * \~english
             * The secret for downloading the thumbnail.
             */
            public string ThumbnailSecret;


            /**
             * \~chinese
             * 缩略图的下载状态。
             *
             * \~english
             * The status for downloading the thumbnail.
             */
            public DownLoadStatus ThumbnailDownStatus = DownLoadStatus.PENDING;
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
            [Preserve]
            public ImageBody(string localPath, string displayName, long fileSize = 0, bool original = false, double width = 0, double height = 0) : base(localPath, displayName, fileSize)
            {
                Original = original;
                Height = height;
                Width = width;
                Type = MessageBodyType.IMAGE;
            }

            [Preserve]
            internal ImageBody()
            {
                Type = MessageBodyType.IMAGE;
            }

            [Preserve]
            internal ImageBody(string json) : base(json)
            {
                Type = MessageBodyType.IMAGE;
            }

            [Preserve]
            internal ImageBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.IMAGE;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("thumbnailLocalPath", ThumbnailLocalPath ?? "");
                jo_body.AddWithoutNull("thumbnailRemotePath", ThumbnailRemotePath ?? "");
                jo_body.AddWithoutNull("thumbnailSecret", ThumbnailSecret ?? "");
                jo_body.AddWithoutNull("thumbnailStatus", ThumbnailDownStatus.ToInt());
                jo_body.AddWithoutNull("height", Height);
                jo_body.AddWithoutNull("width", Width);
                jo_body.AddWithoutNull("sendOriginalImage", Original);

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                base.FromJsonObject(jo);
                ThumbnailLocalPath = jo["thumbnailLocalPath"].Value;
                ThumbnailRemotePath = jo["thumbnailRemotePath"].Value;
                ThumbnailSecret = jo["thumbnailSecret"].Value;
                ThumbnailDownStatus = jo["thumbnailStatus"].AsInt.ToDownLoadStatus();
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

        [Preserve]
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
            [Preserve]
            public VoiceBody(string localPath, string displayName, int duration, long fileSize = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Type = MessageBodyType.VOICE;
            }

            [Preserve]
            internal VoiceBody()
            {
                Type = MessageBodyType.VOICE;
            }

            [Preserve]
            internal VoiceBody(string json) : base(json)
            {
                Type = MessageBodyType.VOICE;
            }

            [Preserve]
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
        [Preserve]
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
            public string ThumbnailRemotePath;

            /**
             * \~chinese
             * 缩略图下载密钥。
             *
             * \~english
             * The secret for downloading the thumbnail.
             */
            public string ThumbnailSecret;


            /**
             * \~chinese
             * 缩略图下载状态。
             *
             * \~english
             * The status for downloading the thumbnail.
             */
            public DownLoadStatus ThumbnailDownStatus = DownLoadStatus.PENDING;
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
            [Preserve]
            public VideoBody(string localPath, string displayName, int duration, long fileSize = 0, string thumbnailLocalPath = "", double width = 0, double height = 0) : base(localPath, displayName, fileSize)
            {
                Duration = duration;
                Height = height;
                Width = width;
                ThumbnaiLocationPath = thumbnailLocalPath;
                Type = MessageBodyType.VIDEO;
            }

            [Preserve]
            internal VideoBody()
            {
                Type = MessageBodyType.VIDEO;
            }

            [Preserve]
            internal VideoBody(string json) : base(json)
            {
                Type = MessageBodyType.VIDEO;
            }

            [Preserve]
            internal VideoBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.VIDEO;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("thumbnailRemotePath", ThumbnailRemotePath ?? "");
                jo_body.AddWithoutNull("thumbnailSecret", ThumbnailSecret ?? "");
                jo_body.AddWithoutNull("thumbnailLocalPath", ThumbnaiLocationPath ?? "");
                jo_body.AddWithoutNull("thumbnailStatus", ThumbnailDownStatus.ToInt());
                jo_body.AddWithoutNull("height", Height);
                jo_body.AddWithoutNull("width", Width);
                jo_body.AddWithoutNull("duration", Duration);

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                base.FromJsonObject(jo);
                ThumbnailRemotePath = jo["thumbnailRemotePath"];
                ThumbnailSecret = jo["thumbnailSecret"];
                ThumbnaiLocationPath = jo["thumbnailLocalPath"];
                ThumbnailDownStatus = jo["thumbnailStatus"].AsInt.ToDownLoadStatus();
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
        [Preserve]
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
            [Preserve]
            public CmdBody(string action, bool deliverOnlineOnly = false)
            {
                Action = action;
                DeliverOnlineOnly = deliverOnlineOnly;
                Type = MessageBodyType.CMD;
            }

            [Preserve]
            internal CmdBody()
            {
                Type = MessageBodyType.CMD;
            }

            [Preserve]
            internal CmdBody(string json) : base(json)
            {
                Type = MessageBodyType.CMD;
            }

            [Preserve]
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
        [Preserve]
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
            [Preserve]
            public CustomBody(string customEvent, Dictionary<string, string> customParams = null)
            {
                CustomEvent = customEvent;
                CustomParams = customParams;
                Type = MessageBodyType.CUSTOM;
            }

            [Preserve]
            internal CustomBody()
            {
                Type = MessageBodyType.CUSTOM;
            }

            [Preserve]
            internal CustomBody(string json) : base(json)
            {
                Type = MessageBodyType.CUSTOM;
            }

            [Preserve]
            internal CustomBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.CUSTOM;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();

                JSONObject jo_body = jo["body"].AsObject;
                jo_body.AddWithoutNull("event", CustomEvent);
                jo_body.AddWithoutNull("params", JsonObject.JsonObjectFromDictionary(CustomParams));

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                CustomEvent = jo["event"].Value;
                CustomParams = Dictionary.StringDictionaryFromJsonObject(jo["params"]);
            }
        }

        /**
         * \~chinese
         * 合并消息体。
         *
         * \~english
         * The body of the combined message.
         */
        [Preserve]
        public class CombineBody : IMessageBody
        {
            /**
             * \~chinese
             * 合并消息的标题。
             *
             * \~english
             * The title of the combined message.
             */
            public string Title;

            /**
             * \~chinese
             * 合并消息的概要。
             *
             * \~english
             * The summary of the combined message.
             */
            public string Summary;

            /**
             * \~chinese
             * 合并消息的兼容文本信息。
             *
             * \~english
             * The compatible text of the combined message.
             */
            public string CompatibleText;

            /**
             * \~chinese
             * 合并消息包含的原始消息 ID 列表。
             *
             * \~english
             * The list of original messages included in the combined message.
             */
            public List<string> MessageList;

            internal string RemotePath;
            internal string Secret;
            internal string LocalPath;

            /**
             * \~chinese
             * 合并消息体构造方法。
             *
             * @param title             合并消息的标题。
             * @param summary           合并消息的概要。
             * @param compatibleText    合并消息的兼容信息。
             * @param messageList       合并消息包含的原始消息 ID 列表。
             *
             * \~english
             * The constructor of the combined message body.
             *
             * @param title             The title of the combined message.
             * @param summary           The summary of the combined message.
             * @param messageList       The list of IDs of original messages included in the combined message.
             *
             */
            [Preserve]
            public CombineBody(string title, string summary, string compatibleText, List<string> messageList)
            {
                Type = MessageBodyType.COMBINE;

                Title = title;
                Summary = summary;
                CompatibleText = compatibleText;
                MessageList = messageList;
            }

            [Preserve]
            internal CombineBody()
            {
                Type = MessageBodyType.COMBINE;
            }

            [Preserve]
            internal CombineBody(string json) : base(json)
            {
                Type = MessageBodyType.COMBINE;
            }

            [Preserve]
            internal CombineBody(JSONObject jo) : base(jo)
            {
                Type = MessageBodyType.COMBINE;
            }

            internal override JSONObject ToJsonObject()
            {
                JSONObject jo = base.ToJsonObject();
                JSONObject jo_body = jo["body"].AsObject;

                jo_body.AddWithoutNull("remotePath", RemotePath);
                jo_body.AddWithoutNull("secret", Secret);
                jo_body.AddWithoutNull("localPath", LocalPath);

                jo_body.AddWithoutNull("title", Title);
                jo_body.AddWithoutNull("summary", Summary);
                jo_body.AddWithoutNull("compatibleText", CompatibleText);
                jo_body.AddWithoutNull("messageList", JsonObject.JsonArrayFromStringList(MessageList));

                return jo;
            }

            internal override void FromJsonObject(JSONObject jo)
            {
                RemotePath = jo["remotePath"];
                Secret = jo["secret"];
                LocalPath = jo["localPath"];

                Title = jo["title"];
                Summary = jo["summary"];
                CompatibleText = jo["compatibleText"];
                MessageList = List.StringListFromJsonArray(jo["messageList"]);
            }
        }
    }

}


