//
//  BridgeHeader.swift
//
//  Created by 杜洁鹏 on 2021/9/27.
//


public extension EMClient {

    static var share: EMClient {
        return EMClient.shared()
    }
}


public extension EMConversationType {
    static var chat:EMConversationType {
        return EMConversationTypeChat
    }

    static var groupChat:EMConversationType {
        return EMConversationTypeGroupChat
    }

    static var chatRoom:EMConversationType {
        return EMConversationTypeChatRoom
    }
}


public extension EMMessageBodyType {
    static var text:EMMessageBodyType {
        return EMMessageBodyTypeText
    }

    static var image:EMMessageBodyType {
        return EMMessageBodyTypeImage
    }

    static var video:EMMessageBodyType {
        return EMMessageBodyTypeVideo
    }
    
    static var location:EMMessageBodyType {
        return EMMessageBodyTypeLocation
    }
    
    static var voice:EMMessageBodyType {
        return EMMessageBodyTypeVoice
    }
    
    static var file:EMMessageBodyType {
        return EMMessageBodyTypeFile
    }
    
    static var cmd:EMMessageBodyType {
        return EMMessageBodyTypeCmd
    }
    
    static var custom:EMMessageBodyType {
        return EMMessageBodyTypeCustom
    }
    
}



public enum EMMessageBaseBody {
    case text(_ text: String)
    case image(_ localPath: String, _ displayName: String)
    case video(_ localPath: String, _ displayName: String)
    case location(_ latitude: Double, _ longitude: Double, _ address: NSString, _ buildingName: NSString)
    case voice(_ localPath: String, _ displayName: String)
    case file(_ localPath: String, _ displayName: String)
    case cmd(_ action: String)
    case custom(_ event: NSString, customExt:NSDictionary)
}


public extension EMChatMessage {

    var swiftBody:EMMessageBaseBody {
        let  tBody:EMMessageBody = self.body
        switch  self.body.type {
        case EMMessageBodyTypeText:
            let textBody = tBody as! EMTextMessageBody
            return .text(textBody.text)
        case EMMessageBodyTypeImage:
            let imageBody = tBody as! EMImageMessageBody
            return .image(imageBody.localPath, imageBody.displayName)
        case EMMessageBodyTypeVideo:
            let videoBody = tBody as! EMVideoMessageBody
            return .video(videoBody.localPath, videoBody.displayName)
        case EMMessageBodyTypeLocation:
            let locationBody = tBody as! EMLocationMessageBody
            return .location(locationBody.latitude, locationBody.longitude, locationBody.address! as NSString,locationBody.buildingName! as NSString)
        case EMMessageBodyTypeVoice:
            let voiceBody = tBody as! EMVoiceMessageBody
            return .voice(voiceBody.localPath, voiceBody.displayName)
        case EMMessageBodyTypeFile:
            let fileBody = tBody as! EMFileMessageBody
            return .file(fileBody.localPath, fileBody.displayName)
        case EMMessageBodyTypeCmd:
            let cmdBody = tBody as! EMCmdMessageBody
            return .cmd(cmdBody.action)
        case EMMessageBodyTypeCustom:
            let customBody = tBody as! EMCustomMessageBody
            return .custom(customBody.event! as NSString, customExt: customBody.customExt! as NSDictionary)
        default:
            let textBody = tBody as! EMTextMessageBody
            return .text(textBody.text)
        }
        
    }

    
    convenience init(conversationId: String, from: String, to: String,body: EMMessageBaseBody!, ext: [AnyHashable: Any]!) {

        var msgBody : EMMessageBody
        switch body {
        case .text(let msg):
            msgBody = EMTextMessageBody(text: msg)
            break
        case .image(let path, let displayName):
            msgBody = EMImageMessageBody(localPath: path, displayName: displayName)
            break
        case .video(let path, let displayName):
            msgBody = EMVideoMessageBody(localPath: path, displayName: displayName)
            break
        case .location(let latitude, let longitude, let address, let buildingName):
            msgBody = EMLocationMessageBody(latitude: latitude, longitude: longitude, address: address as String, buildingName: buildingName as String)
            break
        case .voice(let path, let displayName):
            msgBody = EMVoiceMessageBody(localPath: path, displayName: displayName)
            break
        case .file(let path, let displayName):
            msgBody = EMFileMessageBody(localPath: path, displayName: displayName)
            break
        case .cmd(let action):
            msgBody = EMCmdMessageBody(action: action)
            break
        case .custom(let event, let customExt):
            msgBody = EMCustomMessageBody(event: event as String, ext: customExt as? [AnyHashable : Any])
            break
        case .none:
            msgBody = EMTextMessageBody(text: "a")
            break
        }
        self.init(conversationID: conversationId, from: from, to: to, body: msgBody, ext: ext)
    }
}



