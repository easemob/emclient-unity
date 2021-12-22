using System.Runtime.InteropServices;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace ChatSDK
{

    internal sealed class Client_iOS : IClient
    {
        public override void InitWithOptions(Options options)
        {
            ChatAPIIOS.Client_HandleMethodCall("initWithOptions", options.ToJsonString(), null);
        }

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("password", password);
            ChatAPIIOS.Client_HandleMethodCall("createAccount", obj.ToString(), callBack?.callbackId);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("pwdOrToken", pwdOrToken);
            obj.Add("isToken", isToken);
            ChatAPIIOS.Client_HandleMethodCall("login", obj.ToString(), callBack?.callbackId);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("unbindDeviceToken", unbindDeviceToken);
            ChatAPIIOS.Client_HandleMethodCall("logout", obj.ToString(), callBack?.callbackId);
        }

        public override string CurrentUsername() {
            string jsonString = ChatAPIIOS.Client_GetMethodCall("getCurrentUsername");
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            return jo["getCurrentUsername"].Value;
        }

        public override bool IsConnected {
            get {
                string jsonString = ChatAPIIOS.Client_GetMethodCall("isConnected");
                JSONObject jsonObject = JSON.Parse(jsonString).AsObject;
                return jsonObject["isConnected"].AsBool;
            }

            internal set => IsConnected = value;            
        }

        public override bool IsLoggedIn() {
            string jsonString = ChatAPIIOS.Client_GetMethodCall("isLoggedIn");
            JSONObject jsonObject = JSON.Parse(jsonString).AsObject;
            return jsonObject["isLoggedIn"].AsBool;
        }

        public override string AccessToken()
        {
            string jsonString = ChatAPIIOS.Client_GetMethodCall("accessToken");
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            return jo["accessToken"].Value;
        }

        internal override void StartLog(string logFilePath)
        {
            throw new System.NotImplementedException();
        }

        internal override void StopLog()
        {
            throw new System.NotImplementedException();
        }

        public override void ClearResource()
        {
            throw new System.NotImplementedException();
        }

    }
}



   