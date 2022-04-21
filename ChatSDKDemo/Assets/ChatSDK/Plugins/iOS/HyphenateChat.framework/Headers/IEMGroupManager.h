/**
 *  \~chinese
 *  @header IEMGroupManager.h
 *  @abstract 群组相关操作类。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header IEMGroupManager.h
 *  @abstract This protocol defines the group operations.
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMCommonDefs.h"
#import "EMGroupManagerDelegate.h"
#import "EMGroup.h"
#import "EMGroupOptions.h"
#import "EMCursorResult.h"
#import "EMGroupSharedFile.h"

/**
 *  \~chinese
 *  群组相关操作类。
 *
 *  \~english
 *  The group operations.
 */
@protocol IEMGroupManager <NSObject>

@required

#pragma mark - Delegate

/**
 *  \~chinese
 *  添加回调代理。
 *
 *  @param aDelegate  要添加的代理。
 *  @param aQueue     代理执行的队列，如果是空值，则在主线程。
 *
 *  \~english
 *  Adds delegate.
 *
 *  @param aDelegate  The delegate to be added.
 *  @param aQueue     (optional) The queue of calling delegate methods. Pass in nil to run on main thread.
 */
- (void)addDelegate:(id<EMGroupManagerDelegate>)aDelegate
      delegateQueue:(dispatch_queue_t)aQueue;

/**
 *  \~chinese
 *  移除回调代理。
 *
 *  @param aDelegate  要移除的代理。
 *
 *  \~english
 *  Removes delegate.
 *
 *  @param aDelegate  The delegate to be removed.
 */
- (void)removeDelegate:(id)aDelegate;


#pragma mark - Get Group

/**
 *  \~chinese
 *  获取用户所有群组。
 *
 *  @result  群组列表。
 *
 *  \~english
 *  Gets all groups.
 *
 *  @result  The group list.
 *
 */
- (NSArray *)getJoinedGroups;

/**
 *  \~chinese
 *  从内存中获取屏蔽了推送的群组 ID 列表。
 *
 *  @param pError  错误信息。
 *
 *  @result     群组 ID 列表。

 *  \~english
 *  Gets the list of groups which have disabled Apple Push Notification Service.
 *
 *  @param pError    The error information if the method fails: Error.
 *
 *  @result   The group ID list.
 */
- (NSArray *)getGroupsWithoutPushNotification:(EMError **)pError EM_DEPRECATED_IOS(3_3_2, 3_8_3, "Use -IEMPushManager::noPushGroups");


#pragma mark - Get group from server

/**
 *  \~chinese
 *  按数目从服务器获取自己加入的群组。
 *  
 *  这里需要注意的是：
 *  - 每次调用只返回一页的数据。首次调用传空值，会从最新的第一条开始取；
 *  - aPageSize 是这次接口调用期望返回的列表数据个数，如当前在最后一页，返回的数据会是 count < aPageSize；
 *  - 列表页码 aPageNum 是方便服务器分页查询返回，对于数据量未知且很大的情况，分页获取，服务器会根据每次的页数和每次的pagesize 返回数据，直到返回所有数据。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aPageNum  获取自己加入群的游标，首次调用传空。
 *  @param aPageSize 期望返回结果的数量, 如果小于 0 则一次返回所有结果。
 *  @param pError    出错信息。
 *
 *  @result 群组列表。
 *
 *  \~english
 *  Gets pagesize number group you joined from the server.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aPageNum        Gets the cursor to join the group. Sets the parameter as nil for the first time.
 *  @param aPageSize       The number of results expected to be returned. If <0 then all results will be returned at once
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result  The Group list. 
 */
- (NSArray *)getJoinedGroupsFromServerWithPage:(NSInteger)aPageNum
                                      pageSize:(NSInteger)aPageSize
                                         error:(EMError **)pError;

/**
 *  \~chinese
 *  按数目从服务器获取自己加入的群组。
 * 
 *  异步方法。
 *
 *  @param aPageNum  获取自己加入群的游标，首次调用传空。
 *  @param aPageSize 期望返回结果的数量, 如果小于 0 则一次返回所有结果。
 *  @param aCompletionBlock      该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Gets the pagesize number group you joined from the server.
 * 
 *  This is an asynchronous method.
 *
 *  @param aPageNum            The cursor when joins the group. Sets the parameter as nil for the first time.
 *  @param aPageSize           The number of results expected to be returned, if <0 then all results will be returned at once.
 *  @param aCompletionBlock    The completion block, which contains the error message if the method fails.
 *
 */

- (void)getJoinedGroupsFromServerWithPage:(NSInteger)aPageNum
                                 pageSize:(NSInteger)aPageSize
                               completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  从服务器获取指定范围内的公开群。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aCursor   获取公开群的游标，首次调用传空。
 *  @param aPageSize 期望返回结果的数量, 如果小于 0 则一次返回所有结果。
 *  @param pError    出错信息。
 *
 *  @result   获取的公开群结果。
 *
 *  \~english
 *  Gets the public groups with the specified range from the server.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aCursor   The cursor to join the group. Sets the parameter as nil for the first time.
 *  @param aPageSize The number of results expected to be returned. If the number is less than 0 then all results will be returned at once.
 *  @param pError    The error information if the method fails: Error.
 *
 *  @result     The result. 
 */
- (EMCursorResult *)getPublicGroupsFromServerWithCursor:(NSString *)aCursor
                                               pageSize:(NSInteger)aPageSize
                                                  error:(EMError **)pError;

/**
 *  \~chinese
 *  从服务器获取指定范围内的公开群。
 * 
 *  异步方法。
 *
 *  @param aCursor          获取公开群的游标，首次调用传空。
 *  @param aPageSize        期望返回结果的数量, 如果小于 0 则一次返回所有结果。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets public groups with the specified range from the server.
 * 
 *  This is an asynchronous method.
 *
 *  @param aCursor          Gets the cursor to join the group. Sets the parameter as nil for the first time.
 *  @param aPageSize        The number of results expected to be returned. If the number is less than 0 then all results will be returned at once.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getPublicGroupsFromServerWithCursor:(NSString *)aCursor
                                   pageSize:(NSInteger)aPageSize
                                 completion:(void (^)(EMCursorResult *aResult, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  根据群组 ID 搜索公开群。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroundId   群组 ID。
 *  @param pError      错误信息。
 *
 *  @result   搜索到的群组。
 *
 *  \~english
 *  Searches a public group with the group ID.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroundId   The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result   The groups searched.
 */
- (EMGroup *)searchPublicGroupWithId:(NSString *)aGroundId
                               error:(EMError **)pError;

/**
 *  \~chinese
 *  根据群组 ID 搜索公开群。
 *
 *  @param aGroundId        群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Searches public group with group ID.
 *
 *  @param aGroundId        The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)searchPublicGroupWithId:(NSString *)aGroundId
                     completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

#pragma mark - Create

/**
 *  \~chinese
 *  创建群组。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aSubject        群组名称。
 *  @param aDescription    群组描述。
 *  @param aInvitees       群组成员，不包括创建者自己。
 *  @param aMessage        加入群组的邀请消息。
 *  @param aSetting        群组属性。
 *  @param pError          出错信息。
 *
 *  @result        群组实例。
 *
 *  \~english
 *  Creates a group.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aSubject        The subject of the group.
 *  @param aDescription    The description of the group.
 *  @param aInvitees       The members of the group. Do not include the creator.
 *  @param aMessage        The invitation message.
 *  @param aSetting        The group options.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result   The group instance. 
 */
- (EMGroup *)createGroupWithSubject:(NSString *)aSubject
                        description:(NSString *)aDescription
                           invitees:(NSArray *)aInvitees
                            message:(NSString *)aMessage
                            setting:(EMGroupOptions *)aSetting
                              error:(EMError **)pError;

/**
 *  \~chinese
 *  创建群组。
 * 
 *  异步方法。
 *
 *  @param aSubject         群组名称。
 *  @param aDescription     群组描述。
 *  @param aInvitees        群组成员，不包括创建者自己。
 *  @param aMessage         加入群组的邀请消息。
 *  @param aSetting         群组属性。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Creates a group.
 * 
 *  This is an asynchronous method.
 *
 *  @param aSubject         The subject of the group.
 *  @param aDescription     The description of the group.
 *  @param aInvitees        The members of the group. Do not include the creator.
 *  @param aMessage         The invitation message.
 *  @param aSetting         The group options.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)createGroupWithSubject:(NSString *)aSubject
                   description:(NSString *)aDescription
                      invitees:(NSArray *)aInvitees
                       message:(NSString *)aMessage
                       setting:(EMGroupOptions *)aSetting
                    completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

#pragma mark - Fetch Info

/**
 *  \~chinese
 *  获取群组详情，包含群组 ID, 群组名称，群组描述，群组基本属性，群主，群组管理员。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId              群组 ID。
 *  @param pError                错误信息。
 *
 *  @result     群组实例。
 *
 *  \~english
 *  Fetches the group information，including the group ID, name, description，setting, owner and admins.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance. 
 */
