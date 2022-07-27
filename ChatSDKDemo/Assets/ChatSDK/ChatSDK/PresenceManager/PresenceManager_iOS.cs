using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
	internal sealed class PresenceManager_iOS : IPresenceManager
	{
		private IntPtr client;

		internal PresenceManager_iOS(IClient _client)
		{
			if (_client is Client_Common clientCommon)
			{
				client = clientCommon.client;
			}
		}

		public override void PublishPresence(string description, CallBack handle = null)
		{
			JSONObject obj = new JSONObject();
			obj.Add("desc", description);
			ChatAPIIOS.PresenceManager_HandleMethodCall("publishPresence", obj.ToString(), handle?.callbackId);
		}
		public override void SubscribePresences(List<string> members, long expiry, ValueCallBack<List<Presence>> handle = null)
		{
			JSONArray jsonArray = new JSONArray();
			foreach (string userId in members) {
				jsonArray.Add(userId);
			}
			JSONObject json = new JSONObject();
			json.Add("members", jsonArray);
			json.Add("expiry", expiry);
			ChatAPIIOS.PresenceManager_HandleMethodCall("subscribePresences", json.ToString(), handle?.callbackId);
		}
		public override void UnsubscribePresences(List<string> members, CallBack handle = null)
		{
			JSONArray jsonArray = new JSONArray();
			foreach (string userId in members)
			{
				jsonArray.Add(userId);
			}
			JSONObject json = new JSONObject();
			json.Add("members", jsonArray);
			ChatAPIIOS.PresenceManager_HandleMethodCall("unsubscribePresences", json.ToString(), handle?.callbackId);
		}
		public override void FetchSubscribedMembers(int pageNum, int pageSize, ValueCallBack<List<string>> handle = null)
		{
			JSONObject json = new JSONObject();
			json.Add("pageNum", pageNum);
			json.Add("pageSize", pageSize);
			ChatAPIIOS.PresenceManager_HandleMethodCall("fetchSubscribedMembers", json.ToString(), handle?.callbackId);
		}
		public override void FetchPresenceStatus(List<string> members, ValueCallBack<List<Presence>> handle = null)
		{
			JSONArray jsonArray = new JSONArray();
			foreach (string userId in members)
			{
				jsonArray.Add(userId);
			}
			JSONObject json = new JSONObject();
			json.Add("members", jsonArray);
			ChatAPIIOS.PresenceManager_HandleMethodCall("fetchPresenceStatus", json.ToString(), handle?.callbackId);
		}
	}
}
