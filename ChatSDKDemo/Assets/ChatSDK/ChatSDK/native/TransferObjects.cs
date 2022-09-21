﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ChatSDK.MessageBody;
using SimpleJSON;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
  
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class TextMessageTO : MessageTO
    {
        TextMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct TextMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string Content;
            public string TargetLanguages; // json format
            [MarshalAs(UnmanagedType.LPTStr)]
            public string Translations; // json format

            public TextMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.TXT)
                {
                    var body = message.Body as TextBody;
                    Content = body.Text;
                    TargetLanguages = TransformTool.JsonStringFromStringList(body.TargetLanguages);
                    Translations = TransformTool.JsonStringFromDictionary(body.Translations);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public TextMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.TXT;
            Body = new TextMessageBodyTO(message);
        }

        public TextMessageTO() : base()
        {

        }

        internal Dictionary<string, string> GetUnicodeDicFromUTF8Dict(Dictionary<string, string> inDict)
        {
            Dictionary<string, string> outDict = new Dictionary<string, string>();

            if (0 == inDict.Count) return outDict;
            
            foreach(var it in inDict)
            {
                string key = it.Key;
                string value = TransformTool.GetUnicodeStringFromUTF8(it.Value);
                outDict[key] = value;
            }

            return outDict;
        }

        public override void UpdateMsgBody(Message msg)
        {
            if (null == msg || msg.Body.Type != MessageBodyType.TXT)
                return;

            TextBody tb = (TextBody)msg.Body;

            if (Body.TargetLanguages.Length > 3)
            {
                tb.TargetLanguages = TransformTool.JsonStringToStringList(Body.TargetLanguages);
            }


            if (Body.Translations.Length > 3)
            {
                Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(TransformTool.GetUnicodeStringFromUTF8(Body.Translations));
                tb.Translations = dict;
            }
        }

        public override IMessageBody UnmarshallBody()
        {
            string unicodeStr = TransformTool.GetUnicodeStringFromUTF8(Body.Content);

            // change EMPTY_STR(" ")  to ""
            Body.Content = (unicodeStr.CompareTo(" ") == 0) ? "" : unicodeStr;

            MessageBody.TextBody textBody = new MessageBody.TextBody(Body.Content);

            if (Body.TargetLanguages.Length > 3)
                textBody.TargetLanguages = TransformTool.JsonStringToStringList(Body.TargetLanguages);
            else
                textBody.TargetLanguages = new List<string>();

            if (Body.Translations.Length > 3)
            {
                Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(TransformTool.GetUnicodeStringFromUTF8(Body.Translations));
                textBody.Translations = dict;
            }                
            else
                textBody.Translations = new Dictionary<string, string>();
            return textBody;
        }

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class LocationMessageTO : MessageTO
    {
        LocationMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct LocationMessageBodyTO
        {
            public double Latitude;
            public double Longitude;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string Address;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string BuildingName;

            public LocationMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.LOCATION)
                {
                    var body = message.Body as LocationBody;
                    Latitude = body.Latitude;
                    Longitude = body.Longitude;
                    Address = body.Address;
                    BuildingName = body.BuildingName;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public LocationMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.LOCATION;
            Body = new LocationMessageBodyTO(message);
        }

        public LocationMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
            // do nothing
        }


        public override IMessageBody UnmarshallBody()
        {
            string address = TransformTool.GetUnicodeStringFromUTF8(Body.Address);
            string building = TransformTool.GetUnicodeStringFromUTF8(Body.BuildingName);

            // change EMPTY_STR(" ")  to ""

            Body.Address = (address.CompareTo(" ") == 0) ? "" : address;
            Body.BuildingName = (building.CompareTo(" ") == 0) ? "" : building;

            return new MessageBody.LocationBody(Body.Latitude, Body.Longitude, Body.Address, Body.BuildingName);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class CmdMessageTO : MessageTO
    {
        CmdMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct CmdMessageBodyTO
        {
            public string Action;
            [MarshalAs(UnmanagedType.U1)]
            public bool DeliverOnlineOnly;

            public CmdMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.CMD)
                {
                    var body = message.Body as CmdBody;
                    Action = body.Action;
                    DeliverOnlineOnly = body.DeliverOnlineOnly;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public CmdMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.CMD;
            Body = new CmdMessageBodyTO(message);
        }

        public CmdMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
            // do nothing
        }

        public override IMessageBody UnmarshallBody()
        {
            // change EMPTY_STR(" ")  to ""
            if (Body.Action.CompareTo(" ") == 0) Body.Action = "";

            return new MessageBody.CmdBody(Body.Action, Body.DeliverOnlineOnly);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class FileMessageTO : MessageTO
    {
        FileMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct FileMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string LocalPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DisplayName;
            public string Secret;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string RemotePath;
            public long FileSize;
            public DownLoadStatus DownStatus;

            public FileMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.FILE)
                {
                    var body = message.Body as FileBody;
                    LocalPath = body.LocalPath;
                    DisplayName = body.DisplayName;
                    Secret = body.Secret ?? "";
                    RemotePath = body.RemotePath ?? "";
                    FileSize = body.FileSize;
                    DownStatus = body.DownStatus;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public FileMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.FILE;
            Body = new FileMessageBodyTO(message);
        }

        public FileMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
            if (null == msg || msg.Body.Type != MessageBodyType.FILE)
                return;

            FileBody fb = (FileBody)msg.Body;

            // change EMPTY_STR(" ")  to ""
            if (Body.DisplayName.CompareTo(" ") != 0)   fb.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            if (Body.LocalPath.CompareTo(" ") != 0)     fb.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            if (Body.RemotePath.CompareTo(" ") != 0)    fb.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            if (Body.Secret.CompareTo(" ") != 0)        fb.Secret = Body.Secret;

            fb.FileSize = Body.FileSize;
            fb.DownStatus = Body.DownStatus;
        }

        public override IMessageBody UnmarshallBody()
        {
            MessageBody.FileBody fb = new MessageBody.FileBody();
            fb.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            fb.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            fb.Secret = Body.Secret;
            fb.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            fb.FileSize = Body.FileSize;
            fb.DownStatus = Body.DownStatus;

            // change EMPTY_STR(" ")  to ""
            if (fb.DisplayName.CompareTo(" ") == 0) fb.DisplayName = "";
            if (fb.LocalPath.CompareTo(" ") == 0)   fb.LocalPath = "";
            if (fb.RemotePath.CompareTo(" ") == 0)  fb.RemotePath = "";
            if (fb.Secret.CompareTo(" ") == 0)      fb.Secret = "";

            return fb;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ImageMessageTO : MessageTO
    {
        ImageMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct ImageMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string LocalPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DisplayName;
            public string Secret;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string RemotePath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string ThumbnailLocalPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string ThumbnaiRemotePath;
            public string ThumbnaiSecret;
            public double Height;
            public double Width;
            public long FileSize;
            public DownLoadStatus DownStatus;
            public DownLoadStatus ThumbnaiDownStatus;
            [MarshalAs(UnmanagedType.U1)]
            public bool Original;

            public ImageMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.IMAGE)
                {
                    var body = message.Body as ImageBody;
                    LocalPath = body.LocalPath;
                    DisplayName = body.DisplayName ?? "";
                    Secret = body.Secret ?? "";
                    RemotePath = body.RemotePath ?? "";
                    FileSize = body.FileSize;
                    DownStatus = body.DownStatus;
                    ThumbnailLocalPath = body.ThumbnailLocalPath ?? "";
                    ThumbnaiRemotePath = body.ThumbnaiRemotePath ?? "";
                    ThumbnaiSecret = body.ThumbnaiSecret ?? "";
                    Height = body.Height;
                    Width = body.Width;
                    ThumbnaiDownStatus = body.ThumbnaiDownStatus;
                    Original = body.Original;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public ImageMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.IMAGE;
            Body = new ImageMessageBodyTO(message);
        }

        public ImageMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
            if (null == msg || msg.Body.Type != MessageBodyType.IMAGE)
                return;

            ImageBody ib = (ImageBody)msg.Body;

            // change EMPTY_STR(" ")  to ""
            if (Body.DisplayName.CompareTo(" ") != 0)           ib.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            if (Body.LocalPath.CompareTo(" ") != 0)             ib.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            if (Body.RemotePath.CompareTo(" ") != 0)            ib.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            if (Body.Secret.CompareTo(" ") != 0)                ib.Secret = Body.Secret;
            if (Body.ThumbnaiSecret.CompareTo(" ") != 0)        ib.ThumbnaiSecret = Body.ThumbnaiSecret;
            if (Body.ThumbnaiRemotePath.CompareTo(" ") != 0)    ib.ThumbnaiRemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiRemotePath);
            if (Body.ThumbnailLocalPath.CompareTo(" ") != 0)    ib.ThumbnailLocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnailLocalPath);

            ib.FileSize = Body.FileSize;
            ib.DownStatus = Body.DownStatus;
            ib.ThumbnaiDownStatus = Body.ThumbnaiDownStatus;
        }

        public override IMessageBody UnmarshallBody()
        {
            MessageBody.ImageBody ib = new MessageBody.ImageBody();
            ib.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            ib.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            ib.Secret = Body.Secret;
            ib.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            ib.ThumbnailLocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnailLocalPath);
            ib.ThumbnaiRemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiRemotePath);
            ib.ThumbnaiSecret = Body.ThumbnaiSecret;
            ib.Height = Body.Height;
            ib.Width = Body.Width;
            ib.FileSize = Body.FileSize;
            ib.DownStatus = Body.DownStatus;
            ib.ThumbnaiDownStatus = Body.ThumbnaiDownStatus;
            //ib.Original = Body.Original;

            // change EMPTY_STR(" ")  to ""
            if (ib.DisplayName.CompareTo(" ") == 0)         ib.DisplayName = "";
            if (ib.LocalPath.CompareTo(" ") == 0)           ib.LocalPath = "";
            if (ib.RemotePath.CompareTo(" ") == 0)          ib.RemotePath = "";
            if (ib.Secret.CompareTo(" ") == 0)              ib.Secret = "";
            if (ib.ThumbnaiSecret.CompareTo(" ") == 0)      ib.ThumbnaiSecret = "";
            if (ib.ThumbnaiRemotePath.CompareTo(" ") == 0)  ib.ThumbnaiRemotePath = "";
            if (ib.ThumbnailLocalPath.CompareTo(" ") == 0)  ib.ThumbnailLocalPath = "";

            return ib;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class VoiceMessageTO : MessageTO
    {
        VoiceMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VoiceMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string LocalPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DisplayName;
            public string Secret;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string RemotePath;
            public long FileSize;
            public DownLoadStatus DownStatus;
            public int Duration;

            public VoiceMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.VOICE)
                {
                    var body = message.Body as VoiceBody;
                    LocalPath = body.LocalPath;
                    DisplayName = body.DisplayName ?? "";
                    Secret = body.Secret ?? "";
                    RemotePath = body.RemotePath ?? "";
                    FileSize = body.FileSize;
                    DownStatus = body.DownStatus;
                    Duration = body.Duration;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public VoiceMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.VOICE;
            Body = new VoiceMessageBodyTO(message);
        }

        public VoiceMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
            if (null == msg || msg.Body.Type != MessageBodyType.VOICE)
                return;

            VoiceBody vob = (VoiceBody)msg.Body;

            // change EMPTY_STR(" ")  to ""
            if (Body.DisplayName.CompareTo(" ") != 0)   vob.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            if (Body.LocalPath.CompareTo(" ") != 0)     vob.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            if (Body.RemotePath.CompareTo(" ") != 0)    vob.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            if (Body.Secret.CompareTo(" ") != 0)        vob.Secret = Body.Secret;

            vob.FileSize = Body.FileSize;
            vob.DownStatus = Body.DownStatus;
            vob.Duration = Body.Duration;
        }

        public override IMessageBody UnmarshallBody()
        {
            MessageBody.VoiceBody voi = new MessageBody.VoiceBody();
            voi.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            voi.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            voi.Secret = Body.Secret;
            voi.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            voi.FileSize = Body.FileSize;
            voi.DownStatus = Body.DownStatus;
            voi.Duration = Body.Duration;

            // change EMPTY_STR(" ")  to ""
            if (voi.DisplayName.CompareTo(" ") == 0)    voi.DisplayName = "";
            if (voi.LocalPath.CompareTo(" ") == 0)      voi.LocalPath = "";
            if (voi.RemotePath.CompareTo(" ") == 0)     voi.RemotePath = "";
            if (voi.Secret.CompareTo(" ") == 0)         voi.Secret = "";

            return voi;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class VideoMessageTO : MessageTO
    {
        VideoMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VideoMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string LocalPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DisplayName;
            public string Secret;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string RemotePath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string ThumbnaiLocationPath;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string ThumbnaiRemotePath;
            public string ThumbnaiSecret;
            public double Height;
            public double Width;
            public int Duration;
            public long FileSize;
            public DownLoadStatus DownStatus;

            public VideoMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.VIDEO)
                {
                    var body = message.Body as VideoBody;
                    LocalPath = body.LocalPath;
                    DisplayName = body.DisplayName ?? "";
                    Secret = body.Secret ?? "";
                    RemotePath = body.RemotePath ?? "";
                    ThumbnaiLocationPath = body.ThumbnaiLocationPath ?? "";
                    ThumbnaiRemotePath = body.ThumbnaiRemotePath ?? "";
                    ThumbnaiSecret = body.ThumbnaiSecret ?? "";
                    Height = body.Height;
                    Width = body.Width;
                    Duration = body.Duration;
                    FileSize = body.FileSize;
                    DownStatus = body.DownStatus;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public VideoMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.VIDEO;
            Body = new VideoMessageBodyTO(message);
        }

        public VideoMessageTO()
        {
        
        }

        public override void UpdateMsgBody(Message msg)
        {
            if (null == msg || msg.Body.Type != MessageBodyType.VIDEO)
                return;

            VideoBody vib = (VideoBody)msg.Body;

            // change EMPTY_STR(" ")  to ""
            if (Body.DisplayName.CompareTo(" ") != 0)           vib.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            if (Body.LocalPath.CompareTo(" ") != 0)             vib.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            if (Body.RemotePath.CompareTo(" ") != 0)            vib.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            if (Body.Secret.CompareTo(" ") != 0)                vib.Secret = Body.Secret;
            if (Body.ThumbnaiSecret.CompareTo(" ") != 0)        vib.ThumbnaiSecret = Body.ThumbnaiSecret;
            if (Body.ThumbnaiRemotePath.CompareTo(" ") != 0)    vib.ThumbnaiRemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiRemotePath);
            if (Body.ThumbnaiLocationPath.CompareTo(" ") != 0)  vib.ThumbnaiLocationPath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiLocationPath);
            
            vib.FileSize = Body.FileSize;
            vib.DownStatus = Body.DownStatus;
            vib.Duration = Body.Duration;
        }

        public override IMessageBody UnmarshallBody()
        {
            MessageBody.VideoBody vid = new MessageBody.VideoBody();
            vid.LocalPath = TransformTool.GetUnicodeStringFromUTF8(Body.LocalPath);
            vid.DisplayName = TransformTool.GetUnicodeStringFromUTF8(Body.DisplayName);
            vid.Secret = Body.Secret;
            vid.RemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.RemotePath);
            vid.ThumbnaiLocationPath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiLocationPath);
            vid.ThumbnaiRemotePath = TransformTool.GetUnicodeStringFromUTF8(Body.ThumbnaiRemotePath);
            vid.ThumbnaiSecret = Body.ThumbnaiSecret;
            vid.Height = Body.Height;
            vid.Width = Body.Width;
            vid.Duration = Body.Duration;
            vid.FileSize = Body.FileSize;
            vid.DownStatus = Body.DownStatus;

            // change EMPTY_STR(" ")  to ""
            if (vid.LocalPath.CompareTo(" ") == 0)              vid.LocalPath = "";
            if (vid.DisplayName.CompareTo(" ") == 0)            vid.DisplayName = "";
            if (vid.Secret.CompareTo(" ") == 0)                 vid.Secret = "";
            if (vid.RemotePath.CompareTo(" ") == 0)             vid.RemotePath = "";
            if (vid.ThumbnaiLocationPath.CompareTo(" ") == 0)   vid.ThumbnaiLocationPath = "";
            if (vid.ThumbnaiRemotePath.CompareTo(" ") == 0)     vid.ThumbnaiRemotePath = "";
            if (vid.ThumbnaiSecret.CompareTo(" ") == 0)         vid.ThumbnaiSecret = "";

            return vid;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class CustomMessageTO : MessageTO
    {
        CustomMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct CustomMessageBodyTO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string CustomEvent;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string CustomParams; //corresponding to Dictionary<string, string>

            public CustomMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.CUSTOM)
                {
                    var body = message.Body as CustomBody;
                    CustomEvent = body.CustomEvent;
                    if (null != body.CustomParams && body.CustomParams.Count > 0)
                    {
                        CustomParams = TransformTool.JsonStringFromDictionary(body.CustomParams);
                    }
                    else
                        CustomParams = "";
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public CustomMessageTO(in Message message) : base(message)
        {
            BodyType = MessageBodyType.CUSTOM;
            Body = new CustomMessageBodyTO(message);
        }

        public CustomMessageTO()
        {

        }

        public override void UpdateMsgBody(Message msg)
        {
           // do nothing
        }

        public override IMessageBody UnmarshallBody()
        {
            Dictionary<string, string> dict;
            // valid json format: {a:b}
            if (null != Body.CustomParams && Body.CustomParams.Length > 3)
                dict = TransformTool.JsonStringToDictionary(TransformTool.GetUnicodeStringFromUTF8(Body.CustomParams));
            else
                dict = new Dictionary<string, string>(); // empty dict

            return new MessageBody.CustomBody(TransformTool.GetUnicodeStringFromUTF8(Body.CustomEvent), dict);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class MessageTO
    {
        public string MsgId;
        public string ConversationId;
        public string From;
        public string To;
        public string RecallBy;
        public MessageType Type;
        public MessageDirection Direction;
        public MessageStatus Status;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasDeliverAck;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasReadAck;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsNeedGroupAck;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsRead;
        [MarshalAs(UnmanagedType.U1)]
        public bool MessageOnlineState;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string AttributesValues;
        public long LocalTime;
        public long ServerTime;

        [MarshalAs(UnmanagedType.U1)]
        public bool IsThread;

        public MessageBodyType BodyType;

        protected MessageTO(in Message message)
        {
            MsgId = message.MsgId;
            ConversationId = message.ConversationId;
            From = message.From;
            To = message.To;
            Type = message.MessageType;
            Direction = message.Direction;
            Status = message.Status;
            HasReadAck = message.HasReadAck;
            HasDeliverAck = message.HasDeliverAck;
            IsNeedGroupAck = message.IsNeedGroupAck;
            AttributesValues = TransformTool.JsonStringFromAttributes(message.Attributes);
            LocalTime = message.LocalTime;
            ServerTime = message.ServerTime;
            BodyType = message.Body.Type;
            IsThread = message.IsThread;
        }

        protected MessageTO()
        {

        }

        public static List<Message> ConvertToMessageList(in MessageTO[] _messages, int size)
        {
            List<Message> messages = new List<Message>();
            for (int i = 0; i < size; i++)
            {
                messages.Add(_messages[i].Unmarshall());
            }
            return messages;
        }

        //factory method
        public static MessageTO FromMessage(in Message message)
        {
            MessageTO mto = null;
            switch (message.Body.Type)
            {
                case MessageBodyType.TXT:
                    mto = new TextMessageTO(message);
                    break;
                case MessageBodyType.LOCATION:
                    mto = new LocationMessageTO(message);
                    break;
                case MessageBodyType.CMD:
                    mto = new CmdMessageTO(message);
                    break;
                case MessageBodyType.FILE:
                    mto = new FileMessageTO(message);
                    break;
                case MessageBodyType.IMAGE:
                    mto = new ImageMessageTO(message);
                    break;
                case MessageBodyType.VOICE:
                    mto = new VoiceMessageTO(message);
                    break;
                case MessageBodyType.VIDEO:
                    mto = new VideoMessageTO(message);
                    break;
                case MessageBodyType.CUSTOM:
                    mto = new CustomMessageTO(message);
                    break;
            }
            return mto;
        }

        public static MessageTO FromIntPtr(IntPtr intPtr, MessageBodyType type)
        {
            if (IntPtr.Zero == intPtr)
                return null;

            MessageTO mto = null;

            switch (type)
            {
                case MessageBodyType.TXT:
                    mto = Marshal.PtrToStructure<TextMessageTO>(intPtr);
                    break;
                case MessageBodyType.LOCATION:
                    mto = Marshal.PtrToStructure<LocationMessageTO>(intPtr);
                    break;
                case MessageBodyType.CMD:
                    mto = Marshal.PtrToStructure<CmdMessageTO>(intPtr);
                    break;
                case MessageBodyType.FILE:
                    mto = Marshal.PtrToStructure<FileMessageTO>(intPtr);
                    break;
                case MessageBodyType.IMAGE:
                    mto = Marshal.PtrToStructure<ImageMessageTO>(intPtr);
                    break;
                case MessageBodyType.VOICE:
                    mto = Marshal.PtrToStructure<VoiceMessageTO>(intPtr);
                    break;
                case MessageBodyType.VIDEO:
                    mto = Marshal.PtrToStructure<VideoMessageTO>(intPtr);
                    break;
                case MessageBodyType.CUSTOM:
                    mto = Marshal.PtrToStructure<CustomMessageTO>(intPtr);
                    break;
            }
            return mto;
        }

        public static List<Message> GetMessageListFromIntPtrArray(IntPtr[] _messages, MessageBodyType[] types, int size)
        {
            var messages = new List<Message>(size);
            for (int i = 0; i < size; i++)
            {
                MessageTO mto = null;
                mto = FromIntPtr(_messages[i], types[i]);
                if (null != mto) messages.Add(mto.Unmarshall());
                //_messages[i] memory released at unmanaged side!                
            }
            return messages;
        }

        public virtual IMessageBody UnmarshallBody() { return null; }
        public virtual void UpdateMsgBody(Message msg) { }

        internal void UpdateMsg(Message msg)
        {
            msg.MsgId = MsgId;
            UpdateMsgBody(msg);
        }


        internal Message Unmarshall()
        {
            var result = new Message()
            {
                MsgId = MsgId,
                ConversationId = ConversationId,
                From = From,
                To = To,
                MessageType = Type,
                Direction = Direction,
                Status = Status,
                Attributes = TransformTool.JsonStringToAttributes(TransformTool.GetUnicodeStringFromUTF8(AttributesValues)),
                LocalTime = LocalTime,
                ServerTime = ServerTime,
                HasDeliverAck = HasDeliverAck,
                HasReadAck = HasReadAck,
                IsNeedGroupAck = IsNeedGroupAck,
                IsRead = IsRead,
                MessageOnlineState = MessageOnlineState,
            };

            // change EMPTY_STR(" ")  to ""
            if (result.MsgId.CompareTo(" ") == 0)           result.MsgId = "";
            if (result.ConversationId.CompareTo(" ") == 0)  result.ConversationId = "";
            if (result.To.CompareTo(" ") == 0)              result.To = "";
            if (result.MsgId.CompareTo(" ") == 0)           result.MsgId = "";

            result.Body = UnmarshallBody();
            return result;
        }
    }

    public class AttributeValue
    {
        private AttributeValueType VType;

        private bool BoolV;
        private sbyte CharV;
        private char UCharV;
        private short ShortV;
        private ushort UShortV;
        private int Int32V;
        private uint UInt32V;
        private long Int64V;
        private ulong UInt64V;
        private float FloatV;
        private double DoubleV;
        private string StringV;
        private List<string> StringVecV;
        private string JsonStringV;
        //Dictionary<string, AttributeValue> AttributeV;

        internal static AttributeValue Of(in object value, AttributeValueType type)
        {
            if (type == AttributeValueType.BOOL)
            {
                return Of((bool)value);
            }
            else if (type == AttributeValueType.INT32)
            {
                return Of((int)value);
            }
            else if (type == AttributeValueType.UINT32)
            {
                return Of((uint)value);
            }
            else if (type == AttributeValueType.INT64)
            {
                return Of((long)value);
            }
            else if (type == AttributeValueType.FLOAT)
            {
                return Of((float)value);
            }
            else if (type == AttributeValueType.DOUBLE)
            {
                return Of((double)value);
            }
            else if (type == AttributeValueType.STRING ||
                type == AttributeValueType.JSONSTRING)
            {
                return Of((string)value, type);
            }
            /*
            else if (type == AttributeValueType.STRVECTOR)
            {
                return Of((List<string>)value);
            }
            else if (type == AttributeValueType.ATTRIBUTEVALUE)
            {
                return Of((Dictionary<string, AttributeValue>)value);
            }*/
            else
            {
                return null;
            }
        }

        internal static AttributeValue Of(in bool value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.BOOL,
                BoolV = value
            };
            return result;
        }
        internal static AttributeValue Of(in int value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT32,
                Int32V = value
            };
            return result;
        }

        internal static AttributeValue Of(in uint value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UINT32,
                UInt32V = value
            };
            return result;
        }

        internal static AttributeValue Of(in long value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT64,
                Int64V = value
            };
            return result;
        }
        internal static AttributeValue Of(in float value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.FLOAT,
                FloatV = value
            };
            return result;
        }

        internal static AttributeValue Of(in double value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.DOUBLE,
                DoubleV = value
            };
            return result;
        }

        internal static AttributeValue Of(in string value, AttributeValueType type)
        {
            var result = new AttributeValue();
            if (AttributeValueType.JSONSTRING == type)
            {
                result.VType = AttributeValueType.JSONSTRING;
                result.JsonStringV = value;
            }
            else
            {
                result.VType = AttributeValueType.STRING;
                result.StringV = value;
            }
            return result;
        }

        /*
        internal static AttributeValue Of(in List<string> value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.STRVECTOR,
                StringVecV = value
            };
            return result;
        }

        internal static AttributeValue Of(in Dictionary<string, AttributeValue> value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.ATTRIBUTEVALUE,
                AttributeV = value
            };
            return result;
        }*/

        internal object GetAttributeValue(AttributeValueType type)
        {
            if (type == AttributeValueType.BOOL)
            {
                return BoolV;
            }
            else if (type == AttributeValueType.INT32)
            {
                return Int32V;
            }
            else if (type == AttributeValueType.UINT32)
            {
                return UInt32V;
            }
            else if (type == AttributeValueType.INT64)
            {
                return Int64V;
            }
            else if (type == AttributeValueType.FLOAT)
            {
                return FloatV;
            }
            else if (type == AttributeValueType.DOUBLE)
            {
                return DoubleV;
            }
            else if (type == AttributeValueType.STRING)
            {
                return StringV;
            }
            else if (type == AttributeValueType.JSONSTRING)
            {
                return JsonStringV;
            }
            /*
            else if (type == AttributeValueType.STRVECTOR)
            {
                return StringVecV;
            }
            else if (type == AttributeValueType.ATTRIBUTEVALUE)
            {
                return AttributeV;
            }*/
            else
            {
                return null;
            }
        }


        internal AttributeValueType GetAttributeValueType()
        {
            return VType;
        }

