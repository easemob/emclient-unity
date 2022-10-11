#include "tool.h"

#include "models.h"
#include "sdk_wrapper.h"

namespace sdk_wrapper
{
	int Options::FromJson(const char* json, EMChatConfigsPtr configs)
	{
        if (nullptr == json || strlen(json) == 0 || nullptr == configs) return -1;

        Document d;
        d.Parse(json);
        if (d.HasParseError()) return;

        const Value& jnode = d;

        if (jnode.IsNull()) return;

        if (jnode.HasMember("app_key") && jnode["app_key"].IsString()) {            
            string app_key = jnode["app_key"].GetString();
            configs->setAppKey(app_key);
        }

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
}

