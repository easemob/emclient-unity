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
#include "emmessageencoder.h"
#include "json.hpp"
#include "tool.h"
#include "emjsonstring.h"

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

std::string EMPTY_STR = " ";
std::string DISPLAY_NAME_STR = " "; // used to save display name temprarily

EMMessagePtr BuildEMMessage(void *mto, EMMessageBody::EMMessageBodyType type, bool buildReceiveMsg)
{
    //compose message body
    std::string from, to, msgId;
    std::string attrs = "";
    EMMessage::EMChatType msgType = EMMessage::EMChatType::SINGLE;
    EMMessageBodyPtr messageBody;

    auto wraper_mto = static_cast<MessageTO*>(mto);
    bool isNeedGroupAck = wraper_mto->IsNeedGroupAck;
    bool isThread = wraper_mto->IsThread;

    switch(type) {
        case EMMessageBody::TEXT:
        {
            auto tm = static_cast<TextMessageTO *>(mto);
            LOG("Message id from MTO is: id:%s", tm->MsgId);
            //create message body

            //convert from Unicode to UTF8
            std::string content = GetUTF8FromUnicode(tm->body.Content);

            messageBody = EMMessageBodyPtr(new EMTextMessageBody(content));

            std::string target_langs = std::string(tm->body.TargetLanguages);
            std::vector<std::string> target_languages = JsonStringToVector(target_langs);
            EMTextMessageBody* tb = (EMTextMessageBody*)messageBody.get();
            if (target_languages.size() > 0)
                tb->setTargetLanguages(target_languages);

            from = tm->From;
            to = tm->To;
            msgId = tm->MsgId;
            msgType = tm->Type;

            if (nullptr != tm->AttributesValues)
            	attrs = GetUTF8FromUnicode(tm->AttributesValues);
        }
            break;
        case EMMessageBody::LOCATION:
        {
            auto lm = static_cast<LocationMessageTO *>(mto);
            std::string addr = GetUTF8FromUnicode(lm->body.Address);
            std::string building = GetUTF8FromUnicode(lm->body.BuildingName);
            messageBody = EMMessageBodyPtr(new EMLocationMessageBody(lm->body.Latitude, lm->body.Longitude, addr, building));
            from = lm->From;
            to = lm->To;
            msgId = lm->MsgId;
            msgType = lm->Type;

            if (nullptr != lm->AttributesValues)
            	attrs = GetUTF8FromUnicode(lm->AttributesValues);
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

            if (nullptr != cm->AttributesValues)
            	attrs = GetUTF8FromUnicode(cm->AttributesValues);
        }
            break;
        case EMMessageBody::FILE:
        {
            auto fm = static_cast<FileMessageTO *>(mto);
            auto body = new EMFileMessageBody(GetUTF8FromUnicode(fm->body.LocalPath));
            body->setDisplayName(GetUTF8FromUnicode(fm->body.DisplayName));
            body->setSecretKey(fm->body.Secret);
            body->setRemotePath(GetUTF8FromUnicode(fm->body.RemotePath));
            body->setFileLength(fm->body.FileSize);
            body->setDownloadStatus(fm->body.DownStatus);
            messageBody = EMMessageBodyPtr(body);
            from = fm->From;
            to = fm->To;
            msgId = fm->MsgId;
            msgType = fm->Type;

            if (nullptr != fm->AttributesValues)
            	attrs = GetUTF8FromUnicode(fm->AttributesValues);
        }
            break;
        case EMMessageBody::IMAGE:
        {
            auto im = static_cast<ImageMessageTO *>(mto);
            auto body = new EMImageMessageBody(GetUTF8FromUnicode(im->body.LocalPath), GetUTF8FromUnicode(im->body.ThumbnailLocalPath));
            body->setSecretKey(im->body.Secret);
            body->setFileLength(im->body.FileSize);
            body->setDownloadStatus(im->body.DownStatus);
            body->setDisplayName(GetUTF8FromUnicode(im->body.DisplayName));
            body->setRemotePath(im->body.ThumbnaiRemotePath);
            body->setThumbnailSecretKey(im->body.ThumbnaiSecret);
            body->setThumbnailRemotePath(GetUTF8FromUnicode(im->body.ThumbnaiRemotePath));
            body->setThumbnailDownloadStatus(im->body.ThumbnaiDownStatus);
            //TODO: add ThumbnailDisplayName field later
            messageBody = EMMessageBodyPtr(body);
            from = im->From;
            to = im->To;
            msgId = im->MsgId;
            msgType = im->Type;

            if (nullptr != im->AttributesValues)
            	attrs = GetUTF8FromUnicode(im->AttributesValues);
        }
            break;
        case EMMessageBody::VOICE:
        {
            auto vm = static_cast<VoiceMessageTO *>(mto);
            auto body = new EMVoiceMessageBody(GetUTF8FromUnicode(vm->body.LocalPath), vm->body.Duration);
            body->setDisplayName(GetUTF8FromUnicode(vm->body.DisplayName));
            body->setSecretKey(vm->body.Secret);
            body->setRemotePath(GetUTF8FromUnicode(vm->body.RemotePath));
            body->setFileLength(vm->body.FileSize);
            body->setDownloadStatus(vm->body.DownStatus);
            messageBody = EMMessageBodyPtr(body);
            from = vm->From;
            to = vm->To;
            msgId = vm->MsgId;
            msgType = vm->Type;

            if (nullptr != vm->AttributesValues)
            	attrs = GetUTF8FromUnicode(vm->AttributesValues);
        }
            break;
        case EMMessageBody::VIDEO:
        {
            auto vi = static_cast<VideoMessageTO *>(mto);
            auto body = new EMVideoMessageBody(GetUTF8FromUnicode(vi->body.LocalPath), GetUTF8FromUnicode(vi->body.ThumbnaiLocationPath));

            body->setSecretKey(vi->body.Secret);
            body->setFileLength(vi->body.FileSize);
            body->setDownloadStatus(vi->body.DownStatus);
            body->setDisplayName(GetUTF8FromUnicode(vi->body.DisplayName));
            body->setRemotePath(GetUTF8FromUnicode(vi->body.ThumbnaiRemotePath));
            
            body->setThumbnailSecretKey(vi->body.ThumbnaiSecret);
            body->setThumbnailRemotePath(GetUTF8FromUnicode(vi->body.ThumbnaiRemotePath));
            body->setThumbnailDownloadStatus(vi->body.DownStatus);
            //TODO: same with FileLength?
            body->setSize(vi->body.FileSize);
            body->setDuration(vi->body.Duration);
            
            //TODO: add ThumbnailDisplayName field later
            messageBody = EMMessageBodyPtr(body);

            from = vi->From;
            to = vi->To;
            msgId = vi->MsgId;
            msgType = vi->Type;

            if (nullptr != vi->AttributesValues)
            	attrs = GetUTF8FromUnicode(vi->AttributesValues);
        }
            break;
        case EMMessageBody::CUSTOM:
        {
            auto cm = static_cast<CustomMessageTO *>(mto);
            auto body = new EMCustomMessageBody(GetUTF8FromUnicode(cm->body.CustomEvent));
            
            easemob::EMCustomMessageBody::EMCustomExts exts = MessageTO::CustomExtsFromJson(GetUTF8FromUnicode(cm->body.CustomParams));

            if (exts.size() > 0)
                body->setExts(exts);

            messageBody = EMMessageBodyPtr(body);

            from = cm->From;
            to = cm->To;
            msgId = cm->MsgId;
            msgType = cm->Type;

            if (nullptr != cm->AttributesValues)
            	attrs = GetUTF8FromUnicode(cm->AttributesValues);
        }
            break;
    }
    LOG("Message created: From->%s, To->%s.", from.c_str(), to.c_str());
    if(buildReceiveMsg) {
        EMMessagePtr messagePtr = EMMessage::createReceiveMessage(from, to, messageBody, msgType, msgId);
        AttributesValueTO::SetMessageAttrs(messagePtr, attrs);
        messagePtr->setIsNeedGroupAck(isNeedGroupAck);
        messagePtr->setIsThread(isThread);
        return messagePtr;
    } else {
        EMMessagePtr messagePtr = EMMessage::createSendMessage(from, to, messageBody, msgType);
        messagePtr->setMsgId(msgId);
        messagePtr->setIsNeedGroupAck(isNeedGroupAck);
        AttributesValueTO::SetMessageAttrs(messagePtr, attrs);
        messagePtr->setIsThread(isThread);
        return messagePtr;
    }

}

