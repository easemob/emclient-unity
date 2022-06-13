//
//  EMRecallMessageInfo.h
//  HyphenateChat
//
//  Created by zhangchong on 2022/1/20.
//  Copyright © 2022 easemob.com. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "EMChatMessage.h"


@interface EMRecallMessageInfo : NSObject

/*!
 *  \~chinese
 *  消息撤回者
 *
 *  \~english
 *  Users who recall messages
 */
@property (nonatomic, copy) NSString *recallBy;

/*!
 *  \~chinese
 *  撤回的消息
 *
 *  \~english
 *  A recall message
 */
@property (nonatomic, strong) EMChatMessage *recallMessage;

@end

