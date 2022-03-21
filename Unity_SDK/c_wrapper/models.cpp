//
//  models.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/6/30.
//  Copyright © 2021 easemob. All rights reserved.
//
#include "LogHelper.h"
#include "models.h"
#include "emgroup.h"
#include "json.hpp"
#include "tool.h"

std::string EMPTY_STR = " ";
std::string DISPLAY_NAME_STR = " "; // used to save display name temprarily

EMMessagePtr BuildEMMessage(void *mto, EMMessageBody::EMMessageBodyType type, bool buildReceiveMsg)
{
    //compose message body
    std::string from, to, msgId, attrs;
    EMMessage::EMChatType msgType = EMMessage::EMChatType::SINGLE;
    EMMessageBodyPtr messageBody;
    switch(type) {
        case EMMessageBody::TEXT:
        {
            auto tm = static_cast<TextMessageTO *>(mto);
            LOG("Message id from MTO is: id:%s", tm->MsgId);
            //create message body
            messageBody = EMMessageBodyPtr(new EMTextMessageBody(std::string(tm->body.Content)));
            from = tm->From;
            to = tm->To;
            msgId = tm->MsgId;
            msgType = tm->Type;
            attrs = tm->AttributesValues;
        }
            break;
        case EMMessageBody::LOCATION:
        {
            auto lm = static_cast<LocationMessageTO *>(mto);
            messageBody = EMMessageBodyPtr(new EMLocationMessageBody(lm->body.Latitude, lm->body.Longitude, lm->body.Address, lm->body.BuildingName));
            from = lm->From;
            to = lm->To;
            msgId = lm->MsgId;
            msgType = lm->Type;
            attrs = lm->AttributesValues;
        }
            break;
        case EMMessageBody::COMMAND:
        {
            auto cm = static_cast<CmdMessageTO *>(mto);
            auto body = new EMCmdMessageBody(cm->body.Action);
            body->deliverOnlineOnly(cm->body.DeliverOnlineOnly);
            messageBody = EMMessageBodyPtr(body);
            from = cm->From;
            to = cm->To;
            msgId = cm->MsgId;
            msgType = cm->Type;
            attrs = cm->AttributesValues;
        }
            break;
        case EMMessageBody::FILE:
        {
            auto fm = static_cast<FileMessageTO *>(mto);
            auto body = new EMFileMessageBody(fm->body.LocalPath);
            body->setDisplayName(fm->body.DisplayName);
            body->setSecretKey(fm->body.Secret);
            body->setRemotePath(fm->body.RemotePath);
            body->setFileLength(fm->body.FileSize);
            body->setDownloadStatus(fm->body.DownStatus);
            messageBody = EMMessageBodyPtr(body);
            from = fm->From;
            to = fm->To;
            msgId = fm->MsgId;
            msgType = fm->Type;
            attrs = fm->AttributesValues;
        }
            break;
        case EMMessageBody::IMAGE:
        {
            auto im = static_cast<ImageMessageTO *>(mto);
            auto body = new EMImageMessageBody(im->body.LocalPath, im->body.ThumbnailLocalPath);
            body->setSecretKey(im->body.Secret);
            body->setFileLength(im->body.FileSize);
            body->setDownloadStatus(im->body.DownStatus);
            body->setDisplayName(im->body.DisplayName);
            body->setRemotePath(im->body.ThumbnaiRemotePath);
            body->setThumbnailSecretKey(im->body.ThumbnaiSecret);
            body->setThumbnailRemotePath(im->body.ThumbnaiRemotePath);
            body->setThumbnailDownloadStatus(im->body.ThumbnaiDownStatus);
            //TODO: add ThumbnailDisplayName field later
            messageBody = EMMessageBodyPtr(body);
            from = im->From;
            to = im->To;
            msgId = im->MsgId;
            msgType = im->Type;
            attrs = im->AttributesValues;
        }
            break;
        case EMMessageBody::VOICE:
        {
            auto vm = static_cast<VoiceMessageTO *>(mto);
            auto body = new EMVoiceMessageBody(vm->body.LocalPath, vm->body.Duration);
            body->setDisplayName(vm->body.DisplayName);
            body->setSecretKey(vm->body.Secret);
            body->setRemotePath(vm->body.RemotePath);
            body->setFileLength(vm->body.FileSize);
            body->setDownloadStatus(vm->body.DownStatus);
            messageBody = EMMessageBodyPtr(body);
            from = vm->From;
            to = vm->To;
            msgId = vm->MsgId;
            msgType = vm->Type;
            attrs = vm->AttributesValues;
        }
            break;
        case EMMessageBody::VIDEO:
        {
            auto im = static_cast<VideoMessageTO *>(mto);
            auto body = new EMVideoMessageBody(im->body.LocalPath, im->body.ThumbnaiLocationPath);

            body->setSecretKey(im->body.Secret);
            body->setFileLength(im->body.FileSize);
            body->setDownloadStatus(im->body.DownStatus);
            body->setDisplayName(im->body.DisplayName);
            body->setRemotePath(im->body.ThumbnaiRemotePath);
            
            body->setThumbnailSecretKey(im->body.ThumbnaiSecret);
            body->setThumbnailRemotePath(im->body.ThumbnaiRemotePath);
            body->setThumbnailDownloadStatus(im->body.DownStatus);
            //TODO: same with FileLength?
            body->setSize(im->body.FileSize);
            body->setDuration(im->body.Duration);
            
            //TODO: add ThumbnailDisplayName field later
            messageBody = EMMessageBodyPtr(body);
            from = im->From;
            to = im->To;
            msgId = im->MsgId;
            msgType = im->Type;
            attrs = im->AttributesValues;
        }
            break;
        case EMMessageBody::CUSTOM:
        {
            auto im = static_cast<CustomMessageTO *>(mto);
            auto body = new EMCustomMessageBody(im->body.CustomEvent);
            
            if(nullptr !=  im->body.CustomParams && strlen(im->body.CustomParams) > 0) {
                LOG("json string is:%s", im->body.CustomParams);
                
                nlohmann::json j = nlohmann::json::parse(im->body.CustomParams);
                easemob::EMCustomMessageBody::EMCustomExts ext;
                for(auto it=j.begin(); it!=j.end(); it++) {
                    std::string str = it.value().get<std::string>();
                    LOG("custom message ext str is: %s", str.c_str());
                    std::pair<std::string, std::string> kv{it.key().c_str(), str};
                    ext.push_back(kv);
                }
                if (ext.size() > 0)
                    body->setExts(ext);
            }
            else {
                LOG("Empty json string for CustomerParams.");
            }

            messageBody = EMMessageBodyPtr(body);
            from = im->From;
            to = im->To;
            msgId = im->MsgId;
            msgType = im->Type;
            attrs = im->AttributesValues;
        }
            break;
    }
    LOG("Message created: From->%s, To->%s.", from.c_str(), to.c_str());
    if(buildReceiveMsg) {
        EMMessagePtr messagePtr = EMMessage::createReceiveMessage(to, from, messageBody, msgType, msgId);
        SetMessageAttrs(messagePtr, attrs);
        return messagePtr;
    } else {
        EMMessagePtr messagePtr = EMMessage::createSendMessage(from, to, messageBody, msgType);
        messagePtr->setMsgId(msgId);
        SetMessageAttrs(messagePtr, attrs);
        return messagePtr;
    }

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
    name13:{type:strv, value:["str1", "str2", "str3"]},
    name14:{type:jstr, value:"a json string"},
    name15:{type:attr, value:{
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
void SetMessageAttr(EMMessagePtr msg, std::string& key, nlohmann::json& j)
{
    if(nullptr == msg || key.length() == 0 || j.is_null())
        return;

    std::string type = j.at("type").get<string>();
    std::string value = "";
    std::string attributeStr = j.dump();
    
    if(type.compare("attr") != 0 && type.compare("strv") != 0) {
        value = j.at("value").get<std::string>();
    }
    
    if(type.compare("b") == 0) {
#ifdef _WIN32
        if(_stricmp(value.c_str(), "false") == 0) {
#else
        if(strcasecmp(value.c_str(), "false") == 0) {
#endif
            LOG("Set type bool: value: false");
            msg->setAttribute(key, false);
        }
        else {
            LOG("Set type bool: value: true");
            msg->setAttribute(key, true);
        }
        
    } else if (type.compare("c") == 0) {
        //unsupported in emmessage
        
    } else if (type.compare("uc") == 0) {
        //unsupported in emmessage
        
    } else if (type.compare("s") == 0) {
        //unsupported in emmessage
        
    } else if (type.compare("us") == 0) {
        //unsupported in emmessage
        
    } else if (type.compare("i") == 0) {
        //int i = atoi(value.c_str());
        int i = convertFromString<int32_t>(value);
        msg->setAttribute(key, i);
        LOG("Set type: int32_t, value:%d ", i);
        
    } else if (type.compare("ui") == 0) {
        //uint32_t  ui = (uint32_t)strtoul(value.c_str(), nullptr, 10);
        uint32_t ui = convertFromString<uint32_t>(value);
        msg->setAttribute(key, ui);
        LOG("Set type: uint32_t, value:%u", ui);
        
    } else if (type.compare("l") == 0) {
        //int64_t l = atoll(value.c_str());
        int64_t l = convertFromString<int64_t>(value);
        msg->setAttribute(key, l);
        LOG("Set type: int64_t, value:%lld", l);
        
    } else if (type.compare("ul") == 0) {
        //unsupported in emmessage
        
    } else if (type.compare("f") == 0) {
        //float f = atof(value.c_str());
        float f = convertFromString<float>(value);
        msg->setAttribute(key, f);
        LOG("Set type: float, value:%f", f);
        
    } else if (type.compare("d") == 0) {
        //double d = atof(value.c_str());
        double d = convertFromString<double>(value);
        msg->setAttribute(key, d);
        LOG("Set type: double, value:%f", d);
        
    } else if (type.compare("str") == 0) {
        msg->setAttribute(key, value);
        LOG("Set type: string, value:%s", value.c_str());
        
    } else if (type.compare("strv") == 0) {
        // seems server is not support EMJsonString, so change to string
        msg->setAttribute(key, attributeStr);
        LOG("Set type: strv, value:%s", attributeStr.c_str());
        
    } else if (type.compare("jstr") == 0) {
        msg->setAttribute(key, value);
        LOG("Set type: EMJsonString, value:%s", attributeStr.c_str());
        
    } else if (type.compare("attr") == 0) {
        // seems server is not support EMJsonString, so change to string
        msg->setAttribute(key, attributeStr);
        LOG("Set type: attr, value:%s", attributeStr.c_str());
    }
    else {
        LOG("Error attribute type %s", type.c_str());
    }
}

void SetMessageAttrs(EMMessagePtr msg, std::string attrs)
{
    // Json: as least has { and } two characters
    if(nullptr == msg || attrs.length() <= 2) return;
    nlohmann::json j;
    try
    {
        j = nlohmann::json::parse(attrs);
    }
    catch(std::exception)
    {
        LOG("Parse json failed, skip SetMessageAttr, jstr: %s", attrs.c_str());
        return;
    }
    
    for(auto it=j.begin(); it!=j.end(); it++) {
        std::string key = it.key();
        SetMessageAttr(msg, key, it.value());
    }
}

std::string GetSpecialTypeAttrString(EMAttributeValuePtr attribute, std::string& value)
{
    std::string type = "str";
    if(nullptr == attribute) return type;
    
    nlohmann::json j;
    try
    {
        j = nlohmann::json::parse(attribute->value<EMJsonString>());
    }
    catch(std::exception)
    {
        // value is not json string, just return
        return type;
    }
    
    // JSON string contain a special attribute
    if (!j.at("type").is_null()) {
        type = j.at("type").get<std::string>();
        if (type.compare("strv") == 0 || type.compare("attr") == 0) {
            if (j.at("value").is_object() || j.at("value").is_array()) {
                value = j.at("value").dump();
            }
        }
    }
    return type;
}

nlohmann::json GetAttrJson(EMAttributeValuePtr attribute)
{
    nlohmann::json j;
    if (attribute->is<bool>()) {
        j["type"] = "b";
        if (attribute->value<bool>())
            j["value"] = "True";
        else
            j["value"] = "False";
        
    } else if (attribute->is<char>()) {
        j["type"] = "c";
        j["value"] = convert2String<char>(attribute->value<char>());
        
    } else if (attribute->is<unsigned char>()) {
        j["type"] = "uc";
        j["value"] = convert2String<unsigned char>(attribute->value<unsigned char>());
        
    } else if (attribute->is<short>()) {
        j["type"] = "s";
        j["value"] = convert2String<short>(attribute->value<short>());
        
    } else if (attribute->is<unsigned short>()) {
        j["type"] = "us";
        j["value"] = convert2String<unsigned short>(attribute->value<unsigned short>());
        
    } else if (attribute->is<int32_t>()) {
        j["type"] = "i";
        j["value"] = convert2String<int32_t>(attribute->value<int32_t>());
        
    } else if (attribute->is<uint32_t>()) {
        j["type"] = "ui";
        j["value"] = convert2String<uint32_t>(attribute->value<uint32_t>());
        
    } else if (attribute->is<int64_t>()) {
        j["type"] = "l";
        j["value"] = convert2String<int64_t>(attribute->value<int64_t>());
        
    } else if (attribute->is<uint64_t>()) {
        j["type"] = "ul";
        j["value"] = convert2String<uint64_t>(attribute->value<uint64_t>());
        
    } else if (attribute->is<float>()) {
        j["type"] = "f";
        j["value"] = convert2String<float>(attribute->value<float>());
        
    } else if (attribute->is<double>()) {
        j["type"] = "d";
        j["value"] = convert2String<double>(attribute->value<double>());
        
    } else if (attribute->is<std::string>()) {
        std::string value = attribute->value<std::string>();
        j["type"] = GetSpecialTypeAttrString(attribute, value);;
        j["value"] = value;
        
    } else if (attribute->is<std::vector<std::string>>()) {
        j["type"] = "strv";
        j["value"] = attribute->value<std::vector<std::string>>();
        
    } else if (attribute->is<EMJsonString>()) {
        j["type"] = "attr";
        j["value"] = attribute->value<EMJsonString>();
    } else {
        LOG("Error type in message attributes!");
    }
    return j;
}

std::string GetAttrsStringFromMessage(EMMessagePtr msg)
{
    if (nullptr == msg) return "";
    
    std::map<std::string, EMAttributeValuePtr> ext = msg->ext();
    if (ext.size() == 0) return "";
    
    nlohmann::json j;
    std::string key = "";
    for(auto it : ext) {
        key = it.first;
        EMAttributeValuePtr attribute = it.second;
        nlohmann::json child = GetAttrJson(attribute);
        if (!child.is_null()) {
            j[key] = child;
        }
    }
    return j.dump();
}

//default constructor
MessageTO::MessageTO() {
    //all fields set default zero value
}

//MessageTO
MessageTO::MessageTO(const EMMessagePtr &_message) {
    this->MsgId = _message->msgId().c_str();
    this->ConversationId = _message->conversationId().c_str();
    this->From = _message->from().c_str();
    this->To = _message->to().c_str();
    this->RecallBy = _message->recallBy().c_str();
    
    this->Type = _message->chatType();
    this->Direction = _message->msgDirection();
    this->Status = _message->status();
    this->HasDeliverAck = _message->isDeliverAcked();
    this->HasReadAck = _message->isReadAcked();
    this->LocalTime = _message->localTime();
    this->ServerTime = _message->timestamp();
    
    if (strlen(this->MsgId) == 0) this->MsgId =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->ConversationId) == 0) this->ConversationId =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->From) == 0) this->From =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->To) == 0) this->To =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->RecallBy) == 0) this->RecallBy =  const_cast<char*>(EMPTY_STR.c_str());
    
    char* p = nullptr;
    std::string str = GetAttrsStringFromMessage(_message);
    
    //LOG("Got attributes from others: %s", str.c_str());
    
    if (str.length() > 0) {
        p = new char[str.size() + 1];
        p[str.size()] = '\0';
        strncpy(p, str.c_str(), str.size());
    } else {
        // pass a space to AttributesValues
        // make sure PtrToStructure can be success
        p = new char[2];
        p[0] = ' ';
        p[1] = '\0';
    }
    this->AttributesValues = p;
}