void UpdateMessageTO(void *mto, EMMessagePtr msg, void* localMto)
{
    if(nullptr == msg || nullptr == localMto) {
        LOG("Invalid mto or localMto parameter");
        return;
    }
    
    vector<EMMessageBodyPtr> bodies = msg->bodies();
    if(bodies.size() == 0)
        return;
    
    EMMessageBodyPtr body = bodies[0];
    
    // general messageTO setting
    auto amto = static_cast<MessageTO *>(mto);
    amto->MsgId = msg->msgId().c_str();
    amto->ServerTime = msg->timestamp();
    amto->Status = msg->status();
    
    MessageTOLocal* lmto = (MessageTOLocal*)localMto;
    lmto->MsgId = amto->MsgId;
    lmto->BodyType = body->type();
    
    switch(body->type()) {
        case EMMessageBody::TEXT:
        {
            auto tm = static_cast<TextMessageTO *>(mto);
            EMTextMessageBodyPtr tbody = dynamic_pointer_cast<EMTextMessageBody>(body);
            
            if(tbody->getTargetLanguages().size() > 0) {
                std::vector<string> vec = tbody->getTargetLanguages();
                lmto->TargetLanguages = JsonStringFromVector(vec);
                tm->body.TargetLanguages = lmto->TargetLanguages.c_str();
            } else {
                tm->body.TargetLanguages = EMPTY_STR.c_str();
            }
            
            if(tbody->getTranslations().size() > 0) {
                std::map<std::string, std::string> map = tbody->getTranslations();
                lmto->Translations = JsonStringFromMap(map);
                tm->body.Translations = lmto->Translations.c_str();
            } else {
                tm->body.Translations = EMPTY_STR.c_str();
            }
        }
            break;
        case EMMessageBody::LOCATION:
        {
            //auto lm = static_cast<LocationMessageTO *>(mto);
        }
            break;
        case EMMessageBody::COMMAND:
        {
            //auto cm = static_cast<CmdMessageTO *>(mto);
        }
            break;
        case EMMessageBody::FILE:
        {
            auto fm = static_cast<FileMessageTO *>(mto);
            EMFileMessageBodyPtr fmptr = std::dynamic_pointer_cast<EMFileMessageBody>(body);
            
            fm->body.LocalPath = fmptr->localPath().c_str();
            fm->body.DisplayName = fmptr->displayName().c_str();
            fm->body.Secret = fmptr->secretKey().c_str();
            fm->body.RemotePath = fmptr->remotePath().c_str();
            fm->body.FileSize = fmptr->fileLength();
            fm->body.DownStatus = fmptr->downloadStatus();
            
            if (strlen(fm->body.LocalPath) == 0) fm->body.LocalPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(fm->body.DisplayName) == 0) fm->body.DisplayName =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(fm->body.Secret) == 0) fm->body.Secret =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(fm->body.RemotePath) == 0) fm->body.RemotePath =  const_cast<char*>(EMPTY_STR.c_str());
        }
            break;
        case EMMessageBody::IMAGE:
        {
            auto im = static_cast<ImageMessageTO *>(mto);
            EMImageMessageBodyPtr imptr = std::dynamic_pointer_cast<EMImageMessageBody>(body);
            
            im->body.LocalPath = imptr->localPath().c_str();
            im->body.DisplayName = imptr->displayName().c_str();
            im->body.Secret = imptr->secretKey().c_str();
            im->body.RemotePath = imptr->remotePath().c_str();
            
            im->body.ThumbnailLocalPath	= imptr->thumbnailLocalPath().c_str();
            im->body.ThumbnaiRemotePath = imptr->thumbnailRemotePath().c_str();
            im->body.ThumbnaiSecret = imptr->thumbnailSecretKey().c_str();
            
            im->body.FileSize = imptr->fileLength();
            im->body.DownStatus = imptr->downloadStatus();
            im->body.ThumbnaiDownStatus = imptr->thumbnailDownloadStatus();
            
            if (strlen(im->body.LocalPath) == 0) im->body.LocalPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.DisplayName) == 0) im->body.DisplayName =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.Secret) == 0) im->body.Secret =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.RemotePath) == 0) im->body.RemotePath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.ThumbnailLocalPath) == 0) im->body.ThumbnailLocalPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.ThumbnaiRemotePath) == 0) im->body.ThumbnaiRemotePath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(im->body.ThumbnaiSecret) == 0) im->body.ThumbnaiSecret =  const_cast<char*>(EMPTY_STR.c_str());
        }
            break;
        case EMMessageBody::VOICE:
        {
            auto vm = static_cast<VoiceMessageTO *>(mto);
            
            EMVoiceMessageBodyPtr vomptr = std::dynamic_pointer_cast<EMVoiceMessageBody>(body);
            
            vm->body.LocalPath = vomptr->localPath().c_str();
            vm->body.DisplayName = vomptr->displayName().c_str();
            vm->body.Secret = vomptr->secretKey().c_str();
            vm->body.RemotePath = vomptr->remotePath().c_str();
            
            vm->body.FileSize = vomptr->fileLength();
            vm->body.DownStatus = vomptr->downloadStatus();
            vm->body.Duration = vomptr->duration();
            
            if (strlen(vm->body.LocalPath) == 0) vm->body.LocalPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.DisplayName) == 0) vm->body.DisplayName =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.Secret) == 0) vm->body.Secret =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.RemotePath) == 0) vm->body.RemotePath =  const_cast<char*>(EMPTY_STR.c_str());

        }
            break;
        case EMMessageBody::VIDEO:
        {
            auto vm = static_cast<VideoMessageTO *>(mto);
            EMVideoMessageBodyPtr vimptr = std::dynamic_pointer_cast<EMVideoMessageBody>(body);
            
            vm->body.LocalPath = vimptr->localPath().c_str();
            vm->body.DisplayName = vimptr->displayName().c_str();
            vm->body.Secret = vimptr->secretKey().c_str();
            vm->body.RemotePath = vimptr->remotePath().c_str();
            
            vm->body.ThumbnaiLocationPath = vimptr->thumbnailLocalPath().c_str();
            vm->body.ThumbnaiRemotePath = vimptr->thumbnailRemotePath().c_str();
            vm->body.ThumbnaiSecret = vimptr->thumbnailLocalPath().c_str();
            
            vm->body.FileSize = vimptr->fileLength();
            vm->body.DownStatus = vimptr->downloadStatus();
            vm->body.Duration = vimptr->duration();
            
            if (strlen(vm->body.LocalPath) == 0) vm->body.LocalPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.DisplayName) == 0) vm->body.DisplayName =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.Secret) == 0) vm->body.Secret =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.RemotePath) == 0) vm->body.RemotePath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.ThumbnaiLocationPath) == 0) vm->body.ThumbnaiLocationPath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.ThumbnaiRemotePath) == 0) vm->body.ThumbnaiRemotePath =  const_cast<char*>(EMPTY_STR.c_str());
            if (strlen(vm->body.ThumbnaiSecret) == 0) vm->body.ThumbnaiSecret =  const_cast<char*>(EMPTY_STR.c_str());
        }
            break;
        case EMMessageBody::CUSTOM:
        {
            //auto im = static_cast<CustomMessageTO *>(mto);
        }
            break;
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
    
    this->Type = _message->chatType();
    this->Direction = _message->msgDirection();
    this->Status = _message->status();
    this->HasDeliverAck = _message->isDeliverAcked();
    this->HasReadAck = _message->isReadAcked();
    this->LocalTime = _message->localTime();
    this->ServerTime = _message->timestamp();
    this->IsNeedGroupAck = _message->isNeedGroupAck();
    this->IsRead = _message->isRead();
    this->MessageOnlineState = _message->messageOnlineState();
    
    if (strlen(this->MsgId) == 0) this->MsgId =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->ConversationId) == 0) this->ConversationId =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->From) == 0) this->From =  const_cast<char*>(EMPTY_STR.c_str());
    if (strlen(this->To) == 0) this->To =  const_cast<char*>(EMPTY_STR.c_str());

    if (strlen(_message->recallBy().c_str()) == 0) 
        this->RecallBy =  const_cast<char*>(EMPTY_STR.c_str());
    else
        this->RecallBy = GetPointer(_message->recallBy().c_str());
    
    std::string str = AttributesValueTO::ToJson(_message);
    
    this->AttributesValues = GetPointer(str.c_str());
    if (nullptr == this->AttributesValues || strlen(this->AttributesValues) == 0) this->AttributesValues = const_cast<char*>(EMPTY_STR.c_str());

    this->IsThread = _message->isThread();
}

