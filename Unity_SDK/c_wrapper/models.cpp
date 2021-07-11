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
    return GroupOptions {.Ext=setting->extension().c_str(), .MaxCount=setting->maxUserCount(), .InviteNeedConfirm = setting->inviteNeedConfirm(), .Style=setting->style()};
}

GroupTO * GroupTO::FromEMGroup(EMGroupPtr &group)
{
    GroupTO *gto = new GroupTO();
    gto->GroupId = group->groupId().c_str();
    gto->Name = group->groupSubject().c_str();
    gto->Description = group->groupDescription().c_str();
    gto->Owner = group->groupOwner().c_str();
    gto->Announcement = group->groupAnnouncement().c_str();
    
    gto->MemberCount = (int)group->groupMembers().size();
    gto->AdminCount = (int)group->groupAdmins().size();
    gto->BlockCount = (int)group->groupBans().size();
    gto->MuteCount = (int)group->groupMutes().size();
    
    int i = 0;
    gto->MemberList = new char *[gto->MemberCount];
    for(std::string member : group->groupMembers()) {
        gto->MemberList[i] = new char[member.size()+1];
        std::strcpy(gto->MemberList[i], member.c_str());
        i++;
    }
    i=0;
    gto->AdminList = new char *[gto->AdminCount];
    for(std::string admin : group->groupAdmins()) {
        gto->AdminList[i] = new char[admin.size()+1];
        std::strcpy(gto->AdminList[i], admin.c_str());
        i++;
    }
    i=0;
    gto->BlockList = new char *[gto->BlockCount];
    for(std::string block : group->groupBans()) {
        gto->BlockList[i] = new char[block.size()+1];
        std::strcpy(gto->BlockList[i], block.c_str());
        i++;
    }
    i=0;
    gto->MuteList = new Mute[gto->MuteCount];
    for(auto mute : group->groupMutes()) {
        char *memberStr = new char[mute.first.size()+1];
        std::strcpy(memberStr, mute.first.c_str());
        gto->MuteList[i] = Mute{.Member = memberStr, .Duration=mute.second};
        i++;
    }
    
    gto->Options = GroupOptions::FromMucSetting(group->groupSetting());
    
    gto->PermissionType = group->groupMemberType();
    
    gto->NoticeEnabled = group->isPushEnabled();
    gto->MessageBlocked = group->isMessageBlocked();
    gto->IsAllMemberMuted = group->isMucAllMembersMuted();
    
    return gto;
}

void GroupTO::LogInfo()
{
    LOG("GroupTO's info:");
    LOG("GroupId: %p %p %s", &GroupId, GroupId, GroupId);
    LOG("Name: %p %p %s", &Name, Name, Name);
    LOG("Description: %p %p %s", &Description, Description, Description);
    LOG("Owner: %p %p %s", &Owner, Owner, Owner);
    LOG("Announcement: %p %p %s", &Announcement, Announcement, Announcement);
    
    LOG("MemberList address: %p %p", &MemberList, MemberList);
    LOG("AdminList address: %p %p", &AdminList, AdminList);
    LOG("BlockList address: %p %p", &BlockList, BlockList);
    LOG("MuteList address: %p %p", &MuteList, MuteList);

    
    LOG("Options: %p", &Options);
    
    LOG("MemberCount: %p %d", &MemberCount, MemberCount);
    LOG("AdminCount: %p %d", &AdminCount, AdminCount);
    LOG("BlockCount: %p %d", &BlockCount, BlockCount);
    LOG("MuteCount: %p %d", &MuteCount, MuteCount);
    
    LOG("PermissionType: %p %d", &PermissionType, PermissionType);
    
    LOG("NoticeEnabled: %p %d", &NoticeEnabled, NoticeEnabled);
    LOG("MessageBlocked: %p %d", &MessageBlocked, MessageBlocked);
    LOG("IsAllMemberMuted: %p %d", &IsAllMemberMuted, IsAllMemberMuted);
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
    for(auto mute : room->chatroomMutes()) {
        char *memberStr = new char[mute.first.size()+1];
        std::strcpy(memberStr, mute.first.c_str());
        rto->MuteList[i] = Mute{.Member = memberStr, .Duration=mute.second};
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
