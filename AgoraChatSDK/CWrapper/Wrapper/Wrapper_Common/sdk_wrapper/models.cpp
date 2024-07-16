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

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const vector<int>& vec)
    {
        writer.StartArray();
        for (int i = 0; i < vec.size(); i++) {
            writer.Int(vec[i]);
        }
        writer.EndArray();
    }

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const vector<string>& vec)
    {
        writer.StartArray();
        for (int i = 0; i < vec.size(); i++) {
            writer.String(vec[i].c_str());
        }
        writer.EndArray();
    }

    vector<string> MyJson::FromJsonObjectToVector(const Value& jnode, bool filter)
    {
        vector<string> vec;

        if (jnode.IsArray() == true) {

            int size = jnode.Size();

            for (int i = 0; i < size; i++) {

                string str = jnode[i].GetString();

                if (filter && str.size() == 0) continue;

                vec.push_back(str);
            }
        }
        return vec;
    }

    string MyJson::ToJson(const vector<string>& vec) {

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

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const unordered_map<string, string>& map)
    {
        writer.StartObject();
        for (auto it : map) {
            writer.Key(it.first.c_str());
            writer.String(it.second.c_str());
        }
        writer.EndObject();
    }

    void MyJson::ToJsonObject(Writer<StringBuffer>& writer, const map<string, int>& map)
    {
        writer.StartObject();
        for (auto it : map) {
            writer.Key(it.first.c_str());
            writer.Int(it.second);
        }
        writer.EndObject();
    }

    map<string, string> MyJson::FromJsonObjectToMap(const Value& jnode)
    {
        map<string, string> map;

        if (!jnode.IsObject()) return map;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {
            auto key = iter->name.GetString();
            auto value = iter->value.GetString();

            map.insert(pair<string, string>(key, value));
        }
        return map;
    }

    unordered_map<string, string> MyJson::FromJsonObjectToUnorderedMap(const Value& jnode)
    {
        unordered_map<string, string> map;

        if (!jnode.IsObject()) return map;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {
            auto key = iter->name.GetString();
            auto value = iter->value.GetString();

            map.insert(pair<string, string>(key, value));
        }
        return map;
    }
    /*
    map<string, int> MyJson::FromJsonObjectToIntMap(const Value& jnode)
    {
        map<string, int> map;

        if (!jnode.IsArray()) return map;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {
            auto key = iter->name.GetString();
            auto value = iter->value.GetInt();

            map.insert(pair<string, int>(key, value));
        }
        return map;
    }
    */
    string MyJson::ToJson(const map<string, string>& map) {

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, map);

        string data = s.GetString();
        return data;
    }

    string MyJson::ToJson(const unordered_map<string, string>& map)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, map);

        string data = s.GetString();
        return data;
    }

    string MyJson::ToJson(const map<string, int>& map) {

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, map);

        string data = s.GetString();
        return data;
    }

    map<string, string> MyJson::FromJsonToMap(const string& jstr) {
        map<string, string> map;
        if (jstr.length() < 3) return map;

        Document d;
        if (!d.Parse(jstr.data()).HasParseError()) {
            return FromJsonObjectToMap(d);
        }
        return map;
    }

    unordered_map<string, string> MyJson::FromJsonToUnorderedMap(const string& jstr)
    {
        unordered_map<string, string> map;
        if (jstr.length() < 3) return map;

        Document d;
        if (!d.Parse(jstr.data()).HasParseError()) {
            return FromJsonObjectToUnorderedMap(d);
        }
        return map;
    }
    /*
    map<string, int> MyJson::FromJsonToIntMap(const string& jstr) {
        map<string, int> map;
        if (jstr.length() < 3) return map;

        Document d;
        if (!d.Parse(jstr.data()).HasParseError()) {
            return FromJsonObjectToIntMap(d);
        }
        return map;
    }
    */

    EMChatConfigsPtr Options::FromJson(const char* json, const char* rs, const char* wk)
	{
        if (nullptr == json || strlen(json) == 0) return nullptr;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return nullptr;

        if (!d["options"].IsObject()) return nullptr;

        const Value& jnode = d["options"];
        if (jnode.IsNull()) return nullptr;


        string app_key = "";
        if (jnode.HasMember("appKey") && jnode["appKey"].IsString()) {            
            app_key = jnode["appKey"].GetString();
        }

        string sdk_path = "";
        if (jnode.HasMember("sdkDataPath") && jnode["sdkDataPath"].IsString()) {
            sdk_path = jnode["sdkDataPath"].GetString();
        }

        string rs_str = rs;
        string wk_str = wk;
        if (strlen(sdk_path.c_str()) > 0)
        {
            string rs_pure(rs, 2, string::npos);
            string wk_pure(wk, 2, string::npos);

            rs_str = sdk_path + "/" + rs_pure;
            wk_str = sdk_path + "/" + wk_pure;
        }

        if (CheckAppKey(app_key.c_str()) == false) return nullptr;

        EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs(rs_str, wk_str, app_key, 0));
        configs->setAppKey(app_key);

        if (jnode.HasMember("dnsUrl") && jnode["dnsUrl"].IsString()) {
            string dns_url = jnode["dnsUrl"].GetString();
            configs->setDnsURL(dns_url);
        }

        if (jnode.HasMember("imServer") && jnode["imServer"].IsString()) {
            string im_server = jnode["imServer"].GetString();
            configs->privateConfigs()->chatServer() = im_server;
        }

        if (jnode.HasMember("restServer") && jnode["restServer"].IsString()) {
            string rest_server = jnode["restServer"].GetString();
            configs->privateConfigs()->restServer() = rest_server;
        }

        if (jnode.HasMember("imPort") && jnode["imPort"].IsInt()) {
            int im_port = jnode["imPort"].GetInt();
            configs->privateConfigs()->chatPort() = im_port;
        }

        if (jnode.HasMember("enableDnsConfig") && jnode["enableDnsConfig"].IsBool()) {
            bool enable_dns_config = jnode["enableDnsConfig"].GetBool();
            configs->privateConfigs()->enableDnsConfig(enable_dns_config);
        }

        if (jnode.HasMember("debugMode") && jnode["debugMode"].IsBool()) {
            bool debug_mode = jnode["debugMode"].GetBool();
            if (false == debug_mode) {
                configs->setLogLevel(EMChatConfigs::EMLogLevel::ERROR_LEVEL);
            }
            else {
                configs->setLogLevel(EMChatConfigs::EMLogLevel::DEBUG_LEVEL);
            }
        }

        if (jnode.HasMember("autoLogin") && jnode["autoLogin"].IsBool()) {
            bool auto_login = jnode["autoLogin"].GetBool();
            // according to old code, not set auto_login
        }

        if (jnode.HasMember("acceptInvitationAlways") && jnode["acceptInvitationAlways"].IsBool()) {
            bool accept_invitation_always = jnode["acceptInvitationAlways"].GetBool();
            configs->setAutoAcceptFriend(accept_invitation_always);
        }

        if (jnode.HasMember("autoAcceptGroupInvitation") && jnode["autoAcceptGroupInvitation"].IsBool()) {
            bool auto_accept_group_invitation = jnode["autoAcceptGroupInvitation"].GetBool();
            configs->setAutoAcceptGroup(auto_accept_group_invitation);
        }

        if (jnode.HasMember("requireAck") && jnode["requireAck"].IsBool()) {
            bool require_ack = jnode["requireAck"].GetBool();
            configs->setRequireReadAck(require_ack);
        }

        if (jnode.HasMember("requireDeliveryAck") && jnode["requireDeliveryAck"].IsBool()) {
            bool require_delivery_ack = jnode["requireDeliveryAck"].GetBool();
            configs->setRequireDeliveryAck(require_delivery_ack);
        }

        if (jnode.HasMember("deleteMessagesAsExitGroup") && jnode["deleteMessagesAsExitGroup"].IsBool()) {
            bool delete_messages_as_exit_group = jnode["deleteMessagesAsExitGroup"].GetBool();
            configs->setDeleteMessageAsExitGroup(delete_messages_as_exit_group);
        }

        if (jnode.HasMember("deleteMessagesAsExitRoom") && jnode["deleteMessagesAsExitRoom"].IsBool()) {
            bool delete_messages_as_exit_room = jnode["deleteMessagesAsExitRoom"].GetBool();
            configs->setDeleteMessageAsExitChatRoom(delete_messages_as_exit_room);
        }

        if (jnode.HasMember("isRoomOwnerLeaveAllowed") && jnode["isRoomOwnerLeaveAllowed"].IsBool()) {
            bool is_room_owner_leave_allowed = jnode["isRoomOwnerLeaveAllowed"].GetBool();
            configs->setIsChatroomOwnerLeaveAllowed(is_room_owner_leave_allowed);
        }

        if (jnode.HasMember("sortMessageByServerTime") && jnode["sortMessageByServerTime"].IsBool()) {
            bool sort_message_by_server_time = jnode["sortMessageByServerTime"].GetBool();
            configs->setSortMessageByServerTime(sort_message_by_server_time);
        }

        if (jnode.HasMember("usingHttpsOnly") && jnode["usingHttpsOnly"].IsBool()) {
            bool using_https_only = jnode["usingHttpsOnly"].GetBool();
            configs->setUsingHttps(using_https_only);
        }

        if (jnode.HasMember("serverTransfer") && jnode["serverTransfer"].IsBool()) {
            bool server_transfer = jnode["serverTransfer"].GetBool();
            configs->setTransferAttachments(server_transfer);
        }

        if (jnode.HasMember("isAutoDownload") && jnode["isAutoDownload"].IsBool()) {
            bool is_auto_download = jnode["isAutoDownload"].GetBool();
            configs->setAutoDownloadThumbnail(is_auto_download);
        }

        if (jnode.HasMember("myUUID") && jnode["myUUID"].IsString()) {
            string myUUID = jnode["myUUID"].GetString();
            configs->setMyUUID(myUUID);
        }

        if (jnode.HasMember("enableEmptyConversation") && jnode["enableEmptyConversation"].IsBool()) {
            bool enable_empty_conversation = jnode["enableEmptyConversation"].GetBool();
            configs->setEnableEmptyConversation(enable_empty_conversation);
        }

        if (jnode.HasMember("useReplacedMessageContents") && jnode["useReplacedMessageContents"].IsBool()) {
            bool use_replaced_message_contents = jnode["useReplacedMessageContents"].GetBool();
            configs->setUseReplacedMessageContents(use_replaced_message_contents);
        }

        if (jnode.HasMember("customOSType") && jnode["customOSType"].IsInt()) {
            int custom_os_type = jnode["customOSType"].GetInt();
            if (-1 != custom_os_type) {
                configs->setOs(EMChatConfigs::OSType::OS_CUSTOM);
                configs->setOSCustomValue(custom_os_type);
            }
        }

        if (jnode.HasMember("customDeviceName") && jnode["customDeviceName"].IsString()) {
            string custom_device_name = jnode["customDeviceName"].GetString();
            configs->setDeviceName(custom_device_name);
        }

        if (jnode.HasMember("regardImportMsgAsRead") && jnode["regardImportMsgAsRead"].IsBool()) {
            bool regard_import_msg_as_read = jnode["regardImportMsgAsRead"].GetBool();
            configs->setRegardImportMsgAsRead(regard_import_msg_as_read);
        }

        if (jnode.HasMember("includeSendMessageInMessageListener") && jnode["includeSendMessageInMessageListener"].IsBool()) {
            bool include_send_message_in_message_listener = jnode["includeSendMessageInMessageListener"].GetBool();
            configs->setIncludeSendMessageInMessageListener(include_send_message_in_message_listener);
        }

        //TODO: need to Area code later

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

    int Message::MessageDirectionToInt(EMMessage::EMMessageDirection direction)
    {
        switch (direction)
        {
        case EMMessage::EMMessageDirection::SEND: return 0;
        case EMMessage::EMMessageDirection::RECEIVE: return 1;
        default: return 0;
        }
    }

    EMMessage::EMMessageDirection Message::MessageDirectionFromInt(int i)
    {
        switch (i)
        {
        case 0: return EMMessage::EMMessageDirection::SEND;
        case 1: return EMMessage::EMMessageDirection::RECEIVE;
        default: return EMMessage::EMMessageDirection::SEND;
        }
    }

    int Message::BodyTypeToInt(EMMessageBody::EMMessageBodyType btype)
    {
        switch (btype)
        {
        case EMMessageBody::EMMessageBodyType::TEXT: return 0;
        case EMMessageBody::EMMessageBodyType::IMAGE: return 1;
        case EMMessageBody::EMMessageBodyType::VIDEO: return 2;
        case EMMessageBody::EMMessageBodyType::LOCATION: return 3;
        case EMMessageBody::EMMessageBodyType::VOICE: return 4;
        case EMMessageBody::EMMessageBodyType::FILE: return 5;
        case EMMessageBody::EMMessageBodyType::COMMAND: return 6;
        case EMMessageBody::EMMessageBodyType::CUSTOM: return 7;
        case EMMessageBody::EMMessageBodyType::COMBINE: return 8;
        default: return 0;
        }
    }
    EMMessageBody::EMMessageBodyType Message::BodyTypeFromInt(int i)
    {
        switch (i)
        {
        case 0: return EMMessageBody::EMMessageBodyType::TEXT;
        case 1: return EMMessageBody::EMMessageBodyType::IMAGE;
        case 2: return EMMessageBody::EMMessageBodyType::VIDEO;
        case 3: return EMMessageBody::EMMessageBodyType::LOCATION;
        case 4: return EMMessageBody::EMMessageBodyType::VOICE;
        case 5: return EMMessageBody::EMMessageBodyType::FILE;
        case 6: return EMMessageBody::EMMessageBodyType::COMMAND;
        case 7: return EMMessageBody::EMMessageBodyType::CUSTOM;
        case 8: return EMMessageBody::EMMessageBodyType::COMBINE;
        default:
            return EMMessageBody::EMMessageBodyType::TEXT;
        }
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

        //if (!jnode.IsArray()) return exts;

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

        auto body = msg->bodies().front();

        EMMessageBody::EMMessageBodyType btype = body->type();

        writer.StartObject();
        {
            writer.Key("type");
            writer.Int(Message::BodyTypeToInt(btype));

            writer.Key("operationTime");
            writer.Uint64(body->operationTime());

            writer.Key("operatorId");
            writer.String(body->operatorId().c_str());

            writer.Key("operationCount");
            writer.Uint64(body->operationCount());

            writer.Key("body");
            writer.StartObject();
            {
                switch (btype) {
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

                        writer.Key("thumbnailStatus");
                        writer.Int(DownLoadStatusToInt(ptr->thumbnailDownloadStatus()));

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

                        writer.Key("thumbnailStatus");
                        writer.Int(DownLoadStatusToInt(ptr->thumbnailDownloadStatus()));

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
                    case EMMessageBody::COMBINE:
                    {
                        EMCombineMessageBodyPtr ptr = dynamic_pointer_cast<EMCombineMessageBody>(body);

                        writer.Key("remotePath");
                        writer.String(ptr->remotePath().c_str());

                        writer.Key("secret");
                        writer.String(ptr->secretKey().c_str());

                        writer.Key("localPath");
                        writer.String(ptr->localPath().c_str());

                        writer.Key("title");
                        writer.String(ptr->title().c_str());

                        writer.Key("summary");
                        writer.String(ptr->summary().c_str());

                        writer.Key("compatibleText");
                        writer.String(ptr->compatibleText().c_str());

                        writer.Key("messageList");
                        MyJson::ToJsonObject(writer, ptr->messageList());
                    }
                    break;
                    default:
                    {
                        //message = NULL;
                    }
                }
            }
            writer.EndObject();
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

    // body:{type:iType, "body":{object}}
    EMMessageBodyPtr Message::FromJsonObjectToBody(const Value& jnode)
    {
        if (jnode.IsNull()) return nullptr;

        if (!jnode.HasMember("type") || !jnode.HasMember("body")) return nullptr;

        if (!jnode["type"].IsInt() || !jnode["body"].IsObject()) return nullptr;

        EMMessageBodyPtr bodyptr = nullptr;

        EMMessageBody::EMMessageBodyType btype = Message::BodyTypeFromInt(jnode["type"].GetInt());
        const Value& body = jnode["body"];

        switch (btype) {
        case EMMessageBody::TEXT:
        {
            EMTextMessageBodyPtr ptr = EMTextMessageBodyPtr(new EMTextMessageBody());

            if (body.HasMember("content") && body["content"].IsString()) {
                string str = body["content"].GetString();
                ptr->setText(str);
            }

            if (body.HasMember("translations") && body["translations"].IsObject()) {
                map<string, string> translations = MyJson::FromJsonObjectToMap(body["translations"]);//FromJsonToMap(str);
                ptr->setTranslations(translations);
            }

            if (body.HasMember("targetLanguages") && body["targetLanguages"].IsArray()) {
                vector<string> tagert_languages = MyJson::FromJsonObjectToVector(body["targetLanguages"]);
                ptr->setTargetLanguages(tagert_languages);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::LOCATION:
        {
            double latitude = 0;
            if (body.HasMember("latitude") && body["latitude"].IsNumber()) {
                latitude = body["latitude"].GetDouble();
            }

            double longitude = 0;
            if (body.HasMember("longitude") && body["longitude"].IsNumber()) {
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

            if (body.HasMember("thumbnailStatus") && body["thumbnailStatus"].IsInt()) {
                int i = body["thumbnailStatus"].GetInt();
                ptr->setThumbnailDownloadStatus(DownLoadStatusFromInt(i));
            }

            EMImageMessageBody::Size size;
            size.mWidth = 0;
            size.mHeight = 0;

            if (body.HasMember("height") && body["height"].IsNumber()) {
                size.mHeight = body["height"].GetDouble();
            }

            if (body.HasMember("width") && body["width"].IsNumber()) {
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

            if (body.HasMember("thumbnailStatus") && body["thumbnailStatus"].IsInt()) {
                int i = body["thumbnailStatus"].GetInt();
                ptr->setThumbnailDownloadStatus(DownLoadStatusFromInt(i));
            }

            EMVideoMessageBody::Size size;
            size.mWidth = 0;
            size.mHeight = 0;

            if (body.HasMember("height") && body["height"].IsNumber()) {
                size.mHeight = body["height"].GetDouble();
            }

            if (body.HasMember("width") && body["width"].IsNumber()) {
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

            if (body.HasMember("params") && body["params"].IsObject()) {
                EMCustomMessageBody::EMCustomExts exts = FromJsonObjectToCustomExts(body["params"]);
                ptr->setExts(exts);
            }

            bodyptr = dynamic_pointer_cast<EMMessageBody>(ptr);
        }
        break;
        case EMMessageBody::COMBINE:
        {
            EMCombineMessageBodyPtr ptr = EMCombineMessageBodyPtr(new EMCombineMessageBody());

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("localPath") && body["localPath"].IsString()) {
                string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            if (body.HasMember("title") && body["title"].IsString()) {
                string str = body["title"].GetString();
                ptr->setTitle(str);
            }

            if (body.HasMember("summary") && body["summary"].IsString()) {
                string str = body["summary"].GetString();
                ptr->setSummary(str);
            }

            if (body.HasMember("compatibleText") && body["compatibleText"].IsString()) {
                string str = body["compatibleText"].GetString();
                ptr->setCompatibleText(str);
            }

            if (body.HasMember("messageList") && body["messageList"].IsArray()) {
                vector<string> ml = MyJson::FromJsonObjectToVector(body["messageList"]);
                ptr->setMessageList(ml);
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

            writer.Key("convId");
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
            writer.Int(MessageDirectionToInt(msg->msgDirection()));

            writer.Key("priority");
            writer.Int(msg->priority());

            writer.Key("deliverOnlineOnly");
            writer.Bool(msg->isDeliverOnlineOnly());

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

            map<string, EMAttributeValuePtr> attr = msg->ext();
            if (attr.size() > 0) {
                writer.Key("attr");
                AttributesValue::ToJsonObjectWithAttributes(writer, msg);
            }

            writer.Key("localTime");
            writer.Int64(msg->localTime());

            writer.Key("serverTime");
            writer.Int64(msg->timestamp());

            writer.Key("isThread");
            writer.Bool(msg->isThread());

            writer.Key("broadcast");
            writer.Bool(msg->broadcast());

            writer.Key("isContentReplaced");
            writer.Bool(msg->isContentReplaced());

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
        if (jnode.HasMember("direction") && jnode["direction"].IsInt()) {
            int i = jnode["direction"].GetInt();
            direction = MessageDirectionFromInt(i);
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

        EMMessageBodyPtr body = FromJsonObjectToBody(jnode["body"]);

        EMMessagePtr msg = nullptr;
        if (EMMessage::EMMessageDirection::SEND == direction)
            msg = EMMessage::createSendMessage(from, to, body, msg_type);
        else
            msg = EMMessage::createReceiveMessage(from, to, body, msg_type, msg_id);

        msg->setMsgId(msg_id);
        msg->setMsgDirection(direction);

        if (jnode.HasMember("convId") && jnode["convId"].IsString()) {
            string str = jnode["convId"].GetString();
            msg->setConversationId(str);
        }

        if (jnode.HasMember("recallBy") && jnode["recallBy"].IsString()) {
            string str = jnode["recallBy"].GetString();
            msg->setRecallBy(str);
        }

        if (jnode.HasMember("priority") && jnode["priority"].IsInt()) {
            int i = jnode["priority"].GetInt();
            msg->setPriority(i);
        }

        if (jnode.HasMember("deliverOnlineOnly") && jnode["deliverOnlineOnly"].IsBool()) {
            bool b = jnode["deliverOnlineOnly"].GetBool();
            msg->deliverOnlineOnly(b);
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

        if (jnode.HasMember("attr") && jnode["attr"].IsObject())
            AttributesValue::SetMessageAttrs(msg, jnode["attr"]);

        if (jnode.HasMember("localTime") && jnode["localTime"].IsInt64()) {
            int64_t i = jnode["localTime"].GetInt64();
            msg->setLocalTime(i);
        }

        if (jnode.HasMember("serverTime") && jnode["serverTime"].IsString()) {
            int64_t i = jnode["serverTime"].GetInt64();;
            msg->setTimestamp(i);
        }

        if (jnode.HasMember("isThread") && jnode["isThread"].IsBool()) {
            bool b = jnode["isThread"].GetBool();
            msg->setIsThread(b);
        }

        if (jnode.HasMember("receiverList") && jnode["receiverList"].IsArray()) {
            vector<string> rl = MyJson::FromJsonObjectToVector(jnode["receiverList"]);
            if (rl.size() > 0) msg->setReceiverList(rl);
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
                writer.String("True"); //need to check
            else
                writer.String("False");
        }
        else if (attribute->is<int32_t>()) {
            writer.Key("type");
            writer.String("i");
            writer.Key("value");
            writer.String(convert2String<int32_t>(attribute->value<int32_t>()).c_str());
        }
        else if (attribute->is<int64_t>()) {
            writer.Key("type");
            writer.String("l");
            writer.Key("value");
            writer.String(convert2String<int64_t>(attribute->value<int64_t>()).c_str());
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
        else if (attribute->is<EMJsonString>()) {
            writer.Key("type");
            writer.String("jstr");
            writer.Key("value");
            writer.String(attribute->value<EMJsonString>().c_str());
        }
        //No need to support vector string
        /*
        else if (attribute->is<vector<string>>()) {
            writer.Key("type");
            writer.String("strv");
            writer.Key("value");
            vector<string> vec = attribute->value<vector<string>>();
            writer.String(MyJson::ToJson(vec).c_str());
        }
        */
        else {
        }
        writer.EndObject();
    }

    void AttributesValue::ToJsonObjectWithAttributes(Writer<StringBuffer>& writer, EMMessagePtr msg)
    {
        map<string, EMAttributeValuePtr> ext;

        if (nullptr != msg) {
            ext = msg->ext();
        }

        writer.StartObject();

        for (auto it : ext) {
            string key = it.first;
            EMAttributeValuePtr attribute = it.second;
            writer.Key(key.c_str());
            ToJsonObjectWithAttribute(writer, attribute);
        }

        writer.EndObject();
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
       name2: {type:c, value:11}, --- not support any more!!
       name3: {type:uc, value:a}, --- not support any more!!
       name4: {type:s, value:-123}, --- not support any more!!
       name5: {type:us, value:123}, --- not support any more!!
       name6: {type:i, value:-456},
       name7: {type:ui, value:456}, --- not support any more!!
       name8: {type:l, value:-123456},
       name9: {type:ul, value:123456}, --- not support any more!!
       name10:{type:f, value:1.23},
       name11:{type:d, value:1.23456},
       name12:{type:str, value:"a string"},
       name13:{type:strv, value:["str1", "str2", "str3"]},    --- not support any more!!
       name14:{type:jstr, value:"a json string"}, --- not support any more!!
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
            //uint32_t ui = convertFromString<uint32_t>(v);
            //msg->setAttribute(key, ui);
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
        else if (type.compare("jstr") == 0) {
            EMJsonString json(v);
            msg->setAttribute(key, json);
        }
        /*
        else if (type.compare("strv") == 0) {
            EMJsonString json(v);
            msg->setAttribute(key, json);
        }        
        else if (type.compare("attr") == 0) {
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

        writer.Key("convId");
        writer.String(conversation->conversationId().c_str());

        writer.Key("type");
        writer.Int(ConversationTypeToInt(conversation->conversationType()));

        writer.Key("isThread");
        writer.Bool(conversation->isThread());

        writer.Key("isPinned");
        writer.Bool(conversation->isPinned());

        writer.Key("pinnedTime");
        writer.Int64(conversation->pinnedTime());

        string ext = conversation->extField();
        map<string, string> ext_map = MyJson::FromJsonToMap(ext);
        if (ext_map.size() > 0) {
            writer.Key("ext");
            MyJson::ToJsonObject(writer, ext_map);
        }

        writer.EndObject();
    }

    void Conversation::ToJsonObject(Writer<StringBuffer>& writer, const EMConversationList& conversations)
    {
        writer.StartArray();
        for (auto it : conversations) {
            ToJsonObject(writer, it);
        }
        writer.EndArray();
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
        //if (conversations.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, conversations);

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

    int Conversation::EMMessageSearchDirectionToInt(EMConversation::EMMessageSearchDirection direction)
    {
        switch (direction)
        {
        case EMConversation::EMMessageSearchDirection::UP: return 0;
        case EMConversation::EMMessageSearchDirection::DOWN: return 1;
        default: return 0;
        }
    }
    EMConversation::EMMessageSearchDirection Conversation::EMMessageSearchDirectionFromInt(int i)
    {
        switch (i)
        {
        case 0: return EMConversation::EMMessageSearchDirection::UP;
        case 1: return EMConversation::EMMessageSearchDirection::DOWN;
        default: return EMConversation::EMMessageSearchDirection::UP;
        }
    }

    int Conversation::EMMessageSearchScopeToInt(EMConversation::EMMessageSearchScope scope)
    {
        switch (scope)
        {
        case EMConversation::EMMessageSearchScope::CONTENT: return 0;
        case EMConversation::EMMessageSearchScope::EXT: return 1;
        case EMConversation::EMMessageSearchScope::ALL: return 2;
        default: return 0;
        }
    }
    EMConversation::EMMessageSearchScope Conversation::EMMessageSearchScopeFromInt(int i)
    {
        switch (i)
        {
        case 0: return EMConversation::EMMessageSearchScope::CONTENT;
        case 1: return EMConversation::EMMessageSearchScope::EXT;
        case 2: return EMConversation::EMMessageSearchScope::ALL;
        default: return EMConversation::EMMessageSearchScope::CONTENT;
        }
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

    string GroupReadAck::ToJson(const vector<EMGroupReadAckPtr>& group_read_ack_vec)
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
        if (list.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartArray();
        for (auto it : list) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
        return s.GetString();
    }

    string MessageReaction::ToJson(EMMessagePtr msg)
    {
        if (nullptr == msg) return string();

        EMMessageReactionList& list = msg->reactionList();

        if (list.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartArray();
        for (auto it : list) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
        return s.GetString();
    }

    string MessageReaction::ToJson(map<string, EMMessageReactionList>& map)
    {
        if (map.size() == 0) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartObject();
        for (auto it : map) {
            if (it.second.size() > 0) {
                writer.Key(it.first.c_str());
                ToJsonObject(writer, it.second);
            }
        }
        writer.EndObject();
        return s.GetString();
    }

    void MessageReaction::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionPtr reaction)
    {
        writer.StartObject();
        {
            if (nullptr != reaction) {
                writer.Key("reaction");
                writer.String(reaction->reaction().c_str());
                writer.Key("count");
                writer.Int(reaction->count());
                writer.Key("isAddedBySelf");
                writer.Bool(reaction->state());

                writer.Key("userList");
                MyJson::ToJsonObject(writer, reaction->userList());
            }
        }
        writer.EndObject();
    }

    void MessageReaction::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionList& list)
    {
        writer.StartArray();
        for (EMMessageReactionPtr reaction : list) {
            ToJsonObject(writer, reaction);
        }
        writer.EndArray();
    }

    EMMessageReactionPtr MessageReaction::FromJsonObjectToReaction(const Value& jnode)
    {
        //return EMMessageEncoder::decodeReactionFromJson(jnode);
        //TODO: add your code later, no need now.
        return nullptr;
    }

    EMMessageReactionList MessageReaction::FromJsonObjectToReactionList(const Value& jnode)
    {
        //return EMMessageEncoder::decodeReactionListFromJson(jnode);
        //TODO: add your code later, no need now.
        std::vector<EMMessageReactionPtr> vec;
        return vec;
    }

    EMMessageReactionList MessageReaction::FromJsonToReactionList(string json)
    {
        //return EMMessageEncoder::decodeReactionListFromJson(json);
        //TODO: add your code later, no need now.
        std::vector<EMMessageReactionPtr> vec;
        return vec;
    }

    void MessageReactionChange::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionChangePtr reactionChangePtr, std::string curname)
    {
        std::string covId = reactionChangePtr->to();
        if (covId.compare(curname) == 0)
            covId = reactionChangePtr->from();

        writer.StartObject();
        {
            writer.Key("convId");
            writer.String(covId.c_str());
            writer.Key("msgId");
            writer.String(reactionChangePtr->messageId().c_str());
            writer.Key("reactions");
            MessageReaction::ToJsonObject(writer, reactionChangePtr->reactionList());
            writer.Key("operations");
            MessageReactionOperation::ToJsonObject(writer, reactionChangePtr->operationList());
        }
        writer.EndObject();
    }

    std::string MessageReactionChange::ToJson(EMMessageReactionChangePtr reactionChangePtr, std::string curname)
    {
        if (nullptr == reactionChangePtr) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        ToJsonObject(writer, reactionChangePtr, curname);
        return s.GetString();
    }

    std::string MessageReactionChange::ToJson(EMMessageReactionChangeList list, std::string curname)
    {
        if (list.size() == 0) return std::string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);
        writer.StartArray();
        for (auto it : list)
        {
            ToJsonObject(writer, it, curname);
        }
        writer.EndArray();
        return s.GetString();
    }

    void MessageReactionOperation::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionOperationPtr reactionOperationPtr)
    {
        writer.StartObject();

        if (nullptr != reactionOperationPtr)
        {
            writer.Key("userId");
            writer.String(reactionOperationPtr->userId().c_str());

            writer.Key("reaction");
            writer.String(reactionOperationPtr->reaction().c_str());

            writer.Key("operate");
            writer.Int(reactionOperationPtr->operate());
        }

        writer.EndObject();
    }

    void MessageReactionOperation::ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionOperationList list)
    {
        writer.StartArray();
        for (auto it : list)
        {
            ToJsonObject(writer, it);
        }
        writer.EndArray();
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
        writer.StartObject();
        for (int i = 0; i < vec.size(); i++) {
            writer.Key(vec[i].first.c_str());
            writer.Int64(vec[i].second);
        }
        writer.EndObject();
    }

    void Group::ToJsonObjectWithGroupInfo(Writer<StringBuffer>& writer, EMGroupPtr group)
    {
        writer.StartObject();

        writer.Key("groupId");
        writer.String(group->groupId().c_str());

        writer.Key("name");
        writer.String(group->groupSubject().c_str());

        writer.EndObject();
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

        writer.Key("block");
        writer.Bool(group->isMessageBlocked());

        writer.Key("isMuteAll");
        writer.Bool(group->groupAllMembersMuted());

        if (nullptr != group->groupSetting()) {
            //writer.Key("options");
            //JsonObject(writer, group->groupSetting());

            writer.Key("maxUserCount");
            writer.Int(group->groupSetting()->maxUserCount());

            writer.Key("isMemberOnly");
            writer.Bool(IsMemberOnly(group->groupSetting()->style()));

            writer.Key("isMemberAllowToInvite");
            writer.Bool(IsMemberAllowToInvite(group->groupSetting()->style()));

            writer.Key("ext");
            writer.String(group->groupSetting()->extension().c_str());
        }

        writer.Key("permissionType");
        writer.Int(MemberTypeToInt(group->groupMemberType()));

        writer.Key("isDisabled");
        writer.Bool(group->isDisabled());

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

    void Group::ToJsonObject(Writer<StringBuffer>& writer, const unordered_map<string, unordered_map<string, string>>& map)
    {
        writer.StartObject();
        for (auto it : map)
        {
            unordered_map<string, string> sec_map = it.second;
            writer.Key(it.first.c_str());
            MyJson::ToJsonObject(writer, sec_map);
        }
        writer.EndObject();
    }

    string Group::ToJson(const EMMucMuteList& vec)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        string data = s.GetString();

        return data;
    }

    string Group::ToJson(const EMGroupList& list)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, list);

        return s.GetString();
    }

    string Group::ToJson(const unordered_map<string, unordered_map<string, string>>& map)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, map);

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

        string ext = "";
        if (jnode.HasMember("ext")) {
            ext = jnode["ext"].GetString();
        }

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

    bool Group::IsMemberOnly(EMMucSetting::EMMucStyle style)
    {
        if (EMMucSetting::EMMucStyle::PRIVATE_OWNER_INVITE == style ||
            EMMucSetting::EMMucStyle::PRIVATE_MEMBER_INVITE == style ||
            EMMucSetting::EMMucStyle::PUBLIC_JOIN_APPROVAL == style)
        {
            return true;
        }
        return false;
    }

    bool Group::IsMemberAllowToInvite(EMMucSetting::EMMucStyle style)
    {
        if (EMMucSetting::EMMucStyle::PRIVATE_MEMBER_INVITE == style)
            return true;
        else
            return false;
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

        writer.EndArray();
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
            writer.StartArray();
            MessageReaction::ToJsonObject(writer, reaction);
            writer.EndArray();
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
                Group::ToJsonObjectWithGroupInfo(writer, groupPtr);
            }
            writer.EndArray();
        }
        writer.EndObject();

        string data = s.GetString();
        return data;
    }

    string CursorResult::ToJson(string cursor, EMCursorResultRaw<EMThreadEventPtr> cusorResult)
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

    void Room::ToJsonObject(Writer<StringBuffer>& writer, const EMChatroomPtr room)
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

        writer.Key("isMuteAll");
        writer.Bool(room->isMucAllMembersMuted());

        writer.Key("permissionType");
        writer.Int(EMMucMemberTypeToInt(room->chatroomMemberType()));

        writer.EndObject();
    }
    string Room::ToJson(const EMChatroomPtr room)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, room);

        string data = s.GetString();
        return data;
    }

    void Room::ToJsonObject(Writer<StringBuffer>& writer, const EMChatroomList room_list)
    {
        writer.StartArray();

        for (auto it : room_list) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }
    string Room::ToJson(const EMChatroomList room_list)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, room_list);

        string data = s.GetString();
        return data;
    }

    /*
    * return json string looking like:
    * {
    *      "metaData":{"key":"value"},
    *      "autoDelete":"NO_DELETE"
    * }
    */
    string Room::ToJsonFromRoomAttribute(const map<string, string>& kvs, bool deleteWhenExit)
    {
        string json = "";

        if (kvs.size() > 0)
        {
            StringBuffer s;
            Writer<StringBuffer> writer(s);

            writer.StartObject();
            {
                writer.Key("metaData");
                MyJson::ToJsonObject(writer, kvs);

                string auto_delete = "DELETE";
                if (false == deleteWhenExit) auto_delete = "NO_DELETE";

                writer.Key("autoDelete");
                writer.String(auto_delete.c_str());
            }
            writer.EndObject();

            json = s.GetString();
        }
        return json;
    }

    /*
     * input json string looks like:
     * {
     *  "result":
     *   {
     *      "successKeys":["xxx", "xxx"],
     *      "errorKeys":
     *      {
     *          "xxx":"desc xxx",
     *          "xxx":"desc xxx",
     *      },
     *   },
     *   "is_forced":True,
     *   "properties:"
     *   {
     *      "xxx": "xxx",
     *      "xxx": "xxx",
     *   },
     *   "operator": "xxx"
     * }
     */

    map<string, string> Room::FromJsonToRoomSuccessAttribute(const string& json)
    {
        map<string, string> success_props;
        vector<string> successKeys;
        map<string, string> properties;

        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return success_props;

        //get list from "successKeys" in json
        if (d.HasMember("result") && d["result"].IsObject()) {

            const Value& jn = d["result"];
            if (jn.HasMember("successKeys") && jn["successKeys"].IsArray()) {
                successKeys = MyJson::FromJsonObjectToVector(jn["successKeys"]);
            }
        }

        // get dictionary from "properties" in json
        if (d.HasMember("properties") && d["properties"].IsObject()) {
            properties = MyJson::FromJsonObjectToMap(d["properties"]);
        }

        // generate success properties dictionary
        for (string key : successKeys) {

            auto it = properties.find(key);

            if (properties.end() != it) {
                success_props[key] = it->second;
            }
        }
        return success_props;
    }

    /*
    * input json string looks like:
    * {
    *  "result":
    *   {
    *      "successKeys":["xxx", "xxx"],
    *      "errorKeys":
    *      {
    *          "xxx":"desc xxx",
    *          "xxx":"desc xxx",
    *      },
    *   },
    *   "keys:" ["xxx","xxx"],
    *   "is_forced": True,
    *   "auto_delete": False,
    *   "operator": "xxx"
    * }
    */
    vector<string> Room::FromJsonToRoomSuccessKeys(const string& json)
    {
        vector<string> successKeys;

        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) return successKeys;

        //get list from "successKeys" in json
        if (d.HasMember("result") && d["result"].IsObject()) {

            const Value& jn = d["result"];
            if (jn.HasMember("successKeys") && jn["successKeys"].IsArray()) {
                successKeys = MyJson::FromJsonObjectToVector(jn["successKeys"]);
            }
        }
        return successKeys;
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

            writer.Key("type");
            writer.Int(ThreadOperationToInt(threadEventPtr->threadOperation()));

            writer.Key("thread");
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
            writer.Key("threadId");
            writer.String(threadEventPtr->threadId().c_str());

            writer.Key("msgId");
            writer.String(threadEventPtr->threadMessageId().c_str());

            writer.Key("parentId");
            writer.String(threadEventPtr->parentId().c_str());

            writer.Key("owner");
            writer.String(threadEventPtr->owner().c_str());

            writer.Key("name");
            writer.String(threadEventPtr->threadName().c_str());

            writer.Key("msgCount");
            writer.Int(threadEventPtr->messageCount());

            writer.Key("memberCount");
            writer.Int(threadEventPtr->membersCount());

            writer.Key("createAt");
            writer.Int64(threadEventPtr->createTimestamp());

            if (nullptr != threadEventPtr->lastMessage())
            {
                writer.Key("lastMsg");
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

        if (jnode.HasMember("threadId") && jnode["threadId"].IsString()) {
            std::string str = jnode["threadId"].GetString();
            thread->setThreadId(str);
        }

        if (jnode.HasMember("msgId") && jnode["msgId"].IsString()) {
            std::string str = jnode["msgId"].GetString();
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

        if (jnode.HasMember("msgCount") && jnode["msgCount"].IsInt()) {
            int i = jnode["msgCount"].GetInt();
            thread->setMessageCount(i);
        }

        if (jnode.HasMember("memberCount") && jnode["memberCount"].IsInt()) {
            int i = jnode["memberCount"].GetInt();
            thread->setMembersCount(i);
        }

        if (jnode.HasMember("createAt") && jnode["createAt"].IsInt64()) {
            int64_t i = jnode["createAt"].GetInt64();
            thread->setCreateTimestamp(i);
        }

        if (jnode.HasMember("lastMsg") && jnode["lastMsg"].IsObject()) {
            EMMessagePtr msg = Message::FromJsonObjectToMessage(jnode["lastMsg"]);
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
        writer.StartObject();

        writer.Key("device");
        writer.String(status.first.c_str());

        writer.Key("status");
        writer.Int(status.second);

        writer.EndObject();
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

        writer.Key("desc");
        writer.String(presence->getExt().c_str());

        writer.Key("lastTime");
        writer.Int64(presence->getLatestTime());

        writer.Key("expiryTime");
        writer.Int64(presence->getExpiryTime());

        writer.Key("detail");
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

    int MultiDevices::MultiDevicesOperationToInt(EMMultiDevicesListener::MultiDevicesOperation operation)
    {
        switch (operation)
        {
        case EMMultiDevicesListener::MultiDevicesOperation::UNKNOW: return -1;
        case EMMultiDevicesListener::MultiDevicesOperation::CONTACT_REMOVE: return 2;
        case EMMultiDevicesListener::MultiDevicesOperation::CONTACT_ACCEPT: return 3;
        case EMMultiDevicesListener::MultiDevicesOperation::CONTACT_DECLINE: return 4;
        case EMMultiDevicesListener::MultiDevicesOperation::CONTACT_BAN: return 5;
        case EMMultiDevicesListener::MultiDevicesOperation::CONTACT_ALLOW: return 6;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_CREATE: return 10;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_DESTROY: return 11;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_JOIN: return 12;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_LEAVE: return 13;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY: return 14;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY_ACCEPT: return 15;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY_DECLINE: return 16;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE: return 17;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE_ACCEPT: return 18;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE_DECLINE: return 19;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_KICK: return 20;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_BAN: return 21;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ALLOW: return 22;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_BLOCK: return 23;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_UNBLOCK: return 24;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ASSIGN_OWNER: return 25;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_ADMIN: return 26;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_ADMIN: return 27;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_MUTE: return 28;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_MUTE: return 29;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_USER_WHITE_LIST: return 30;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_USER_WHITE_LIST: return 31;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_ALL_BAN: return 32;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_ALL_BAN: return 33;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_CREATE: return 40;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_DESTROY: return 41;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_JOIN: return 42;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_LEAVE: return 43;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_UPDATE: return 44;
        case EMMultiDevicesListener::MultiDevicesOperation::THREAD_KICK: return 45;
        case EMMultiDevicesListener::MultiDevicesOperation::SET_METADATA: return 50;
        case EMMultiDevicesListener::MultiDevicesOperation::DELETE_METADATA: return 51;
        case EMMultiDevicesListener::MultiDevicesOperation::GROUP_MEMBER_METADATA_CHANGED: return 52;
        case EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_PINNED: return 60;
        case EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_UNPINNED: return 61;
        case EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_DELETED: return 62;
        case EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_MARK: return 63;
        default:
            return -1;
        }
    }
    EMMultiDevicesListener::MultiDevicesOperation MultiDevices::MultiDevicesOperationFromInt(int i)
    {
        switch (i)
        {
        case -1: return EMMultiDevicesListener::MultiDevicesOperation::UNKNOW;
        case 2: return EMMultiDevicesListener::MultiDevicesOperation::CONTACT_REMOVE;
        case 3: return EMMultiDevicesListener::MultiDevicesOperation::CONTACT_ACCEPT;
        case 4: return EMMultiDevicesListener::MultiDevicesOperation::CONTACT_DECLINE;
        case 5: return EMMultiDevicesListener::MultiDevicesOperation::CONTACT_BAN;
        case 6: return EMMultiDevicesListener::MultiDevicesOperation::CONTACT_ALLOW;
        case 10: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_CREATE;
        case 11: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_DESTROY;
        case 12: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_JOIN;
        case 13: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_LEAVE;
        case 14: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY;
        case 15: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY_ACCEPT;
        case 16: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_APPLY_DECLINE;
        case 17: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE;
        case 18: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE_ACCEPT;
        case 19: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_INVITE_DECLINE;
        case 20: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_KICK;
        case 21: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_BAN;
        case 22: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ALLOW;
        case 23: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_BLOCK;
        case 24: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_UNBLOCK;
        case 25: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ASSIGN_OWNER;
        case 26: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_ADMIN;
        case 27: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_ADMIN;
        case 28: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_MUTE;
        case 29: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_MUTE;
        case 30: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ADD_USER_WHITE_LIST;
        case 31: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_USER_WHITE_LIST;
        case 32: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_ALL_BAN;
        case 33: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_REMOVE_ALL_BAN;
        case 40: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_CREATE;
        case 41: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_DESTROY;
        case 42: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_JOIN;
        case 43: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_LEAVE;
        case 44: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_UPDATE;
        case 45: return EMMultiDevicesListener::MultiDevicesOperation::THREAD_KICK;
        case 50: return EMMultiDevicesListener::MultiDevicesOperation::SET_METADATA;
        case 51: return EMMultiDevicesListener::MultiDevicesOperation::DELETE_METADATA;
        case 52: return EMMultiDevicesListener::MultiDevicesOperation::GROUP_MEMBER_METADATA_CHANGED;
        case 60: return EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_PINNED;
        case 61: return EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_UNPINNED;
        case 62: return EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_DELETED;
        default: return EMMultiDevicesListener::MultiDevicesOperation::UNKNOW;
        }
    }

    void DeviceInfo::ToJsonObject(Writer<StringBuffer>& writer, const EMDeviceInfoPtr di)
    {
        writer.StartObject();

        writer.Key("resource");
        writer.String(di->mResource.c_str());

        writer.Key("deviceUUID");
        writer.String(di->mDeviceUUID.c_str());

        writer.Key("deviceName");
        writer.String(di->mDeviceName.c_str());

        writer.EndObject();
    }
    string DeviceInfo::ToJson(const EMDeviceInfoPtr di)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, di);

        std::string data = s.GetString();
        return data;
    }

    void DeviceInfo::ToJsonObject(Writer<StringBuffer>& writer, const vector<EMDeviceInfoPtr> vec)
    {
        writer.StartArray();

        for (auto it : vec) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }
    string DeviceInfo::ToJson(const vector<EMDeviceInfoPtr> vec)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        std::string data = s.GetString();
        return data;
    }

    EMContactPtr Contact::FromJson(const char* json)
    {
        // No need to implement this yet.
        return nullptr;
    }

    void Contact::ToJsonObject(Writer<StringBuffer>& writer, const EMContactPtr contact)
    {
        writer.StartObject();

        writer.Key("userId");
        writer.String(contact->getUsername().c_str());

        writer.Key("remark");
        writer.String(contact->getRemark().c_str());

        writer.EndObject();
    }

    string Contact::ToJson(const EMContactPtr contact)
    {
        if (nullptr == contact || nullptr == contact.get()) return string();

        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, contact);

        std::string data = s.GetString();
        return data;
    }

    void Contact::ToJsonObject(Writer<StringBuffer>& writer, const vector<EMContactPtr> vec)
    {
        writer.StartArray();

        for (auto it : vec) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string Contact::ToJson(const vector<EMContactPtr> vec)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, vec);

        std::string data = s.GetString();
        return data;
    }

    vector<EMMessageBody::EMMessageBodyType> FetchMessageOption::FromJsonObjectToBodyTypeVector(const Value& jnode)
    {
        vector<EMMessageBody::EMMessageBodyType> vec;

        if (!jnode.IsArray()) return vec;

        int size = jnode.Size();

        for (int i = 0; i < size; i++) {

            int type = jnode[i].GetInt();

            EMMessageBody::EMMessageBodyType btype = Message::BodyTypeFromInt(type);

            vec.push_back(btype);
        }

        return vec;
    }

    EMFetchMessageOptionPtr FetchMessageOption::FromJsonObject(const Value& jnode)
    {
        if (jnode.IsNull()) return nullptr;

        EMFetchMessageOptionPtr option = EMFetchMessageOptionPtr(new EMFetchMessageOption());

        if (jnode.HasMember("isSave") && jnode["isSave"].IsBool()) {
            bool isSave = jnode["isSave"].GetBool();
            option->setIsSave(isSave);
        }

        if (jnode.HasMember("direction") && jnode["direction"].IsInt()) {
            int direction = jnode["direction"].GetInt();
            option->setDirection(Conversation::EMMessageSearchDirectionFromInt(direction));
        }

        if (jnode.HasMember("from") && jnode["from"].IsString()) {
            std::string from = jnode["from"].GetString();
            option->setFrom(from);
        }

        if (jnode.HasMember("types") && jnode["types"].IsArray()) {
            const Value& array = jnode["types"];
            vector<EMMessageBody::EMMessageBodyType> vec = FromJsonObjectToBodyTypeVector(array);
            option->setMsgTypes(vec);
        }

        if (jnode.HasMember("startTime") && jnode["startTime"].IsInt64()) {
            int64_t i = jnode["startTime"].GetInt64();
            option->setStartTime(i);
        }

        if (jnode.HasMember("endTime") && jnode["endTime"].IsInt64()) {
            int64_t i = jnode["endTime"].GetInt64();
            option->setEndTime(i);
        }

        return option;
    }

    EMFetchMessageOptionPtr FetchMessageOption::FromJson(std::string json)
    {
        if (json.length() < 3) return nullptr;

        Document d;
        if (!d.Parse(json.data()).HasParseError()) {
            return FromJsonObject(d);
        }
        return nullptr;
    }

    UserInfo::UserInfo()
    {
        nickName = "";
        avatarUrl = "";
        email = "";
        phoneNumber = "";
        signature = "";
        birth = "";
        userId = "";
        ext = "";
        gender = 0;
    }

    void UserInfo::ToJsonObject(Writer<StringBuffer>& writer, UserInfo& ui)
    {
        writer.StartObject();
        {
            if (ui.nickName.size() > 0) {
                writer.Key("nickName");
                writer.String(ui.nickName.c_str());
            }

            if (ui.avatarUrl.size() > 0) {
                writer.Key("avatarUrl");
                writer.String(ui.avatarUrl.c_str());
            }
            if (ui.email.size() > 0) {
                writer.Key("mail");
                writer.String(ui.email.c_str());
            }
            if (ui.phoneNumber.size() > 0) {
                writer.Key("phone");
                writer.String(ui.phoneNumber.c_str());
            }
            if (ui.signature.size() > 0) {
                writer.Key("sign");
                writer.String(ui.signature.c_str());
            }
            if (ui.birth.size() > 0) {
                writer.Key("birth");
                writer.String(ui.birth.c_str());
            }
            if (ui.userId.size() > 0) {
                writer.Key("userId");
                writer.String(ui.userId.c_str());
            }
            if (ui.ext.size() > 0) {
                writer.Key("ext");
                writer.String(ui.ext.c_str());
            }

            writer.Key("gender");
            writer.Int(ui.gender);
        }
        writer.EndObject();
    }

    void UserInfo::ToJsonObject(Writer<StringBuffer>& writer, map<string, UserInfo>& uis)
    {
        writer.StartObject();
        {
            for (auto it : uis) {
                ToJsonObject(writer, it.second);
            }
        }
        writer.EndObject();
    }

    UserInfo UserInfo::FromJsonObject(const Value& jnode)
    {
        UserInfo ui;

        if (jnode.HasMember("nickName") && jnode["nickName"].IsString())
            ui.nickName = jnode["nickName"].GetString();

        if (jnode.HasMember("avatarUrl") && jnode["avatarUrl"].IsString())
            ui.avatarUrl = jnode["avatarUrl"].GetString();

        if (jnode.HasMember("mail") && jnode["mail"].IsString())
            ui.email = jnode["mail"].GetString();

        if (jnode.HasMember("phone") && jnode["phone"].IsString())
            ui.phoneNumber = jnode["phone"].GetString();

        if (jnode.HasMember("sign") && jnode["sign"].IsString())
            ui.signature = jnode["sign"].GetString();

        if (jnode.HasMember("birth") && jnode["birth"].IsString())
            ui.birth = jnode["birth"].GetString();

        if (jnode.HasMember("ext") && jnode["ext"].IsString())
            ui.ext = jnode["ext"].GetString();

        if (jnode.HasMember("gender") && jnode["gender"].IsInt())
            ui.gender = jnode["gender"].GetInt();

        if (jnode.HasMember("userId") && jnode["userId"].IsString())
            ui.userId = jnode["userId"].GetString();
        
        return ui;
    }

    string UserInfo::ToJson(UserInfo& ui)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObject(writer, ui);

        std::string data = s.GetString();
        return data;
    }

    string UserInfo::ToJson(map<string, UserInfo>& uis)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        writer.StartObject();

        for (auto it : uis) {
            writer.Key(it.first.c_str());
            ToJsonObject(writer, it.second);
        }

        writer.EndObject();

        std::string data = s.GetString();
        return data;
    }

    UserInfo UserInfo::FromJson(string json)
    {
        UserInfo ui;

        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) {
            return ui;
        }
        else {
            return FromJsonObject(d["userInfo"]);
        }
    }

    void UserInfo::ToJsonObjectForServer(Writer<StringBuffer>& writer, UserInfo& ui)
    {
        writer.StartObject();

        if (ui.nickName.length() > 0) {
            writer.Key("nickname");
            writer.String(ui.nickName.c_str());
        }

        if (ui.avatarUrl.length() > 0) {
            writer.Key("avatarurl");
            writer.String(ui.avatarUrl.c_str());
        }

        if (ui.email.length() > 0) {
            writer.Key("mail");
            writer.String(ui.email.c_str());
        }

        if (ui.phoneNumber.length() > 0) {
            writer.Key("phone");
            writer.String(ui.phoneNumber.c_str());
        }

        if (0 == ui.gender || 1 == ui.gender || 2 == ui.gender) {
            writer.Key("gender");
            writer.Int(ui.gender);
        }

        if (ui.signature.length() > 0) {
            writer.Key("sign");
            writer.String(ui.signature.c_str());
        }

        if (ui.birth.length() > 0) {
            writer.Key("birth");
            writer.String(ui.birth.c_str());
        }

        if (ui.ext.length() > 0) {
            writer.Key("ext");
            writer.String(ui.ext.c_str());
        }

        writer.EndObject();
    }

    map<string, UserInfo> UserInfo::FromJsonObjectFromServer(const Value& jnode)
    {
        std::map<std::string, UserInfo> userinfoMap;

        for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {

            auto key = iter->name.GetString();
            const Value& objItem = iter->value;

            if (objItem.IsObject()) {

                UserInfo ui;

                if (objItem.HasMember("nickname") && objItem["nickname"].IsString())
                    ui.nickName = objItem["nickname"].GetString();

                if (objItem.HasMember("avatarurl") && objItem["avatarurl"].IsString())
                    ui.avatarUrl = objItem["avatarurl"].GetString();

                if (objItem.HasMember("mail") && objItem["mail"].IsString())
                    ui.email = objItem["mail"].GetString();

                if (objItem.HasMember("phone") && objItem["phone"].IsString())
                    ui.phoneNumber = objItem["phone"].GetString();

                if (objItem.HasMember("sign") && objItem["sign"].IsString())
                    ui.signature = objItem["sign"].GetString();

                if (objItem.HasMember("birth") && objItem["birth"].IsString())
                    ui.birth = objItem["birth"].GetString();

                if (objItem.HasMember("ext") && objItem["ext"].IsString())
                    ui.ext = objItem["ext"].GetString();

                if (objItem.HasMember("gender") && objItem["gender"].IsString()) {
                    std::string gender_str = objItem["gender"].GetString();
                    if (gender_str.compare("0") == 0 || gender_str.compare("1") == 0 || gender_str.compare("2") == 0)
                        ui.gender = std::stoi(gender_str);
                }

                ui.userId = key;
                userinfoMap[key] = ui;
            }
        }
        return userinfoMap;
    }

    string UserInfo::ToJsonForServer(UserInfo& ui)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObjectForServer(writer, ui);

        std::string data = s.GetString();
        return data;
    }

    map<string, UserInfo> UserInfo::FromJsonFromServer(string json)
    {
        map<string, UserInfo> userinfoMap;

        Document d;
        d.Parse(json.c_str());
        if (d.HasParseError()) {
            return userinfoMap;
        }
        else {
            return FromJsonObjectFromServer(d);
        }        
    }

    void PinnedInfo::ToJsonObject(Writer<StringBuffer>& writer, bool isPinned, const string& operatorId, int64_t ts)
    {
        writer.StartObject();
        {
            //writer.Key("isPinned");
            //writer.Bool(isPinned);

            writer.Key("pinnedBy");
            writer.String(operatorId.c_str());

            writer.Key("pinnedAt");
            writer.Int64(ts);
        }
        writer.EndObject();
    }

    void RecallMessageInfo::ToJsonObject(Writer<StringBuffer>& writer, std::tuple<std::string, std::string, std::string, easemob::EMMessagePtr>& tuple)
    {
        writer.StartObject();
        {
            writer.Key("recallBy");
            writer.String(std::get<0>(tuple).c_str());

            writer.Key("recallMessageId");
            writer.String(std::get<1>(tuple).c_str());

            writer.Key("ext");
            writer.String(std::get<2>(tuple).c_str());

            easemob::EMMessagePtr msg = nullptr;
            msg = std::get<3>(tuple);

            if (nullptr != msg && nullptr != msg.get()) {
                writer.Key("recallMessage");
                Message::ToJsonObjectWithMessage(writer, msg);
            }
        }
        writer.EndObject();
    }

    void RecallMessageInfo::ToJsonObjectWithList(Writer<StringBuffer>& writer, const std::vector<std::tuple<std::string, std::string, std::string, easemob::EMMessagePtr>>& vec)
    {
        writer.StartArray();

        for (auto it : vec) {
            ToJsonObject(writer, it);
        }

        writer.EndArray();
    }

    string RecallMessageInfo::ToJson(const std::vector<std::tuple<std::string, std::string, std::string, easemob::EMMessagePtr>>& vec)
    {
        StringBuffer s;
        Writer<StringBuffer> writer(s);

        ToJsonObjectWithList(writer, vec);

        std::string data = s.GetString();
        return data;
    }
}

