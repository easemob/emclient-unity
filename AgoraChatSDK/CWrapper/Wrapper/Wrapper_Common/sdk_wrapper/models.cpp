#include "tool.h"
#include "models.h"

namespace sdk_wrapper
{
    string MyJson::ToJsonWithResult(const char* cbid, int process, int code, const char* desc, const char* jstr)
    {
        if (nullptr == cbid || strlen(cbid) == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartObject();

        writer.Key("callbackId");
        writer.String(cbid);

        if (process >= 0) {
            writer.Key("progress");
            writer.Int(process);
        }

        if (code >= 0) {
            writer.Key("error");
            writer.StartObject();

            writer.Key("code");
            writer.Int(code);

            writer.Key("desc");
            if (nullptr != desc && strlen(desc) != 0) {
                writer.String(desc);
            }
            else {
                writer.String("");
            }

            writer.EndObject();
        }

        if (nullptr != jstr && strlen(jstr) != 0) {
            writer.Key("value");
            writer.String(jstr);
        }

        writer.EndObject();

        return s.GetString();
    }

    string MyJson::ToJsonWithError(const char* cbid, int code, const char* desc)
    {
        return ToJsonWithResult(cbid, -1, code, desc, nullptr);
    }

    string MyJson::ToJsonWithErrorResult(const char* cbid, int code, const char* desc, const char* jstr)
    {
        return ToJsonWithResult(cbid, -1, code, desc, jstr);
    }

    string MyJson::ToJsonWithSuccess(const char* cbid)
    {
        return ToJsonWithResult(cbid, -1, -1, nullptr, nullptr);
    }

    string MyJson::ToJsonWithSuccessResult(const char* cbid, const char* jstr)
    {
        return ToJsonWithResult(cbid, -1, -1, nullptr, jstr);
    }

    string MyJson::ToJsonWithProcess(const char* cbid, int process)
    {
        return ToJsonWithResult(cbid, process, -1, nullptr, nullptr);
    }

    string MyJson::ToJsonWithJsonObject(const Value& obj)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);
        obj.Accept(writer);
        return s.GetString();
    }

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const vector<string>& vec)
    {
        writer.StartArray();
        for (int i = 0; i < vec.size(); i++) {
            writer.String(vec[i].c_str());
        }
        writer.EndArray();
    }

    vector<string> MyJson::FromJsonObjectToVector(const Value& jnode)
    {
        vector<string> vec;

        if (jnode.IsArray() == true) {
            int size = jnode.Size();
            for (int i = 0; i < size; i++) {
                vec.push_back(jnode[i].GetString());
            }
        }
        return vec;
    }

    string MyJson::ToJson(const vector<string>& vec) {
        if (vec.size() == 0) return string("");

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        string data = s.GetString();

        return data;
    }

    vector<string> MyJson::FromJsonToVector(string& jstr) {

        vector<string> vec;
        if (jstr.length() < 3) return vec;

        Document d;
        if (!d.Parse(jstr.data()).HasParseError()) {
            return FromJsonObjectToVector(d);
        }
        else
        {
            return vec;
        }
    }

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const map<string, string>& map)
    {
        writer.StartObject();
        for (auto it : map) {
            writer.Key(it.first.c_str());
            writer.String(it.second.c_str());
        }
        writer.EndObject();
    }

    map<string, string> MyJson::FromJsonObjectToMap(const Value& jnode)
    {
        map<string, string> map;

        if (!jnode.IsArray()) return map;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {
            auto key = iter->name.GetString();
            auto value = iter->value.GetString();

            map.insert(pair<string, string>(key, value));
        }
        return map;
    }

    string MyJson::ToJson(const map<string, string>& map) {
        if (map.size() == 0) return string("");

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, map);

        string data = s.GetString();
        return data;
    }

    map<string, string> MyJson::FromJsonToMap(string& jstr) {
        map<string, string> map;
        if (jstr.length() < 3) return map;

        Document d;
        if (!d.Parse(jstr.data()).HasParseError()) {
            return FromJsonObjectToMap(d);
        }
        return map;
    }



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

#ifndef _WIN32
        string uuid = GetMacUuid();
        if (uuid.size() > 0)
            configs->setDeviceUuid(uuid);