- (EMGroup *)getGroupSpecificationFromServerWithId:(NSString *)aGroupId
                                             error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群组详情，包含群组 ID，群组名称，群组描述，群组基本属性，群主，群组管理员。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId              群组 ID。
 *  @param fetchMembers          是否获取群组成员，默认最多取 200 人。
 *  @param pError                错误信息。
 *
 *  @result  群组实例。
 *
 *  \~english
 *  Fetches the group specification, including the group ID, name, description, setting, owner, admins.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId              The group ID.
 *  @param fetchMembers          Whether to fetch the group members. The default action fetches at most 200 members.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)getGroupSpecificationFromServerWithId:(NSString *)aGroupId
                                      fetchMembers:(BOOL)fetchMembers
                                             error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群组详情，包含群组 ID，群组名称，群组描述，群组基本属性，群主，群组管理员。
 * 
 *  异步方法。
 *
 *  @param aGroupId              群组 ID。
 *  @param aCompletionBlock      该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Fetches the group specification, including: ID, name, description, setting, owner, admins.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId              The group ID.
 *  @param aCompletionBlock      The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupSpecificationFromServerWithId:(NSString *)aGroupId
                                   completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  获取群组详情，包含群组 ID，群组名称，群组描述，群组基本属性，群主，群组管理员。
 * 
 *  异步方法。
 *
 *  @param aGroupId              群组 ID。
 *  @param fetchMembers          是否获取群组成员，默认最多取 200 人数。
 *  @param aCompletionBlock      该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Fetches the group specification, including: ID, name, description, setting, owner, admins.
 *
 *  @param aGroupId              The group ID.
 *  @param fetchMembers          Whether to fetch the group members. The default action fetches at most 200 members.
 *  @param aCompletionBlock      The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupSpecificationFromServerWithId:(NSString *)aGroupId
                                 fetchMembers:(BOOL)fetchMembers
                                   completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  获取群组成员列表。
 *
 *  这里需要注意的是：
 *  - 每次调用只返回一页的数据。首次调用传空值，会从最新的第一条开始取；
 *  - aPageSize 是这次接口调用期望返回的列表数据个数，如当前在最后一页，返回的数据会是 count < aPageSize；
 *  - 列表页码 aPageNum 是方便服务器分页查询返回，对于数据量未知且很大的情况，分页获取，服务器会根据每次的页数和每次的pagesize 返回数据，直到返回所有数据。
 * 
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCursor          游标，首次调用传空。使用场景：第一次传 nil ，然后根据服务器返回的数据，其中有一个字段是 aCursor，保存本地，下次调用接口时，会把更新的aCursor 传入作为获取数据的标志位置。
 *  @param aPageSize        调用接口时，指定期望返回的列表数据个数。
 *  @param pError           错误信息。
 *
 *  @result    列表和游标。
 *
 *  \~english
 *  Gets the list of group members from the server.
 * 
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId         The group ID.
 *  @param aCursor          The cursor when joins the group. Sets the parameter as nil for the first time.
 *  @param aPageSize        The expect entry number of the list.
 *  @param pError           The error information if the method fails: Error.
 *
 *  @result    The list and cursor.
 *
 */
