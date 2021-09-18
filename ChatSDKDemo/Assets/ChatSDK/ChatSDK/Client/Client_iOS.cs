using System.Runtime.InteropServices;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace ChatSDK
{

    internal sealed class Client_iOS : IClient
    {

        static string Connection_Obj = "unity_chat_emclient_connection_obj";

        GameObject listenerGameObj;

        public override void InitWithOptions(Options options)
        {
            ClientNative.Client_HandleMethodCall("initWithOptions", options.ToJsonString(), null);
        }

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("password", password);
            ClientNative.Client_HandleMethodCall("createAccount", obj.ToString(), callBack?.callbackId);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("pwdOrToken", pwdOrToken);
            obj.Add("isToken", isToken);
            ClientNative.Client_HandleMethodCall("login", obj.ToString(), callBack?.callbackId);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("unbindDeviceToken", unbindDeviceToken);
            ClientNative.Client_HandleMethodCall("logout", obj.ToString(), callBack?.callbackId);
        }

        public override string CurrentUsername() {
            string jsonString = ClientNative.Client_GetMethodCall("getCurrentUsername");
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            return jo["getCurrentUsername"].Value;
        }

        public override bool IsConnected {
            get {
                string jsonString = ClientNative.Client_GetMethodCall("isConnected");
                JSONObject jsonObject = JSON.Parse(jsonString).AsObject;
                return jsonObject["isConnected"].AsBool;
            }

            internal set => IsConnected = value;            
        }

        public override bool IsLoggedIn() {
            string jsonString = ClientNative.Client_GetMethodCall("isLoggedIn");
            JSONObject jsonObject = JSON.Parse(jsonString).AsObject;
            return jsonObject["isLoggedIn"].AsBool;
        }

        public override string AccessToken()
        {
            string jsonString = ClientNative.Client_GetMethodCall("accessToken");
            JSONObject jo = JSON.Parse(jsonString).AsObject;
            return jo["accessToken"].Value;
        }

        public override void StartLog(string logFilePath)
        {
            throw new System.NotImplementedException();
        }

        public override void StopLog()
        {
            throw new System.NotImplementedException();
        }

        public override void ClearResource()
        {
            throw new System.NotImplementedException();
        }

    }

    class ClientNative
    {
        [DllImport("__Internal")]
        internal extern static void Client_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string Client_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}



   