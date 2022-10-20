using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    /**
         * \~chinese
         * 分页类。
         * 
         * 该类包含下次查询的页码以及相应页面上的数据条数。
         * 
         * 该对象在分页获取数据时返回。
         * 
         * @param <T> 泛型类型 T。  
         *
         * \~english
         * The pagination class.
         * 
         * This class contains the page number for the next query and the number of records on the page. 
         * 
         * The class instance is returned when you make a paginated query.
         * 
         * @param <T> The generic <T> type.
         */
    public class PageResult<T>: BaseModel
    {
        /**
        * \~chinese
        * 当前页面上的数据条数。
        * 
        * 若 `PageCount` 小于传入的每页要获取的数量，表示当前是最后一页。
        *
        * \~english
        * The number of records on the current page.
        * 
        * If the value of `PageCount` is smaller than the number of records that you expect to get on each page, the current page is the last page.
        * 
        */
        public int PageCount { get; internal set; }

        /**
        * \~chinese
        * <T> 泛型数据。  
        *
        * \~english
        * The data of the generic List<T> type.
        */
        public List<T> Data { get; internal set; }

        internal PageResult() { }

        internal PageResult(string jsonString, ItemCallback callback = null) : base(jsonString)
        {
            this.callback = callback;
        }

        internal PageResult(JSONObject josnObject, ItemCallback callback = null) : base(josnObject)
        {
            this.callback = callback;
        }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            PageCount = jsonObject["count"].AsInt;
            JSONNode jn = jsonObject["list"];
            if (jn.IsArray)
            {
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
        internal delegate T ItemCallback(SimpleJSON.JSONObject jsonObject);
    }
}