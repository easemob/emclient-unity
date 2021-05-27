//
//  emchatprivateconfigs.h
//  easemob
//
//  Created by Neil Cao on 15/12/21.
//
//

#ifndef emchatprivateconfigs_hpp
#define emchatprivateconfigs_hpp

#include <string>

namespace easemob {

class EMChatConfigs;
class EMChatPrivateConfigs {
public:
    virtual ~EMChatPrivateConfigs() {}
    
    bool& enableDns() { return mEnableDnsConfig; }
    
    std::string& chatServer() { return mChatServer; }
    
    int& chatPort() { return mChatPort; }
    
    std::string& restServer() { return mRestServer; }
    
    std::string& resolverServer() { return mResolverServer; }
    
    std::string& chatDomain() { return mChatDomain; }
    
    std::string& groupDomain() { return mGroupDomain; }
    
    std::string& rtcUrlDomain() {return mRtcUrlDomain; }
    
private:
    EMChatPrivateConfigs() : mEnableDnsConfig(true), mChatPort(-1),mRtcUrlDomain("") {}
    friend EMChatConfigs;
    
    bool        mEnableDnsConfig;
    int         mChatPort;
    std::string mChatServer;
    std::string mRestServer;
    std::string mResolverServer;
    std::string mChatDomain;
    std::string mGroupDomain;
    std::string mRtcUrlDomain;
};

} // namespace easemob

#endif /* emchatprivateconfigs_hpp */
