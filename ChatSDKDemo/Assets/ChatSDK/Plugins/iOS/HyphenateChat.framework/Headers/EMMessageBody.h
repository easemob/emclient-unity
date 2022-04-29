/*!
 *  \~chinese
 *  @header EMMessageBody.h
 *  @abstract 消息体类型的基类
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMMessageBody.h
 *  @abstract Base class of message body
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>
#import <CoreGraphics/CGGeometry.h>

/*!
 *  \~chinese 
 *  消息体枚举类型
 *
 *  \~english
 *  Message body type
 */
typedef enum {
    EMMessageBodyTypeText   = 1,    /*! \~chinese 文本类型 \~english Text type*/
    EMMessageBodyTypeImage,         /*! \~chinese 图片类型 \~english Image type*/
    EMMessageBodyTypeVideo,         /*! \~chinese 视频类型 \~english Video type*/
    EMMessageBodyTypeLocation,      /*! \~chinese 位置类型 \~english Location type*/
    EMMessageBodyTypeVoice,         /*! \~chinese 语音类型 \~english Voice type*/
    EMMessageBodyTypeFile,          /*! \~chinese 文件类型 \~english File type*/
    EMMessageBodyTypeCmd,           /*! \~chinese 命令类型 \~english Command type*/
    EMMessageBodyTypeCustom,        /*! \~chinese 自定义类型 \~english Custom type*/
} EMMessageBodyType;

/*!
 *  \~chinese 
 *  消息体
 *  不直接使用，由子类继承实现
 *
 *  \~english 
 *  Message body
 */
@interface EMMessageBody : NSObject

/*!
 *  \~chinese 
 *  消息体类型
 *
 *  \~english 
 *  Message body type
 */
@property (nonatomic, readonly) EMMessageBodyType type;

@end
