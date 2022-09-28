using System.Collections.Generic;
using UnityEngine;


namespace AgoraChat
{
	internal sealed class PresenceManager_Android : IPresenceManager
	{

		private AndroidJavaObject wrapper;

		public PresenceManager_Android()
		{
			using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMPresenceManagerWrapper"))
			{
				wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
			}
		}

		public override void PublishPresence(string description, CallBack handle = null)
		{
			wrapper.Call("publishPresence", description, handle?.callbackId);
		}
		public override void SubscribePresences(List<string> members, long expiry, ValueCallBack<List<Presence>> handle = null)
		{
			wrapper.Call("subscribePresences", TransformTool.JsonStringFromStringList(members), expiry, handle?.callbackId);
		}
		public override void UnsubscribePresences(List<string> members, CallBack handle = null)
		{
			wrapper.Call("unsubscribePresences", TransformTool.JsonStringFromStringList(members), handle?.callbackId);
		}
		public override void FetchSubscribedMembers(int pageNum, int pageSize, ValueCallBack<List<string>> handle = null)
		{
			wrapper.Call("fetchSubscribedMembers", pageNum, pageSize, handle?.callbackId);
		}
		public override void FetchPresenceStatus(List<string> members, ValueCallBack<List<Presence>> handle = null)
		{
			wrapper.Call("fetchPresenceStatus", TransformTool.JsonStringFromStringList(members), handle?.callbackId);
		}

	}
}
