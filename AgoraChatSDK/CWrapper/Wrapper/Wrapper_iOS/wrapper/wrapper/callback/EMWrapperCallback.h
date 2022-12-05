//
//  EMWrapperCallback.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import <Foundation/Foundation.h>


NS_ASSUME_NONNULL_BEGIN

typedef void(^OnSuccessCallback)(NSObject *valueObj);
typedef void(^OnErrorCallback)(NSDictionary *errorDict);
typedef void(^OnProgressCallback)(int progress);

@interface EMWrapperCallback : NSObject

@property (nonatomic, copy) OnSuccessCallback onSuccessCallback;
@property (nonatomic, copy) OnErrorCallback onErrorCallback;
@property (nonatomic, copy) OnProgressCallback onProgressCallback;

- (void)onSuccess:(NSString *)str;
- (void)onError:(NSDictionary *)errDict;
- (void)onProgress:(int)aProgress;

- (void)runInQueue:(void(^)(void))aciton;


@end

NS_ASSUME_NONNULL_END