- (EMCursorResult *)getGroupMemberListFromServerWithId:(NSString *)aGroupId
                                                cursor:(NSString *)aCursor
                                              pageSize:(NSInteger)aPageSize
                                                 error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群组成员列表。
 *  这里需要注意的是：
 *  - 每次调用只返回一页的数据。首次调用传空值，会从最新的第一条开始取；
 *  - aPageSize 是这次接口调用期望返回的列表数据个数，如当前在最后一页，返回的数据会是 count < aPageSize；
 *  - 列表页码 aPageNum 是方便服务器分页查询返回，对于数据量未知且很大的情况，分页获取，服务器会根据每次的页数和每次的pagesize 返回数据，直到返回所有数据。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCursor          游标，首次调用传空。
 *  @param aPageSize        获取多少条。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets the list of group members from the server.
 *
 *  @param aGroupId         The group ID.
 *  @param aCursor          The cursor when joins the group. Sets the parameter as nil for the first time.
 *  @param aPageSize        The page size.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupMemberListFromServerWithId:(NSString *)aGroupId
                                    cursor:(NSString *)aCursor
                                  pageSize:(NSInteger)aPageSize
                                completion:(void (^)(EMCursorResult *aResult, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  获取群组黑名单列表。
 *  这里需要注意的是：
 *  - 每次调用只返回一页的数据。首次调用传空值，会从最新的第一条开始取；
 *  - aPageSize 是这次接口调用期望返回的列表数据个数，如当前在最后一页，返回的数据会是 count < aPageSize；
 *  - 列表页码 aPageNum 是方便服务器分页查询返回，对于数据量未知且很大的情况，分页获取，服务器会根据每次的页数和每次的pagesize 返回数据，直到返回所有数据。
 * 
 *  该方法只有群主和管理员才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param pError           错误信息。
 *
 *  @result  黑名单列表。

 *  \~english
 *  Gets the blocklist of group from the server.
 * 
 *  Only the group owner or admin can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param pError           The error information if the method fails: Error.
 *
 *  @result     The blockList of the group.
 */
- (NSArray *)getGroupBlacklistFromServerWithId:(NSString *)aGroupId
                                    pageNumber:(NSInteger)aPageNum
                                      pageSize:(NSInteger)aPageSize
                                         error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群组黑名单列表。
 * 
 *  该方法只有群主和管理员才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets the group's blocklist.
 * 
 *  Only the group owner or admin can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupBlacklistFromServerWithId:(NSString *)aGroupId
                               pageNumber:(NSInteger)aPageNum
                                 pageSize:(NSInteger)aPageSize
                               completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  获取群组被禁言列表。
 *
 *  该方法只有群主和群管理员允许调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param pError           错误信息。
 *
 *  @result      群组被禁言列表。
 *
 *
 *  \~english
 *  Gets the mutelist of the group from the server.
 *
 *  Only the group owner or admin can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param pError           The error information if the method fails: Error.
 *
 *  @result     The mutelist of the group.
 */
- (NSArray *)getGroupMuteListFromServerWithId:(NSString *)aGroupId
                                   pageNumber:(NSInteger)aPageNum
                                     pageSize:(NSInteger)aPageSize
                                        error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群组被禁言列表。
 *
 *  该方法只有群主和群管理员允许调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets the mutelist of the group from the server.
 *
 *  Only the group owner or admin can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupMuteListFromServerWithId:(NSString *)aGroupId
                              pageNumber:(NSInteger)aPageNum
                                pageSize:(NSInteger)aPageSize
                              completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  获取群共享文件列表。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param pError           错误信息。
 *
 *  @result 群共享文件列表。
 *
 *  \~english
 *  Gets the share files of group from the server.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param pError           The error information if the method fails: Error.
 *
 *  @result   The list of share files of group.
 */
- (NSArray *)getGroupFileListWithId:(NSString *)aGroupId
                         pageNumber:(NSInteger)aPageNum
                           pageSize:(NSInteger)aPageSize
                              error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群共享文件列表。
 * 
 *  异步方法。
 *
 *  @param aGroupId         群组 ID。
 *  @param aPageNum         获取第几页。
 *  @param aPageSize        获取多少条。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Gets the share files of group from the server.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aPageNum         The page number.
 *  @param aPageSize        The page size.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 */
- (void)getGroupFileListWithId:(NSString *)aGroupId
                    pageNumber:(NSInteger)aPageNum
                      pageSize:(NSInteger)aPageSize
                    completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  获取群组白名单列表。
 *
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result        群组白名单列表。
 *
 *
 *  \~english
 *  Gets the allowlist of group from the server.
 *
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result     The allowList of the group.
 *
 */
- (NSArray *)getGroupWhiteListFromServerWithId:(NSString *)aGroupId
                                         error:(EMError **)pError;


/**
 *  \~chinese
 *  获取群组白名单列表。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets the allowlist of group from the server.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupWhiteListFromServerWithId:(NSString *)aGroupId
                               completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  查看自己是否在群组白名单中。
 *
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result  布尔值。 YES: 在白名单；  NO: 不在白名单。
 *
 *
 *  \~english
 *  Gets whether the member is on the allowlist.
 *
 *  @param aGroupId        The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result  BOOL.    YES: in whiteList.    NO: not in whiteList.
 *
 */
- (BOOL)isMemberInWhiteListFromServerWithGroupId:(NSString *)aGroupId
                                           error:(EMError **)pError;

/**
 *  \~chinese
 *  查看自己是否在群组白名单中。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets whether the member is on the allowlist.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)isMemberInWhiteListFromServerWithGroupId:(NSString *)aGroupId
                                      completion:(void (^)(BOOL inWhiteList, EMError *aError))aCompletionBlock;
/**
 *  \~chinese
 *  获取群公告。
 *
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result  群公告。失败返回空值。
 *
 *  \~english
 *  Gets the announcement of group from the server.
 *
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group Announcement. The SDK will return nil if fails.
 */
- (NSString *)getGroupAnnouncementWithId:(NSString *)aGroupId
                                   error:(EMError **)pError;

/**
 *  \~chinese
 *  获取群公告。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets the announcement of group from the server.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupAnnouncementWithId:(NSString *)aGroupId
                        completion:(void (^)(NSString *aAnnouncement, EMError *aError))aCompletionBlock;

#pragma mark - Edit Group

/**
 *  \~chinese
 *  邀请用户加入群组。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aOccupants      被邀请的用户名列表。
 *  @param aGroupId        群组 ID。
 *  @param aWelcomeMessage 欢迎信息。
 *  @param pError          错误信息。
 *
 *  @result    群组实例。失败返回空值。
 *
 *  \~english
 *  Invites users to join a group.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aOccupants      The users who are invited.
 *  @param aGroupId        The group ID.
 *  @param aWelcomeMessage The welcome message.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance. The SDK will return nil if fails.
 */
- (EMGroup *)addOccupants:(NSArray *)aOccupants
                  toGroup:(NSString *)aGroupId
           welcomeMessage:(NSString *)aWelcomeMessage
                    error:(EMError **)pError;

/**
 *  \~chinese
 *  邀请用户加入群组。
 *
 *  @param aUsers           被邀请的用户名列表。
 *  @param aGroupId         群组 ID。
 *  @param aMessage         欢迎信息。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Invites users to join a group.
 *
 *  @param aUsers           The users who are invited to join the group.
 *  @param aGroupId         The group ID.
 *  @param aMessage         The welcome message.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)addMembers:(NSArray *)aUsers
           toGroup:(NSString *)aGroupId
           message:(NSString *)aMessage
        completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  将群成员移出群组。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aOccupants 要移出群组的用户列表。
 *  @param aGroupId   群组 ID。
 *  @param pError     错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Removes members from the group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aOccupants  The users to be removed from the group.
 *  @param aGroupId    The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result            The group instance.
 */
- (EMGroup *)removeOccupants:(NSArray *)aOccupants
                   fromGroup:(NSString *)aGroupId
                       error:(EMError **)pError;

/**
 *  \~chinese
 *  将群成员移出群组。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aUsers           要移出群组的用户列表。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Removes members from the group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aUsers           The members to be removed from the group.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)removeMembers:(NSArray *)aUsers
            fromGroup:(NSString *)aGroupId
           completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  加人到群组黑名单。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aOccupants 要加入黑名单的用户。
 *  @param aGroupId   群组 ID。
 *  @param pError     错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Adds users to blocklist of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aOccupants  The users to be added to the blockList.
 *  @param aGroupId    The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance. 
 */
- (EMGroup *)blockOccupants:(NSArray *)aOccupants
                  fromGroup:(NSString *)aGroupId
                      error:(EMError **)pError;

/**
 *  \~chinese
 *  加人到群组黑名单。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aMembers         要加入黑名单的用户。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Adds users to blocklist of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aMembers         The users to be added to the blockList.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)blockMembers:(NSArray *)aMembers
           fromGroup:(NSString *)aGroupId
          completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  从群组黑名单中移除。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aOccupants 要从黑名单中移除的用户名列表。
 *  @param aGroupId   群组 ID。
 *  @param pError     错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Removes users from the blocklist of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aOccupants  The users to be removed from the blockList.
 *  @param aGroupId    The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result   The group instance.
 */
- (EMGroup *)unblockOccupants:(NSArray *)aOccupants
                     forGroup:(NSString *)aGroupId
                        error:(EMError **)pError;

/**
 *  \~chinese
 *  从群组黑名单中移除。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aMembers         要从黑名单中移除的用户名列表。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Removes users out of the blocklist of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aMembers         The users to be removed from the blockList.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)unblockMembers:(NSArray *)aMembers
             fromGroup:(NSString *)aGroupId
            completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  更改群组主题。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aSubject  新主题。
 *  @param aGroupId  群组 ID。
 *  @param pError    错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Changes the subject of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aSubject   The new subject of the group.
 *  @param aGroupId   The group ID.
 *  @param pError     The error information if the method fails: Error.
 *
 *  @result    The group instance. 
 */
- (EMGroup *)changeGroupSubject:(NSString *)aSubject
                       forGroup:(NSString *)aGroupId
                          error:(EMError **)pError;

/**
 *  \~chinese
 *  更改群组主题 。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aSubject         新主题。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Changes the group subject.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aSubject         The new subject of the group.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateGroupSubject:(NSString *)aSubject
                  forGroup:(NSString *)aGroupId
                completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  更改群组说明信息。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aDescription 说明信息。
 *  @param aGroupId     群组 ID。
 *  @param pError       错误信息。
 *
 *  @result   群组实例。
 *
 *  \~english
 *  Changes the group description.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aDescription  The new group description.
 *  @param aGroupId      The group ID.
 *  @param pError        The error information if the method fails: Error.
 *
 *  @result       The group instance. 
 */
- (EMGroup *)changeDescription:(NSString *)aDescription
                      forGroup:(NSString *)aGroupId
                         error:(EMError **)pError;

/**
 *  \~chinese
 *  更改群组说明信息。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aDescription     说明信息。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Changes the group description.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aDescription     The new group‘s description.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateDescription:(NSString *)aDescription
                 forGroup:(NSString *)aGroupId
               completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  退出群组，群主不能退出群，只能销毁群。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId  群组 ID。
 *  @param pError    错误信息。
 *
 *
 *  \~english
 *  Leaves a group. The owner can't leave the group, can only destroy the group.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId   The group ID.
 *  @param pError     The error information if the method fails: Error.
 *
 */
- (void)leaveGroup:(NSString *)aGroupId
             error:(EMError **)pError;

/**
 *  \~chinese
 *  退出群组，群主不能退出群，只能销毁群。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Leaves a group. The owner can't leave the group, can only destroy the group.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)leaveGroup:(NSString *)aGroupId
        completion:(void (^)(EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  解散群组。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId   群组 ID。
 *
 *  @result EMError   错误信息。成功返回 nil。
 *
 *  \~english
 *  Destroys a group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId  The group ID.
 *
 *  @result   The error information if the method fails: Error. The SDK wil return nil if the method succeed.
 */
- (EMError *)destroyGroup:(NSString *)aGroupId;

/**
 *  \~chinese
 *  解散群组。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Destroys a group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)destroyGroup:(NSString *)aGroupId
    finishCompletion:(void (^)(EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  屏蔽群消息，服务器不再发送此群的消息给用户，群主不能屏蔽群消息。
 * 
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId   要屏蔽的群 ID。
 *  @param pError     错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Blocks group messages. The server will block the messages from the group.
 * 
 *  The group owner can't block the group's messages.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId    The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)blockGroup:(NSString *)aGroupId
                  error:(EMError **)pError;

/**
 *  \~chinese
 *  屏蔽群消息，服务器不再发送此群的消息给用户，群主不能屏蔽群消息。
 *
 *  @param aGroupId         要屏蔽的群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Blocks group messages, so the server blocks the messages from the group. 
 *  
 *  The group owner can't block the group's messages.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)blockGroup:(NSString *)aGroupId
        completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  取消屏蔽群消息
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId   要取消屏蔽的群组 ID。
 *  @param pError     错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Unblocks group messages.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId   The group ID.
 *  @param pError     The error information if the method fails: Error.
 *
 *  @result    The group instance. 
 */
- (EMGroup *)unblockGroup:(NSString *)aGroupId
                    error:(EMError **)pError;

/**
 *  \~chinese
 *  取消屏蔽群消息。
 *
 *  @param aGroupId         要取消屏蔽的群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Unblocks group messages.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)unblockGroup:(NSString *)aGroupId
          completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  改变群主。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId   群组 ID。
 *  @param aNewOwner  新群主。
 *  @param pError     错误信息。
 *
 *  @result    返回群组实例。
 *
 *  \~english
 *  Changes the owner of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId    The group ID.
 *  @param aNewOwner   The new group owner.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance. 
 */
- (EMGroup *)updateGroupOwner:(NSString *)aGroupId
                     newOwner:(NSString *)aNewOwner
                        error:(EMError **)pError;

/**
 *  \~chinese
 *  改变群主。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aGroupId   群组 ID。
 *  @param aNewOwner  新群主。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Changes the owner of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aGroupId   The group ID.
 *  @param aNewOwner  The new group owner.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateGroupOwner:(NSString *)aGroupId
                newOwner:(NSString *)aNewOwner
              completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  添加群组管理员。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aAdmin     要添加的群组管理员。
 *  @param aGroupId   群组 ID。
 *  @param pError     错误信息。
 *  @result           返回群组实例。
 *
 *  \~english
 *  Adds group admin.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aAdmin     The admin to be added.
 *  @param aGroupId   The group ID.
 *  @param pError     The error information if the method fails: Error.
 *  @result           The group instance.
 */
- (EMGroup *)addAdmin:(NSString *)aAdmin
              toGroup:(NSString *)aGroupId
                error:(EMError **)pError;

/**
 *  \~chinese
 *  添加群组管理员。
 * 
 *  异步方法。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aAdmin     要添加的群组管理员。
 *  @param aGroupId   群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Adds group admin.
 * 
 *  This is an asynchronous method.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aAdmin      The admin to be added.
 *  @param aGroupId    The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)addAdmin:(NSString *)aAdmin
         toGroup:(NSString *)aGroupId
      completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  移除群组管理员。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aAdmin     要移除的群组管理员。
 *  @param aGroupId   群组 ID。
 *  @param pError     错误信息。
 *
 *  @result    返回群组实例。
 *
 *  \~english
 *  Removes a group admin.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aAdmin      The admin to be removed.
 *  @param aGroupId    The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)removeAdmin:(NSString *)aAdmin
               fromGroup:(NSString *)aGroupId
                   error:(EMError **)pError;

/**
 *  \~chinese
 *  移除群组管理员。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aAdmin     要移除的群组管理员。
 *  @param aGroupId   群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Removes the group admin.
 * 
 *  Only the group owner can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aAdmin     The admin to be removed.
 *  @param aGroupId   The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)removeAdmin:(NSString *)aAdmin
          fromGroup:(NSString *)aGroupId
         completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  将一组成员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aMuteMembers         要禁言的成员列表。
 *  @param aMuteMilliseconds    禁言时长。
 *  @param aGroupId             群组 ID。
 *  @param pError               错误信息。
 *
 *  @result    返回群组实例。
 *
 *  \~english
 *  Mutes group members.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aMuteMembers         The list of members to be muted.
 *  @param aMuteMilliseconds    The muted time duration in millisecond.
 *  @param aGroupId             The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)muteMembers:(NSArray *)aMuteMembers
        muteMilliseconds:(NSInteger)aMuteMilliseconds
               fromGroup:(NSString *)aGroupId
                   error:(EMError **)pError;

/**
 *  \~chinese
 *  将一组成员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aMuteMembers         要禁言的成员列表。
 *  @param aMuteMilliseconds    禁言时长。
 *  @param aGroupId             群组 ID。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Mutes group members.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is an asynchronous method.
 *
 *  @param aMuteMembers         The list of mute, type is <NSString>
 *  @param aMuteMilliseconds    Muted time duration in millisecond
 *  @param aGroupId             The group ID.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)muteMembers:(NSArray *)aMuteMembers
   muteMilliseconds:(NSInteger)aMuteMilliseconds
          fromGroup:(NSString *)aGroupId
         completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  解除禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aMuteMembers     被解除禁言的用户列表。
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result    返回群组实例。
 *
 *  \~english
 *  Unmutes group members.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aMembers        The list of members to be unmuted.
 *  @param aGroupId        The group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)unmuteMembers:(NSArray *)aMembers
                 fromGroup:(NSString *)aGroupId
                     error:(EMError **)pError;

/**
 *  \~chinese
 *  解除禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aMuteMembers     被解除禁言的用户列表。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Unmutes group members.
 * 
 *  Only the chatroom owner or admin can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMembers        The list of members to be unmuted.
 *  @param aGroupId        The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)unmuteMembers:(NSArray *)aMembers
            fromGroup:(NSString *)aGroupId
           completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  设置全员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Mutes all members.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)muteAllMembersFromGroup:(NSString *)aGroupId
                                  error:(EMError **)pError;

/**
 *  \~chinese
 *  设置全员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  mute all members.
 * 
 *  Only the chatroom owner or admin can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)muteAllMembersFromGroup:(NSString *)aGroupId
                     completion:(void(^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  解除全员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Unmutes all members.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)unmuteAllMembersFromGroup:(NSString *)aGroupId
                                 error:(EMError **)pError;


/**
 *  \~chinese
 *  解除全员禁言。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Unmutes all members.
 * 
 *  Only the chatroom owner or admin can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)unmuteAllMembersFromGroup:(NSString *)aGroupId
                       completion:(void(^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  添加白名单。 
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aMuteMembers     要添加的成员列表。
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Adds members to the allowlist.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aMembers        The members to be added to the allowlist.
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)addWhiteListMembers:(NSArray *)aMembers
                       fromGroup:(NSString *)aGroupId
                           error:(EMError **)pError;

/**
 *  \~chinese
 *  添加白名单。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aMembers         要添加的成员列表。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Adds members to the allowlist.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  @param aMembers         The members to be added to the allowlist.
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)addWhiteListMembers:(NSArray *)aMembers
                  fromGroup:(NSString *)aGroupId
                 completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  移除白名单。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aMuteMembers     要添加的成员列表。
 *  @param aGroupId         群组 ID。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Removes members from the allowlist.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aMembers        The members to be removed from the allowlist.
 *  @param aGroupId        The group ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)removeWhiteListMembers:(NSArray *)aMembers
                          fromGroup:(NSString *)aGroupId
                              error:(EMError **)pError;

/**
 *  \~chinese
 *  移除白名单。
 * 
 *  异步方法。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  @param aMembers         被移除的列表。
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Removes members from the allowlist.
 * 
 *  This is an asynchronous method.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  @param aMembers        The members to be removed from the allowlist.
 *  @param aGroupId        The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)removeWhiteListMembers:(NSArray *)aMembers
                     fromGroup:(NSString *)aGroupId
                    completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  上传群共享文件。
 *
 *  @param aGroupId         群组 ID。
 *  @param aFilePath        文件路径。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Uploads the share file of group.
 *
 *  @param aGroupId        The group ID.
 *  @param aFilePath       The path of file.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (void)uploadGroupSharedFileWithId:(NSString *)aGroupId
                           filePath:(NSString*)aFilePath
                           progress:(void (^)(int progress))aProgressBlock
                         completion:(void (^)(EMGroupSharedFile *aSharedFile, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  下载群共享文件。
 *
 *  @param aGroupId         群组 ID。
 *  @param aFilePath        文件路径。
 *  @param aSharedFileId    共享文件 ID。
 *  @param aProgressBlock   文件下载进度回调。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Downloads the share file of group.
 *
 *  @param aGroupId         The group ID.
 *  @param aFilePath        The path of file.
 *  @param aSharedFileId    The shared file ID.
 *  @param aProgressBlock   The block of attachment upload progress
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 */
- (void)downloadGroupSharedFileWithId:(NSString *)aGroupId
                             filePath:(NSString *)aFilePath
                         sharedFileId:(NSString *)aSharedFileId
                             progress:(void (^)(int progress))aProgressBlock
                           completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  删除群共享文件。
 *
 *  @param aGroupId         群组 ID。
 *  @param aSharedFileId    共享文件 ID。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Removes the share file of the group.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId        The group ID.
 *  @param aSharedFileId   The share file ID.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)removeGroupSharedFileWithId:(NSString *)aGroupId
                            sharedFileId:(NSString *)aSharedFileId
                                   error:(EMError **)pError;

/**
 *  \~chinese
 *  删除群共享文件。
 *
 *  @param aGroupId         群组 ID。
 *  @param aSharedFileId    共享文件 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Removes the share file of group.
 *
 *  @param aGroupId         The group ID.
 *  @param aSharedFileId    The share file ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 */
- (void)removeGroupSharedFileWithId:(NSString *)aGroupId
                       sharedFileId:(NSString *)aSharedFileId
                         completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  修改群公告。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aAnnouncement    群公告。
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Updates the announcement of group.
 * 
 *  Only the chatroom owner or admin can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId         The group ID.
 *  @param aAnnouncement    The announcement of the group.
 *  @param pError           The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)updateGroupAnnouncementWithId:(NSString *)aGroupId
                              announcement:(NSString *)aAnnouncement
                                     error:(EMError **)pError;

