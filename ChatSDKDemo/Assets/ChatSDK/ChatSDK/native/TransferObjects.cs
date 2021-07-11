using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ChatSDK.MessageBody;
using SimpleJSON;

namespace ChatSDK
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class TextMessageTO : MessageTO
    {
        TextMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct TextMessageBodyTO
        {
            public string Content;

            public TextMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.TXT)
                {
                    var body = message.Body as TextBody;
                    Content = body.Text;
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

        public override IMessageBody UnmarshallBody()
        {
            return new MessageBody.TextBody(Body.Content);
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
            public string Address;

            public LocationMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.LOCATION)
                {
                    var body = message.Body as LocationBody;
                    (Latitude, Longitude, Address) = (body.Latitude, body.Longitude, body.Address);
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

        public override IMessageBody UnmarshallBody()
        {
            return new MessageBody.LocationBody(Body.Latitude, Body.Longitude, Body.Address);
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
                    (Action, DeliverOnlineOnly) = (body.Action, body.DeliverOnlineOnly);
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

        public override IMessageBody UnmarshallBody()
        {
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
            public string LocalPath;
            public string DisplayName;
            public string Secret;
            public string RemotePath;
            public long FileSize;
            public DownLoadStatus DownStatus;

            public FileMessageBodyTO(in Message message)
            {
                if (message.Body.Type == MessageBodyType.FILE)
                {
                    var body = message.Body as FileBody;
                    (LocalPath, DisplayName, Secret, RemotePath, FileSize, DownStatus) =
                        (body.LocalPath, body.DisplayName, body.Secret ?? "", body.RemotePath ?? "", body.FileSize, body.DownStatus);
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

        public override IMessageBody UnmarshallBody()
        {
            return new MessageBody.FileBody(Body.LocalPath, Body.DisplayName, Body.FileSize);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ImageMessageTO : MessageTO
    {
        ImageMessageBodyTO Body;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct ImageMessageBodyTO
        {
            public string LocalPath;
            public string DisplayName;
            public string Secret;
            public string RemotePath;
            public string ThumbnailLocalPath;
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
                    (LocalPath, DisplayName, Secret, RemotePath, FileSize, DownStatus, ThumbnailLocalPath, ThumbnaiRemotePath, ThumbnaiSecret, Height, Width, ThumbnaiDownStatus, Original) =
                        (body.LocalPath, body.DisplayName ?? "", body.Secret ?? "", body.RemotePath ?? "", body.FileSize, body.DownStatus,
                        body.ThumbnailLocalPath ?? "", body.ThumbnaiRemotePath ?? "", body.ThumbnaiSecret ?? "", body.Height, body.Width,
                        body.ThumbnaiDownStatus, body.Original);
                    
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

        public override IMessageBody UnmarshallBody()
        {
            return new MessageBody.ImageBody(Body.LocalPath,Body.DisplayName,Body.FileSize,Body.Original,Body.Width,Body.Height);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public abstract class MessageTO
    {
        public string MsgId;
        public string ConversationId;
        public string From;
        public string To;
        public MessageType Type;
        public MessageDirection Direction;
        public MessageStatus Status;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasDeliverAck;
        [MarshalAs(UnmanagedType.U1)]
        public bool HasReadAck;

        /*public string[] AttributesKeys;
        public AttributeValue[] AttributesValues;
        public int AttributesSize;*/
        public long LocalTime;
        public long ServerTime;
        public MessageBodyType BodyType;

        protected MessageTO(in Message message) =>
            (ConversationId, From, To, Direction, HasReadAck, BodyType)
                = (message.ConversationId, message.From, message.To, message.Direction, message.HasReadAck, message.Body.Type);

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
            }
            return mto;
        }

        public abstract IMessageBody UnmarshallBody();

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
                LocalTime = LocalTime,
                ServerTime = ServerTime,
                HasDeliverAck = HasDeliverAck,
                HasReadAck = HasReadAck,
            };
            result.Body = UnmarshallBody();
            return result;
        }
    }

    // AttributeValue Union
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct AttributeValue
    {
        enum AttributeValueType : byte
        {
            BOOL = 0,
            CHAR,
            UCHAR,
            SHORT,
            USHORT,
            INT32,
            UINT32,
            INT64,
            UINT64,
            FLOAT,
            DOUBLE,
            STRING,
            STRVECTOR,
            JSONSTRING,
            NULLOBJ
        }

        [FieldOffset(0)]
        AttributeValueType VType;
        [FieldOffset(1), MarshalAs(UnmanagedType.U1)]
        bool BoolV;
        [FieldOffset(1)]
        sbyte CharV;
        [FieldOffset(1)]
        char UCharV;
        [FieldOffset(1)]
        short ShortV;
        [FieldOffset(1)]
        ushort UShortV;
        [FieldOffset(1)]
        int Int32V;
        [FieldOffset(1)]
        uint UInt32V;
        [FieldOffset(1)]
        long Int64V;
        [FieldOffset(1)]
        ulong UInt64V;
        [FieldOffset(1)]
        float FloatV;
        [FieldOffset(1)]
        double DoubleV;
        [FieldOffset(1)]
        string StringV;

        /*[FieldOffset(1)]
        string[] StringVec;
        [FieldOffset(1)]
        string JsonStringV;
        [FieldOffset(1)]
        IntPtr NullV;*/

        public static AttributeValue Of(in bool value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.BOOL,
                BoolV = value
            };
            return result;
        }

        public static AttributeValue Of(in sbyte value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.CHAR,
                CharV = value
            };
            return result;
        }

        public static AttributeValue Of(in char value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UCHAR,
                UCharV = value
            };
            return result;
        }

        public static AttributeValue Of(in short value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.SHORT,
                ShortV = value
            };
            return result;
        }

        public static AttributeValue Of(in ushort value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.USHORT,
                UShortV = value
            };
            return result;
        }

        public static AttributeValue Of(in int value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT32,
                Int32V = value
            };
            return result;
        }

        public static AttributeValue Of(in uint value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UINT32,
                UInt32V = value
            };
            return result;
        }

        public static AttributeValue Of(in long value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT64,
                Int64V = value
            };
            return result;
        }

        public static AttributeValue Of(in ulong value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.UINT64,
                UInt64V = value
            };
            return result;
        }

        public static AttributeValue Of(in float value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.FLOAT,
                FloatV = value
            };
            return result;
        }

        public static AttributeValue Of(in double value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.DOUBLE,
                DoubleV = value
            };
            return result;
        }

        public static AttributeValue Of(in string value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.STRING,
                StringV = value
            };
            return result;
        }

        public string ToJsonString()
        {
            JSONObject jo = new JSONObject();
            string _type;
            string value;
            switch (VType)
            {
                case AttributeValueType.BOOL:
                    _type = "b";
                    value = BoolV.ToString();
                    break;
                case AttributeValueType.CHAR:
                    _type = "c";
                    value = CharV.ToString();
                    break;
                case AttributeValueType.UCHAR:
                    _type = "uc";
                    value = UCharV.ToString();
                    break;
                case AttributeValueType.SHORT:
                    _type = "s";
                    value = ShortV.ToString();
                    break;
                case AttributeValueType.USHORT:
                    _type = "us";
                    value = UShortV.ToString();
                    break;
                case AttributeValueType.INT32:
                    _type = "i1";
                    value = Int32V.ToString();
                    break;
                case AttributeValueType.UINT32:
                    _type = "ui1";
                    value = UInt32V.ToString();
                    break;
                case AttributeValueType.INT64:
                    _type = "i2";
                    value = Int64V.ToString();
                    break;
                case AttributeValueType.UINT64:
                    _type = "ui2";
                    value = UInt64V.ToString();
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
                //TODO: add STRVECTOR, JSONSTRING, NULLOBJ
                default:
                    throw new NotImplementedException();
            }
            jo["type"] = _type;
            jo["value"] = value;
            return jo.ToString();
        }

        internal static AttributeValue FromJsonString(string jsonString)
        {
            if (jsonString == null) return new AttributeValue();
            AttributeValue result = new AttributeValue();
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            string typeString = jo["type"];
            string value = jo["value"].Value;
            switch (typeString)
            {
                case "b":
                    result.VType = AttributeValueType.BOOL;
                    result.BoolV = Boolean.Parse(value);
                    break;
                case "c":
                    result.VType = AttributeValueType.CHAR;
                    result.CharV = (sbyte)Char.Parse(value);
                    break;
                case "uc":
                    result.VType = AttributeValueType.UCHAR;
                    result.UCharV = Char.Parse(value);
                    break;
                case "s":
                    result.VType = AttributeValueType.SHORT;
                    result.ShortV = short.Parse(value);
                    break;
                case "us":
                    result.VType = AttributeValueType.USHORT;
                    result.UShortV = ushort.Parse(value);
                    break;
                case "i1":
                    result.VType = AttributeValueType.INT32;
                    result.Int32V = int.Parse(value);
                    break;
                case "ui1":
                    result.VType = AttributeValueType.UINT32;
                    result.UInt32V = uint.Parse(value);
                    break;
                case "i2":
                    result.VType = AttributeValueType.INT64;
                    result.Int64V = long.Parse(value);
                    break;
                case "ui2":
                    result.VType = AttributeValueType.UINT64;
                    result.UInt64V = ulong.Parse(value);
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
                default: throw new NotImplementedException();
            }
            return result;
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
        public string Name;
        public string Description;
        public string Owner;
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
                Annoumcement = Announcement,
                Options = Options,
                MemberCount = MemberCount,
                PermissionType = PermissionType,
                NoticeEnabled = NoticeEnabled,
                MessageBlocked = MessageBlocked,
                IsAllMemberMuted = IsAllMemberMuted
            };
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
            current = AdminList;
            for(int i= 0; i < BlockCount; i++)
            {
                IntPtr blockPtr = Marshal.PtrToStructure<IntPtr>(current);
                string b = Marshal.PtrToStructure<string>(blockPtr);
                blockList.Add(b);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }
            var muteList = new List<Mute>();
            current = MuteList;
            for(int i=0; i<MuteCount; i++)
            {
                Mute m = Marshal.PtrToStructure<Mute>(current);
                muteList.Add(m);
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
    public struct RoomTO
    {
        public string RoomId;
        public string Name;
        public string Description;
        public string Owner;
        public string Announcement;
        public IntPtr MemberList;
        public IntPtr AdminList;
        public IntPtr BlockList;
        public IntPtr MuteList;
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
                PermissionType = PermissionType,
                MaxUsers = MaxUsers,
                IsAllMemberMuted = IsAllMemberMuted
            };
            var memberList = new List<string>();
            IntPtr current = MemberList;
            for (int i = 0; i < MemberCount; i++)
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
            var muteList = new List<Mute>();
            current = MuteList;
            for (int i = 0; i < MuteCount; i++)
            {
                Mute m = Marshal.PtrToStructure<Mute>(current);
                muteList.Add(m);
                current = (IntPtr)((long)current + Marshal.SizeOf(current));
            }

            result.MemberList = memberList;
            result.AdminList = adminList;
            result.BlockList = blockList;
            result.MuteList = muteList;

            return result;
        }
    }
}