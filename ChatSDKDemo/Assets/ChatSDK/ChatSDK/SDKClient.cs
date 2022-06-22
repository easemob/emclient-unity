﻿using System.Threading.Tasks;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
    public class SDKClient
    {
        private Options _Options;
        private static SDKClient _instance;
        private IClient _Sdk;

        public static SDKClient Instance
        {
            get
            {
                return _instance ?? (_instance = new SDKClient());
            }
        }

        /// <summary>
        /// 聊天管理
        /// </summary>
        public IChatManager ChatManager { get => _Sdk.ChatManager(); }

        /// <summary>
        /// 联系人管理类
        /// </summary>
        public IContactManager ContactManager { get => _Sdk.ContactManager(); }

        /// <summary>
        /// 群组管理类
        /// </summary>
        public IGroupManager GroupManager { get => _Sdk.GroupManager(); }

        /// <summary>
        /// 聊天室管理类
        /// </summary>
        public IRoomManager RoomManager { get => _Sdk.RoomManager(); }

        /// <summary>
        /// 推送管理类
        /// </summary>
        public IPushManager PushManager { get => _Sdk.PushManager(); }

        /// <summary>
        /// 会话管理类
        /// </summary>
        internal IConversationManager ConversationManager { get => _Sdk.ConversationManager(); }

        /// <summary>
        /// 用户信息管理类
        /// </summary>
        public IUserInfoManager UserInfoManager { get => _Sdk.UserInfoManager(); }

        /// <summary>
        /// 获取sdk配置信息
        /// </summary>
        public Options Options { get { return _Options; } }


        /// <summary>
        /// 获取sdk版本号
        /// </summary>
        public string SdkVersion { get => "1.0.2"; }

        /// <summary>
        /// 获取当前登录的环信id
        /// </summary>
        public string CurrentUsername { get => _Sdk.CurrentUsername(); }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        public bool IsLoggedIn { get => _Sdk.IsLoggedIn(); }

        /// <summary>
        /// 当前是否连接到服务器
        /// </summary>

        public bool IsConnected { get => _Sdk.IsConnected(); }

        /// <summary>
        /// 当前用户的token
        /// </summary>
        public string AccessToken { get => _Sdk.AccessToken(); }

        /// <summary>
        /// 添加连接回调监听
        /// </summary>
        /// <param name="connectionDelegate"></param>
        public void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate)) {
                CallbackManager.Instance().connectionListener.delegater.Add(connectionDelegate);
            }
        }


        public void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate))
            {
                CallbackManager.Instance().connectionListener.delegater.Remove(connectionDelegate);
            }
        }

        public void AddMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (!CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Add(multiDeviceDelegate);
            }
        }

        public void DeleteMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Remove(multiDeviceDelegate);
            }
        }

        /// <summary>
        /// 初始化sdk
        /// </summary>
        /// <param name="options">环信sdk配置</param>
        public void InitWithOptions(Options options)
        {
            CallbackManager.Instance();
            _Options = options;
            _Sdk.InitWithOptions(options);
            registerManagers();
        }

        /// <summary>
        /// 创建环信环信id，需要环信后台开启开放注册才可用
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="password">环信id对应密码</param>
        /// <param name="handle">结果回调</param>
        public void CreateAccount(string username, string password, CallBack handle = null)
        {
            _Sdk.CreateAccount(username, password, handle);
        }

        /// <summary>
        /// 登录环信服务器
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="pwdOrToken">环信id的密码或者token</param>
        /// <param name="isToken">pwdOrToken是否是token</param>
        /// <param name="handle">结果回调</param>
        public void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null)
        {
            _Sdk.Login(username, pwdOrToken, isToken, handle);
        }

        /// <summary>
        /// 使用声网token登录环信服务器
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="token">声网token</param>
        /// <param name="handle">结果回调</param>
        public void LoginWithAgoraToken(string username, string token, CallBack handle = null)
        {
            _Sdk.LoginWithAgoraToken(username, token, handle);
        }

        /// <summary>
        /// 使用声网token刷新有效期
        /// </summary>
        /// <param name="token">声网token</param>
        public void RenewAgoraToken(string token)
        {
            _Sdk.RenewAgoraToken(token);
        }

        /// <summary>
        /// 退出环信登录
        /// </summary>
        /// <param name="unbindDeviceToken">解除推送绑定</param>
        /// <param name="handle">结果回调</param>
        public void Logout(bool unbindDeviceToken, CallBack handle = null)
        {
            _Sdk.Logout(unbindDeviceToken, new CallBack(
                onSuccess: () => {
                    CallbackManager.Instance().CleanAllCallback();
                    handle?.Success?.Invoke();
                },
                onError:(code, desc) => {
                    handle?.Error?.Invoke(code, desc);
                }
                ));
        }

        private SDKClient()
        {
            _Sdk = IClient.Instance;
        }

        private void registerManagers() {
            _Sdk.ContactManager();
            _Sdk.ChatManager();
            _Sdk.GroupManager();
            _Sdk.RoomManager();
            _Sdk.ConversationManager();
        }
    }
}





