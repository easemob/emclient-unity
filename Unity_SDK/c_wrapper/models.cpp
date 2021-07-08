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
}

//TextMessageTO
TextMessageTO::TextMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMTextMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Content = body->text().c_str();
}

//LocationMessageTO
LocationMessageTO::LocationMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMLocationMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Latitude = body->latitude();
    this->body.Longitude = body->longitude();
    this->body.Address = body->address().c_str();
}

//CmdMessageTO
CmdMessageTO::CmdMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMCmdMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Action = body->action().c_str();
    this->body.DeliverOnlineOnly = body->isDeliverOnlineOnly();
}

//FileMessageTO
FileMessageTO::FileMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMFileMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.DisplayName = body->displayName().c_str();
    this->body.DownStatus = body->downloadStatus();
    this->body.FileSize = body->fileLength();
    this->body.LocalPath = body->localPath().c_str();
    this->body.RemotePath = body->remotePath().c_str();
    this->body.Secret = body->secretKey().c_str();
}

//ImageMessageTO
ImageMessageTO::ImageMessageTO(const EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMImageMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.DisplayName = body->displayName().c_str();
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
        default:
            message = NULL;
    }    
    return message;
}

GroupOptions GroupOptions::FromMucSetting(EMMucSettingPtr setting) {
    auto extension = setting->extension();
    auto str = extension.c_str();
    GroupOptions options {.Ext=str, .MaxCount=setting->maxUserCount(), .InviteNeedConfirm = setting->inviteNeedConfirm(), .Style=setting->style()};
    return options;
}

GroupTO * GroupTO::FromEMGroup(EMGroupPtr &group)
{
    GroupTO *gto = new GroupTO();
    gto->GroupId = group->groupId().c_str();
    gto->Name = group->groupSubject().c_str();
    gto->Description = group->groupDescription().c_str();
    gto->Owner = group->groupOwner().c_str();
    gto->Annoumcement = group->groupAnnouncement().c_str();
    gto->MemberCount = (int)group->groupMembers().size();
    gto->AdminCount = (int)group->groupAdmins().size();
    gto->BlockCount = (int)group->groupBans().size();
    gto->MuteCount = (int)group->groupMutes().size();
    gto->Options = GroupOptions::FromMucSetting(group->groupSetting());
    gto->NoticeEnabled = group->isPushEnabled();
    gto->MessageBlocked = group->isMessageBlocked();
    gto->IsAllMemberMuted = group->isMucAllMembersMuted();
    gto->PermissionType = group->groupMemberType();
    int i = 0;
    for(std::string member : group->groupMembers()) {
        gto->MemberList[i] = member.c_str();
        i++;
    }
    i=0;
    for(std::string admin : group->groupAdmins()) {
        gto->AdminList[i] = admin.c_str();
        i++;
    }
    i=0;
    for(std::string block : group->groupBans()) {
        gto->BlockList[i] = block.c_str();
        i++;
    }
    i=0;
    for(auto mute : group->groupMutes()) {
        gto->MuteList[i] = Mute{.Member = mute.first.c_str(), .Duration=mute.second};
        i++;
    }
    return gto;
}
