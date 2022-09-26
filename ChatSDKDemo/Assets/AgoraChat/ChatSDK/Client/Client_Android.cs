﻿using UnityEngine;

namespace AgoraChat
{
    internal sealed class Client_Android : IClient
    {
        private AndroidJavaObject wrapper;

        public Client_Android()
        {

            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMClientWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override void InitWithOptions(Options options)
        {
            wrapper.Call("init", options.ToJsonString());
        }

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            wrapper.Call("createAccount", username, password, callBack?.callbackId);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            wrapper.Call("login", username, pwdOrToken, isToken, callBack?.callbackId);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            wrapper.Call("logout", unbindDeviceToken, callBack?.callbackId);
        }
        public override string CurrentUsername()
        {
            return wrapper.Call<string>("currentUsername");
        }

        public override bool IsConnected() 
        {
            return wrapper.Call<bool>("isConnected");
        }

        public override bool IsLoggedIn()
        {
            return wrapper.Call<bool>("isLoggedIn");
        }

        public override string AccessToken()
        {
            return wrapper.Call<string>("accessToken");
        }

        public override void LoginWithAgoraToken(string username, string token, CallBack handle = null)
        {
            wrapper.Call("loginWithAgoraToken", username, token, handle?.callbackId);
        }

        public override void RenewAgoraToken(string token)
        {
            wrapper.Call("renewToken", token, null);
        }

        /*
        public override void AutoLogin(CallBack callback = null)
        {
            //TODO: add code
        }
        */

        public override void ClearResource()
        {
            //throw new System.NotImplementedException();
        }

    }
}