/**
 *  \~chinese
 *  修改群公告。
 * 
 *  该方法只有管理员或者群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aGroupId         群组 ID。
 *  @param aAnnouncement    群公告。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Updates the announcement of group.
 * 
 *  Only the chatroom owner or admin can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aAnnouncement    The announcement of the group.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateGroupAnnouncementWithId:(NSString *)aGroupId
                         announcement:(NSString *)aAnnouncement
                           completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  修改群扩展信息。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aExt             扩展信息
 *  @param pError           错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Updates the extended of the group.
 * 
 *  Only the owner of the group can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId        The group ID.
 *  @param aExt            The extended information of the group.
 *  @param pError          The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)updateGroupExtWithId:(NSString *)aGroupId
                              ext:(NSString *)aExt
                            error:(EMError **)pError;

/**
 *  \~chinese
 *  修改群扩展信息。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  @param aGroupId         群组 ID。
 *  @param aExt             扩展信息。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Updates the extended information of the group.
 * 
 *  Only the group owner can call this method.
 *
 *  @param aGroupId         The group ID.
 *  @param aExt             The extended information of the group.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateGroupExtWithId:(NSString *)aGroupId
                         ext:(NSString *)aExt
                  completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

#pragma mark - Edit Public Group

/**
 *  \~chinese
 *  加入一个公开群组，群类型应该是 EMGroupStylePublicOpenJoin。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId 公开群组的 ID。
 *  @param pError   错误信息。
 *
 *  @result    所加入的公开群组。
 *
 *  \~english
 *  Joins a public group. The group style should be EMGroupStylePublicOpenJoin.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId    The public group ID.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)joinPublicGroup:(NSString *)aGroupId
                       error:(EMError **)pError;

/**
 *  \~chinese
 *  加入一个公开群组，群类型应该是 EMGroupStylePublicOpenJoin。
 *
 *  @param aGroupId         公开群组的 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Joins a public group. The group style should be EMGroupStylePublicOpenJoin.
 *
 *  @param aGroupId         The public group ID。
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)joinPublicGroup:(NSString *)aGroupId
             completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  申请加入一个需批准的公开群组，群类型应该是 EMGroupStylePublicJoinNeedApproval。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId    公开群组的 ID。
 *  @param aMessage    请求加入的信息。
 *  @param pError      错误信息。
 *
 *  @result    申请加入的公开群组。
 *
 *  \~english
 *  The request to join a public group. The group style should be EMGroupStylePublicJoinNeedApproval.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId    The public group ID。
 *  @param aMessage    The message in the request.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)applyJoinPublicGroup:(NSString *)aGroupId
                          message:(NSString *)aMessage
                            error:(EMError **)pError;

/**
 *  \~chinese
 *  申请加入一个需批准的公开群组，群类型应该是 EMGroupStylePublicJoinNeedApproval。
 *
 *  @param aGroupId         公开群组的 ID。
 *  @param aMessage         请求加入的信息。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Requests to join a public group. The group style should be EMGroupStylePublicJoinNeedApproval.
 *
 *  @param aGroupId         The public group ID.
 *  @param aMessage         The information in the request.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)requestToJoinPublicGroup:(NSString *)aGroupId
                         message:(NSString *)aMessage
                      completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

#pragma mark - Application

/**
 *  \~chinese
 *  批准入群申请。
 * 
 *  该方法只有群主才有权限调用。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId   所申请的群组 ID。
 *  @param aUsername  申请人。
 *
 *  @result 错误信息。
 *
 *  \~english
 *  Accepts a group request.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId   The group ID.
 *  @param aUsername  The user who sends the request for join the group.
 *
 *  @result Error
 */
