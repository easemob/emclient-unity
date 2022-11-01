using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class CursorResult<T>: BaseModel
    {
        /**
	     * \~chinese
	     * 游标。
	     *
	     * \~english
	     * The cursor.
	     */
        public string Cursor { get; internal set; }

        /**
        * \~chinese
        * 数据列表。
        *
        * \~english
        * The data list.
        */
        public List<T> Data { get; internal set; }

        internal CursorResult() { }

        internal CursorResult(string jsonString, ItemCallback callback = null) : base(jsonString)
        {
            this.callback = callback;
        }

        internal CursorResult(JSONObject jsonObject, ItemCallback callback = null) : base(jsonObject)
        {
            this.callback = callback;
        }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            Cursor = jsonObject["cursor"].Value;
            JSONNode jn = jsonObject["list"];
            if (jn.IsArray) {
                JSONArray jsonArray = jn.AsArray;
                Data = new List<T>();
                if (typeof(T).IsAssignableFrom(typeof(BaseModel)))
                {
                    foreach (JSONObject jsonObj in jsonArray)
                    {
                        Data.Add(callback(jsonObj));
                    }
                }
            }
            callback = null;
        }

        internal override JSONObject ToJsonObject()
        {
            return null;
        }

        private ItemCallback callback;
        internal delegate T ItemCallback(JSONObject jsonObject);
    }
}