#endif
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

    string Message::ToJson(EMCustomMessageBody::EMCustomExts& exts)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, exts);

        string data = s.GetString();
        return data;
    }

    void Message::ToJsonObject(Writer<StringBuffer>& writer, EMCustomMessageBody::EMCustomExts& exts)
    {
        writer.StartObject();
        for (auto it : exts) {
            writer.Key(it.first.c_str());
            writer.String(it.second.c_str());
        }
        writer.EndObject();
    }

    EMCustomMessageBody::EMCustomExts Message::FromJsonToCustomExts(string json)
    {
        Document d;
        d.Parse(json.c_str());

        if (d.HasParseError()) {
            EMCustomMessageBody::EMCustomExts exts;
            return exts;
        }

        return FromJsonObjectToCustomExts(d);
    }

    EMCustomMessageBody::EMCustomExts Message::FromJsonObjectToCustomExts(const Value& jnode)
    {
        EMCustomMessageBody::EMCustomExts exts;

        if (!jnode.IsArray()) return exts;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {

            string key = iter->name.GetString();
            string value = iter->value.GetString();

            pair<string, string> kv{ key, value };
            exts.push_back(kv);
        }

        return exts;
    }

    void Message::ToJsonObjectWithBody(Writer<StringBuffer>& writer, EMMessagePtr msg)
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
            MyJson::ToJsonObject(writer, vec);

            writer.Key("translations");
            map<string, string> map = ptr->getTranslations();
            MyJson::ToJsonObject(writer, map);
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
            ToJsonObject(writer, exts);
        }
        break;
        default:
        {
            //message = NULL;
        }
        }
        writer.EndObject();
    }

    string Message::ToJsonWithBody(EMMessagePtr msg)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);
        ToJsonObjectWithBody(writer, msg);
        string data = s.GetString();
        return data;
    }

    EMMessageBodyPtr Message::FromJsonObjectToBody(const Value& jnode)
    {
        if (jnode.IsNull()) return nullptr;
        if (!jnode.HasMember("bodyType") || !jnode.HasMember("body")) return nullptr;

        if (!jnode["bodyType"].IsString() || !jnode["body"].IsString()) return nullptr;

        //Body type must have and must be string
        EMMessageBody::EMMessageBodyType btype = BodyTypeFromString(jnode["bodyType"].GetString());

        EMMessageBodyPtr bodyptr = nullptr;

        Document d;
        d.Parse(jnode["body"].GetString());
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
                vector<string> tagert_languages = MyJson::FromJsonObjectToVector(body["translations"]);
                ptr->setTargetLanguages(tagert_languages);
            }

            if (body.HasMember("targetLanguages") && body["targetLanguages"].IsString()) {
                map<string, string> translations = MyJson::FromJsonObjectToMap(body["targetLanguages"]);//FromJsonToMap(str);
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
                EMCustomMessageBody::EMCustomExts exts = FromJsonObjectToCustomExts(body["params"]);
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

    EMMessageBodyPtr Message::FromJsonToBody(string json)
    {
        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return nullptr;

        return FromJsonObjectToBody(d);
    }

    void Message::ToJsonObjectWithMessage(Writer<StringBuffer>& writer, EMMessagePtr msg)
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
            AttributesValue::ToJsonObjectWithAttributes(writer, msg);

            writer.Key("localTime");
            writer.String(to_string(msg->localTime()).c_str());

            writer.Key("serverTime");
            writer.String(to_string(msg->timestamp()).c_str());

            writer.Key("isThread");
            writer.Bool(msg->isThread());

            writer.Key("bodyType");
            writer.String(BodyTypeToString(msg->bodies().front()->type()).c_str());

            writer.Key("body");
            Message::ToJsonObjectWithBody(writer, msg);
        }
        writer.EndObject();
    }

    string Message::ToJson(EMMessagePtr msg)
    {
        if (nullptr == msg) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObjectWithMessage(writer, msg);
        return s.GetString();
    }

    string Message::ToJson(EMMessageList messages)
    {
        if (messages.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObjectWithMessageList(writer, messages);

        return s.GetString();
    }

    void Message::ToJsonObjectWithMessageList(Writer<StringBuffer>& writer, EMMessageList messages)
    {
        writer.StartArray();
        for (auto msg : messages) {
            ToJsonObjectWithMessage(writer, msg);
        }
        writer.EndArray();
    }

    EMMessagePtr Message::FromJsonObjectToMessage(const Value& jnode)
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

        EMMessageBodyPtr body = FromJsonObjectToBody(jnode);

        EMMessagePtr msg = nullptr;
        if (EMMessage::EMMessageDirection::SEND == direction)
            msg = EMMessage::createSendMessage(from, to, body, msg_type);
        else
            msg = EMMessage::createReceiveMessage(from, to, body, msg_type, msg_id);

        msg->setMsgId(msg_id);
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

    EMMessagePtr Message::FromJsonToMessage(const char* json)
    {
        if (nullptr == json || strlen(json) == 0) return nullptr;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return nullptr;

        return FromJsonObjectToMessage(d);
    }

    EMMessageList Message::FromJsonToMessageList(const char* json)
    {
        EMMessageList vec;
        vec.clear();

        if (nullptr == json || strlen(json) == 0) vec;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return vec;

        if (d.HasMember("list") && d["list"].IsArray()) {
            const Value& array = d["list"];
            size_t len = array.Size();

            for (size_t i = 0; i < len; i++) {
                EMMessagePtr msg = Message::FromJsonObjectToMessage(array[i]);
                if (nullptr != msg)
                    vec.push_back(msg);
            }
        }

        return vec;
    }

    void AttributesValue::ToJsonObjectWithAttribute(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute)
    {
        if (nullptr == attribute) return;

        writer.StartObject();
        if (attribute->is<bool>()) {
            writer.Key("type");
            writer.String("b");

            writer.Key("value");
            if (attribute->value<bool>())
                writer.String("True"); //TODO: need to check
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
            writer.String(MyJson::ToJson(vec).c_str());  //TODO: need to check
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

    void AttributesValue::ToJsonObjectWithAttributes(Writer<StringBuffer>& writer, EMMessagePtr msg)
    {
        if (nullptr == msg) return;

        map<string, EMAttributeValuePtr> ext = msg->ext();
        if (ext.size() == 0) return;

        for (auto it : ext) {
            string key = it.first;
            EMAttributeValuePtr attribute = it.second;
            writer.Key(key.c_str());
            ToJsonObjectWithAttribute(writer, attribute);
        }
    }

    string AttributesValue::ToJson(EMMessagePtr msg)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartObject();
        ToJsonObjectWithAttributes(writer, msg);
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
        if (nullptr == msg || key.length() == 0)
            return;

        if (!jnode.HasMember("type") || !jnode["type"].IsString()) return;
        string type = jnode["type"].GetString();

        if (!jnode.HasMember("value")) return;

        string v = "";
        if (jnode["value"].IsString())
            v = jnode["value"].GetString();

        else if (jnode["value"].IsArray())
            v = MyJson::ToJsonWithJsonObject(jnode["value"]);

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
        if (nullptr == msg) return;

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

    void Conversation::ToJsonObject(Writer<StringBuffer>& writer, EMConversationPtr conversation)
    {
        writer.StartObject();

        writer.Key("con_id");
        writer.String(conversation->conversationId().c_str());

        writer.Key("type");
        writer.Int(ConversationTypeToInt(conversation->conversationType()));

        writer.Key("isThread");
        writer.Bool(conversation->isThread());

        writer.EndObject();
    }

    string Conversation::ToJson(EMConversationPtr conversation)
    {
        if (nullptr == conversation) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        ToJsonObject(writer, conversation);
        string jstr = s.GetString();
        return jstr;
    }

    string Conversation::ToJson(EMConversationList conversations)
    {
        if (conversations.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartArray();

        for (auto it : conversations) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
        string jstr = s.GetString();
        return jstr;
    }

    int Conversation::ConversationTypeToInt(EMConversation::EMConversationType type)
    {
        int int_type = 0;
        switch (type) {
        case EMConversation::EMConversationType::CHAT: int_type = 0; break;
        case EMConversation::EMConversationType::GROUPCHAT: int_type = 1; break;
        case EMConversation::EMConversationType::CHATROOM: int_type = 2; break;
        case EMConversation::EMConversationType::DISCUSSIONGROUP: int_type = 3; break;
        case EMConversation::EMConversationType::HELPDESK: int_type = 5; break;
        }
        return int_type;
    }

    EMConversation::EMConversationType Conversation::ConversationTypeFromInt(int i)
    {
        EMConversation::EMConversationType type = EMConversation::EMConversationType::CHAT;
        switch (type)
        {
        case 0: type = EMConversation::EMConversationType::CHAT; break;
        case 1: type = EMConversation::EMConversationType::GROUPCHAT; break;
        case 2: type = EMConversation::EMConversationType::CHATROOM; break;
        }
        return type;
    }

    string SupportLanguage::ToJson(tuple<string, string, string>& lang)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, lang);

        string jstr = s.GetString();
        return jstr;
    }

    string SupportLanguage::ToJson(vector<tuple<string, string, string>>& langs)
    {
        if (langs.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, langs);

        string jstr = s.GetString();
        return jstr;
    }

    void SupportLanguage::ToJsonObject(Writer<StringBuffer>& writer, tuple<string, string, string>& lang)
    {
        writer.StartObject();

        writer.Key("code");
        writer.String(get<0>(lang).c_str());

        writer.Key("name");
        writer.String(get<1>(lang).c_str());

        writer.Key("nativeName");
        writer.String(get<1>(lang).c_str());

        writer.EndObject();
    }

    void SupportLanguage::ToJsonObject(Writer<StringBuffer>& writer, vector<tuple<string, string, string>>& langs)
    {
        writer.StartArray();

        for (auto it : langs) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string GroupReadAck::ToJson(EMGroupReadAckPtr group_read_ack)
    {
        if (nullptr == group_read_ack) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        
        ToJsonObject(writer, group_read_ack);

        string jstr = s.GetString();
        return jstr;
    }

    string GroupReadAck::ToJson(vector<EMGroupReadAckPtr>& group_read_ack_vec)
    {
        if (group_read_ack_vec.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, group_read_ack_vec);

        string jstr = s.GetString();
        return jstr;
    }

    void GroupReadAck::ToJsonObject(Writer<StringBuffer>& writer, EMGroupReadAckPtr group_read_ack)
    {
        writer.StartObject();

        writer.Key("ackId");
        writer.String(group_read_ack->meta_id.c_str());

        writer.Key("msgId");
        writer.String(group_read_ack->msgPtr->msgId().c_str());

        writer.Key("from");
        writer.String(group_read_ack->from.c_str());

        writer.Key("content");
        writer.String(group_read_ack->content.c_str());

        writer.Key("count");
        writer.Int(group_read_ack->count);

        writer.Key("timestamp");
        writer.Int64(group_read_ack->timestamp);

        writer.EndObject();
    }

    void GroupReadAck::ToJsonObject(Writer<StringBuffer>& writer, const EMGroupReadAckList& group_read_ack_vec)
    {
        writer.StartArray();

        for (auto it : group_read_ack_vec) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string MessageReaction::ToJson(EMMessageReactionPtr reaction)
    {
        if (nullptr == reaction) return "";

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        EMMessageEncoder::encodeReactionToJsonWriter(writer, reaction);
        return s.GetString();
    }

    string MessageReaction::ToJson(EMMessageReactionList& list)
    {
        return EMMessageEncoder::encodeReactionToJson(list);
    }

    string MessageReaction::ToJson(EMMessage& msg)
    {
        return EMMessageEncoder::encodeReactionToJson(msg);
    }

    string MessageReaction::ToJson(map<string, EMMessageReactionList>& map)
    {
        if (map.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartObject();
        for (auto it : map) {
            writer.Key(it.first.c_str());
            ToJsonObject(writer, it.second);
        }
        writer.EndObject();
        return s.GetString();
    }

    void MessageReaction::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionPtr reaction)
    {
        EMMessageEncoder::encodeReactionToJsonWriter(writer, reaction);
    }

    void MessageReaction::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionList& list)
    {
        if (list.size() == 0) return;

        writer.StartArray();
        for (EMMessageReactionPtr reaction : list) {
            EMMessageEncoder::encodeReactionToJsonWriter(writer, reaction);
        }
        writer.EndArray();
    }

    EMMessageReactionPtr MessageReaction::FromJsonObjectToReaction(const Value& jnode)
    {
        return EMMessageEncoder::decodeReactionFromJson(jnode);
    }

    EMMessageReactionList MessageReaction::FromJsonObjectToReactionList(const Value& jnode)
    {
        return EMMessageEncoder::decodeReactionListFromJson(jnode);
    }

    EMMessageReactionList MessageReaction::FromJsonToReactionList(string json)
    {
        return EMMessageEncoder::decodeReactionListFromJson(json);
    }

    string Group::ToJson(EMGroupPtr group)
    {
        if (nullptr == group) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        ToJsonObject(writer, group);
        return s.GetString();
    }

    void Group::ToJsonObject(Writer<StringBuffer>& writer, const EMMucMuteList& vec)
    {
        writer.StartArray();
        for (int i = 0; i < vec.size(); i++) {
            writer.String(vec[i].first.c_str());
        }
        writer.EndArray();
    }

    void Group::ToJsonObject(Writer<StringBuffer>& writer, EMGroupPtr group)
    {
        writer.StartObject();

        writer.Key("groupId");
        writer.String(group->groupId().c_str());

        writer.Key("name");
        writer.String(group->groupSubject().c_str());

        writer.Key("desc");
        writer.String(group->groupDescription().c_str());

        writer.Key("owner");
        writer.String(group->groupOwner().c_str());

        writer.Key("announcement");
        writer.String(group->groupAnnouncement().c_str());

        writer.Key("memberCount");
        writer.Int(group->groupMembersCount());

        writer.Key("memberList");
        MyJson::ToJsonObject(writer, group->groupMembers());

        writer.Key("adminList");
        MyJson::ToJsonObject(writer, group->groupAdmins());

        writer.Key("blockList");
        MyJson::ToJsonObject(writer, group->groupBans());

        writer.Key("muteList");
        ToJsonObject(writer, group->groupMutes());

        writer.Key("noticeEnable");
        writer.Bool(group->isPushEnabled());

        writer.Key("messageBlocked");
        writer.Bool(group->isMessageBlocked());

        writer.Key("isAllMemberMuted");
        writer.Bool(group->groupAllMembersMuted());

        writer.Key("options");
        JsonObject(writer, group->groupSetting());

        writer.Key("permissionType");
        writer.Int(MemberTypeToInt(group->groupMemberType()));

        writer.EndObject();
    }

    void Group::ToJsonObject(Writer<StringBuffer>& writer, const EMGroupList& list)
    {
        writer.StartArray();
        for (int i = 0; i < list.size(); i++) {
            ToJsonObject(writer, list[i]);
        }
        writer.EndArray();
    }

    string Group::ToJson(const EMMucMuteList& vec)
    {
        if (vec.size() == 0) return string("");

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        string data = s.GetString();

        return data;
    }

    string Group::ToJson(const EMGroupList& list)
    {
        if (list.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, list);

        return s.GetString();
    }

    void Group::JsonObject(Writer<StringBuffer>& writer, const EMMucSettingPtr setting)
    {
        writer.StartObject();

        writer.Key("style");
        writer.Int(GroupStyleToInt(setting->style()));

        writer.Key("maxCount");
        writer.Int(setting->maxUserCount());

        writer.Key("inviteNeedConfirm");
        writer.Bool(setting->inviteNeedConfirm());

        writer.Key("ext");
        writer.String(setting->extension().c_str());

        writer.EndObject();
    }

    EMMucSettingPtr Group::JsonObjectToMucSetting(const Value& jnode)
    {
        EMMucSetting::EMMucStyle style = GroupStyleFromInt(jnode["style"].GetInt());
        int count = jnode["maxCount"].GetInt();
        bool invite_need_confirm = jnode["inviteNeedConfirm"].GetBool();
        string ext = jnode["ext"].GetString();

        EMMucSettingPtr setting = EMMucSettingPtr(new EMMucSetting(style, count, invite_need_confirm, ext));
        return setting;
    }

    EMMucSettingPtr Group::FromJsonToMucSetting(string json)
    {
        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return nullptr;

        return JsonObjectToMucSetting(d);
    }

    string Group::ToJson(const EMMucSettingPtr setting)
    {
        if (nullptr == setting) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        JsonObject(writer, setting);

        return s.GetString();
    }

    int Group::MemberTypeToInt(EMMuc::EMMucMemberType type)
    {
        int ret = -1;
        switch (type)
        {
            case EMMuc::EMMucMemberType::MUC_UNKNOWN:   ret = -1; break;
            case EMMuc::EMMucMemberType::MUC_MEMBER:    ret = 0; break;
            case EMMuc::EMMucMemberType::MUC_ADMIN:     ret = 1; break;
            case EMMuc::EMMucMemberType::MUC_OWNER:     ret = 2; break;
            default: ret = -1; break;
        }
        return ret;
    }

    int Group::GroupStyleToInt(EMMucSetting::EMMucStyle style)
    {
        int ret = 0;
        switch (style)
        {
        case EMMucSetting::EMMucStyle::PRIVATE_OWNER_INVITE:   ret = 0; break;
        case EMMucSetting::EMMucStyle::PRIVATE_MEMBER_INVITE:  ret = 1; break;
        case EMMucSetting::EMMucStyle::PUBLIC_JOIN_APPROVAL:   ret = 2; break;
        case EMMucSetting::EMMucStyle::PUBLIC_JOIN_OPEN:       ret = 3; break;
        default: ret = 0; break;
        }
        return ret;
    }

    EMMucSetting::EMMucStyle Group::GroupStyleFromInt(int i)
    {
        EMMucSetting::EMMucStyle style = EMMucSetting::EMMucStyle::PRIVATE_OWNER_INVITE;

        switch (i)
        {
        case 0:  style = EMMucSetting::EMMucStyle::PRIVATE_OWNER_INVITE; break;
        case 1:  style = EMMucSetting::EMMucStyle::PRIVATE_MEMBER_INVITE; break;
        case 2:  style = EMMucSetting::EMMucStyle::PUBLIC_JOIN_APPROVAL; break;
        case 3:  style = EMMucSetting::EMMucStyle::PUBLIC_JOIN_OPEN; break;
        default: style = EMMucSetting::EMMucStyle::PRIVATE_OWNER_INVITE; break;
        }
        return style;
    }

    void GroupSharedFile::ToJsonObject(Writer<StringBuffer>& writer, EMMucSharedFilePtr file)
    {
        writer.StartObject();

        writer.Key("name");
        writer.String(file->fileName().c_str());

        writer.Key("fileId");
        writer.String(file->fileId().c_str());

        writer.Key("owner");
        writer.String(file->fileOwner().c_str());

        writer.Key("createTime");
        writer.Uint64(file->create());

        writer.Key("fileSize");
        writer.Uint64(file->fileSize());

        writer.EndObject();
    }

    void GroupSharedFile::ToJsonObject(Writer<StringBuffer>& writer, EMMucSharedFileList file_list)
    {
        writer.StartArray();

        for (auto it : file_list) {
            ToJsonObject(writer, it);
        }

        writer.EndObject();
    }

    string GroupSharedFile::ToJson(EMMucSharedFilePtr file)
    {
        if (nullptr == file) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, file);

        return s.GetString();
    }

    string GroupSharedFile::ToJson(EMMucSharedFileList file_list)
    {
        if (file_list.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, file_list);

        return s.GetString();
    }

    string CursorResult::ToJson(string cursor, const vector<string> list)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, list);
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    string CursorResult::ToJson(string cursor, const EMMessageList& msg_list)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");
            Message::ToJsonObjectWithMessageList(writer, msg_list);
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }
    string CursorResult::ToJson(string cursor, const EMGroupReadAckList& group_ack_list)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");
            GroupReadAck::ToJsonObject(writer, group_ack_list);
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }
    string CursorResult::ToJson(string cursor, const EMMessageReactionPtr reaction)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");
            MessageReaction::ToJsonObject(writer, reaction);
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    string CursorResult::ToJsonWithGroupInfo(string cursor, const EMCursorResult& result)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");
            size_t size = result.result().size();
            writer.StartArray();
            for (size_t i = 0; i < size; i++) {
                EMGroupPtr groupPtr = dynamic_pointer_cast<EMGroup>(result.result().at(i));
                Group::ToJsonObject(writer, groupPtr);
            }
            writer.EndArray();
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    std::string CursorResult::ToJson(string cursor, EMCursorResultRaw<EMThreadEventPtr> cusorResult)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("cursor");
            writer.String(cursor.c_str());

            writer.Key("list");

            std::vector<EMThreadEventPtr> vec = cusorResult.result();
            writer.StartArray();
            for (size_t i = 0; i < vec.size(); i++) {
                ChatThread::ToJsonObject(writer, vec[i]);
            }
            writer.EndArray();
        }
        writer.EndObject();

        std::string data = s.GetString();
        return data;
    }

    void Room::ToJsonObject(Writer<StringBuffer>& writer, EMChatroomPtr room)
    {
        if (nullptr == room) return;

        writer.StartObject();

        writer.Key("roomId");
        writer.String(room->chatroomId().c_str());

        writer.Key("name");
        writer.String(room->chatroomSubject().c_str());

        writer.Key("desc");
        writer.String(room->chatroomDescription().c_str());

        writer.Key("announcement");
        writer.String(room->chatroomAnnouncement().c_str());

        writer.Key("memberCount");
        writer.Int(room->chatroomMemberCount());

        writer.Key("adminList");
        MyJson::ToJsonObject(writer, room->chatroomAdmins());

        writer.Key("memberList");
        MyJson::ToJsonObject(writer, room->chatroomMembers());

        writer.Key("blockList");
        MyJson::ToJsonObject(writer, room->chatroomBans());

        writer.Key("muteList");
        Group::ToJsonObject(writer, room->chatroomMutes());

        writer.Key("maxUsers");
        writer.Int(room->chatroomMemberMaxCount());

        writer.Key("owner");
        writer.String(room->owner().c_str());

        writer.Key("isAllMemberMuted");
        writer.Bool(room->isMucAllMembersMuted());

        writer.Key("permissionType");
        writer.Int(EMMucMemberTypeToInt(room->chatroomMemberType()));

        writer.EndObject();
    }
    string Room::ToJson(EMChatroomPtr room)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, room);

        string data = s.GetString();
        return data;
    }

    int Room::EMMucMemberTypeToInt(EMMuc::EMMucMemberType type)
    {
        int ret = -1;
        switch (type)
        {
        case EMMuc::EMMucMemberType::MUC_MEMBER:
            ret = 0;
            break;
        case EMMuc::EMMucMemberType::MUC_ADMIN:
            ret = 1;
            break;
        case EMMuc::EMMucMemberType::MUC_OWNER:
            ret = 2;
            break;
        case EMMuc::EMMucMemberType::MUC_UNKNOWN:
            ret = -1;
            break;
        }

        return ret;
    }
    EMMuc::EMMucMemberType Room::EMMucMemberTypeFromInt(int i)
    {
        EMMuc::EMMucMemberType ret = EMMuc::EMMucMemberType::MUC_UNKNOWN;
        switch (i)
        {
        case 0:
            ret = EMMuc::EMMucMemberType::MUC_MEMBER;
            break;
        case 1:
            ret = EMMuc::EMMucMemberType::MUC_ADMIN;
            break;
        case 2:
            ret = EMMuc::EMMucMemberType::MUC_OWNER;
            break;
        case -1:
            ret = EMMuc::EMMucMemberType::MUC_UNKNOWN;
            break;
        }
        return ret;
    }

    string PageResult::ToJsonWithRoom(int count, const EMPageResult result)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("count");
            writer.Int(count);

            writer.Key("list");
            size_t size = result.result().size();
            writer.StartArray();
            for (size_t i = 0; i < size; i++) {
                EMChatroomPtr room = dynamic_pointer_cast<EMChatroom>(result.result().at(i));
                Room::ToJsonObject(writer, room);
            }
            writer.EndArray();
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    int ChatThreadEvent::ThreadOperationToInt(const std::string& operation)
    {
        if (operation.compare("create") == 0)
            return 1;
        if (operation.compare("update") == 0)
            return 2;
        if (operation.compare("delete") == 0)
            return 3;
        if (operation.compare("update_msg") == 0)
            return 4;
        return 0;
    }

    std::string ChatThreadEvent::ToJson(EMThreadEventPtr threadEventPtr)
    {
        if (nullptr == threadEventPtr) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        {
            writer.Key("from");
            writer.String(threadEventPtr->fromId().c_str());

            writer.Key("operation");
            writer.Int(ThreadOperationToInt(threadEventPtr->threadOperation()));

            writer.Key("chatThread");
            ChatThread::ToJsonObject(writer, threadEventPtr);

        }
        writer.EndObject();

        std::string data = s.GetString();
        return data;
    }

    void ChatThread::ToJsonObject(Writer<StringBuffer>& writer, EMThreadEventPtr threadEventPtr)
    {
        if (nullptr == threadEventPtr) return;

        writer.StartObject();
        {
            writer.Key("tId");
            writer.String(threadEventPtr->threadId().c_str());

            writer.Key("messageId");
            writer.String(threadEventPtr->threadMessageId().c_str());

            writer.Key("parentId");
            writer.String(threadEventPtr->parentId().c_str());

            writer.Key("owner");
            writer.String(threadEventPtr->owner().c_str());

            writer.Key("name");
            writer.String(threadEventPtr->threadName().c_str());

            writer.Key("messageCount");
            writer.Int(threadEventPtr->messageCount());

            writer.Key("membersCount");
            writer.Int(threadEventPtr->membersCount());

            writer.Key("createTimestamp");
            writer.Int64(threadEventPtr->createTimestamp());

            if (nullptr != threadEventPtr->lastMessage())
            {
                writer.Key("lastMessage");
                Message::ToJsonObjectWithMessage(writer, threadEventPtr->lastMessage());
            }
        }
        writer.EndObject();
    }

    std::string ChatThread::ToJson(EMThreadEventPtr threadEventPtr)
    {
        if (nullptr == threadEventPtr) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        ToJsonObject(writer, threadEventPtr);
        std::string data = s.GetString();
        return data;
    }

    std::string ChatThread::ToJson(std::vector<EMThreadEventPtr>& vec)
    {
        if (vec.size() == 0) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartArray();
        for (auto it : vec) {
            ToJsonObject(writer, it);
        }
        writer.EndArray();

        std::string data = s.GetString();
        return data;
    }

    std::string ChatThread::ToJson(std::map<std::string, EMMessagePtr> map)
    {
        if (map.size() == 0) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();
        for (auto it : map) {
            writer.Key(it.first.c_str());
            Message::ToJsonObjectWithMessage(writer, it.second);
        }
        writer.EndObject();

        std::string data = s.GetString();
        return data;
    }

    EMThreadEventPtr ChatThread::FromJsonObject(const Value& jnode)
    {
        if (jnode.IsNull()) return nullptr;

        EMThreadEventPtr thread = EMThreadEventPtr(new EMThreadEvent());

        if (jnode.HasMember("tId") && jnode["tId"].IsString()) {
            std::string str = jnode["tId"].GetString();
            thread->setThreadId(str);
        }

        if (jnode.HasMember("messageId") && jnode["messageId"].IsString()) {
            std::string str = jnode["messageId"].GetString();
            thread->setThreadMessageId(str);
        }

        if (jnode.HasMember("parentId") && jnode["parentId"].IsString()) {
            std::string str = jnode["parentId"].GetString();
            thread->setParentId(str);
        }

        if (jnode.HasMember("owner") && jnode["owner"].IsString()) {
            std::string str = jnode["owner"].GetString();
            thread->setOwner(str);
        }

        if (jnode.HasMember("name") && jnode["name"].IsString()) {
            std::string str = jnode["name"].GetString();
            thread->setThreadName(str);
        }

        if (jnode.HasMember("messageCount") && jnode["messageCount"].IsInt()) {
            int i = jnode["messageCount"].GetInt();
            thread->setMessageCount(i);
        }

        if (jnode.HasMember("membersCount") && jnode["membersCount"].IsInt()) {
            int i = jnode["membersCount"].GetInt();
            thread->setMembersCount(i);
        }

        if (jnode.HasMember("createTimestamp") && jnode["createTimestamp"].IsInt64()) {
            int64_t i = jnode["createTimestamp"].GetInt64();
            thread->setCreateTimestamp(i);
        }

        if (jnode.HasMember("lastMessage") && jnode["lastMessage"].IsObject()) {
            EMMessagePtr msg = Message::FromJsonObjectToMessage(jnode["lastMessage"]);
            if (nullptr != msg)
                thread->setLastMessage(msg);
        }

        return thread;
    }

    EMThreadEventPtr ChatThread::FromJson(std::string json)
    {
        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return nullptr;

        return FromJsonObject(d);
    }

    EMThreadLeaveReason ChatThread::ThreadLeaveReasonFromInt(int i)
    {
        EMThreadLeaveReason ret = EMThreadLeaveReason::LEAVE;
        switch (i)
        {
        case 0:
            ret = EMThreadLeaveReason::LEAVE;
            break;
        case 1:
            ret = EMThreadLeaveReason::BE_KICKED;
            break;
        case 2:
            ret = EMThreadLeaveReason::DESTROYED;
            break;
        }

        return ret;
    }

    int ChatThread::ThreadLeaveReasonToInt(EMThreadLeaveReason reason)
    {
        int ret = 0;
        switch (reason)
        {
        case EMThreadLeaveReason::LEAVE:
            ret = 0;
            break;
        case EMThreadLeaveReason::BE_KICKED:
            ret = 1;
            break;
        case EMThreadLeaveReason::DESTROYED:
            ret = 2;
            break;
        }

        return ret;
    }

    void PreseceDeviceStatus::ToJsonObject(Writer<StringBuffer>& writer, const pair<string, int>& status)
    {
        writer.Key(status.first.c_str());
        writer.Int(status.second);
    }
    void PreseceDeviceStatus::ToJsonObject(Writer<StringBuffer>& writer, const set<pair<string, int>>& status_set)
    {
        writer.StartArray();

        for (auto it : status_set) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string PreseceDeviceStatus::ToJson(const pair<string, int>& status)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, status);

        std::string data = s.GetString();
        return data;
    }
    string PreseceDeviceStatus::ToJson(const set<pair<string, int>>& status_set)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, status_set);

        std::string data = s.GetString();
        return data;
    }

    void Presence::ToJsonObject(Writer<StringBuffer>& writer, EMPresencePtr presence)
    {
        writer.StartObject();

        writer.Key("publisher");
        writer.String(presence->getPublisher().c_str());

        writer.Key("statusDescription");
        writer.String(presence->getExt().c_str());

        writer.Key("lastTime");
        writer.Int64(presence->getLatestTime());

        writer.Key("expiryTime");
        writer.Int64(presence->getExpiryTime());

        writer.Key("statusDetails");
        PreseceDeviceStatus::ToJsonObject(writer, presence->getStatusList());

        writer.EndObject();
    }
    void Presence::ToJsonObject(Writer<StringBuffer>& writer, const vector<EMPresencePtr>& vec)
    {
        writer.StartArray();

        for (auto it : vec) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string Presence::ToJson(EMPresencePtr presence)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, presence);

        std::string data = s.GetString();
        return data;
    }
    string Presence::ToJson(const vector<EMPresencePtr>& vec)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        std::string data = s.GetString();
        return data;
    }

    TokenWrapper::TokenWrapper()
    {
        autologin_config_.userName = "";
        autologin_config_.passwd = "";
        autologin_config_.token = "";
        autologin_config_.expireTS = "";
        autologin_config_.expireTsInt = -1;
        autologin_config_.availablePeriod = -1;
    }

    bool TokenWrapper::GetTokenCofigFromJson(string& raw, string& token, string& expireTS)
    {
        const string ACCESS_TOKEN = "access_token";
        const string EXPIRE_TS = "expire_timestamp";

        //string expire_in;

        int64_t expireTS_int = 0;

        if (raw.length() < 3) {
            return false;
        }

        Document d;
        if (d.Parse(raw.c_str()).HasParseError()) {
            return false;
        }

        if (d.HasMember(ACCESS_TOKEN.c_str())) {
            token = d[ACCESS_TOKEN.c_str()].GetString();
        }
        else {
            return false;
        }

        if (d.HasMember(EXPIRE_TS.c_str())) {
            expireTS_int = d[EXPIRE_TS.c_str()].GetInt64();
            expireTS = to_string(expireTS_int);
        }
        else {
            return false;
        }

        return true;
    }

    int TokenWrapper::GetTokenCheckInterval(int pre_check_interval, int availablePeriod)
    {
        if (pre_check_interval > floor(availablePeriod / 2)) {
            pre_check_interval = floor(availablePeriod / 3);
        }

        return pre_check_interval;
    }

    bool TokenWrapper::SetTokenInAutoLogin(const string& username, const string& token, const string expireTS)
    {
        if (username.size() == 0 || token.size() == 0) {
            return false;
        }

        if (expireTS.size() > 0) {

            int64_t expireTsInt = atoll(expireTS.c_str()); // milli-second
            expireTsInt = expireTsInt / 1000; // second

            time_t nowTS = time(NULL);

            int64_t availablePeriod = expireTsInt - nowTS;
            // no available time or expireTs is same with last time, then no need to update related fields
            if (availablePeriod <= 0) {               
                return false;
            }

            //expireTs is same with last time, then no need to update related fields
            if (autologin_config_.expireTsInt == expireTsInt) {
                return false;
            }

            autologin_config_.expireTS = expireTS;
            autologin_config_.expireTsInt = expireTsInt;
            autologin_config_.availablePeriod = availablePeriod;
        }
        else {
            // Note: if expireTS is empty, but token is not, means
            // current using easemob token to login, not agora token!
            autologin_config_.expireTS = "";
            autologin_config_.expireTsInt = 0;
            autologin_config_.availablePeriod = 0;
        }

        autologin_config_.userName = username;
        autologin_config_.passwd = "";
        autologin_config_.token = token;
        return true;
    }

    void TokenWrapper::SetPasswdInAutoLogin(const string& username, const string& passwd)
    {
        autologin_config_.userName = username;
        autologin_config_.passwd = passwd;
        autologin_config_.token = "";
        autologin_config_.expireTS = "";
        autologin_config_.expireTsInt = 0;
        autologin_config_.availablePeriod = 0;
    }

    void TokenWrapper::SaveAutoLoginConfigToFile(const string& uuid)
    {
        // merge token config
        string mergeStr = "userName";
        mergeStr.append("=");
        mergeStr.append(autologin_config_.userName);
        mergeStr.append("\n");

        mergeStr.append("passwd");
        mergeStr.append("=");
        mergeStr.append(autologin_config_.passwd);
        mergeStr.append("\n");

        mergeStr.append("token");
        mergeStr.append("=");
        mergeStr.append(autologin_config_.token);
        mergeStr.append("\n");

        mergeStr.append("expireTS");
        mergeStr.append("=");
        mergeStr.append(autologin_config_.expireTS);

        EncryptAndSaveToFile(mergeStr, uuid);
    }

    void TokenWrapper::GetAutoLoginConfigFromFile(const string& uuid)
    {
        string mergeStr = DecryptAndGetFromFile(uuid);

        // mergeStr has the format: aaa\nbbb\nccc
        string::size_type pos, pos1;

        string userName = "";
        pos = mergeStr.find("\n");
        if (string::npos != pos) {
            string str = string(mergeStr, 0, pos - 0);
            userName = GetRightValue(str);
        }
        else {
            return;
        }

        string passwd = "";
        pos = pos + 1;
        pos1 = mergeStr.find("\n", pos);
        if (string::npos != pos1) {
            string str = string(mergeStr, pos, pos1 - pos);
            passwd = GetRightValue(str);
        }
        else {
            return;
        }

        string token = "";
        pos = pos1 + 1;
        pos1 = mergeStr.find("\n", pos);
        if (string::npos != pos1) {
            string str = string(mergeStr, pos, pos1 - pos);
            token = GetRightValue(str);
        }
        else {
            return;
        }

        pos = pos1 + 1;
        if (pos > mergeStr.size() - 1) {
            return;
        }

        string expireTS = "";
        string str = string(mergeStr, pos, mergeStr.size() - pos);
        expireTS = GetRightValue(str);

        autologin_config_.userName = userName;
        autologin_config_.passwd = passwd;
        autologin_config_.token = token;
        autologin_config_.expireTS = expireTS;
        autologin_config_.expireTsInt = 0;
        autologin_config_.availablePeriod = 0;

        if (autologin_config_.expireTS.size() > 0) {
            time_t nowTS = time(NULL); // second
            int64_t expireTsInt = atoll(autologin_config_.expireTS.c_str()); // milli-second

            autologin_config_.expireTsInt = (int)(expireTsInt / 1000); // second
            autologin_config_.availablePeriod = autologin_config_.expireTsInt - nowTS;
        }
    }
}