//TextMessageTO
TextMessageTO::TextMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMTextMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Content = body->text().c_str();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.Content) == 0) this->body.Content = const_cast<char*>(EMPTY_STR.c_str());
}

//LocationMessageTO
LocationMessageTO::LocationMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMLocationMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Latitude = body->latitude();
    this->body.Longitude = body->longitude();
    this->body.Address = body->address().c_str();
    this->body.BuildingName = body->buildingName().c_str();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.Address) == 0) this->body.Address = const_cast<char*>(EMPTY_STR.c_str());
}

//CmdMessageTO
CmdMessageTO::CmdMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMCmdMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Action = body->action().c_str();
    this->body.DeliverOnlineOnly = body->isDeliverOnlineOnly();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.Action) == 0) this->body.Action = const_cast<char*>(EMPTY_STR.c_str());
}

//FileMessageTO
FileMessageTO::FileMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMFileMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    
    DISPLAY_NAME_STR = body->displayName();
    this->body.DisplayName = DISPLAY_NAME_STR.c_str();
    
    this->body.DownStatus = body->downloadStatus();
    this->body.FileSize = body->fileLength();
    this->body.LocalPath = body->localPath().c_str();
    this->body.RemotePath = body->remotePath().c_str();
    this->body.Secret = body->secretKey().c_str();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.DisplayName) == 0) this->body.DisplayName = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.LocalPath) == 0) this->body.LocalPath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.RemotePath) == 0) this->body.RemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.Secret) == 0) this->body.Secret = const_cast<char*>(EMPTY_STR.c_str());
}