#if DEBUG
        public JSONObject ToJsonObject()
#else
        internal JSONObject ToJsonObject()
#endif
        {
            JSONObject jo = new JSONObject();
            string _type;
            string value;
            JSONObject jo_attr = null;
            JSONArray array = null;
            switch (VType)
            {
                case AttributeValueType.BOOL:
                    _type = "b";
                    value = BoolV.ToString();
                    break;
                case AttributeValueType.INT32:
                    _type = "i";
                    value = Int32V.ToString();
                    break;
                case AttributeValueType.UINT32:
                    _type = "ui";
                    value = UInt32V.ToString();
                    break;
                case AttributeValueType.INT64:
                    _type = "l";
                    value = Int64V.ToString();
                    break;
                case AttributeValueType.FLOAT:
                    _type = "f";
                    value = FloatV.ToString();
                    break;
                case AttributeValueType.DOUBLE:
                    _type = "d";
                    value = DoubleV.ToString();
                    break;
                case AttributeValueType.STRING:
                    _type = "str";
                    value = StringV;
                    break;
                /*
                case AttributeValueType.STRVECTOR:
                    _type = "strv";
                    array = new JSONArray();
                    foreach (var it in StringVecV)
                    {
                        array.Add(it);
                    }
                    value = ""; // here use JSONObject, not string
                    break;
                */
                case AttributeValueType.JSONSTRING:
                    _type = "jstr";
                    value = JsonStringV;
                    break;
                /*
                case AttributeValueType.ATTRIBUTEVALUE:
                    _type = "attr";
                    jo_attr = new JSONObject();
                    foreach (var item in AttributeV)
                    {
                        jo_attr[item.Key] = item.Value.ToJsonObject();
                    }
                    value = ""; // here use JSONObject, not string
                    break;
                */
                default:
                    throw new NotImplementedException();
            }

            jo["type"] = _type;
            if ("attr" == _type)
            {
                jo["value"] = jo_attr;
            }
            else if ("strv" == _type)
            {
                jo["value"] = array;
            }
            else
            {
                jo["value"] = value;
            }
            return jo;
        }

        internal static AttributeValue FromJsonObject(JSONNode jn)
        {
            if (null == jn) return null;

            JSONObject jo = jn.AsObject;
            AttributeValue result = new AttributeValue();

            string typeString = jo["type"];
            JSONNode jvalue = jo["value"];
            string value = null;
            if ("strv" != typeString && "attr" != typeString)
            {
                value = jvalue.Value;
            }

            switch (typeString)
            {
                case "b":
                    result.VType = AttributeValueType.BOOL;
                    result.BoolV = Boolean.Parse(value);
                    break;
                case "i":
                    result.VType = AttributeValueType.INT32;
                    result.Int32V = int.Parse(value);
                    break;
                case "ui":
                    result.VType = AttributeValueType.UINT32;
                    result.UInt32V = uint.Parse(value);
                    break;
                case "l":
                    result.VType = AttributeValueType.INT64;
                    result.Int64V = long.Parse(value);
                    break;
                case "f":
                    result.VType = AttributeValueType.FLOAT;
                    result.FloatV = float.Parse(value);
                    break;
                case "d":
                    result.VType = AttributeValueType.DOUBLE;
                    result.DoubleV = double.Parse(value);
                    break;
                case "str":
                    result.VType = AttributeValueType.STRING;
                    result.StringV = value;
                    break;
                /*
                case "strv":
                    result.VType = AttributeValueType.STRVECTOR;
                    result.StringVecV = new List<string>();
                    JSONArray jo_vec = jvalue.AsArray;
                    if (null == jo_vec)
                    {
                        if (jvalue.IsString)
                        {
                            JSONNode jchild = JSON.Parse(jvalue.Value);
                            jo_vec = jchild.AsArray;
                            if (null == jo_vec)
                            {
                                Debug.Log($"Cannot parse strv type, value:{jvalue}");
                                break;
                            }
                        }
                        else
                        {
                            Debug.Log($"Cannot parse strv type, value:{jvalue}");
                            break;
                        }
                    }
                    int count = 0;
                    while (count < jo_vec.Count)
                    {
                        result.StringVecV.Add(jo_vec[count]);
                        count++;
                    }
                    break;
                */
                case "jstr":
                    result.VType = AttributeValueType.JSONSTRING;
                    result.JsonStringV = value;
                    break;
                    /*
                case "attr":
                    result.VType = AttributeValueType.ATTRIBUTEVALUE;
                    result.AttributeV = new Dictionary<string, AttributeValue>();
                    JSONNode jo_attr = jvalue.AsObject;
                    if(null == jo_attr)
                    {
                        if(jvalue.IsString)
                        {
                            JSONNode jchild = JSON.Parse(jvalue.Value);
                            jo_attr = jchild.AsObject;
                            if (null == jo_attr)
                            {
                                Debug.Log($"Cannot parse attr type, value:{jvalue}");
                                break;
                            }
                        }
                        else
                        {
                            Debug.Log($"Cannot parse attr type, value:{jvalue}");
                            break;
                        }
                    }
                    foreach (string k in jo_attr.Keys)
                    {
                        result.AttributeV.Add(k, FromJsonObject(jo_attr[k]));
                    }
                    break;
                    */
                default:
                    break;
            }
            return result;
        }

        internal static void PrintAttribute(AttributeValue value)
        {
            switch (value.GetAttributeValueType())
            {
                case AttributeValueType.BOOL:
                    Debug.Log($"type: {AttributeValueType.BOOL.ToString()}, value is {value.BoolV.ToString()}");
                    break;
                case AttributeValueType.INT32:
                    Debug.Log($"type: {AttributeValueType.INT32.ToString()}, value is {value.Int32V.ToString()}");
                    break;
                case AttributeValueType.UINT32:
                    Debug.Log($"type: {AttributeValueType.UINT32.ToString()}, value is {value.UInt32V.ToString()}");
                    break;
                case AttributeValueType.INT64:
                    Debug.Log($"type: {AttributeValueType.INT64.ToString()}, value is {value.Int64V.ToString()}");
                    break;
                case AttributeValueType.FLOAT:
                    Debug.Log($"type: {AttributeValueType.FLOAT.ToString()}, value is {value.FloatV.ToString()}");
                    break;
                case AttributeValueType.DOUBLE:
                    Debug.Log($"type: {AttributeValueType.DOUBLE.ToString()}, value is {value.DoubleV.ToString()}");
                    break;
                case AttributeValueType.STRING:
                    Debug.Log($"type: {AttributeValueType.STRING.ToString()}, value is {value.StringV.ToString()}");
                    break;
                /*
                case AttributeValueType.STRVECTOR:
                    foreach (var item_print in value.StringVecV)
                    {
                        Debug.Log($"type: {AttributeValueType.STRVECTOR.ToString()}, array: {item_print}");
                    }
                    break;
                */
                case AttributeValueType.JSONSTRING:
                    Debug.Log($"type: {AttributeValueType.JSONSTRING.ToString()}, value is {value.JsonStringV.ToString()}");
                    break;
                /*
                case AttributeValueType.ATTRIBUTEVALUE:
                    Debug.Log($"type: {AttributeValueType.ATTRIBUTEVALUE.ToString()}");
                    foreach (var dict_item in value.AttributeV)
                    {
                        Debug.Log($"key: {dict_item.Key}--");
                        PrintAttribute(dict_item.Value);
                    }
                    break;
                */
                default:
                    break;
            }
        }

        internal static void ToAndFromTest(Dictionary<string, AttributeValue> attriMap)
        {
            // to json string
            JSONObject jo = new JSONObject();
            foreach (var item in attriMap)
            {
                jo[item.Key] = item.Value.ToJsonObject();
            }
            string jsonString = jo.ToString();
            Debug.Log($"toJson: --------------{jsonString}");

            // from json string
            JSONNode jn = JSON.Parse(jsonString);
            JSONNode jo_from = jn.AsObject;
            foreach (string k in jo_from.Keys)
            {
                AttributeValue attr_parsed = FromJsonObject(jo_from[k]);
                PrintAttribute(attr_parsed);
            }
        }

        internal static void ParseAttributeExample(Message msg)
        {
            if (null == msg) return;
            if (null == msg.Attributes || msg.Attributes.Count == 0) return;

            foreach (var item in msg.Attributes)
            {
                AttributeValue.PrintAttribute(item.Value);
            }
        }

