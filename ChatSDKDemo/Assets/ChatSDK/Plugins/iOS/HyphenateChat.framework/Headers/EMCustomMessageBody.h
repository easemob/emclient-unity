/**
 *  \~chinese
 *  @header EMCustomMessageBody.h
 *  @abstract 自定义消息体
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMCustomMessageBody.h
 *  @abstract Custom message body
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>
#import "EMCommonDefs.h"
#import "EMMessageBody.h"

/**
 *  \~chinese
 *  自定义消息体。
 *
 *  \~english
 *  The custom message body.
 */
@interface EMCustomMessageBody : EMMessageBody

/**
 *  \~chinese
 *  自定义事件。
 *
 *  \~english
 *  The custom event.
 */
@property (nonatomic, copy) NSString *event;

/**
 *  \~chinese
 *  自定义扩展字典。
 *
 *  \~english
 *  The custom extension dictionary.
 */
@property (nonatomic, copy) NSDictionary<NSString *,NSString *> *customExt;


/**
 *  \~chinese
 *  初始化自定义消息体。
 *
 *  @param aEvent   自定义事件。
 *  @param aCustomExt 自定义扩展字典。
 *
 *  @result 自定义消息体实例。
 *
 *  \~english
 *  Initializes a custom message body instance.
 *
 *  @param aEvent   The custom event.
 *  @param aCustomExt The custom extension dictionary.
 *
 *  @result The custom message body instance.
 */
- (instancetype)initWithEvent:(NSString *)aEvent customExt:(NSDictionary<NSString *,NSString *> *)aCustomExt;

#pragma mark - EM_DEPRECATED_IOS 3.7.2
/**
 *  \~chinese
 *  扩展字典。
 * 
 *  已废弃，请用 {@link customExt} 代替。
 *
 *  \~english
 *  The extension dictionary.
 * 
 *  Deprecated. Please use  {@link customExt}  instead.
 */
@property (nonatomic, copy) NSDictionary *ext EM_DEPRECATED_IOS(3_6_5, 3_7_2, "Use -customExt instead");

/**
 *  \~chinese
 *  初始化自定义消息体。
 * 
 *  已废弃，请用 {@link initWithEvent:customExt:} 代替。
 *
 *  @param aEvent   自定义事件。
 *  @param aExt     扩展字典。
 *
 *  @result         自定义消息体实例。
 *
 *  \~english
 *  Initializes a custom message body instance.
 * 
 *  Deprecated. Please use  {@link initWithEvent:customExt:}  instead.
 *
 *  @param aEvent   The custom event.
 *  @param aExt     The extension dictionary.
 *
 *  @result The custom message body instance.
 */
- (instancetype)initWithEvent:(NSString *)aEvent ext:(NSDictionary *)aExt; EM_DEPRECATED_IOS(3_6_5, 3_7_2, "Use - initWithEvent:customExt: instead");

@end