//ImageMessageTO
ImageMessageTO::ImageMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMImageMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined

    DISPLAY_NAME_STR = body->displayName();
    this->body.DisplayName = DISPLAY_NAME_STR.c_str();
    
    this->body.DownStatus = body->downloadStatus();
    this->body.FileSize = body->fileLength();
    this->body.LocalPath = body->localPath().c_str();
    this->body.RemotePath = body->remotePath().c_str();
    this->body.Secret = body->secretKey().c_str();
    this->body.Height = body->size().mHeight;
    this->body.Width = body->size().mWidth;
    // TODO: set this->body.Original
    this->body.ThumbnaiDownStatus = body->thumbnailDownloadStatus();
    this->body.ThumbnaiSecret = body->thumbnailSecretKey().c_str();
    this->body.ThumbnaiRemotePath = body->thumbnailRemotePath().c_str();
    this->body.ThumbnailLocalPath = body->thumbnailLocalPath().c_str();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.DisplayName) == 0) this->body.DisplayName = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.LocalPath) == 0) this->body.LocalPath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.RemotePath) == 0) this->body.RemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.Secret) == 0) this->body.Secret = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnaiSecret) == 0) this->body.ThumbnaiSecret = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnaiRemotePath) == 0) this->body.ThumbnaiRemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnailLocalPath) == 0) this->body.ThumbnailLocalPath = const_cast<char*>(EMPTY_STR.c_str());
     
}

