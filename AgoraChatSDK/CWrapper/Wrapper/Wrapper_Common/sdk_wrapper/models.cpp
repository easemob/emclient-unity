#include "tool.h"
#include "models.h"
#include "sdk_wrapper.h"

namespace sdk_wrapper
{
    EMChatConfigsPtr Options::FromJson(const char* json, const char* rs, const char* wk)
	{
        if (nullptr == json || strlen(json) == 0) return nullptr;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return nullptr;

        const Value& jnode = d;
        if (jnode.IsNull()) return nullptr;

        string rs_str = rs;
        string wk_str = wk;

        string app_key = "";
        if (jnode.HasMember("app_key") && jnode["app_key"].IsString()) {            
            app_key = jnode["app_key"].GetString();
        }

        if (app_key.size() == 0) return nullptr;

        EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs(rs, wk, app_key, 0));
        configs->setAppKey(app_key);

        if (jnode.HasMember("dns_url") && jnode["dns_url"].IsString()) {
            string dns_url = jnode["dns_url"].GetString();
            configs->setDnsURL(dns_url);
        }

        if (jnode.HasMember("im_server") && jnode["im_server"].IsString()) {
            string im_server = jnode["im_server"].GetString();
            configs->privateConfigs()->chatServer() = im_server;
        }

        if (jnode.HasMember("rest_server") && jnode["rest_server"].IsString()) {
            string rest_server = jnode["rest_server"].GetString();
            configs->privateConfigs()->restServer() = rest_server;
        }

        if (jnode.HasMember("im_port") && jnode["im_port"].IsInt()) {
            int im_port = jnode["im_port"].GetInt();
            configs->privateConfigs()->chatPort() = im_port;
        }

        if (jnode.HasMember("enable_dns_config") && jnode["enable_dns_config"].IsBool()) {
            bool enable_dns_config = jnode["enable_dns_config"].GetBool();
            configs->privateConfigs()->enableDnsConfig(enable_dns_config);
        }

        if (jnode.HasMember("debug_mode") && jnode["debug_mode"].IsBool()) {
            bool debug_mode = jnode["debug_mode"].GetBool();
            // according to old code, not set debug_mode
        }

        if (jnode.HasMember("auto_login") && jnode["auto_login"].IsBool()) {
            bool auto_login = jnode["auto_login"].GetBool();
            // according to old code, not set auto_login
        }

        if (jnode.HasMember("accept_invitation_always") && jnode["accept_invitation_always"].IsBool()) {
            bool accept_invitation_always = jnode["accept_invitation_always"].GetBool();
            configs->setAutoAcceptFriend(accept_invitation_always);
        }

        if (jnode.HasMember("auto_accept_group_invitation") && jnode["auto_accept_group_invitation"].IsBool()) {
            bool auto_accept_group_invitation = jnode["auto_accept_group_invitation"].GetBool();
            configs->setAutoAcceptGroup(auto_accept_group_invitation);
        }

        if (jnode.HasMember("require_ack") && jnode["require_ack"].IsBool()) {
            bool require_ack = jnode["require_ack"].GetBool();
            configs->setRequireReadAck(require_ack);
        }

        if (jnode.HasMember("require_delivery_ack") && jnode["require_delivery_ack"].IsBool()) {
            bool require_delivery_ack = jnode["require_delivery_ack"].GetBool();
            configs->setRequireDeliveryAck(require_delivery_ack);
        }

        if (jnode.HasMember("delete_messages_as_exit_group") && jnode["delete_messages_as_exit_group"].IsBool()) {
            bool delete_messages_as_exit_group = jnode["delete_messages_as_exit_group"].GetBool();
            configs->setDeleteMessageAsExitGroup(delete_messages_as_exit_group);
        }

        if (jnode.HasMember("delete_messages_as_exit_room") && jnode["delete_messages_as_exit_room"].IsBool()) {
            bool delete_messages_as_exit_room = jnode["delete_messages_as_exit_room"].GetBool();
            configs->setDeleteMessageAsExitChatRoom(delete_messages_as_exit_room);
        }

        if (jnode.HasMember("is_room_owner_leave_allowed") && jnode["is_room_owner_leave_allowed"].IsBool()) {
            bool is_room_owner_leave_allowed = jnode["is_room_owner_leave_allowed"].GetBool();
            configs->setIsChatroomOwnerLeaveAllowed(is_room_owner_leave_allowed);
        }

        if (jnode.HasMember("sort_message_by_server_time") && jnode["sort_message_by_server_time"].IsBool()) {
            bool sort_message_by_server_time = jnode["sort_message_by_server_time"].GetBool();
            configs->setSortMessageByServerTime(sort_message_by_server_time);
        }

