using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

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
    [Preserve]
    public class PageResult<T> : BaseModel
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

        [Preserve]
        internal PageResult() { }

        [Preserve]
        internal PageResult(string jsonString, ItemCallback callback = null)
        {
            this.callback = callback;
        }

        [Preserve]
        internal PageResult(JSONObject josnObject, ItemCallback callback = null)
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
                foreach (var jsonObj in jsonArray)
                {
                    object ret = callback(jsonObj);
                    if (ret != null)
                    {
                        Data.Add((T)ret);
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
        internal delegate T ItemCallback(JSONNode jsonObject);
    }
}