VoiceMessageTO::VoiceMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMVoiceMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    
    DISPLAY_NAME_STR = body->displayName();
    this->body.DisplayName = DISPLAY_NAME_STR.c_str();
    
    this->body.DownStatus = body->downloadStatus();
    this->body.FileSize = body->fileLength();
    this->body.LocalPath = body->localPath().c_str();
    this->body.RemotePath = body->remotePath().c_str();
    this->body.Secret = body->secretKey().c_str();
    this->body.Duration = body->duration();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.DisplayName) == 0) this->body.DisplayName = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.LocalPath) == 0) this->body.LocalPath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.RemotePath) == 0) this->body.RemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.Secret) == 0) this->body.Secret = const_cast<char*>(EMPTY_STR.c_str());
}

VideoMessageTO::VideoMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMVideoMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type();
    this->body.LocalPath = body->localPath().c_str();
    
    DISPLAY_NAME_STR = body->displayName();
    this->body.DisplayName = DISPLAY_NAME_STR.c_str();
    
    this->body.Secret = body->secretKey().c_str();
    this->body.RemotePath = body->remotePath().c_str();
    this->body.ThumbnaiLocationPath = body->thumbnailLocalPath().c_str();
    this->body.ThumbnaiRemotePath = body->thumbnailRemotePath().c_str();
    this->body.ThumbnaiSecret = body->thumbnailSecretKey().c_str();
    this->body.Height = body->size().mHeight;
    this->body.Width = body->size().mWidth;
    this->body.Duration = body->duration();
    this->body.FileSize = body->fileLength();
    this->body.DownStatus = body->downloadStatus();

    //Bug fix: User Empty_str to replace "", avoid error from PtrToStructure at c# side
    if (strlen(this->body.LocalPath) == 0) this->body.LocalPath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.DisplayName) == 0) this->body.DisplayName = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.Secret) == 0) this->body.Secret = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.RemotePath) == 0) this->body.RemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnaiLocationPath) == 0) this->body.ThumbnaiLocationPath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnaiRemotePath) == 0) this->body.ThumbnaiRemotePath = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->body.ThumbnaiSecret) == 0) this->body.ThumbnaiSecret = const_cast<char*>(EMPTY_STR.c_str());
}