//TextMessageTO
TextMessageTO::TextMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMTextMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Content = body->text().c_str();
    
    std::string targetLanguages;
    if(body->getTargetLanguages().size() > 0) {
        std::vector<string> vec = body->getTargetLanguages();
        targetLanguages = JsonStringFromVector(vec);
        
    } else {
        targetLanguages = EMPTY_STR;
    }
    char* p = new char[targetLanguages.size() + 1];
    p[targetLanguages.size()] = '\0';
    strncpy(p, targetLanguages.c_str(), targetLanguages.size());
    this->body.TargetLanguages = p;
    
    std::string translations;
    if(body->getTranslations().size() > 0) {
        std::map<std::string, std::string> map = body->getTranslations();
        translations = JsonStringFromMap(map);
    } else {
        translations = EMPTY_STR;
    }
    p = new char[translations.size() + 1];
    p[translations.size()] = '\0';
    strncpy(p, translations.c_str(), translations.size());
    this->body.Translations = p;

    if (strlen(p) > 3)
        LOG("Translations is %s", p);

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
    if (strlen(this->body.BuildingName) == 0) this->body.BuildingName = const_cast<char*>(EMPTY_STR.c_str());
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

    EMCustomMessageBody::EMCustomExts exts = body->exts();
    std::string json = MessageTO::CustomExtsToJson(exts);
    
    std::string str;
    if(json.length() > 0){
        str = json;
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
    
    if (nullptr != mto->AttributesValues && mto->AttributesValues != EMPTY_STR.c_str())
        delete mto->AttributesValues;

    if (nullptr != mto->RecallBy && mto->RecallBy != EMPTY_STR.c_str())
        delete mto->RecallBy;

    switch(mto->BodyType) {
        case EMMessageBody::TEXT:
        {
            TextMessageTO* tmto = (TextMessageTO*)mto;
            if(nullptr != tmto->body.TargetLanguages)
                delete tmto->body.TargetLanguages;
            if(nullptr != tmto->body.Translations)
                delete tmto->body.Translations;
        }
            break;
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

int MessageTO::MsgTypeToInt(EMMessage::EMChatType type)
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

EMMessage::EMChatType MessageTO::MsgTypeFromInt(int i)
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

int MessageTO::StatusToInt(EMMessage::EMMessageStatus status)
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

EMMessage::EMMessageStatus MessageTO::StatusFromInt(int i)
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

std::string MessageTO::MessageDirectionToString(EMMessage::EMMessageDirection direction)
{
    std::string str = "send";

    if (EMMessage::EMMessageDirection::RECEIVE == direction)
        str = "recv";

    return str;
}

EMMessage::EMMessageDirection MessageTO::MessageDirectionFromString(std::string str)
{
    EMMessage::EMMessageDirection direction = EMMessage::EMMessageDirection::SEND;

    if (str.compare("recv") == 0)
        direction = EMMessage::EMMessageDirection::RECEIVE;

    return direction;
}

std::string MessageTO::BodyTypeToString(EMMessageBody::EMMessageBodyType btype)
{
    std::string stype = "txt";
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
    case EMMessageBody::CUSTOM :
        stype = "custom";
        break;
    default:
        break;
    }
    return stype;
}

EMMessageBody::EMMessageBodyType MessageTO::BodyTypeFromString(std::string str)
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

int MessageTO::DownLoadStatusToInt(EMFileMessageBody::EMDownloadStatus download_status)
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

EMFileMessageBody::EMDownloadStatus MessageTO::DownLoadStatusFromInt(int i)
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

std::string MessageTO::CustomExtsToJson(EMCustomMessageBody::EMCustomExts& exts)
{
    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartObject();
    for (auto it : exts) {
        writer.Key(it.first.c_str());
        writer.String(it.second.c_str());
    }
    writer.EndObject();

    std::string data = s.GetString();
    return data;
}

EMCustomMessageBody::EMCustomExts MessageTO::CustomExtsFromJson(std::string json)
{
    EMCustomMessageBody::EMCustomExts exts;

    Document d;
    d.Parse(json.c_str());
    if (d.HasParseError()) return exts;

    for (auto iter = d.MemberBegin(); iter != d.MemberEnd(); ++iter) {

        std::string key = iter->name.GetString();
        std::string value = iter->value.GetString();

        std::pair<std::string, std::string> kv{ key, value };
        exts.push_back(kv);
    }

    return exts;
}

void MessageTO::BodyToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
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
            std::vector<std::string> vec = ptr->getTargetLanguages();
            writer.String(JsonStringFromVector(vec).c_str());

            writer.Key("translations");
            std::map<std::string, std::string> map = ptr->getTranslations();
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

std::string MessageTO::BodyToJson(EMMessagePtr msg)
{
    StringBuffer s;
    Writer<StringBuffer> writer(s);
    BodyToJsonWriter(writer, msg);
    std::string data = s.GetString();
    return data;
}

void MessageTO::ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
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
        writer.String(AttributesValueTO::ToJson(msg).c_str());

        writer.Key("localTime");
        writer.String(std::to_string(msg->localTime()).c_str());

        writer.Key("serverTime");
        writer.String(std::to_string(msg->timestamp()).c_str());

        writer.Key("isThread");
        writer.Bool(msg->isThread());

        writer.Key("bodyType");
        writer.String(BodyTypeToString(msg->bodies().front()->type()).c_str());

        writer.Key("body");
        writer.String(BodyToJson(msg).c_str());
    }
    writer.EndObject();
}