#if DEBUG
        public static void MakeMsgAttributesExample(Message msg)
#else
        internal static void MakeMsgAttributesExample(Message msg)
#endif
        {
            if (null == msg) return;

            // make level3
            
            Dictionary<string, AttributeValue> level3_map = new Dictionary<string, AttributeValue>();

            bool b = true;
            Message.SetAttribute(level3_map, "level3-bool", b, AttributeValueType.BOOL);

            int i = -39999;
            Message.SetAttribute(level3_map, "level3-int32", i, AttributeValueType.INT32);

            uint ui = 3294967294;
            Message.SetAttribute(level3_map, "level3-uint32", ui, AttributeValueType.UINT32);

            long l = -3223372036854775807;
            Message.SetAttribute(level3_map, "level3-int64", l, AttributeValueType.INT64);

            float f = 3.123F;
            Message.SetAttribute(level3_map, "level3-float", f, AttributeValueType.FLOAT);

            double d = 3.23456D;
            Message.SetAttribute(level3_map, "level3-double", d, AttributeValueType.DOUBLE);

            string str = "a level3 string";
            Message.SetAttribute(level3_map, "level3-string", str, AttributeValueType.STRING);

            string jstr = "a level3 json string";
            Message.SetAttribute(level3_map, "level3-jstring", jstr, AttributeValueType.JSONSTRING);

            /*
            List<string> list3 = new List<string>();
            list3.Add("level3-array1");
            list3.Add("level3-array2");
            list3.Add("level3-array3");
            Message.SetAttribute(level3_map, "level3-list", list3, AttributeValueType.STRVECTOR);
            */
            // make level2
            Dictionary<string, AttributeValue> level2_map = new Dictionary<string, AttributeValue>();

            b = false;
            Message.SetAttribute(level2_map, "level2-bool", b, AttributeValueType.BOOL);

            i = -29999;
            Message.SetAttribute(level2_map, "level2-int32", i, AttributeValueType.INT32);

            ui = 2294967294;
            Message.SetAttribute(level2_map, "level2-uint32", ui, AttributeValueType.UINT32);

            l = -2223372036854775807;
            Message.SetAttribute(level2_map, "level2-int64", l, AttributeValueType.INT64);

            f = 2.123F;
            Message.SetAttribute(level2_map, "level2-float", f, AttributeValueType.FLOAT);

            d = 2.23456D;
            Message.SetAttribute(level2_map, "level2-double", d, AttributeValueType.DOUBLE);

            str = "a level2 string";
            Message.SetAttribute(level2_map, "level2-string", str, AttributeValueType.STRING);

            jstr = "a level2 json string";
            Message.SetAttribute(level2_map, "level2-jstring", jstr, AttributeValueType.JSONSTRING);

            /*
            List<string> list2 = new List<string>();
            list2.Add("level2-array1");
            list2.Add("level2-array2");
            list2.Add("level2-array3");
            Message.SetAttribute(level2_map, "level2-list", list2, AttributeValueType.STRVECTOR);
            */
            //Message.SetAttribute(level2_map, "level2-attr", level3_map, AttributeValueType.ATTRIBUTEVALUE);
            
            // make level1
            Dictionary<string, AttributeValue> level1_map = new Dictionary<string, AttributeValue>();

            //string jstr = "a level1 json string";
            //msg.SetAttribute(level1_map, "level1-jstring", jstr, AttributeValueType.JSONSTRING);
            
            bool b1 = true;
            Message.SetAttribute(level1_map, "level1-bool", b1, AttributeValueType.BOOL);

            
            int i1 = -19999;
            Message.SetAttribute(level1_map, "level1-int32", i1, AttributeValueType.INT32);

            uint ui1 = 1294967294;
            Message.SetAttribute(level1_map, "level1-uint32", ui1, AttributeValueType.UINT32);

            long l1 = -1223372036854775807;
            Message.SetAttribute(level1_map, "level1-int64", l1, AttributeValueType.INT64);
            
            float f1 = 1.123F;
            Message.SetAttribute(level1_map, "level1-float", f1, AttributeValueType.FLOAT);

            double d1 = 1.23456D;
            Message.SetAttribute(level1_map, "level1-double", d1, AttributeValueType.DOUBLE);
            
            string str1 = "这是中文字符串的内容，Content in Chinese.";
            Message.SetAttribute(level1_map, "level1-string-中文", str1, AttributeValueType.STRING);

            string str2 = "a level1 string1-jasldjfasdlfjasdlfjasdlfjasofasudf9asdf7as9d6fas98d6f9asd7f9asd7fs9af7as97fa9s8dsa97fasd890f7asd98fuzxd9f87zuxd09f7ad098f7uyas89df07asd09f8as7df0as";
            Message.SetAttribute(level1_map, "level1-string-en", str2, AttributeValueType.STRING);
            
            // this is not reasonable json string, so failed to send!!
            //string jstr1 = "a level1 json string";
            //Message.SetAttribute(level1_map, "level1-jstring", jstr1, AttributeValueType.JSONSTRING);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("key1", "value1");
            dictionary.Add("key2", "value2");
            dictionary.Add("钥匙3", "值three");

            string jstr2 = TransformTool.JsonStringFromDictionary(dictionary);
            Message.SetAttribute(level1_map, "level1-jstring-dict", jstr2, AttributeValueType.JSONSTRING);

            List<string> list1 = new List<string>();
            list1.Add("level1-array1");
            list1.Add("level1-array2");
            list1.Add("level1-数组3");
            string jstr3 = TransformTool.JsonStringFromStringList(list1);
            Message.SetAttribute(level1_map, "level1-list-vector", jstr3, AttributeValueType.JSONSTRING);

            /*
            List<string> list1 = new List<string>();
            list1.Add("level1-array1");
            list1.Add("level1-array2");
            list1.Add("level1-array3");
            Message.SetAttribute(level1_map, "level1-list", list1, AttributeValueType.STRVECTOR);
            */
            //Message.SetAttribute(level1_map, "level1-attr", level2_map, AttributeValueType.ATTRIBUTEVALUE);

            msg.Attributes = level1_map;

            //ToAndFromTest(msg.Attributes);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Mute
    {
        public string Member;
        public Int64 Duration;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct GroupTO
    {
        public string GroupId;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Name;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Description;

        public string Owner;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Announcement;

        public IntPtr MemberList;
        public IntPtr AdminList;
        public IntPtr BlockList;
        public IntPtr MuteList;
        public GroupOptions Options;
        public int MemberCount;
        public int AdminCount;
        public int BlockCount;
        public int MuteCount;
        public GroupPermissionType PermissionType;
        [MarshalAs(UnmanagedType.U1)]
        public bool NoticeEnabled;
        [MarshalAs(UnmanagedType.U1)]
        public bool MessageBlocked;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsAllMemberMuted;

        internal Group GroupInfo()
        {
            var result =new Group()
            {
                GroupId = GroupId,
                Name = Name,
                Description = Description,
                Owner = Owner,
                Announcement = Announcement,
                Options = Options,
                MemberCount = MemberCount,
                PermissionType = PermissionType,
                NoticeEnabled = NoticeEnabled,
                MessageBlocked = MessageBlocked,
                IsAllMemberMuted = IsAllMemberMuted
            };
            string name = TransformTool.GetUnicodeStringFromUTF8(Name);
            string desc = TransformTool.GetUnicodeStringFromUTF8(Description);
            string announcement = TransformTool.GetUnicodeStringFromUTF8(Announcement);

            result.Name = (name.CompareTo(" ") == 0) ? "" : name;
            result.Description = (desc.CompareTo(" ") == 0) ? "" : desc;
            result.Announcement = (announcement.CompareTo(" ") == 0) ? "" : announcement;

            // change EMPTY_STR(" ")  to ""
            if (result.Options.Ext.CompareTo(" ") == 0)    result.Options.Ext = "";

            var memberList = new List<string>();
            IntPtr current = MemberList;
            for(int i=0; i<MemberCount; i++)
            {
                IntPtr memberPtr = Marshal.PtrToStructure<IntPtr>(current);
                string m = Marshal.PtrToStringAnsi(memberPtr);
                memberList.Add(m);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var adminList = new List<string>();
            current = AdminList;
            for(int i=0; i<AdminCount; i++)
            {
                IntPtr adminPtr = Marshal.PtrToStructure<IntPtr>(current);
                string a = Marshal.PtrToStringAnsi(adminPtr);
                adminList.Add(a);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var blockList = new List<string>();
            current = BlockList;
            for(int i= 0; i < BlockCount; i++)
            {
                IntPtr blockPtr = Marshal.PtrToStructure<IntPtr>(current);
                string b = Marshal.PtrToStringAnsi(blockPtr);
                blockList.Add(b);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var muteList = new List<string>();
            current = MuteList;
            for(int i=0; i<MuteCount; i++)
            {
                Mute m = Marshal.PtrToStructure<Mute>(current);
                muteList.Add(m.Member);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }

            result.MemberList = memberList;
            result.AdminList = adminList;
            result.BlockList = blockList;
            result.MuteList = muteList;

            return result;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class GroupInfoTO
    {
        public string GroupId;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string GroupName;

        public GroupInfoTO()
        {

        }

        internal GroupInfo GroupInfo()
        {
            var groupInfo = new GroupInfo();
            groupInfo.GroupId = GroupId;

            string name = TransformTool.GetUnicodeStringFromUTF8(GroupName);
            groupInfo.GroupName = name;

            return groupInfo;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RoomTO
    {
        public string RoomId;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Name;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Description;

        public string Owner;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Announcement;

        public IntPtr MemberList;
        public IntPtr AdminList;
        public IntPtr BlockList;
        public IntPtr MuteList;
        public int MemberListCount;
        public int MemberCount;
        public int AdminCount;
        public int BlockCount;
        public int MuteCount;
        public RoomPermissionType PermissionType;
        public int MaxUsers;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsAllMemberMuted;

        internal Room RoomInfo()
        {
            var result = new Room()
            {
                RoomId = RoomId,
                Name = Name,
                Description = Description,
                Owner = Owner,
                Announcement = Announcement,
                MemberCount = MemberCount,
                PermissionType = PermissionType,
                MaxUsers = MaxUsers,
                IsAllMemberMuted = IsAllMemberMuted
            };

            string name = TransformTool.GetUnicodeStringFromUTF8(Name);
            string desc = TransformTool.GetUnicodeStringFromUTF8(Description);
            string announcement = TransformTool.GetUnicodeStringFromUTF8(Announcement);

            result.Name = (name.CompareTo(" ") == 0) ? "" : name;
            result.Description = (desc.CompareTo(" ") == 0) ? "" : desc;
            result.Announcement = (announcement.CompareTo(" ") == 0) ? "" : announcement;

            var memberList = new List<string>();
            IntPtr current = MemberList;
            for (int i = 0; i < MemberListCount; i++)
            {
                IntPtr memberPtr = Marshal.PtrToStructure<IntPtr>(current);
                string m = Marshal.PtrToStringAnsi(memberPtr);
                memberList.Add(m);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var adminList = new List<string>();
            current = AdminList;
            for (int i = 0; i < AdminCount; i++)
            {
                IntPtr adminPtr = Marshal.PtrToStructure<IntPtr>(current);
                string a = Marshal.PtrToStringAnsi(adminPtr);
                adminList.Add(a);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var blockList = new List<string>();
            current = AdminList;
            for (int i = 0; i < BlockCount; i++)
            {
                IntPtr blockPtr = Marshal.PtrToStructure<IntPtr>(current);
                string b = Marshal.PtrToStructure<string>(blockPtr);
                blockList.Add(b);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var muteList = new List<string>();
            current = MuteList;
            for (int i = 0; i < MuteCount; i++)
            {
                Mute m = Marshal.PtrToStructure<Mute>(current);
                muteList.Add(m.Member);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }

            result.MemberList = memberList;
            result.AdminList = adminList;
            result.BlockList = blockList;
            result.MuteList = muteList;

            return result;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class GroupReadAckTO
    {
        public string AckId;
        public string MsgId;
        public string From;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string Content;
        public int Count;
        public long Timestamp;

        public GroupReadAckTO()
        {

        }

        internal GroupReadAck Unmarshall()
        {
            var result = new GroupReadAck();
            result.AckId = AckId;
            result.MsgId = MsgId;
            result.From = From;
            result.Content = TransformTool.GetUnicodeStringFromUTF8(Content);
            result.Count = Count;
            result.Timestamp = Timestamp;
            return result;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ConversationTO
    {
        public string ConverationId;
        public ConversationType Type;
        public string ExtField;
        [MarshalAs(UnmanagedType.U1)]
        public bool isThread;

        public ConversationTO()
        {

        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorResultTOV2
    {
        public string NextPageCursor;
        public DataType Type;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PresenceTO
    {
        internal string Publisher;
        internal IntPtr DeviceJson;
        internal IntPtr StatusJson;
        internal IntPtr Ext;
        internal long LatestTime;
        internal long ExpiryTime;

        internal Presence Unmarshall()
        {
            var result = new Presence();

            result.Publisher = Publisher;
            result.LatestTime = LatestTime;
            result.ExpiryTime = ExpiryTime;

            result.statusDescription = TransformTool.PtrToString(Ext);
            if (result.statusDescription.CompareTo(" ") == 0) result.statusDescription = "";


            string deviceJson = Marshal.PtrToStringAnsi(DeviceJson);
            string statusJson = Marshal.PtrToStringAnsi(StatusJson);

            result.StatusList = new List<PresenceDeviceStatus>();
            if (deviceJson.Length > 3 && statusJson.Length > 3)
            {
                Dictionary<string, string> deviceMap = TransformTool.JsonStringToDictionary(deviceJson);
                Dictionary<string, string> statusMap = TransformTool.JsonStringToDictionary(statusJson);

                foreach (var it in deviceMap)
                {
                    PresenceDeviceStatus pds = new PresenceDeviceStatus();

                    if (statusMap.ContainsKey(it.Key) == false)
                    {
                        Debug.LogError($"Cannot find {it.Key} in statusMap.");
                        continue;
                    }

                    pds.DeviceId = it.Value;
                    pds.Status = int.Parse(statusMap[it.Key]);
                    result.StatusList.Add(pds);
                }
            }
            return result;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SupportLanguagesTO
    {
        internal string LanguageCode;
        internal string LanguageName;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string LanguageNativeName;
        public SupportLanguage SupportLanguagesInfo()
        {
            var supportLanguage = new SupportLanguage();
            supportLanguage.LanguageCode = LanguageCode;
            supportLanguage.LanguageName = LanguageName;
            supportLanguage.LanguageNativeName = TransformTool.GetUnicodeStringFromUTF8(LanguageNativeName);
            return supportLanguage;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TOItem
    {
        public int Type;
        public IntPtr Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TOArray
    {
        public DataType Type;
        public int Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public IntPtr[] Data; //list of data
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TOArrayDiff
    {
        public int Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public IntPtr[] Data; //list of data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public int[] Type; //list of type
    }
}