CustomMessageTO::CustomMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMCustomMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type();
    this->body.CustomEvent = body->event().c_str();
    
    EMCustomMessageBody::EMCustomExts ext = body->exts();
    nlohmann::json j;
    for(size_t i=0; i<ext.size(); i++) {
        j[ext[i].first] = ext[i].second;
    }
    
    std::string str;
    if(ext.size() > 0){
        str = j.dump();
    }
    else {
        str = EMPTY_STR; // Make sure PtrToStructure can be successful.
    }

    // need to be freed in FreeResource function
    char* p = new char[str.size() + 1];
    p[str.size()] = '\0';
    strncpy(p, str.c_str(), str.size());
    this->body.CustomParams = p;
}

void MessageTO::FreeResource(MessageTO * mto)
{
    if(nullptr == mto)
        return;
    
    if (nullptr != mto->AttributesValues) {
        delete mto->AttributesValues;
    }
    
    switch(mto->BodyType) {
        case EMMessageBody::CUSTOM:
        {
            CustomMessageTO* cmto = (CustomMessageTO*)mto;
            if(nullptr != cmto->body.CustomParams)
                delete cmto->body.CustomParams;
        }
            break;
        default:
            return;
    }
}

MessageTO * MessageTO::FromEMMessage(const EMMessagePtr &_message)
{
    //TODO: assume that only 1 body in _message->bodies
    MessageTO * message;
    auto body = _message->bodies().front();
    switch(body->type()) {
        case EMMessageBody::TEXT:
        {
            message = new TextMessageTO(_message);
        }
            break;
        case EMMessageBody::LOCATION:
        {
            message = new LocationMessageTO(_message);
        }
            break;
        case EMMessageBody::COMMAND:
        {
            message = new CmdMessageTO(_message);
        }
            break;
        case EMMessageBody::FILE:
        {
            message = new FileMessageTO(_message);
        }
            break;
        case EMMessageBody::IMAGE:
        {
            message = new ImageMessageTO(_message);
        }
            break;
        case EMMessageBody::VOICE:
        {
            message = new VoiceMessageTO(_message);
        }
            break;
        case EMMessageBody::VIDEO:
        {
            message = new VideoMessageTO(_message);
        }
            break;
        case EMMessageBody::CUSTOM:
        {
            message = new CustomMessageTO(_message);
        }
            break;
        default:
            message = NULL;
    }    
    return message;
}

GroupOptions GroupOptions::FromMucSetting(EMMucSettingPtr setting) {
    GroupOptions go;
    go.Ext = setting->extension().c_str();
    go.MaxCount = setting->maxUserCount();
    go.InviteNeedConfirm = setting->inviteNeedConfirm();
    go.Style = setting->style();
    return go;
}

GroupTO::~GroupTO()
{
    if (MemberCount > 0) {
        for(int i=0; i<MemberCount; i++) {
            delete (char *)MemberList[i];
        }
    }
    delete MemberList;
    MemberList = NULL;
    
    
    if (AdminCount > 0) {
        for(int i=0; i<AdminCount; i++) {
            delete (char *)AdminList[i];
        }
    }
    delete AdminList;
    AdminList = NULL;
    
    if (BlockCount > 0) {
        for(int i=0; i<BlockCount; i++) {
            delete (char *)BlockList[i];
        }
    }
    delete BlockList;
    BlockList = NULL;
    
    if (MuteCount > 0) {
        for(int i=0; i<MuteCount; i++) {
            delete (char *)MuteList[i].Member;
        }
    }
    delete MuteList;
    MuteList = NULL;
}

