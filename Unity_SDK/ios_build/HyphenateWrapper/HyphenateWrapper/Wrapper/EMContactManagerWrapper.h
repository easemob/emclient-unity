//
//  EMContactManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMContactManagerWrapper : EMWrapper
- (void)acceptInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)addContact:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)addUserToBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)declineInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)deleteContact:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getAllContactsFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getAllContactsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)removeUserFromBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getSelfIdsOnOtherPlatform:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