- (EMError *)acceptJoinApplication:(NSString *)aGroupId
                         applicant:(NSString *)aUsername;

/**
 *  \~chinese
 *  批准入群申请。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aGroupId         所申请的群组 ID。
 *  @param aUsername        申请人。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Approves a group request.
 * 
 *  Only the group owner can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The user who sends the request for join the group.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)approveJoinGroupRequest:(NSString *)aGroupId
                         sender:(NSString *)aUsername
                     completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  拒绝入群申请。
 * 
 *  该方法只有群主才有权限调用。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId  被拒绝的群组 ID。
 *  @param aUsername 申请人。
 *  @param aReason   拒绝理由。
 *
 *  @result 错误信息。
 *
 *  \~english
 *  Declines a group request.
 * 
 *  Only the group owner can call this method.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId  The group ID.
 *  @param aUsername The user who sends the request for join the group.
 *  @param aReason   The reason of declining.
 *
 *  @result Error
 */
- (EMError *)declineJoinApplication:(NSString *)aGroupId
                          applicant:(NSString *)aUsername
                             reason:(NSString *)aReason;

/**
 *  \~chinese
 *  拒绝入群申请。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  异步方法。
 *
 *  @param aGroupId         被拒绝的群组 ID。
 *  @param aUsername        申请人。
 *  @param aReason          拒绝理由。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Declines a group request.
 * 
 *  Only the group owner can call this method.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The user who sends the request for join the group.
 *  @param aReason          The reason for declining.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)declineJoinGroupRequest:(NSString *)aGroupId
                         sender:(NSString *)aUsername
                         reason:(NSString *)aReason
                     completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  接受入群邀请。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param groupId     接受的群组 ID。
 *  @param aUsername   邀请者。
 *  @param pError      错误信息。
 *
 *  @result 接受的群组实例。
 *
 *  \~english
 *  Accepts a group invitation.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId     The group ID.
 *  @param aUsername    The user who initiates the invitation.
 *  @param pError       The error information if the method fails: Error.
 *
 *  @result  The group instance.
 */
- (EMGroup *)acceptInvitationFromGroup:(NSString *)aGroupId
                               inviter:(NSString *)aUsername
                                 error:(EMError **)pError;

/**
 *  \~chinese
 *  接受入群邀请。
 * 
 *  异步方法。
 *
 *  @param groupId          接受的群组 ID。
 *  @param aUsername        邀请者。
 *  @param pError           错误信息。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Accepts a group invitation.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The user who initiates the invitation. 
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)acceptInvitationFromGroup:(NSString *)aGroupId
                          inviter:(NSString *)aUsername
                       completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  拒绝入群邀请。
 *  
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId  被拒绝的群组 ID。
 *  @param aUsername 邀请人。
 *  @param aReason   拒绝理由。
 *
 *  @result 错误信息。
 *
 *  \~english
 *  Declines a group invitation.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId  The group ID.
 *  @param aUsername The user who initiates the invitation.
 *  @param aReason   The reason for declining.
 *
 *  @result  The error information if the method fails: Error.
 */
- (EMError *)declineInvitationFromGroup:(NSString *)aGroupId
                                inviter:(NSString *)aUsername
                                 reason:(NSString *)aReason;

/**
 *  \~chinese
 *  拒绝入群邀请。
 * 
 *  异步方法。
 *
 *  @param aGroupId         被拒绝的群组 ID。
 *  @param aInviter         邀请人。
 *  @param aReason          拒绝理由。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Declines a group invitation.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupId         The group ID.
 *  @param aInviter         The user who send the invitation.
 *  @param aReason          The reason of declining.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)declineGroupInvitation:(NSString *)aGroupId
                       inviter:(NSString *)aInviter
                        reason:(NSString *)aReason
                    completion:(void (^)(EMError *aError))aCompletionBlock;

#pragma mark - Apns

/**
 *  \~chinese
 *  屏蔽/取消屏蔽群组消息的推送。
 *  
 *  同步方法，会阻塞当前线程。
 * 
 *  已废弃，请用 {@link IEMPushManager::updatePushServiceForGroups:disablePush:} 代替。
 *
 *  @param aGroupId    群组 ID。
 *  @param aIgnore     是否屏蔽。
 *
 *  @result 错误信息。
 *
 *  \~english
 *  Blocks/unblocks group message‘s push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 * 
 *  Deprecated, please use  {@link IEMPushManager::updatePushServiceForGroups:disablePush:}  instead.
 *
 *  @param aGroupId     The group ID.
 *  @param aIsIgnore    Whether to show or ignore the push notification.
 *
 *  @result    The error information if the method fails: Error.
 */
- (EMError *)ignoreGroupPush:(NSString *)aGroupId
                      ignore:(BOOL)aIsIgnore EM_DEPRECATED_IOS(3_3_2, 3_8_3, "Use -IEMPushManager::updatePushServiceForGroups:disablePush:");


/**
 *  \~chinese
 *  屏蔽/取消屏蔽群组消息的推送。
 * 
 *  已废弃，请用 {@link IEMPushManager::updatePushServiceForGroups:disablePush:completion:} 代替。
 *
 *  @param aGroupId          群组 ID。
 *  @param aIsEnable         是否允许推送。
 *  @param aCompletionBlock  该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Blocks/unblocks the group message‘s push notification.
 * 
 *  Deprecated, please use  {@link IEMPushManager::updatePushServiceForGroups:disablePush:completion:}  instead.
 *
 *  @param aGroupId          The group ID.
 *  @param aIsEnable         Whether to block or unblock the push notification.
 *  @param aCompletionBlock  The completion block, which contains the error message if the method fails.
 *
 */
- (void)updatePushServiceForGroup:(NSString *)aGroupId
                    isPushEnabled:(BOOL)aIsEnable
                       completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_3_2, 3_8_3, "Use -IEMPushManager::updatePushServiceForGroups:disablePush:completion:");