GroupTO * GroupTO::FromEMGroup(EMGroupPtr &group)
{
    GroupTO *gto = new GroupTO();
    gto->GroupId = group->groupId().c_str();
    gto->Name = group->groupSubject().c_str();
    gto->Description = group->groupDescription().c_str();
    gto->Owner = group->groupOwner().c_str();
    gto->Announcement = group->groupAnnouncement().c_str();
    
    // empty string may cause error of illegal sequence byte of
    // PtrToStructure at c# side, here add " " to them.
    if (strlen(gto->Description) == 0) gto->Description = const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(gto->Announcement) == 0) gto->Announcement = const_cast<char*>(EMPTY_STR.c_str());
    
    gto->MemberCount = (int)group->groupMembers().size();
    gto->AdminCount = (int)group->groupAdmins().size();
    gto->BlockCount = (int)group->groupBans().size();
    gto->MuteCount = (int)group->groupMutes().size();
    
    int i = 0;
    // Branch:xxx <=0 is used to avoid illegal sequnence error from Marshal.PtrtoStructure
    if (gto->MemberCount <= 0) {
        gto->MemberList = new char *[1];
        gto->MemberList[0] = const_cast<char*>(EMPTY_STR.c_str());
    } else {
        gto->MemberList = new char *[gto->MemberCount];
        for(std::string member : group->groupMembers()) {
            gto->MemberList[i] = new char[member.size()+1];
            std::strncpy(gto->MemberList[i], member.c_str(), member.size()+1);
            i++;
        }
    }

    if (gto->AdminCount <= 0) {
        gto->AdminList = new char *[1];
        gto->AdminList[0] = const_cast<char*>(EMPTY_STR.c_str());
        
    } else {
        i=0;
        gto->AdminList = new char *[gto->AdminCount];
        for(std::string admin : group->groupAdmins()) {
            gto->AdminList[i] = new char[admin.size()+1];
            std::strncpy(gto->AdminList[i], admin.c_str(), admin.size()+1);
            i++;
        }
    }

    if (gto->BlockCount <= 0) {
        gto->BlockList = new char *[1];
        gto->BlockList[0] = const_cast<char*>(EMPTY_STR.c_str());
    } else {
        i=0;
        gto->BlockList = new char *[gto->BlockCount];
        for(std::string block : group->groupBans()) {
            gto->BlockList[i] = new char[block.size()+1];
            std::strncpy(gto->BlockList[i], block.c_str(), block.size()+1);
            i++;
        }
    }

    if (gto->MuteCount <= 0) {
        gto->MuteList = new Mute[1];
	    Mute mute;
	    mute.Member = EMPTY_STR.c_str();
	    mute.Duration = 1000;
	    gto->MuteList[0] = mute;
    } else {
        i=0;
        gto->MuteList = new Mute[gto->MuteCount];
	    Mute m;
        for(auto mute : group->groupMutes()) {
            char *memberStr = new char[mute.first.size()+1];
            std::strncpy(memberStr, mute.first.c_str(), mute.first.size()+1);
	        m.Member = memberStr;
	        m.Duration = mute.second;
	        gto->MuteList[i] = m;
            i++;
        }
    }
    
    if(group->groupSetting()) {
        LOG("groupSetting exist, groupid:%s, ext:%s, ext len:%d", gto->GroupId,
            group->groupSetting()->extension().c_str(), strlen(group->groupSetting()->extension().c_str()));
        gto->Options = GroupOptions::FromMucSetting(group->groupSetting());
        //FromMucSetting cannot set Ext address correctly(byte NOT alianed?), here set again!
        gto->Options.Ext = group->groupSetting()->extension().c_str();
        if(strlen(gto->Options.Ext) == 0) gto->Options.Ext = EMPTY_STR.c_str();
    } else {
        LOG("groupSetting is NOT exist, so not set group setting, groupid:%s", gto->GroupId);
    }

    gto->PermissionType = group->groupMemberType();
    gto->NoticeEnabled = group->isPushEnabled();
    gto->MessageBlocked = group->isMessageBlocked();
    gto->IsAllMemberMuted = group->isMucAllMembersMuted();
    
    return gto;
}

void GroupTO::LogInfo()
{
    LOG("---------------------------");
    LOG("GroupTO's info:");
    LOG("GroupId: %p %p %s, len:%d", &GroupId, GroupId, GroupId, strlen(GroupId));
    LOG("Name: %p %p %s, len:%d", &Name, Name, Name, strlen(Name));
    LOG("Description: %p %p %s, len:%d", &Description, Description, Description, strlen(Description));
    LOG("Owner: %p %p %s, len:%d", &Owner, Owner, Owner, strlen(Owner));
    LOG("Announcement: %p %p %s, len:%d", &Announcement, Announcement, Announcement, strlen(Announcement));
    
    LOG("MemberList address: %p %p", &MemberList, MemberList);
    for(int i=0; i<MemberCount; i++) {
        LOG("member %d: %s, len:%d", i, MemberList[i], strlen(MemberList[i]));
    }
    LOG("AdminList address: %p %p", &AdminList, AdminList);
    for(int i=0; i<AdminCount; i++) {
        LOG("admin %1: %s, len:%d", i, AdminList[i], strlen(AdminList[i]));
    }
    LOG("BlockList address: %p %p", &BlockList, BlockList);
    for(int i=0; i<BlockCount; i++) {
        LOG("blocker %1: %s, len:%d", i, BlockList[i], strlen(BlockList[i]));
    }
    LOG("MuteList address: %p %p", &MuteList, MuteList);
    for(int i=0; i<MuteCount; i++) {
        LOG("mute %1: %s, len:%d", i, MuteList[i].Member, strlen(MuteList[i].Member));
    }
    
    LOG("Options: %p, ext:%s, ext len: %d", &Options, Options.Ext, strlen(Options.Ext));
    
    LOG("MemberCount: %p %d", &MemberCount, MemberCount);
    LOG("AdminCount: %p %d", &AdminCount, AdminCount);
    LOG("BlockCount: %p %d", &BlockCount, BlockCount);
    LOG("MuteCount: %p %d", &MuteCount, MuteCount);
    
    LOG("PermissionType: %p %d", &PermissionType, PermissionType);
    
    LOG("NoticeEnabled: %p %d", &NoticeEnabled, NoticeEnabled);
    LOG("MessageBlocked: %p %d", &MessageBlocked, MessageBlocked);
    LOG("IsAllMemberMuted: %p %d", &IsAllMemberMuted, IsAllMemberMuted);
    LOG("---------------------------");
}