std::string MessageTO::ToJson(EMMessagePtr msg)
{
    if (nullptr == msg) return std::string();

    StringBuffer s;
    Writer<StringBuffer> writer(s);

    ToJsonWriter(writer, msg);
    return s.GetString();
}

EMMessageBodyPtr MessageTO::BodyFromJsonObject(const Value& jnode)
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
                std::string str = body["content"].GetString();
                ptr->setText(str);
            }

            if (body.HasMember("translations") && body["translations"].IsString()) {
                std::string str = body["translations"].GetString();
                std::vector<std::string> tagert_languages = JsonStringToVector(str);
                ptr->setTargetLanguages(tagert_languages);
            }

            if (body.HasMember("targetLanguages") && body["targetLanguages"].IsString()) {
                std::string str = body["targetLanguages"].GetString();
                std::map<std::string, std::string> translations = JsonStringToMap(str);
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
                std::string str = body["address"].GetString();
                ptr->setAddress(str);
            }

            if (body.HasMember("buildingName") && body["buildingName"].IsString()) {
                std::string str = body["buildingName"].GetString();
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
                std::string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                std::string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                std::string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                std::string str = body["secret"].GetString();
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
                std::string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                std::string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                std::string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                std::string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            if (body.HasMember("thumbnailLocalPath") && body["thumbnailLocalPath"].IsString()) {
                std::string str = body["thumbnailLocalPath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailRemotePath") && body["thumbnailRemotePath"].IsString()) {
                std::string str = body["thumbnailRemotePath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailSecret") && body["thumbnailSecret"].IsString()) {
                std::string str = body["thumbnailSecret"].GetString();
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
                std::string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                std::string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                std::string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                std::string str = body["secret"].GetString();
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
                std::string str = body["localPath"].GetString();
                ptr->setLocalPath(str);
            }

            //TODO: need to check this.
            if (body.HasMember("fileSize") && body["fileSize"].IsInt64()) {
                int64_t fz = body["fileSize"].GetInt64();
                ptr->setFileLength(fz);
            }

            if (body.HasMember("displayName") && body["displayName"].IsString()) {
                std::string str = body["displayName"].GetString();
                ptr->setDisplayName(str);
            }

            if (body.HasMember("remotePath") && body["remotePath"].IsString()) {
                std::string str = body["remotePath"].GetString();
                ptr->setRemotePath(str);
            }

            if (body.HasMember("secret") && body["secret"].IsString()) {
                std::string str = body["secret"].GetString();
                ptr->setSecretKey(str);
            }

            if (body.HasMember("fileStatus") && body["fileStatus"].IsInt()) {
                int i = body["fileStatus"].GetInt();
                ptr->setDownloadStatus(DownLoadStatusFromInt(i));
            }

            if (body.HasMember("thumbnailLocalPath") && body["thumbnailLocalPath"].IsString()) {
                std::string str = body["thumbnailLocalPath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailRemotePath") && body["thumbnailRemotePath"].IsString()) {
                std::string str = body["thumbnailRemotePath"].GetString();
                ptr->setThumbnailRemotePath(str);
            }

            if (body.HasMember("thumbnailSecret") && body["thumbnailSecret"].IsString()) {
                std::string str = body["thumbnailSecret"].GetString();
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
            std::string event = "";
            if (body.HasMember("event") && body["event"].IsString()) {
                event = body["event"].GetString();
            }

            EMCustomMessageBodyPtr ptr = EMCustomMessageBodyPtr(new EMCustomMessageBody(event));

            if (body.HasMember("params") && body["params"].IsString()) {
                std::string str = body["params"].GetString();
                EMCustomMessageBody::EMCustomExts exts = MessageTO::CustomExtsFromJson(str);
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

EMMessageBodyPtr MessageTO::BodyFromJson(std::string json)
{
    Document d;
    d.Parse(json.c_str());
    if (d.HasParseError()) return nullptr;

    return BodyFromJsonObject(d);
}

EMMessagePtr MessageTO::FromJsonObject(const Value& jnode)
{
    std::string from = "";
    if (jnode.HasMember("from") && jnode["from"].IsString()) {
        from = jnode["from"].GetString();
    }

    std::string to = "";
    if (jnode.HasMember("to") && jnode["to"].IsString()) {
        to = jnode["to"].GetString();
    }

    EMMessage::EMMessageDirection direction = EMMessage::EMMessageDirection::SEND;
    if (jnode.HasMember("direction") && jnode["direction"].IsString()) {
        std::string str = jnode["direction"].GetString();
        direction = MessageDirectionFromString(str);
    }

    EMMessage::EMChatType msg_type = EMMessage::EMChatType::SINGLE;
    if (jnode.HasMember("chatType") && jnode["chatType"].IsInt()) {
        int i = jnode["chatType"].GetInt();
        msg_type = MsgTypeFromInt(i);
    }

    std::string msg_id = "";
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
        std::string str = jnode["conversationId"].GetString();
        msg->setConversationId(str);
    }

    if (jnode.HasMember("recallBy") && jnode["recallBy"].IsString()) {
        std::string str = jnode["recallBy"].GetString();
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
        SetMessageAttrs(msg, jnode["attributes"].GetString());

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

EMMessagePtr MessageTO::FromJson(std::string json)
{
    Document d;
    d.Parse(json.c_str());
    if (d.HasParseError()) return nullptr;

    return FromJsonObject(d);
}

MessageTO * MessageTO::FromEMMessage(const EMMessagePtr &_message)
{
    //TODO: assume that only 1 body in _message->bodies
    MessageTO * message;
    auto body = _message->bodies().front();
    switch(body->type()) {
        case EMMessageBody::TEXT:
            message = new TextMessageTO(_message);
            break;
        case EMMessageBody::LOCATION:
            message = new LocationMessageTO(_message);
            break;
        case EMMessageBody::COMMAND:
            message = new CmdMessageTO(_message);
            break;
        case EMMessageBody::FILE:
            message = new FileMessageTO(_message);
            break;
        case EMMessageBody::IMAGE:
            message = new ImageMessageTO(_message);
            break;
        case EMMessageBody::VOICE:
            message = new VoiceMessageTO(_message);
            break;
        case EMMessageBody::VIDEO:
            message = new VideoMessageTO(_message);
            break;
        case EMMessageBody::CUSTOM:
            message = new CustomMessageTO(_message);
            break;
        default:
            message = NULL;
    }
    return message;
}

GroupOptions GroupOptions::FromMucSetting(EMMucSettingPtr setting) {
    GroupOptions go;

    go.Ext = nullptr;

    go.Ext = GetPointer(setting->extension().c_str());
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

    if (nullptr != Options.Ext && Options.Ext != EMPTY_STR.c_str())
        delete Options.Ext;
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
        gto->Options = GroupOptions::FromMucSetting(group->groupSetting());

        if(nullptr == gto->Options.Ext || strlen(gto->Options.Ext) == 0) gto->Options.Ext = EMPTY_STR.c_str();

        LOG("groupSetting exist, groupid:%s, ext:%s, ext len:%d", gto->GroupId,
            gto->Options.Ext, strlen(gto->Options.Ext));

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
    
    rto->MemberCount = room->chatroomMemberCount();

    rto->MemberListCount = (int)room->chatroomMembers().size();
    rto->AdminCount = (int)room->chatroomAdmins().size();
    rto->BlockCount = (int)room->chatroomBans().size();
    rto->MuteCount = (int)room->chatroomMutes().size();
    
    int i = 0;
    if (rto->MemberListCount <= 0) {
        rto->MemberList = new char* [1];
        rto->MemberList[0] = const_cast<char*>(EMPTY_STR.c_str());
    }
    else {
        rto->MemberList = new char* [rto->MemberCount];
        for (std::string member : room->chatroomMembers()) {
            rto->MemberList[i] = new char[member.size() + 1];
            std::strcpy(rto->MemberList[i], member.c_str());
            i++;
        }
    }
    
    if (rto->AdminCount <= 0) {
        rto->AdminList = new char* [1];
        rto->AdminList[0] = const_cast<char*>(EMPTY_STR.c_str());
    }
    else {
        i = 0;
        rto->AdminList = new char* [rto->AdminCount];
        for (std::string admin : room->chatroomAdmins()) {
            rto->AdminList[i] = new char[admin.size() + 1];
            std::strcpy(rto->AdminList[i], admin.c_str());
            i++;
        }
    }

    if (rto->BlockCount <= 0) {
        rto->BlockList = new char* [1];
        rto->BlockList[0] = const_cast<char*>(EMPTY_STR.c_str());
    }
    else
    {
        i = 0;
        rto->BlockList = new char* [rto->BlockCount];
        for (std::string block : room->chatroomBans()) {
            rto->BlockList[i] = new char[block.size() + 1];
            std::strcpy(rto->BlockList[i], block.c_str());
            i++;
        }
    }

    if (rto->MuteCount <= 0) {
        rto->MuteList = new Mute[1];
        Mute mute;
        mute.Member = EMPTY_STR.c_str();
        mute.Duration = 1000;
        rto->MuteList[0] = mute;
    }
    else {
        i = 0;
        rto->MuteList = new Mute[rto->MuteCount];
        Mute m;
        for (auto mute : room->chatroomMutes()) {
            char* memberStr = new char[mute.first.size() + 1];
            std::strcpy(memberStr, mute.first.c_str());
            m.Member = memberStr;
            m.Duration = mute.second;
            rto->MuteList[i] = m;
            i++;
        }
    }
    
    rto->PermissionType = room->chatroomMemberType();
    
    rto->MaxUsers = room->chatroomMemberMaxCount();
    
    rto->IsAllMemberMuted = room->isMucAllMembersMuted();
    
    return rto;
}

RoomTO::~RoomTO()
{
    if (MemberListCount > 0) {
        for (int i = 0; i < MemberListCount; i++) {
            delete (char*)MemberList[i];
        }
    }
    delete MemberList;
    MemberList = NULL;


    if (AdminCount > 0) {
        for (int i = 0; i < AdminCount; i++) {
            delete (char*)AdminList[i];
        }
    }
    delete AdminList;
    AdminList = NULL;

    if (BlockCount > 0) {
        for (int i = 0; i < BlockCount; i++) {
            delete (char*)BlockList[i];
        }
    }
    delete BlockList;
    BlockList = NULL;

    if (MuteCount > 0) {
        for (int i = 0; i < MuteCount; i++) {
            delete (char*)MuteList[i].Member;
        }
    }
    delete MuteList;
    MuteList = NULL;
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

    LOG("MemberListCount: %p %d", &MemberListCount, MemberListCount);
    LOG("MemberCount: %p %d", &MemberCount, MemberCount);
    LOG("AdminCount: %p %d", &AdminCount, AdminCount);
    LOG("BlockCount: %p %d", &BlockCount, BlockCount);
    LOG("MuteCount: %p %d", &MuteCount, MuteCount);
    
    LOG("PermissionType: %p %d", &PermissionType, PermissionType);
    
    LOG("MaxUsers: %p %d", &MaxUsers, MaxUsers);
    
    LOG("IsAllMemberMuted: %p %d", &IsAllMemberMuted, IsAllMemberMuted);
}

GroupReadAckTO * GroupReadAckTO::FromGroupReadAck(const EMGroupReadAckPtr&  groupReadAckPtr)
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
        pushConfigTO->NoDisturb = false;
    else
        pushConfigTO->NoDisturb = true;

    pushConfigTO->NoDisturbStartHour = pushConfigPtr->getNoDisturbingStartHour();
    pushConfigTO->NoDisturbEndHour = pushConfigPtr->getNoDisturbingEndHour();
    pushConfigTO->Style = pushConfigPtr->getDisplayStyle();
    LOG("Push config, starthour:%d, endhour:%d, style:%d", pushConfigTO->NoDisturbStartHour, pushConfigTO->NoDisturbEndHour, pushConfigTO->Style);
    return pushConfigTO;
}

//refer to 批量获取用户属性 in: https://docs-im.easemob.com/ccim/rest/userprofile 
std::map<std::string, UserInfo> UserInfo::FromResponse(std::string json, std::map<UserInfoType, std::string>& utypeMap)
{
    std::map<std::string, UserInfo> userinfoMap;
    if (json.length() <= 2) return userinfoMap;
    
    Document d;
    if (!d.Parse(json.data()).HasParseError()) {

        for (auto iter = d.MemberBegin(); iter != d.MemberEnd(); ++iter) {

            auto key = iter->name.GetString();
            const Value& objItem = iter->value;

            if (objItem.IsObject()) {

                UserInfo ui;

                if (objItem.HasMember(utypeMap[NICKNAME].c_str()) && objItem[utypeMap[NICKNAME].c_str()].IsString())
                    ui.nickName = objItem[utypeMap[NICKNAME].c_str()].GetString();

                if (objItem.HasMember(utypeMap[AVATAR_URL].c_str()) && objItem[utypeMap[AVATAR_URL].c_str()].IsString())
                    ui.avatarUrl = objItem[utypeMap[AVATAR_URL].c_str()].GetString();

                if (objItem.HasMember(utypeMap[EMAIL].c_str()) && objItem[utypeMap[EMAIL].c_str()].IsString())
                    ui.email = objItem[utypeMap[EMAIL].c_str()].GetString();

                if (objItem.HasMember(utypeMap[PHONE].c_str()) && objItem[utypeMap[PHONE].c_str()].IsString())
                    ui.phoneNumber = objItem[utypeMap[PHONE].c_str()].GetString();

                if (objItem.HasMember(utypeMap[SIGN].c_str()) && objItem[utypeMap[SIGN].c_str()].IsString())
                    ui.signature = objItem[utypeMap[SIGN].c_str()].GetString();

                if (objItem.HasMember(utypeMap[BIRTH].c_str()) && objItem[utypeMap[BIRTH].c_str()].IsString())
                    ui.birth = objItem[utypeMap[BIRTH].c_str()].GetString();

                if (objItem.HasMember(utypeMap[EXT].c_str()) && objItem[utypeMap[EXT].c_str()].IsString())
                    ui.ext = objItem[utypeMap[EXT].c_str()].GetString();

                if (objItem.HasMember(utypeMap[GENDER].c_str()) && objItem[utypeMap[GENDER].c_str()].IsString()) {
                    std::string gender_str = objItem[utypeMap[GENDER].c_str()].GetString();
                    if (gender_str.compare("0") == 0 || gender_str.compare("1") == 0 || gender_str.compare("2") == 0)
                        ui.gender = std::stoi(gender_str);
                }
                
                ui.userId = key;
                userinfoMap[key] = ui;
            }
        }
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

void PresenceTOWrapper::FromLocalWrapper()
{
    presenceTO.publisher = publisher.c_str();

    presenceTO.deviceList = deviceListJson.c_str();
    presenceTO.statusList = statusListJson.c_str();

    presenceTO.ext = ext.c_str();

    presenceTO.latestTime = latestTime;
    presenceTO.expiryTime = expiryTime;
}

 PresenceTOWrapper PresenceTOWrapper::FromPresence(EMPresencePtr presencePtr)
{
    PresenceTOWrapper ptoWrapper;
    ptoWrapper.publisher.clear();
    ptoWrapper.deviceListJson.clear();
    ptoWrapper.statusListJson.clear();
    ptoWrapper.ext.clear();
    
    ptoWrapper.publisher = presencePtr->getPublisher();
    
    // go through status list
    std::set<std::pair<std::string,int>> devices = presencePtr->getStatusList();
    if(devices.size() == 0) {
        ptoWrapper.deviceListJson = EMPTY_STR;
        ptoWrapper.statusListJson = EMPTY_STR;
    } else {
        std::map<std::string, std::string> deviceMap;
        std::map<std::string, std::string> statusMap;
        int index = 0;
        for(auto it = devices.begin(); it != devices.end(); it++) {
            deviceMap[std::to_string(index)] = it->first;
            statusMap[std::to_string(index)] = std::to_string(it->second);
            index++;
        }
        // change set to map, change map to json
        ptoWrapper.deviceListJson = JsonStringFromMap(deviceMap);
        ptoWrapper.statusListJson = JsonStringFromMap(statusMap);
    }
    
    if(presencePtr->getExt().length() == 0) {
        ptoWrapper.ext = EMPTY_STR;
    } else {
        ptoWrapper.ext = presencePtr->getExt();
    }
    ptoWrapper.latestTime = presencePtr->getLatestTime();
    ptoWrapper.expiryTime = presencePtr->getExpiryTime();
    
    return ptoWrapper;
}

 void MessageReactionChangeTO::ToJsonWriter(Writer<StringBuffer>& writer, EMMessageReactionChangePtr reactionChangePtr, std::string curname)
 {
     std::string covId = reactionChangePtr->to();
     if (covId.compare(curname) == 0)
         covId = reactionChangePtr->from();

     writer.StartObject();
     {
         writer.Key("conversationId");
         writer.String(covId.c_str());
         writer.Key("messageId");
         writer.String(reactionChangePtr->messageId().c_str());
         writer.Key("reactionList");
         writer.String(MessageReactionTO::ToJson(reactionChangePtr->reactionList()).c_str());
     }
     writer.EndObject();
 }

 std::string MessageReactionChangeTO::ToJson(EMMessageReactionChangePtr reactionChangePtr, std::string curname)
 {
     if (nullptr == reactionChangePtr) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);
     ToJsonWriter(writer, reactionChangePtr, curname);
     return s.GetString();
 }

 std::string MessageReactionChangeTO::ToJson(EMMessageReactionChangeList list, std::string curname)
 {
     if (list.size() == 0) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);
     writer.StartArray();
     for (auto it : list) 
     {
         ToJsonWriter(writer, it, curname);
     }
     writer.EndArray();
     return s.GetString();
 }


 std::string MessageReactionTO::ToJson(EMMessageReactionPtr reaction)
 {
     if (nullptr == reaction) return "";

     StringBuffer s;
     Writer<StringBuffer> writer(s);
     EMMessageEncoder::encodeReactionToJsonWriter(writer, reaction);
     return s.GetString();
 }

 std::string MessageReactionTO::ToJson(EMMessageReactionList list)
 {
     return EMMessageEncoder::encodeReactionToJson(list);
 }

 std::string MessageReactionTO::ToJson(EMMessage& msg)
 {
     return EMMessageEncoder::encodeReactionToJson(msg);
 }

 std::string MessageReactionTO::ToJson(std::map<std::string, EMMessageReactionList> map)
 {
     if (map.size() == 0) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);
     writer.StartObject();
     for (auto it : map) {
         writer.Key(it.first.c_str());
         ListToJsonWriter(writer, it.second);
     }
     writer.EndObject();
     return s.GetString();
 }

 void MessageReactionTO::ListToJsonWriter(Writer<StringBuffer>& writer, EMMessageReactionList list)
 {
     if (list.size() == 0) return;

     writer.StartArray();
     for (EMMessageReactionPtr reaction : list) {
         EMMessageEncoder::encodeReactionToJsonWriter(writer, reaction);
     }
     writer.EndArray();
 }

 EMMessageReactionPtr MessageReactionTO::FromJsonObject(const Value& jnode)
 {
     return EMMessageEncoder::decodeReactionFromJson(jnode);
 }

 EMMessageReactionList MessageReactionTO::ListFromJsonObject(const Value& jnode)
 {
     return EMMessageEncoder::decodeReactionListFromJson(jnode);
 }

 EMMessageReactionList MessageReactionTO::ListFromJson(std::string json)
 {
     return EMMessageEncoder::decodeReactionListFromJson(json);
 }

 EMSilentModeParamPtr SilentModeParamTO::FromJson(std::string json)
 {
     Document d;
     d.Parse(json.c_str());

     if (d.HasParseError() || !d.IsObject()) {
         LOG("SilentModeParamTO::FromJson failed, json:%s", json.c_str());
         return nullptr;
     }
     else {
         EMSilentModeParamPtr ptr = std::shared_ptr<EMSilentModeParam>(new easemob::EMSilentModeParam());

         if (d.HasMember("paramType") && d["paramType"].IsInt())
             ptr->mParamType = (EMPushConfigs::EMSilentModeParamType)(d["paramType"].GetInt());

         if (d.HasMember("duration") && d["duration"].IsInt())
             ptr->mSilentModeDuration = d["duration"].GetInt();

         if (d.HasMember("type") && d["type"].IsInt())
             ptr->mRemindType = (EMPushConfigs::EMPushRemindType)(d["type"].GetInt());

         if (d.HasMember("startHour") && d["startHour"].IsInt()) {
             if (nullptr == ptr->mSilentModeStartTime) {
                 ptr->mSilentModeStartTime = EMSilentModeTimePtr(new EMSilentModeTime());
             }
             ptr->mSilentModeStartTime->hours = d["startHour"].GetInt();
         }

         if (d.HasMember("startMin") && d["startMin"].IsInt()) {
             if (nullptr == ptr->mSilentModeStartTime) {
                 ptr->mSilentModeStartTime = EMSilentModeTimePtr(new EMSilentModeTime());
             }
             ptr->mSilentModeStartTime->minutes = d["startMin"].GetInt();
         }

         if (d.HasMember("endHour") && d["endHour"].IsInt()) {
             if (nullptr == ptr->mSilentModeEndTime) {
                 ptr->mSilentModeEndTime = EMSilentModeTimePtr(new EMSilentModeTime());
             }
             ptr->mSilentModeEndTime->hours = d["endHour"].GetInt();
         }

         if (d.HasMember("endMin") && d["endMin"].IsInt()) {
             if (nullptr == ptr->mSilentModeEndTime) {
                 ptr->mSilentModeEndTime = EMSilentModeTimePtr(new EMSilentModeTime());
             }
             ptr->mSilentModeEndTime->minutes = d["endMin"].GetInt();
         }

         return ptr;
     }
 }

 std::string SilentModeItemTO::ToJson(EMSilentModeItemPtr itemPtr)
 {
     if (nullptr == itemPtr) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);

     ToJsonWriter(writer, itemPtr);
     return s.GetString();
 }

 std::string SilentModeItemTO::ToJson(std::map<std::string, EMSilentModeItemPtr> map)
 {
     if (map.size() == 0) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);

     writer.StartObject();
     for (auto it : map) {
         writer.Key(it.first.c_str());
         ToJsonWriter(writer, it.second);
     }
     writer.EndObject();
     return s.GetString();
 }

 void SilentModeItemTO::ToJsonWriter(Writer<StringBuffer>& writer, EMSilentModeItemPtr itemPtr)
 {
     writer.StartObject();
     {
         writer.Key("expireTS");
         writer.Int64(itemPtr->mExpireTimestamp);

         writer.Key("type");
         writer.Int(itemPtr->mRemindType);

         writer.Key("startHour");
         writer.Int(itemPtr->mSilentModeStartTime->hours);

         writer.Key("startMin");
         writer.Int(itemPtr->mSilentModeStartTime->minutes);

         writer.Key("endHour");
         writer.Int(itemPtr->mSilentModeEndTime->hours);

         writer.Key("endMin");
         writer.Int(itemPtr->mSilentModeEndTime->minutes);

         writer.Key("conversationId");
         writer.String(itemPtr->mConversationID.c_str());

         writer.Key("conversationType");
         writer.Int(itemPtr->mConversationType);
     }
     writer.EndObject();
 }

 void AttributesValueTO::ToJsonWriter(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute)
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
     else if (attribute->is<std::string>()) {
         writer.Key("type");
         writer.String("str");
         writer.Key("value");
         writer.String(attribute->value<std::string>().c_str());
         //Note: here not support to parse str value further
     }
     else if (attribute->is<std::vector<std::string>>()) {
         writer.Key("type");
         writer.String("strv");
         writer.Key("value");
         std::vector<std::string> vec = attribute->value<std::vector<std::string>>();
         writer.String(JsonStringFromVector(vec).c_str());
     }
     else if (attribute->is<EMJsonString>()) {
         writer.Key("type");
         writer.String("jstr");
         writer.Key("value");
         writer.String(attribute->value<EMJsonString>().c_str());
     }
     else {
         LOG("Error type in message attributes!");
     }
     writer.EndObject();
 }

 void AttributesValueTO::ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg)
 {
     if (nullptr == msg) return;

     std::map<std::string, EMAttributeValuePtr> ext = msg->ext();
     if (ext.size() == 0) return;

     for (auto it : ext) {
         std::string key = it.first;
         EMAttributeValuePtr attribute = it.second;
         writer.Key(key.c_str());
         ToJsonWriter(writer, attribute);
     }
 }

 std::string AttributesValueTO::ToJson(EMMessagePtr msg)
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
    name13:{type:strv, value:["str1", "str2", "str3"]},
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
 void AttributesValueTO::SetMessageAttr(EMMessagePtr msg, std::string& key, const Value& jnode)
 {
     if (nullptr == msg || key.length() == 0 || jnode.IsNull())
         return;

     if (!jnode.HasMember("type") || !jnode["type"].IsString()) return;
     std::string type = jnode["type"].GetString();
     
     if (!jnode.HasMember("value")) return;

     std::string v = "";
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
             LOG("Set type bool: value: false");
             msg->setAttribute(key, false);
         }
         else {
             LOG("Set type bool: value: true");
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
         LOG("Set type: int32_t, value:%d ", i);
     }
     else if (type.compare("ui") == 0) {
         uint32_t ui = convertFromString<uint32_t>(v);
         msg->setAttribute(key, ui);
         LOG("Set type: uint32_t, value:%u", ui);
     }
     else if (type.compare("l") == 0) {
         int64_t l = convertFromString<int64_t>(v);
         msg->setAttribute(key, l);
         LOG("Set type: int64_t, value:%lld", l);
     }
     else if (type.compare("ul") == 0) {
         //unsupported in emmessage
     }
     else if (type.compare("f") == 0) {
         float f = convertFromString<float>(v);
         msg->setAttribute(key, f);
         LOG("Set type: float, value:%f", f);
     }
     else if (type.compare("d") == 0) {
         double d = convertFromString<double>(v);
         msg->setAttribute(key, d);
         LOG("Set type: double, value:%f", d);
     }
     else if (type.compare("str") == 0) {
         msg->setAttribute(key, v);
         LOG("Set type: string, value:%s", v.c_str());
     }
     else if (type.compare("strv") == 0) {
         EMJsonString json(v);
         msg->setAttribute(key, json);
         LOG("Set type: strv, value:%s", json.c_str());
     }
     else if (type.compare("jstr") == 0) {
         EMJsonString json(v);
         msg->setAttribute(key, json);
         LOG("Set type: EMJsonString, value:%s", json.c_str());
     }
     /*else if (type.compare("attr") == 0) {
         EMJsonString json(value.GetString());
         msg->setAttribute(key, json);
         LOG("Set type: attr, value:%s", attributeStr.c_str());
     }*/
     else {
         LOG("Error attribute type %s", type.c_str());
     }
 }

 void AttributesValueTO::SetMessageAttrs(EMMessagePtr msg, const Value & jnode)
 {
     if (nullptr == msg || jnode.IsNull()) return;

     for (auto iter = jnode.MemberBegin(); iter != jnode.MemberEnd(); ++iter) {

         std::string key = iter->name.GetString();
         const Value& obj = iter->value;

         if (obj.IsObject()) {
             SetMessageAttr(msg, key, obj);
         }
     }
 }

 void AttributesValueTO::SetMessageAttrs(EMMessagePtr msg, std::string json)
 {
     // Json: as least has { and } two characters
     if (nullptr == msg) return;

     Document d;
     if (!d.Parse(json.data()).HasParseError()) {

         SetMessageAttrs(msg, d);
     }
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
         ChatThread::ToJsonWriter(writer, threadEventPtr);

     }
     writer.EndObject();

     std::string data = s.GetString();
     return data;
 }

 void ChatThread::ToJsonWriter(Writer<StringBuffer>& writer, EMThreadEventPtr threadEventPtr)
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
             writer.String(MessageTO::ToJson(threadEventPtr->lastMessage()).c_str());
         }
     }
     writer.EndObject();
 }

 std::string ChatThread::ToJson(EMThreadEventPtr threadEventPtr)
 {
     if (nullptr == threadEventPtr) return std::string();

     StringBuffer s;
     Writer<StringBuffer> writer(s);
     ToJsonWriter(writer, threadEventPtr);
     std::string data = s.GetString();
     return data;
 }

 std::string ChatThread::ToJson(EMCursorResultRaw<EMThreadEventPtr> cusorResult)
 {
     StringBuffer s;
     Writer<StringBuffer> writer(s);

     writer.StartObject();
     {
         writer.Key("cursor");
         writer.String(cusorResult.nextPageCursor().c_str());

         writer.Key("list");
         std::vector<EMThreadEventPtr> vec = cusorResult.result();
         writer.String(ToJson(vec).c_str());
     }
     writer.EndObject();
     
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
         ToJsonWriter(writer, it);
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
         writer.String(MessageTO::ToJson(it.second).c_str());
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

     if (jnode.HasMember("lastMessage") && jnode["lastMessage"].IsString()) {
         EMMessagePtr msg = MessageTO::FromJson(jnode["lastMessage"].GetString());
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