/**
 *  \~chinese
 *  屏蔽/取消屏蔽群组消息的推送。
 * 
 *  已废弃，请用 {@link IEMPushManager::updatePushServiceForGroups:disablePush:} 代替。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupIDs   群组 ID 列表。
 *  @param aIgnore     是否屏蔽。
 *
 *  @result 错误信息。
 *
 *  \~english
 *  Blocks/unblocks the group message‘s push notification.
 * 
 *  Deprecated, please use  {@link IEMPushManager::updatePushServiceForGroups:disablePush:}  instead.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupIDs    The group ID list.
 *  @param aIsIgnore    Whether to show or ignore the push notification.
 *
 *  @result    The error information if the method fails: Error.
 */
- (EMError *)ignoreGroupsPush:(NSArray *)aGroupIDs
                       ignore:(BOOL)aIsIgnore EM_DEPRECATED_IOS(3_3_2, 3_8_3, "Use -IEMPushManager::updatePushServiceForGroups:disablePush:");

/**
 *  \~chinese
 *  屏蔽/取消屏蔽群组消息的推送。
 * 
 *  已废弃，请用 {@link IEMPushManager::updatePushServiceForGroups:disablePush:completion:} 代替。
 *
 *  @param aGroupIDs         群组 ID 列表。
 *  @param aIsEnable         是否允许推送。
 *  @param aCompletionBlock  该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Blocks/unblocks the group message‘s push notification.
 * 
 *  Deprecated, please use  {@link IEMPushManager::updatePushServiceForGroups:disablePush:completion:}  instead.
 *
 *  @param aGroupIDs         The group ID list.
 *  @param aIsEnable         Whether to enable group messages push.
 *  @param aCompletionBlock  The completion block, which contains the error message if the method fails.
 *
 */