RoomTO * RoomTO::FromEMChatRoom(EMChatroomPtr &room)
{
    RoomTO *rto = new RoomTO();
    rto->RoomId = room->chatroomId().c_str();
    rto->Name = room->chatroomSubject().c_str();
    rto->Description = room->chatroomDescription().c_str();
    rto->Owner = room->owner().c_str();
    rto->Announcement = room->chatroomAnnouncement().c_str();
    
    rto->MemberCount = (int)room->chatroomMembers().size();
    rto->AdminCount = (int)room->chatroomAdmins().size();
    rto->BlockCount = (int)room->chatroomBans().size();
    rto->MuteCount = (int)room->chatroomMutes().size();
    
    int i = 0;
    rto->MemberList = new char *[rto->MemberCount];
    for(std::string member : room->chatroomMembers()) {
        rto->MemberList[i] = new char[member.size()+1];
        std::strcpy(rto->MemberList[i], member.c_str());
        i++;
    }
    i=0;
    rto->AdminList = new char *[rto->AdminCount];
    for(std::string admin : room->chatroomAdmins()) {
        rto->AdminList[i] = new char[admin.size()+1];
        std::strcpy(rto->AdminList[i], admin.c_str());
        i++;
    }
    i=0;
    rto->BlockList = new char *[rto->BlockCount];
    for(std::string block : room->chatroomBans()) {
        rto->BlockList[i] = new char[block.size()+1];
        std::strcpy(rto->BlockList[i], block.c_str());
        i++;
    }
    i=0;
    rto->MuteList = new Mute[rto->MuteCount];
    Mute m;
    for(auto mute : room->chatroomMutes()) {
        char *memberStr = new char[mute.first.size()+1];
        std::strcpy(memberStr, mute.first.c_str());
	    m.Member = memberStr;
	    m.Duration = mute.second;
	    rto->MuteList[i] = m;
        i++;
    }
    
    rto->PermissionType = room->chatroomMemberType();
    
    rto->MaxUsers = room->chatroomMemberMaxCount();
    
    rto->IsAllMemberMuted = room->isMucAllMembersMuted();
    
    return rto;
}

void RoomTO::LogInfo()
{
    LOG("RoomTO's info:");
    LOG("RoomId: %p %p %s", &RoomId, RoomId, RoomId);
    LOG("Name: %p %p %s", &Name, Name, Name);
    LOG("Description: %p %p %s", &Description, Description, Description);
    LOG("Owner: %p %p %s", &Owner, Owner, Owner);
    LOG("Announcement: %p %p %s", &Announcement, Announcement, Announcement);
    
    LOG("MemberList address: %p %p", &MemberList, MemberList);
    LOG("AdminList address: %p %p", &AdminList, AdminList);
    LOG("BlockList address: %p %p", &BlockList, BlockList);
    LOG("MuteList address: %p %p", &MuteList, MuteList);

    LOG("MemberCount: %p %d", &MemberCount, MemberCount);
    LOG("AdminCount: %p %d", &AdminCount, AdminCount);
    LOG("BlockCount: %p %d", &BlockCount, BlockCount);
    LOG("MuteCount: %p %d", &MuteCount, MuteCount);
    
    LOG("PermissionType: %p %d", &PermissionType, PermissionType);
    
    LOG("MaxUsers: %p %d", &MaxUsers, MaxUsers);
    
    LOG("IsAllMemberMuted: %p %d", &IsAllMemberMuted, IsAllMemberMuted);
}

GroupReadAckTO * GroupReadAckTO::FromGroupReadAck(EMGroupReadAckPtr&  groupReadAckPtr)
{
    GroupReadAckTO *groupReadAckTO = new GroupReadAckTO();
    groupReadAckTO->metaId = groupReadAckPtr->meta_id.c_str();
    groupReadAckTO->msgId = groupReadAckPtr->msgPtr->msgId().c_str();
    groupReadAckTO->from = groupReadAckPtr->from.c_str();
    groupReadAckTO->content = groupReadAckPtr->content.c_str();
    groupReadAckTO->count = groupReadAckPtr->count;
    groupReadAckTO->timestamp = groupReadAckPtr->timestamp;
    return groupReadAckTO;
}

ConversationTO * ConversationTO::FromEMConversation(EMConversationPtr&  conversationPtr)
{
    ConversationTO* conversationTO = new ConversationTO();
    conversationTO->ConverationId = conversationPtr->conversationId().c_str();
    conversationTO->type = conversationPtr->conversationType();
    conversationTO->ExtField = conversationPtr->extField().c_str();
    return conversationTO;
}

GroupSharedFileTO * GroupSharedFileTO::FromEMGroupSharedFile(const EMMucSharedFilePtr &sharedFile)
{
    GroupSharedFileTO* gsTO = new GroupSharedFileTO();
    //PtrToStructure cannot work if no end "\0"
    char* p = new char[strlen(sharedFile->fileName().c_str()) + 1];
    strncpy(p, sharedFile->fileName().c_str(), strlen(sharedFile->fileName().c_str()) + 1);
    gsTO->FileName = p;
            
    p = new char[strlen(sharedFile->fileId().c_str()) + 1];
    strncpy(p, sharedFile->fileId().c_str(), strlen(sharedFile->fileId().c_str()) + 1);
    gsTO->FileId = p;
            
    p = new char[strlen(sharedFile->fileOwner().c_str()) + 1];
    strncpy(p, sharedFile->fileOwner().c_str(), strlen(sharedFile->fileOwner().c_str()) + 1);
    gsTO->FileOwner = p;
    
    gsTO->CreateTime = sharedFile->create();
    gsTO->FileSize = sharedFile->fileSize();
    return gsTO;
}