        if (jnode.HasMember("using_https_only") && jnode["using_https_only"].IsBool()) {
            bool using_https_only = jnode["using_https_only"].GetBool();
            configs->setUsingHttps(using_https_only);
        }

        if (jnode.HasMember("server_transfer") && jnode["server_transfer"].IsBool()) {
            bool server_transfer = jnode["server_transfer"].GetBool();
            configs->setTransferAttachments(server_transfer);
        }

        if (jnode.HasMember("is_auto_download") && jnode["is_auto_download"].IsBool()) {
            bool is_auto_download = jnode["is_auto_download"].GetBool();
            configs->setAutoDownloadThumbnail(is_auto_download);
        }

        //TODO: setDeviceUuid
        return configs;
	}

    int Message::MsgTypeToInt(EMMessage::EMChatType type)
    {
        int i = 0;
        switch (type)
        {
        case EMMessage::EMChatType::SINGLE:
            i = 0;
            break;
        case EMMessage::EMChatType::GROUP:
            i = 1;
            break;
        case EMMessage::EMChatType::CHATROOM:
            i = 2;
            break;
        default:
            i = 0;
            break;
        }
        return i;
    }

    EMMessage::EMChatType Message::MsgTypeFromInt(int i)
    {
        EMMessage::EMChatType type = EMMessage::EMChatType::SINGLE;
        if (0 == i)
            type = EMMessage::EMChatType::SINGLE;
        else if (1 == i)
            type = EMMessage::EMChatType::GROUP;
        else if (2 == i)
            type = EMMessage::EMChatType::CHATROOM;

        return type;
    }

    int Message::StatusToInt(EMMessage::EMMessageStatus status)
    {
        int i = 0;
        switch (status)
        {
        case EMMessage::EMMessageStatus::NEW:
            i = 0;
            break;
        case EMMessage::EMMessageStatus::DELIVERING:
            i = 1;
            break;
        case EMMessage::EMMessageStatus::SUCCESS:
            i = 2;
            break;
        case EMMessage::EMMessageStatus::FAIL:
            i = 3;
            break;
        default:
            i = 0;
            break;
        }
        return i;
    }

    EMMessage::EMMessageStatus Message::StatusFromInt(int i)
    {
        EMMessage::EMMessageStatus status = EMMessage::EMMessageStatus::NEW;
        if (0 == i)
            status = EMMessage::EMMessageStatus::NEW;
        else if (1 == i)
            status = EMMessage::EMMessageStatus::DELIVERING;
        else if (2 == i)
            status = EMMessage::EMMessageStatus::SUCCESS;
        else if (3 == i)
            status = EMMessage::EMMessageStatus::FAIL;

        return status;
    }

    string Message::MessageDirectionToString(EMMessage::EMMessageDirection direction)
    {
        string str = "send";

        if (EMMessage::EMMessageDirection::RECEIVE == direction)
            str = "recv";

        return str;
    }

    EMMessage::EMMessageDirection Message::MessageDirectionFromString(string str)
    {
        EMMessage::EMMessageDirection direction = EMMessage::EMMessageDirection::SEND;

        if (str.compare("recv") == 0)
            direction = EMMessage::EMMessageDirection::RECEIVE;

        return direction;
    }

    string Message::BodyTypeToString(EMMessageBody::EMMessageBodyType btype)
    {
        string stype = "txt";
        switch (btype)
        {
        case EMMessageBody::TEXT:
            stype = "txt";
            break;
        case EMMessageBody::LOCATION:
            stype = "loc";
            break;
        case EMMessageBody::COMMAND:
            stype = "cmd";
            break;
        case EMMessageBody::FILE:
            stype = "file";
            break;
        case EMMessageBody::IMAGE:
            stype = "img";
            break;
        case EMMessageBody::VOICE:
            stype = "voice";
            break;
        case EMMessageBody::VIDEO:
            stype = "video";
            break;
        case EMMessageBody::CUSTOM:
            stype = "custom";
            break;
        default:
            break;
        }
        return stype;
    }

    EMMessageBody::EMMessageBodyType Message::BodyTypeFromString(string str)
    {
        EMMessageBody::EMMessageBodyType btype = EMMessageBody::EMMessageBodyType::TEXT;
        if (str.compare("txt") == 0)
            btype = EMMessageBody::TEXT;
        else if (str.compare("loc") == 0)
            btype = EMMessageBody::LOCATION;
        else if (str.compare("cmd") == 0)
            btype = EMMessageBody::COMMAND;
        else if (str.compare("file") == 0)
            btype = EMMessageBody::FILE;
        else if (str.compare("img") == 0)
            btype = EMMessageBody::IMAGE;
        else if (str.compare("voice") == 0)
            btype = EMMessageBody::VOICE;
        else if (str.compare("video") == 0)
            btype = EMMessageBody::VIDEO;
        else if (str.compare("custom") == 0)
            btype = EMMessageBody::CUSTOM;
        else
            btype = EMMessageBody::TEXT;
        return btype;
    }

    int Message::DownLoadStatusToInt(EMFileMessageBody::EMDownloadStatus download_status)
    {
        int ret = 0;
        switch (download_status)
        {
        case EMFileMessageBody::EMDownloadStatus::DOWNLOADING:
            ret = 0;
            break;
        case EMFileMessageBody::EMDownloadStatus::SUCCESSED:
            ret = 1;
            break;
        case EMFileMessageBody::EMDownloadStatus::FAILED:
            ret = 2;
            break;
        case EMFileMessageBody::EMDownloadStatus::PENDING:
            ret = 3;
            break;
        }

        return ret;
    }

    EMFileMessageBody::EMDownloadStatus Message::DownLoadStatusFromInt(int i)
    {
        EMFileMessageBody::EMDownloadStatus ret = EMFileMessageBody::EMDownloadStatus::DOWNLOADING;
        switch (i)
        {
        case 0:
            ret = EMFileMessageBody::EMDownloadStatus::DOWNLOADING;
            break;
        case 1:
            ret = EMFileMessageBody::EMDownloadStatus::SUCCESSED;
            break;
        case 2:
            ret = EMFileMessageBody::EMDownloadStatus::FAILED;
            break;
        case 3:
            ret = EMFileMessageBody::EMDownloadStatus::PENDING;
            break;
        }

        return ret;
    }

    string Message::CustomExtsToJson(EMCustomMessageBody::EMCustomExts& exts)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        for (auto it : exts) {
            writer.Key(it.first.c_str());
            writer.String(it.second.c_str());
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    EMCustomMessageBody::EMCustomExts Message::CustomExtsFromJson(string json)
    {
        EMCustomMessageBody::EMCustomExts exts;

        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return exts;

        for (auto iter = d.MemberBegin(); iter != d.MemberEnd(); ++iter) {

            string key = iter->name.GetString();
            string value = iter->value.GetString();

            pair<string, string> kv{ key, value };
            exts.push_back(kv);
        }

        return exts;
    }

    void Message::BodyToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
    {
        if (nullptr == msg) return;

        writer.StartObject();
        auto body = msg->bodies().front();
        switch (body->type()) {
        case EMMessageBody::TEXT:
        {
            EMTextMessageBodyPtr ptr = dynamic_pointer_cast<EMTextMessageBody>(body);
            writer.Key("content");
            writer.String(ptr->text().c_str()); //null or emtpy, then?

            writer.Key("targetLanguages");
            vector<string> vec = ptr->getTargetLanguages();
            writer.String(JsonStringFromVector(vec).c_str());

            writer.Key("translations");
            map<string, string> map = ptr->getTranslations();
            writer.String(JsonStringFromMap(map).c_str());
        }
        break;
        case EMMessageBody::LOCATION:
        {
            EMLocationMessageBodyPtr ptr = dynamic_pointer_cast<EMLocationMessageBody>(body);
            writer.Key("latitude");
            writer.Double(ptr->latitude());

            writer.Key("longitude");
            writer.Double(ptr->longitude());

            writer.Key("address");
            writer.String(ptr->address().c_str());

            writer.Key("buildingName");
            writer.String(ptr->buildingName().c_str());
        }
        break;
        case EMMessageBody::COMMAND:
        {
            EMCmdMessageBodyPtr ptr = dynamic_pointer_cast<EMCmdMessageBody>(body);

            writer.Key("action");
            writer.String(ptr->action().c_str());

            writer.Key("deliverOnlineOnly");
            writer.Bool(ptr->isDeliverOnlineOnly());
        }
        break;
        case EMMessageBody::FILE:
        {
            EMFileMessageBodyPtr ptr = dynamic_pointer_cast<EMFileMessageBody>(body);
            writer.Key("localPath");
            writer.String(ptr->localPath().c_str());

            writer.Key("displayName");
            writer.String(ptr->displayName().c_str());

            writer.Key("secret");
            writer.String(ptr->secretKey().c_str());

            writer.Key("remotePath");
            writer.String(ptr->remotePath().c_str());

            writer.Key("fileSize");
            writer.Int64(ptr->fileLength());

            writer.Key("fileStatus");
            writer.Int(DownLoadStatusToInt(ptr->downloadStatus()));
        }
        break;
        case EMMessageBody::IMAGE:
        {
            EMImageMessageBodyPtr ptr = dynamic_pointer_cast<EMImageMessageBody>(body);
            writer.Key("localPath");
            writer.String(ptr->localPath().c_str());

            writer.Key("displayName");
            writer.String(ptr->displayName().c_str());

            writer.Key("secret");
            writer.String(ptr->secretKey().c_str());

            writer.Key("remotePath");
            writer.String(ptr->remotePath().c_str());

            writer.Key("thumbnailLocalPath");
            writer.String(ptr->thumbnailLocalPath().c_str());

            writer.Key("thumbnailRemotePath");
            writer.String(ptr->thumbnailRemotePath().c_str());

            writer.Key("thumbnailSecret");
            writer.String(ptr->thumbnailSecretKey().c_str());

            writer.Key("height");
            writer.Double(ptr->size().mHeight);

            writer.Key("width");
            writer.Double(ptr->size().mWidth);

            writer.Key("fileSize");
            writer.Int64(ptr->fileLength());

            writer.Key("fileStatus");
            writer.Int(DownLoadStatusToInt(ptr->downloadStatus()));

            //writer.Key("ThumbnaiDownStatus");
            //writer.Int((int)ptr->thumbnailDownloadStatus());

            // Not find original related !!!
            //writer.Key("Original");
            //writer.Bool((int)ptr->downloadStatus());
        }
        break;
        case EMMessageBody::VOICE:
        {
            EMVoiceMessageBodyPtr ptr = dynamic_pointer_cast<EMVoiceMessageBody>(body);
            writer.Key("localPath");
            writer.String(ptr->localPath().c_str());

            writer.Key("displayName");
            writer.String(ptr->displayName().c_str());

            writer.Key("secret");
            writer.String(ptr->secretKey().c_str());

            writer.Key("remotePath");
            writer.String(ptr->remotePath().c_str());

            writer.Key("fileSize");
            writer.Int64(ptr->fileLength());

            writer.Key("fileStatus");
            writer.Int(DownLoadStatusToInt(ptr->downloadStatus()));

            writer.Key("duration");
            writer.Int(ptr->duration());
        }
        break;
        case EMMessageBody::VIDEO:
        {
            EMVideoMessageBodyPtr ptr = dynamic_pointer_cast<EMVideoMessageBody>(body);
            writer.Key("localPath");
            writer.String(ptr->localPath().c_str());

            writer.Key("displayName");
            writer.String(ptr->displayName().c_str());

            writer.Key("secret");
            writer.String(ptr->secretKey().c_str());

            writer.Key("remotePath");
            writer.String(ptr->remotePath().c_str());

            writer.Key("thumbnailLocalPath");
            writer.String(ptr->thumbnailLocalPath().c_str());

            writer.Key("thumbnailRemotePath");
            writer.String(ptr->thumbnailRemotePath().c_str());

            writer.Key("thumbnailSecret");
            writer.String(ptr->thumbnailSecretKey().c_str());

            writer.Key("height");
            writer.Double(ptr->size().mHeight);

            writer.Key("width");
            writer.Double(ptr->size().mWidth);

            writer.Key("duration");
            writer.Int(ptr->duration());

            writer.Key("fileSize");
            writer.Int64(ptr->fileLength());

            writer.Key("fileStatus");
            writer.Int(DownLoadStatusToInt(ptr->downloadStatus()));
        }
        break;
        case EMMessageBody::CUSTOM:
        {
            EMCustomMessageBodyPtr ptr = dynamic_pointer_cast<EMCustomMessageBody>(body);
            writer.Key("event");
            writer.String(ptr->event().c_str());

            writer.Key("params");

            EMCustomMessageBody::EMCustomExts exts = ptr->exts();
            writer.String(CustomExtsToJson(exts).c_str());
        }
        break;
        default:
        {
            //message = NULL;
        }
        }
        writer.EndObject();
    }

    string Message::BodyToJson(EMMessagePtr msg)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);
        BodyToJsonWriter(writer, msg);
        string data = s.GetString();
        return data;
    }

    EMMessageBodyPtr Message::BodyFromJsonObject(const Value& jnode)
    {
        if (jnode.IsNull()) return nullptr;
        if (jnode["BodyType"].IsNull() || !jnode["BodyType"].IsString()) return nullptr;
        if (jnode["Body"].IsNull() || !jnode["Body"].IsString()) return nullptr;

        //Body type must have and must be string
        EMMessageBody::EMMessageBodyType btype = BodyTypeFromString(jnode["BodyType"].GetString());

        EMMessageBodyPtr bodyptr = nullptr;

        Document d;
        d.Parse(jnode["Body"].GetString());
        if (d.HasParseError()) return nullptr;

        const Value& body = d;

        switch (btype) {
        case EMMessageBody::TEXT:
        {
            EMTextMessageBodyPtr ptr = EMTextMessageBodyPtr(new EMTextMessageBody());

            if (body.HasMember("content") && body["content"].IsString()) {
                string str = body["content"].GetString();
                ptr->setText(str);
            }

            if (body.HasMember("translations") && body["translations"].IsString()) {
                string str = body["translations"].GetString();
                vector<string> tagert_languages = JsonStringToVector(str);
                ptr->setTargetLanguages(tagert_languages);
            }

            if (body.HasMember("targetLanguages") && body["targetLanguages"].IsString()) {
                string str = body["targetLanguages"].GetString();
                map<string, string> translations = JsonStringToMap(str);
                ptr->setTranslations(translations);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::LOCATION:
        {
            double latitude = 0;
            if (body.HasMember("latitude") && body["latitude"].IsDouble()) {
                latitude = body["latitude"].GetDouble();
            }

            double longitude = 0;
            if (body.HasMember("longitude") && body["longitude"].IsDouble()) {
                longitude = body["longitude"].GetDouble();
            }

            EMLocationMessageBodyPtr ptr = EMLocationMessageBodyPtr(new EMLocationMessageBody(latitude, longitude));

            if (body.HasMember("address") && body["address"].IsString()) {
                string str = body["address"].GetString();
                ptr->setAddress(str);
            }

            if (body.HasMember("buildingName") && body["buildingName"].IsString()) {
                string str = body["buildingName"].GetString();
                ptr->setBuildingName(str);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::COMMAND:
        {
            string action = "";
            if (body.HasMember("action") && body["action"].IsString()) {
                action = body["action"].GetString();
            }

            EMCmdMessageBodyPtr ptr = EMCmdMessageBodyPtr(new EMCmdMessageBody(action));

            if (body.HasMember("deliverOnlineOnly") && body["deliverOnlineOnly"].IsBool()) {
                bool b = body["deliverOnlineOnly"].GetBool();
                ptr->deliverOnlineOnly(b);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::FILE:
        {
            EMFileMessageBodyPtr ptr = EMFileMessageBodyPtr(new EMFileMessageBody());

            if (body.HasMember("localPath") && body["localPath"].IsString()) {
                string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::IMAGE:
        {
            EMImageMessageBodyPtr ptr = EMImageMessageBodyPtr(new EMImageMessageBody());

            if (body.HasMember("localPath") && body["localPath"].IsString()) {
                string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            if (body.HasMember("thumbnailLocalPath") && body["thumbnailLocalPath"].IsString()) {
                string str = body["thumbnailLocalPath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailRemotePath") && body["thumbnailRemotePath"].IsString()) {
                string str = body["thumbnailRemotePath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailSecret") && body["thumbnailSecret"].IsString()) {
                string str = body["thumbnailSecret"].GetString();
                ptr->setThumbnailSecretKey(str);
            }

            EMImageMessageBody::Size size;
            size.mWidth = 0;
            size.mHeight = 0;

            if (body.HasMember("height") && body["height"].IsDouble()) {
                size.mHeight = body["height"].GetDouble();
            }

            if (body.HasMember("width") && body["width"].IsDouble()) {
                size.mWidth = body["width"].GetDouble();
            }

            ptr->setSize(size);

            //if (body.HasMember("sendOriginalImage") && body["sendOriginalImage"].IsBool()) {
            //    bool b = body["sendOriginalImage"].GetBool();            
            //}

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::VOICE:
        {

            EMVoiceMessageBodyPtr ptr = EMVoiceMessageBodyPtr(new EMVoiceMessageBody());

            if (body.HasMember("localPath") && body["localPath"].IsString()) {
                string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            if (body.HasMember("duration") && body["duration"].IsInt()) {
                int i = body["duration"].GetInt();
                ptr->setDuration(i);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::VIDEO:
        {
            EMVideoMessageBodyPtr ptr = EMVideoMessageBodyPtr(new EMVideoMessageBody());

            if (body.HasMember("localPath") && body["localPath"].IsString()) {
                string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            if (body.HasMember("thumbnailLocalPath") && body["thumbnailLocalPath"].IsString()) {
                string str = body["thumbnailLocalPath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailRemotePath") && body["thumbnailRemotePath"].IsString()) {
                string str = body["thumbnailRemotePath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailSecret") && body["thumbnailSecret"].IsString()) {
                string str = body["thumbnailSecret"].GetString();
                ptr->setThumbnailSecretKey(str);
            }

            EMVideoMessageBody::Size size;
            size.mWidth = 0;
            size.mHeight = 0;

            if (body.HasMember("height") && body["height"].IsDouble()) {
                size.mHeight = body["height"].GetDouble();
            }

            if (body.HasMember("width") && body["width"].IsDouble()) {
                size.mWidth = body["width"].GetDouble();
            }

            ptr->setSize(size);

            if (body.HasMember("duration") && body["duration"].IsInt()) {
                int i = body["duration"].GetInt();
                ptr->setDuration(i);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::CUSTOM:
        {
            string event = "";
            if (body.HasMember("event") && body["event"].IsString()) {
                event = body["event"].GetString();
            }

            EMCustomMessageBodyPtr ptr = EMCustomMessageBodyPtr(new EMCustomMessageBody(event));

            if (body.HasMember("params") && body["params"].IsString()) {
                string str = body["params"].GetString();
                EMCustomMessageBody::EMCustomExts exts = CustomExtsFromJson(str);
                ptr->setExts(exts);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        default:
        {
            //message = NULL;
        }
        }
        return bodyptr;
    }

    EMMessageBodyPtr Message::BodyFromJson(string json)
    {
        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return nullptr;

        return BodyFromJsonObject(d);
    }

    void Message::ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
    {
        if (nullptr == msg) return;

        writer.StartObject();
        {
            writer.Key("msgId");
            writer.String(msg->msgId().c_str());

            writer.Key("conversationId");
            writer.String(msg->conversationId().c_str());

            writer.Key("from");
            writer.String(msg->from().c_str());

            writer.Key("to");
            writer.String(msg->to().c_str());

            writer.Key("recallBy");
            writer.String(msg->recallBy().c_str());

            writer.Key("chatType");
            writer.Int(MsgTypeToInt(msg->chatType()));

            writer.Key("direction");
            writer.String(MessageDirectionToString(msg->msgDirection()).c_str());

            writer.Key("status");
            writer.Int(StatusToInt(msg->status()));

            writer.Key("hasDeliverAck");
            writer.Bool(msg->isDeliverAcked());

            writer.Key("hasReadAck");
            writer.Bool(msg->isReadAcked());

            writer.Key("isNeedGroupAck");
            writer.Bool(msg->isNeedGroupAck());

            writer.Key("isRead");
            writer.Bool(msg->isRead());

            writer.Key("messageOnlineState");
            writer.Bool(msg->messageOnlineState());

            writer.Key("attributes");
            writer.String(AttributesValue::ToJson(msg).c_str());

            writer.Key("localTime");
            writer.String(to_string(msg->localTime()).c_str());

            writer.Key("serverTime");
            writer.String(to_string(msg->timestamp()).c_str());

            writer.Key("isThread");
            writer.Bool(msg->isThread());

            writer.Key("bodyType");
            writer.String(BodyTypeToString(msg->bodies().front()->type()).c_str());

            writer.Key("body");
            writer.String(BodyToJson(msg).c_str());
        }
        writer.EndObject();
    }

    string Message::ToJson(EMMessagePtr msg)
    {
        if (nullptr == msg) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonWriter(writer, msg);
        return s.GetString();
    }

    EMMessagePtr Message::FromJsonObject(const Value& jnode)
    {
        string from = "";
        if (jnode.HasMember("from") && jnode["from"].IsString()) {
            from = jnode["from"].GetString();
        }

        string to = "";
        if (jnode.HasMember("to") && jnode["to"].IsString()) {
            to = jnode["to"].GetString();
        }

        EMMessage::EMMessageDirection direction = EMMessage::EMMessageDirection::SEND;
        if (jnode.HasMember("direction") && jnode["direction"].IsString()) {
            string str = jnode["direction"].GetString();
            direction = MessageDirectionFromString(str);
        }

        EMMessage::EMChatType msg_type = EMMessage::EMChatType::SINGLE;
        if (jnode.HasMember("chatType") && jnode["chatType"].IsInt()) {
            int i = jnode["chatType"].GetInt();
            msg_type = MsgTypeFromInt(i);
        }

        string msg_id = "";
        if (jnode.HasMember("msgId") && jnode["msgId"].IsString()) {
            msg_id = jnode["msgId"].GetString();
        }

        EMMessageBodyPtr body = BodyFromJsonObject(jnode);

        EMMessagePtr msg = nullptr;
        if (EMMessage::EMMessageDirection::SEND == direction)
            msg = EMMessage::createSendMessage(from, to, body, msg_type);
        else
            msg = EMMessage::createReceiveMessage(from, to, body, msg_type, msg_id);

        msg->setMsgDirection(direction);

        if (jnode.HasMember("conversationId") && jnode["conversationId"].IsString()) {
            string str = jnode["conversationId"].GetString();
            msg->setConversationId(str);
        }

        if (jnode.HasMember("recallBy") && jnode["recallBy"].IsString()) {
            string str = jnode["recallBy"].GetString();
            msg->setRecallBy(str);
        }

        if (jnode.HasMember("status") && jnode["status"].IsInt()) {
            int i = jnode["status"].GetInt();
            msg->setStatus(StatusFromInt(i));
        }

        if (jnode.HasMember("hasDeliverAck") && jnode["hasDeliverAck"].IsBool()) {
            bool b = jnode["hasDeliverAck"].GetBool();
            msg->setIsDeliverAcked(b);
        }

        if (jnode.HasMember("hasReadAck") && jnode["hasReadAck"].IsBool()) {
            bool b = jnode["hasReadAck"].GetBool();
            msg->setIsReadAcked(b);
        }

        if (jnode.HasMember("isNeedGroupAck") && jnode["isNeedGroupAck"].IsBool()) {
            bool b = jnode["isNeedGroupAck"].GetBool();
            msg->setIsNeedGroupAck(b);
        }

        if (jnode.HasMember("isRead") && jnode["isRead"].IsBool()) {
            bool b = jnode["isRead"].GetBool();
            msg->setIsRead(b);
        }

        if (jnode.HasMember("messageOnlineState") && jnode["messageOnlineState"].IsBool()) {
            bool b = jnode["messageOnlineState"].GetBool();
            msg->setMessageOnlineState(b);
        }

        if (jnode.HasMember("attributes") && jnode["attributes"].IsString())
            AttributesValue::SetMessageAttrs(msg, jnode["attributes"].GetString());

        if (jnode.HasMember("localTime") && jnode["localTime"].IsString()) {
            int64_t i = convertFromString<int64_t>(jnode["localTime"].GetString());
            msg->setLocalTime(i);
        }

        if (jnode.HasMember("serverTime") && jnode["serverTime"].IsString()) {
            int64_t i = convertFromString<int64_t>(jnode["serverTime"].GetString());
            msg->setTimestamp(i);
        }

        if (jnode.HasMember("isThread") && jnode["isThread"].IsBool()) {
            bool b = jnode["isThread"].GetBool();
            msg->setIsThread(b);
        }

        return msg;
    }

    EMMessagePtr Message::FromJson(const char* json)
    {
        if (nullptr == json || strlen(json) == 0) return nullptr;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return nullptr;

        return FromJsonObject(d);
    }

    void AttributesValue::ToJsonWriter(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute)
    {
        if (nullptr == attribute) return;

        writer.StartObject();
        if (attribute->is<bool>()) {
            writer.Key("type");
            writer.String("b");

            writer.Key("value");
            if (attribute->value<bool>())
                writer.String("True");
            else
                writer.String("False");
        }
        else if (attribute->is<char>()) {
            writer.Key("type");
            writer.String("c");
            writer.Key("value");
            writer.String(convert2String<char>(attribute->value<char>()).c_str());
        }
        else if (attribute->is<unsigned char>()) {
            writer.Key("type");
            writer.String("uc");
            writer.Key("value");
            writer.String(convert2String<unsigned char>(attribute->value<unsigned char>()).c_str());
        }
        else if (attribute->is<short>()) {
            writer.Key("type");
            writer.String("s");
            writer.Key("value");
            writer.String(convert2String<short>(attribute->value<short>()).c_str());
        }
        else if (attribute->is<unsigned short>()) {
            writer.Key("type");
            writer.String("us");
            writer.Key("value");
            writer.String(convert2String<unsigned short>(attribute->value<unsigned short>()).c_str());
        }
        else if (attribute->is<int32_t>()) {
            writer.Key("type");
            writer.String("i");
            writer.Key("value");
            writer.String(convert2String<int32_t>(attribute->value<int32_t>()).c_str());
        }
        else if (attribute->is<uint32_t>()) {
            writer.Key("type");
            writer.String("ui");
            writer.Key("value");
            writer.String(convert2String<uint32_t>(attribute->value<uint32_t>()).c_str());
        }
        else if (attribute->is<int64_t>()) {
            writer.Key("type");
            writer.String("l");
            writer.Key("value");
            writer.String(convert2String<int64_t>(attribute->value<int64_t>()).c_str());
        }
        else if (attribute->is<uint64_t>()) {
            writer.Key("type");
            writer.String("ul");
            writer.Key("value");
            writer.String(convert2String<uint64_t>(attribute->value<uint64_t>()).c_str());
        }
        else if (attribute->is<float>()) {
            writer.Key("type");
            writer.String("f");
            writer.Key("value");
            writer.String(convert2String<float>(attribute->value<float>()).c_str());
        }
        else if (attribute->is<double>()) {
            writer.Key("type");
            writer.String("d");
            writer.Key("value");
            writer.String(convert2String<double>(attribute->value<double>()).c_str());
        }
        else if (attribute->is<string>()) {
            writer.Key("type");
            writer.String("str");
            writer.Key("value");
            writer.String(attribute->value<string>().c_str());
            //Note: here not support to parse str value further
        }
        else if (attribute->is<vector<string>>()) {
            writer.Key("type");
            writer.String("strv");
            writer.Key("value");
            vector<string> vec = attribute->value<vector<string>>();
            writer.String(JsonStringFromVector(vec).c_str());
        }
        else if (attribute->is<EMJsonString>()) {
            writer.Key("type");
            writer.String("jstr");
            writer.Key("value");
            writer.String(attribute->value<EMJsonString>().c_str());
        }
        else {
        }
        writer.EndObject();
    }

    void AttributesValue::ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
    {
        if (nullptr == msg) return;

        map<string, EMAttributeValuePtr> ext = msg->ext();
        if (ext.size() == 0) return;

        for (auto it : ext) {
            string key = it.first;
            EMAttributeValuePtr attribute = it.second;
            writer.Key(key.c_str());
            ToJsonWriter(writer, attribute);
        }
    }

    string AttributesValue::ToJson(EMMessagePtr msg)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartObject();
        ToJsonWriter(writer, msg);
        writer.EndObject();
        string jstr = s.GetString();
        return jstr;
    }

    /*
    attrs may looks like(most quote symbol are removed, and all items in {} are string):
    {
       name1: {type:b, value:true},
       name2: {type:c, value:11},
       name3: {type:uc, value:a},
       name4: {type:s, value:-123},
       name5: {type:us, value:123},
       name6: {type:i, value:-456},
       name7: {type:ui, value:456},
       name8: {type:l, value:-123456},
       name9: {type:ul, value:123456},
       name10:{type:f, value:1.23},
       name11:{type:d, value:1.23456},
       name12:{type:str, value:"a string"},
       name13:{type:strv, value:["str1", "str2", "str3"]},    --- not support any more!!
       name14:{type:jstr, value:"a json string"},
       name15:{type:attr, value:{ --- not support any more!!
           name1: {type:b, value:true},
           name2: {type:c, value:11},
           ...
           name15: {type:attr, value:{
               name1: {type:b, value:true},
               ...
               name15:{type:str, value:"end"}
           }}
       }}
    }
    */

    void AttributesValue::SetMessageAttr(EMMessagePtr msg, string& key, const Value& jnode)
    {
        if (nullptr == msg || key.length() == 0 || jnode.IsNull())
            return;

        if (!jnode.HasMember("type") || !jnode["type"].IsString()) return;
        string type = jnode["type"].GetString();

        if (!jnode.HasMember("value")) return;

        string v = "";
        if (jnode["value"].IsString())
            v = jnode["value"].GetString();

        else if (jnode["value"].IsArray())
            v = JsonStringFromObject(jnode["value"]);

        if (type.compare("b") == 0) {
#ifdef _WIN32
            if (_stricmp(v.c_str(), "false") == 0) {
#else
            if (strcasecmp(v.c_str(), "false") == 0) {
#endif
                msg->setAttribute(key, false);
            }
            else {
                msg->setAttribute(key, true);
            }
            }
        else if (type.compare("c") == 0) {
            //unsupported in emmessage
        }
        else if (type.compare("uc") == 0) {
            //unsupported in emmessage
        }
        else if (type.compare("s") == 0) {
            //unsupported in emmessage
        }
        else if (type.compare("us") == 0) {
            //unsupported in emmessage
        }
        else if (type.compare("i") == 0) {
            int i = convertFromString<int32_t>(v);
            msg->setAttribute(key, i);
        }
        else if (type.compare("ui") == 0) {
            uint32_t ui = convertFromString<uint32_t>(v);
            msg->setAttribute(key, ui);
        }
        else if (type.compare("l") == 0) {
            int64_t l = convertFromString<int64_t>(v);
            msg->setAttribute(key, l);
        }
        else if (type.compare("ul") == 0) {
            //unsupported in emmessage
        }
        else if (type.compare("f") == 0) {
            float f = convertFromString<float>(v);
            msg->setAttribute(key, f);
        }
        else if (type.compare("d") == 0) {
            double d = convertFromString<double>(v);
            msg->setAttribute(key, d);
        }
        else if (type.compare("str") == 0) {
            msg->setAttribute(key, v);
        }
        else if (type.compare("strv") == 0) {
            EMJsonString json(v);
            msg->setAttribute(key, json);
        }
        else if (type.compare("jstr") == 0) {
            EMJsonString json(v);
            msg->setAttribute(key, json);
        }
        /*else if (type.compare("attr") == 0) {
            EMJsonString json(value.GetString());
            msg->setAttribute(key, json);
            LOG("Set type: attr, value:%s", attributeStr.c_str());
        }*/
        else {
        }
    }

    void AttributesValue::SetMessageAttrs(EMMessagePtr msg, const Value & jnode)
    {
        if (nullptr == msg || jnode.IsNull()) return;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {

            string key = iter->name.GetString();
            const Value& obj = iter->value;

            if (obj.IsObject()) {
                SetMessageAttr(msg, key, obj);
            }
        }
    }

    void AttributesValue::SetMessageAttrs(EMMessagePtr msg, string json)
    {
        // Json: as least has { and } two characters
        if (nullptr == msg) return;

        Document d;
        if (!d.Parse(json.data()).HasParseError()) {

            SetMessageAttrs(msg, d);
        }
    }
}