- (void)updatePushServiceForGroups:(NSArray *)aGroupIDs
                     isPushEnabled:(BOOL)aIsEnable
                        completion:(void (^)(NSArray *groups, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_3_2, 3_8_3, "Use -IEMPushManager::updatePushServiceForGroups:disablePush:completion:");

#pragma mark - EM_DEPRECATED_IOS 3.3.0

/**
 *  \~chinese
 *  从服务器获取用户所有的群组，成功后更新 SDK 本地数据库中的群组列表。
 * 
 *  已废弃，请用 {@link  getJoinedGroupsFromServerWithPage:pageSize:error:} 代替。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param pError  错误信息。
 *
 *  @result 群组列表。
 *
 *  \~english
 *  Gets all of user's groups from the server.
 * 
 *  Deprecated, please use  {@link  getJoinedGroupsFromServerWithPage:pageSize:error:}  instead.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param pError    The error information if the method fails: Error.
 *
 *  @result    The group list.
 */
- (NSArray *)getMyGroupsFromServerWithError:(EMError **)pError EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getJoinedGroupsFromServerWithPage:pageSize:error:");

/**
 *  \~chinese
 *  从服务器获取用户所有的群组，成功后更新 SDK 本地数据库中的群组列表。
 * 
 *  已废弃，请用 {@link  getJoinedGroupsFromServerWithPage:pageSize:completion:} 代替。
 * 
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets all of user's groups from the server.
 * 
 *  Deprecated, please use  {@link  getJoinedGroupsFromServerWithPage:pageSize:completion:}  instead.
 *
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getJoinedGroupsFromServerWithCompletion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getJoinedGroupsFromServerWithPage:pageSize:completion:");

/**
 *  \~chinese
 *  获取群组详情。
 * 
 *  已废弃，请用 {@link  getGroupSpecificationFromServerWithId:error:} 代替。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId              群组 ID。
 *  @param aIncludeMembersList   是否获取成员列表，为 YES 时，返回 200 个成员。
 *  @param pError                错误信息。
 *
 *  @result    群组实例。
 *
 *  \~english
 *  Fetches the group information.
 * 
 *  Deprecated, please use  {@link  getGroupSpecificationFromServerWithId:error:}  instead.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId              The group ID.
 *  @param aIncludeMembersList   Whether to get member list，When YES, returns at most 200 members.
 *  @param pError      The error information if the method fails: Error.
 *
 *  @result    The group instance.
 */
- (EMGroup *)fetchGroupInfo:(NSString *)aGroupId
         includeMembersList:(BOOL)aIncludeMembersList
                      error:(EMError **)pError EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getGroupSpecificationFromServerWithId:error:");

/**
 *  \~chinese
 *  获取群组详情。
 * 
 *  已废弃，请用 {@link  getGroupSpecificationFromServerWithId:completion:} 代替。
 *
 *  @param aGroupId              群组 ID。
 *  @param aIncludeMembersList   是否获取成员列表，为 YES 时，返回 200 个成员。
 *  @param aCompletionBlock      该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Fetches the group specification.
 * 
 *  Deprecated, please use  {@link  getGroupSpecificationFromServerWithId:completion:}  instead.
 *
 *  @param aGroupId              The group ID.
 *  @param aIncludeMembersList   Whether to get member list，When YES, returns at most 200 members.
 *  @param aCompletionBlock      The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupSpecificationFromServerByID:(NSString *)aGroupID
                         includeMembersList:(BOOL)aIncludeMembersList
                                 completion:(void (^)(EMGroup *aGroup, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getGroupSpecificationFromServerWithId:completion:");

/**
 *  \~chinese
 *  获取群组黑名单列表。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link  getGroupBlacklistFromServerWithId:pageNumber:pageSize:error:} 代替。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId  群组 ID。
 *  @param pError    错误信息。
 *
 *  @result    群组黑名单列表。
 *
 *  \~english
 *  Gets the group‘s blocklist.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link  getGroupBlacklistFromServerWithId:pageNumber:pageSize:error:}  instead.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId   The group ID.
 *  @param pError     The error information if the method fails: Error.
 *
 *  @result    The group blocklist.
 */
- (NSArray *)fetchGroupBansList:(NSString *)aGroupId
                          error:(EMError **)pError EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getGroupBlacklistFromServerWithId:pageNumber:pageSize:error:");

/**
 *  \~chinese
 *  获取群组黑名单列表。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link  getGroupBlacklistFromServerWithId:pageNumber:pageSize:completion:} 代替。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Gets group's blocklist.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link  getGroupBlacklistFromServerWithId:pageNumber:pageSize:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)getGroupBlackListFromServerByID:(NSString *)aGroupId
                             completion:(void (^)(NSArray *aList, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - getGroupBlacklistFromServerWithId:pageNumber:pageSize:completion:");

/**
 *  \~chinese
 *  解散群组。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link  destroyGroup:} 代替。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupId  群组 ID。
 *  @param pError    错误信息。
 *
 *  @result    销毁的群组实例。失败返回空值。
 *
 *  \~english
 *  Destroys a group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link  destroyGroup:}  instead.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupId  The group ID.
 *  @param pError     The error information if the method fails: Error.
 *
 *  @result    Destroyed group. The SDK will return nil if fails.
 */
- (EMGroup *)destroyGroup:(NSString *)aGroupId
                    error:(EMError **)pError EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - destroyGroup:");

/**
 *  \~chinese
 *  解散群组。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link  destroyGroup:finishCompletion:} 代替。
 *
 *  @param aGroupId         群组 ID。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Destroys a group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link  destroyGroup:finishCompletion:}  instead.
 *
 *  @param aGroupId        The group ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)destroyGroup:(NSString *)aGroupId
          completion:(void (^)(EMGroup* aGroup, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use - destroyGroup:finishCompletion:");

#pragma mark - EM_DEPRECATED_IOS 3.2.3

/**
 *  \~chinese
 *  添加回调代理。
 * 
 *  已废弃，请用 {@link  addDelegate:delegateQueue:} 代替。
 *
 *  @param aDelegate  要添加的代理。
 *
 *  \~english
 *  Adds delegate.
 * 
 *  Deprecated, please use  {@link  addDelegate:delegateQueue:}  instead.
 *
 *  @param aDelegate  The delegate to be added.
 */
- (void)addDelegate:(id<EMGroupManagerDelegate>)aDelegate EM_DEPRECATED_IOS(3_1_0, 3_2_2, "Use - addDelegate:delegateQueue:");

#pragma mark - EM_DEPRECATED_IOS < 3.2.3

/**
 *  \~chinese
 *  获取所有群组，如果内存中不存在，则先从 SDK 本地数据库加载。
 * 
 *  已废弃，请用 {@link getJoinedGroups} 代替。
 *
 *  @result 群组列表。
 *
 *  \~english
 *  Gets all groups. The SDK will load from DB if not exist in memory.
 * 
 *  Deprecated, please use  {@link getJoinedGroups}  instead.
 *
 *  @result The group list. 
 */
- (NSArray *)getAllGroups __deprecated_msg("Use -getJoinedGroups instead");

/**
 *  \~chinese
 *  从 SDK 本地数据库加载所有群组，加载后更新内存中的群组列表。
 * 
 *  已废弃，请用 {@link getJoinedGroups} 代替。
 *
 *  @result 群组列表。
 *
 *  \~english
 *  Loads all groups from DB. The SDK will update group list in memory after loading.
 * 
 *  Deprecated, please use  {@link getJoinedGroups}  instead.
 *
 *  @result The group list. 
 */
- (NSArray *)loadAllMyGroupsFromDB __deprecated_msg("Use -getJoinedGroups instead");

/**
 *  \~chinese
 *  从内存中获取屏蔽了推送的群组 ID 列表。
 * 
 *  已废弃，请用 {@link IEMPushManager::noPushGroups} 代替。
 *
 *  @result 群组 ID列表。
 *
 *  \~english
 *  Gets the group ID list which block push from memory.
 * 
 *  Deprecated, please use  {@link IEMPushManager::noPushGroups}   instead.
 *
 *  @result The group ID list. 
 */
- (NSArray *)getAllIgnoredGroupIds __deprecated_msg("Use -IEMPushManager::noPushGroups instead");

/**
 *  \~chinese
 *  从服务器获取用户所有的群组，成功后更新 SDK 本地数据库中的群组列表。
 * 
 *  已废弃，请用 {@link getJoinedGroupsFromServerWithCompletion:} 代替。
 *
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Gets all of user's groups from server. The SDK will update the group list in memory and DB after success.
 * 
 *  Deprecated, please use  {@link getJoinedGroupsFromServerWithCompletion:}  instead.
 *
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncGetMyGroupsFromServer:(void (^)(NSArray *aList))aSuccessBlock
                           failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -getJoinedGroupsFromServerWithCompletion: instead");

/**
 *  \~chinese
 *  从服务器获取指定范围内的公开群。
 * 
 *  已废弃，请用 {@link getPublicGroupsFromServerWithCursor:pageSize:completion:} 代替。
 *
 *  @param aCursor          获取公开群的游标，首次调用传空。
 *  @param aPageSize        期望返回结果的数量, 如果小于 0 则一次返回所有结果。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Gets public groups in the specified range from the server.
 * 
 *  Deprecated, please use  {@link getPublicGroupsFromServerWithCursor:pageSize:completion:}  instead.
 *
 *  @param aCursor          Get the cursor to join the group. Sets the parameter as nil for the first time.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncGetPublicGroupsFromServerWithCursor:(NSString *)aCursor
                                        pageSize:(NSInteger)aPageSize
                                         success:(void (^)(EMCursorResult *aCursor))aSuccessBlock
                                         failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -getPublicGroupsFromServerWithCursor:pageSize:completion: instead");

/**
 *  \~chinese
 *  根据群组 ID 搜索公开群。
 * 
 *  已废弃，请用 {@link searchPublicGroupWithId:completion:} 代替。
 *
 *  @param aGroundId        群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Searches public group with the group ID.
 * 
 *  Deprecated, please use  {@link searchPublicGroupWithId:completion:}  instead.
 *
 *  @param aGroundId        The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncSearchPublicGroupWithId:(NSString *)aGroundId
                             success:(void (^)(EMGroup *aGroup))aSuccessBlock
                             failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -searchPublicGroupWithId:completion: instead");

/**
 *  \~chinese
 *  创建一个群组。
 * 
 *  已废弃，请用 {@link createGroupWithSubject:description:invitees:message:setting:completion:} 代替。
 *
 *  @param aSubject         群组名称。
 *  @param aDescription     群组描述。
 *  @param aInvitees        群组成员，不包括创建者自己。
 *  @param aMessage         邀请消息。
 *  @param aSetting         群组属性。
 *  @param aSuccessBlock    创建成功回调。
 *  @param aFailureBlock    创建失败回调。
 *
 *  \~english
 *  Creates a group.
 * 
 *  Deprecated, please use  {@link createGroupWithSubject:description:invitees:message:setting:completion:}  instead.
 *
 *  @param aSubject         The subject of the group.
 *  @param aDescription     The description of the group.
 *  @param aInvitees        The members of the group. Do not include the creator.
 *  @param aMessage         The invitation message.
 *  @param aSetting         The group options.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncCreateGroupWithSubject:(NSString *)aSubject
                        description:(NSString *)aDescription
                           invitees:(NSArray *)aInvitees
                            message:(NSString *)aMessage
                            setting:(EMGroupOptions *)aSetting
                            success:(void (^)(EMGroup *aGroup))aSuccessBlock
                            failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -createGroupWithSubject:description:invitees:message:setting:completion: instead");


/**
 *  \~chinese
 *  获取群组详情。
 * 
 *  已废弃，请用 {@link getGroupSpecificationFromServerByID:includeMembersList:completion:} 代替。
 *
 *  @param aGroupId              群组 ID。
 *  @param aIncludeMembersList   是否获取成员列表。
 *  @param aSuccessBlock         成功的回调。
 *  @param aFailureBlock         失败的回调。
 *
 *
 *  \~english
 *  Fetches the group detail.
 * 
 *  Deprecated, please use  {@link getGroupSpecificationFromServerByID:includeMembersList:completion:}  instead.
 *
 *  @param aGroupId              The group ID.
 *  @param aIncludeMembersList   Whether to get the member list.
 *  @param aSuccessBlock         The callback block of success.
 *  @param aFailureBlock         The callback block of failure.
 *
 */
- (void)asyncFetchGroupInfo:(NSString *)aGroupId
         includeMembersList:(BOOL)aIncludeMembersList
                    success:(void (^)(EMGroup *aGroup))aSuccessBlock
                    failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -getGroupSpecificationFromServerByID:includeMembersList:completion: instead");

/**
 *  \~chinese
 *  获取群组黑名单列表。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link getGroupBlackListFromServerByID:completion:} 代替。
 *
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Gets the blocklist of the group.
 * 
 *  Deprecated, please use  {@link getGroupBlackListFromServerByID:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncFetchGroupBansList:(NSString *)aGroupId
                        success:(void (^)(NSArray *aList))aSuccessBlock
                        failure:(void (^)(EMError *aError))aFailureBlock  __deprecated_msg("Use -getGroupBlackListFromServerByID:completion: instead");

/**
 *  \~chinese
 *  邀请用户加入群组。
 * 
 *  已废弃，请用 {@link addMembers:toGroup:message:completion:} 代替。
 *
 *  @param aOccupants       被邀请的用户名列表。
 *  @param aGroupId         群组 ID。
 *  @param aWelcomeMessage  欢迎信息。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Invites users to join a group.
 * 
 *  Deprecated, please use  {@link addMembers:toGroup:message:completion:}  instead.
 *
 *  @param aOccupants       The users who are invited.
 *  @param aGroupId         The group ID.
 *  @param aWelcomeMessage  The welcome message.
 *  @param aSuccessBlock    The callback block of success
 *  @param aFailureBlock    The callback block of failure
 *
 */
- (void)asyncAddOccupants:(NSArray *)aOccupants
                  toGroup:(NSString *)aGroupId
           welcomeMessage:(NSString *)aWelcomeMessage
                  success:(void (^)(EMGroup *aGroup))aSuccessBlock
                  failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -addMembers:toGroup:message:completion: instead");

/**
 *  \~chinese
 *  将群成员移出群组。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link removeMembers:fromGroup:completion:} 代替。
 *
 *  @param aOccupants       要移出群组的用户列表。
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Removes members from the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link removeMembers:fromGroup:completion:}  instead.
 *
 *  @param aOccupants       The users to be removed.
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncRemoveOccupants:(NSArray *)aOccupants
                   fromGroup:(NSString *)aGroupId
                     success:(void (^)(EMGroup *aGroup))aSuccessBlock
                     failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -removeMembers:fromGroup:completion: instead");

/**
 *  \~chinese
 *  加人到群组黑名单。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link blockMembers:fromGroup:completion:} 代替。
 *
 *  @param aOccupants       要加入黑名单的用户。
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Adds users to the blocklist of the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link blockMembers:fromGroup:completion:}  instead.
 *
 *  @param aOccupants       The users to be added to the blocklist.
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncBlockOccupants:(NSArray *)aOccupants
                  fromGroup:(NSString *)aGroupId
                    success:(void (^)(EMGroup *aGroup))aSuccessBlock
                    failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -blockMembers:fromGroup:completion: instead");

/**
 *  \~chinese
 *  从群组黑名单中移除。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link unblockMembers:fromGroup:completion:} 代替。
 *
 *  @param aOccupants       要从黑名单中移除的用户名列表。
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Removes users from the blocklist of the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link unblockMembers:fromGroup:completion:}  instead.
 *
 *  @param aOccupants       The users to be removed
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncUnblockOccupants:(NSArray *)aOccupants
                     forGroup:(NSString *)aGroupId
                      success:(void (^)(EMGroup *aGroup))aSuccessBlock
                      failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -unblockMembers:fromGroup:completion: instead");

/**
 *  \~chinese
 *  更改群组主题。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link updateGroupSubject:forGroup:completion:} 代替。
 *
 *  @param aSubject         新主题。
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Changes the subject of the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link updateGroupSubject:forGroup:completion:}  instead.
 *
 *  @param aSubject         The new subject of the group.
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncChangeGroupSubject:(NSString *)aSubject
                       forGroup:(NSString *)aGroupId
                        success:(void (^)(EMGroup *aGroup))aSuccessBlock
                        failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -updateGroupSubject:forGroup:completion: instead");

/**
 *  \~chinese
 *  更改群组说明信息。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link updateDescription:forGroup:completion:} 代替。
 *
 *  @param aDescription     说明信息。
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Changes the description of the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link updateDescription:forGroup:completion:}  instead.
 *
 *  @param aDescription     The new description of the group.
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncChangeDescription:(NSString *)aDescription
                      forGroup:(NSString *)aGroupId
                       success:(void (^)(EMGroup *aGroup))aSuccessBlock
                       failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -updateDescription:forGroup:completion: instead");

/**
 *  \~chinese 
 *  退出群组，群主不能退出群，只能销毁群。
 * 
 *  已废弃，请用 {@link leaveGroup:completion:} 代替。
 *
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Leaves a group. The group owner can't leave the group, can only destroy the group.
 * 
 *  Deprecated, please use  {@link leaveGroup:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncLeaveGroup:(NSString *)aGroupId
                success:(void (^)(EMGroup *aGroup))aSuccessBlock
                failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -leaveGroup:completion: instead");

/**
 *  \~chinese
 *  解散群组。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link destroyGroup:completion:} 代替。
 *
 *  @param aGroupId         群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *  \~english
 *  Destroys a group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link destroyGroup:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncDestroyGroup:(NSString *)aGroupId
                  success:(void (^)(EMGroup *aGroup))aSuccessBlock
                  failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -destroyGroup:completion: instead");

/**
 *  \~chinese
 *  屏蔽群消息，服务器不再发送此群的消息给用户。
 * 
 *  群主不能屏蔽群消息。
 * 
 *  已废弃，请用 {@link blockGroup:completion:} 代替。
 *
 *  @param aGroupId         要屏蔽的群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *  \~english
 *  Blocks the group’s messages. The server will blocks the messages of the group to user. 
 * 
 *  The owner can't block the group's message.
 * 
 *  Deprecated, please use  {@link blockGroup:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncBlockGroup:(NSString *)aGroupId
                success:(void (^)(EMGroup *aGroup))aSuccessBlock
                failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -blockGroup:completion: instead");

/**
 *  \~chinese
 *  取消屏蔽群消息。
 * 
 *  已废弃，请用 {@link unblockGroup:completion:} 代替。
 *
 *  @param aGroupId         要取消屏蔽的群组 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *  \~english
 *  Unblocks group messages.
 * 
 *  Deprecated, please use  {@link unblockGroup:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncUnblockGroup:(NSString *)aGroupId
                  success:(void (^)(EMGroup *aGroup))aSuccessBlock
                  failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -unblockGroup:completion: instead");

/**
 *  \~chinese
 *  加入一个公开群组，群类型应该是 EMGroupStylePublicOpenJoin。
 * 
 *  已废弃，请用 {@link joinPublicGroup:completion:} 代替。
 *
 *  @param aGroupId         公开群组的 ID。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *  \~english
 *  Joins a public group. The group style should be EMGroupStylePublicOpenJoin.
 * 
 *  Deprecated, please use  {@link joinPublicGroup:completion:}  instead.
 *
 *  @param aGroupId         The public group ID.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncJoinPublicGroup:(NSString *)aGroupId
                     success:(void (^)(EMGroup *aGroup))aSuccessBlock
                     failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -joinPublicGroup:completion: instead");

/**
 *  \~chinese
 *  申请加入一个需批准的公开群组，群类型应该是 EMGroupStylePublicJoinNeedApproval。
 * 
 *  已废弃，请用 {@link requestToJoinPublicGroup:message:completion:} 代替。
 *
 *  @param aGroupId         公开群组的 ID。
 *  @param aMessage         请求加入的信息。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Applies to join a public group. The group style should be EMGroupStylePublicJoinNeedApproval.
 * 
 *  Deprecated, please use  {@link requestToJoinPublicGroup:message:completion:}  instead.
 *
 *  @param aGroupId         The public group ID.
 *  @param aMessage         The apply message.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncApplyJoinPublicGroup:(NSString *)aGroupId
                          message:(NSString *)aMessage
                          success:(void (^)(EMGroup *aGroup))aSuccessBlock
                          failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -requestToJoinPublicGroup:message:completion: instead");

/**
 *  \~chinese
 *  批准入群申请。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link approveJoinGroupRequest:sender:completion:} 代替。
 *
 *  @param aGroupId         所申请的群组 ID。
 *  @param aUsername        申请人。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Accepts the user's application of join the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link approveJoinGroupRequest:sender:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The username of the applicant.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncAcceptJoinApplication:(NSString *)aGroupId
                         applicant:(NSString *)aUsername
                           success:(void (^)())aSuccessBlock
                           failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -approveJoinGroupRequest:sender:completion: instead");

/**
 *  \~chinese
 *  拒绝入群申请。
 * 
 *  该方法只有群主才有权限调用。
 * 
 *  已废弃，请用 {@link declineJoinGroupRequest:sender:reason:completion:} 代替。
 *
 *  @param aGroupId         被拒绝的群组 ID。
 *  @param aUsername        申请人。
 *  @param aReason          拒绝理由。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 * 
 *
 *  \~english
 *  Declines the user's application of join the group.
 * 
 *  Only the group owner can call this method.
 * 
 *  Deprecated, please use  {@link declineJoinGroupRequest:sender:reason:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The username of the applicant.
 *  @param aReason          The decline reason.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncDeclineJoinApplication:(NSString *)aGroupId
                          applicant:(NSString *)aUsername
                             reason:(NSString *)aReason
                            success:(void (^)())aSuccessBlock
                            failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -declineJoinGroupRequest:sender:reason:completion: instead");

/**
 *  \~chinese
 *  接受入群邀请。
 * 
 *  已废弃，请用 {@link acceptInvitationFromGroup:inviter:completion:} 代替。
 *
 *  @param groupId          接受的群组 ID。
 *  @param aUsername        邀请者。
 *  @param pError           错误信息。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Accepts the invitation of join the group.
 * 
 *  Deprecated, please use  {@link acceptInvitationFromGroup:inviter:completion:}  instead.
 *
 *  @param groupId          The group ID.
 *  @param aUsername        The user who initiated the invitation.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncAcceptInvitationFromGroup:(NSString *)aGroupId
                               inviter:(NSString *)aUsername
                               success:(void (^)(EMGroup *aGroup))aSuccessBlock
                               failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -acceptInvitationFromGroup:inviter:completion: instead");

/**
 *  \~chinese
 *  拒绝入群邀请。
 * 
 *  已废弃，请用 {@link declineGroupInvitation:inviter:reason:completion:} 代替。
 *
 *  @param aGroupId         被拒绝的群组 ID。
 *  @param aUsername        邀请人。
 *  @param aReason          拒绝入群理由。
 *  @param aSuccessBlock    成功的回调。
 *  @param aFailureBlock    失败的回调。
 *
 *
 *  \~english
 *  Declines a group invitation.
 * 
 *  Deprecated, please use  {@link declineGroupInvitation:inviter:reason:completion:}  instead.
 *
 *  @param aGroupId         The group ID.
 *  @param aUsername        The user who initiates the invitation.
 *  @param aReason          The reason for declining.
 *  @param aSuccessBlock    The callback block of success.
 *  @param aFailureBlock    The callback block of failure.
 *
 */
- (void)asyncDeclineInvitationFromGroup:(NSString *)aGroupId
                                inviter:(NSString *)aUsername
                                 reason:(NSString *)aReason
                                success:(void (^)())aSuccessBlock
                                failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -declineGroupInvitation:inviter:reason:completion: instead");

/**
 *  \~chinese
 *  屏蔽/取消屏蔽群组消息的推送。
 * 
 *  已废弃，请用 {@link updatePushServiceForGroup:isPushEnabled:completion:} 代替。
 *
 *  @param aGroupId          群组 ID。
 *  @param aIgnore           是否屏蔽。
 *  @param aSuccessBlock     成功的回调。
 *  @param aFailureBlock     失败的回调。
 *
 *
 *  \~english
 *  Blocks / unblocks the group message‘s push notification.
 * 
 *  Deprecated, please use  {@link updatePushServiceForGroup:isPushEnabled:completion:}  instead.
 *
 *  @param aGroupId          The group ID.
 *  @param aIgnore           Whether to block the push notification.
 *  @param aSuccessBlock     The callback block of success.
 *  @param aFailureBlock     The callback block of failure.
 *
 */
- (void)asyncIgnoreGroupPush:(NSString *)aGroupId
                      ignore:(BOOL)aIsIgnore
                     success:(void (^)())aSuccessBlock
                     failure:(void (^)(EMError *aError))aFailureBlock __deprecated_msg("Use -updatePushServiceForGroup:isPushEnabled:completion: instead");

@end