void GroupSharedFileTO::DeleteGroupSharedFileTO(GroupSharedFileTO* gto)
{
    if(nullptr == gto) return;
    
    if(nullptr != gto->FileName) delete gto->FileName;
    if(nullptr != gto->FileId) delete gto->FileId;
    if(nullptr != gto->FileOwner) delete gto->FileOwner;
    
    delete gto;
}

PushConfigTO * PushConfigTO::FromEMPushConfig(EMPushConfigsPtr&  pushConfigPtr)
{
    PushConfigTO* pushConfigTO = new PushConfigTO();
    
    if(pushConfigPtr->getDisplayStatus() == easemob::EMPushConfigs::EMPushNoDisturbStatus::Close)
        pushConfigTO->NoDisturb = true;
    else
        pushConfigTO->NoDisturb = false;

    pushConfigTO->NoDisturbStartHour = pushConfigPtr->getNoDisturbingStartHour();
    pushConfigTO->NoDisturbEndHour = pushConfigPtr->getNoDisturbingEndHour();
    pushConfigTO->Style = pushConfigPtr->getDisplayStyle();
    LOG("Push config, starthour:%d, endhour:%d, style:%d", pushConfigTO->NoDisturbStartHour, pushConfigTO->NoDisturbEndHour, pushConfigTO->Style);
    return pushConfigTO;
}


std::map<std::string, UserInfo> UserInfo::FromResponse(std::string json, std::map<UserInfoType, std::string>& utypeMap)
{
    std::map<std::string, UserInfo> userinfoMap;
    if (json.length() <= 2) return userinfoMap;
    
    nlohmann::json j;
    try {
        j = nlohmann::json::parse(json);
    }
    catch(std::exception) {
        LOG("Failed to parse response from server to json.");
        return userinfoMap;
    }
    
    for(auto it=j.begin(); it!=j.end(); it++) {
        std::string user = it.key();
        nlohmann::json u = it.value();
        
        if (u.is_null() || u.empty() || !u.is_object()) continue;

        UserInfo ui;
        if (u.count(utypeMap[NICKNAME]) > 0 && u.at(utypeMap[NICKNAME]).is_string())
            ui.nickName = u.at(utypeMap[NICKNAME]).get<std::string>();
        else
            ui.nickName = "";
        
        if (u.count(utypeMap[AVATAR_URL]) > 0 && u.at(utypeMap[AVATAR_URL]).is_string())
            ui.avatarUrl = u.at(utypeMap[AVATAR_URL]).get<std::string>();
        else
            ui.avatarUrl = "";
        
        if (u.count(utypeMap[EMAIL]) > 0 && u.at(utypeMap[EMAIL]).is_string())
            ui.email = u.at(utypeMap[EMAIL]).get<std::string>();
        else
            ui.email = "";
        
        if (u.count(utypeMap[PHONE]) > 0 && u.at(utypeMap[PHONE]).is_string())
            ui.phoneNumber = u.at(utypeMap[PHONE]).get<std::string>();
        else
            ui.phoneNumber = "";
        
        if (u.count(utypeMap[SIGN]) > 0 && u.at(utypeMap[SIGN]).is_string())
            ui.signature = u.at(utypeMap[SIGN]).get<std::string>();
        else
            ui.signature = "";
        
        if (u.count(utypeMap[BIRTH]) > 0 && u.at(utypeMap[BIRTH]).is_string())
            ui.birth = u.at(utypeMap[BIRTH]).get<std::string>();
        else
            ui.birth = "";
        
        if (u.count(utypeMap[EXT]) > 0 && u.at(utypeMap[EXT]).is_string())
            ui.ext = u.at(utypeMap[EXT]).get<std::string>();
        else
            ui.ext = "";
        
        if (u.count(utypeMap[GENDER]) > 0 && u.at(utypeMap[GENDER]).is_string()) {
            std::string str = u.at(utypeMap[GENDER]).get<std::string>();
            ui.gender = std::stoi(str);
        }
        else
            ui.gender = 0;

        ui.userId = user;
        
        userinfoMap[user] = ui;
    }
    return userinfoMap;
}

std::map<std::string, UserInfoTO> UserInfo::Convert2TO(std::map<std::string, UserInfo>& userInfoMap)
{
    std::map<std::string, UserInfoTO> userinfoToMap;
    if (userInfoMap.size() == 0) return userinfoToMap;
    
    // DO not use "for(auto it : userInfoMap)" !!, since the "it" will be a copied value from userInfoMap
    // Not a poiter to userInfoMap!!
    for (auto it = userInfoMap.begin(); it != userInfoMap.end(); it++) {
        UserInfoTO uto;
        uto.nickName    = it->second.nickName.c_str();
        uto.avatarUrl   = it->second.avatarUrl.c_str();
        uto.email       = it->second.email.c_str();
        uto.phoneNumber = it->second.phoneNumber.c_str();
        uto.gender      = it->second.gender;
        uto.signature   = it->second.signature.c_str();
        uto.birth       = it->second.birth.c_str();
        uto.ext         = it->second.ext.c_str();
        uto.userId      = it->second.userId.c_str();
        userinfoToMap[it->first] = uto;
    }
    
    return userinfoToMap;
}
