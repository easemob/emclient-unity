using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{

    /**
    * \~chinese
    * 翻译功能支持的语种。
    *
    * \~english
    * Support languages of translation function.
    * 
    */
    [Preserve]
    public class SupportLanguage : BaseModel
    {
        /**
        *  \~chinese
        *  语言代码，如中文简体为"zh-Hans"
        *
        *  \~english
        *  Language code, for example: "zh-Hans" for Chinese Simplified
        */
        public string LanguageCode { get; internal set; }

        /**
        *  \~chinese
        *  语言名称，如中文简体为"Chinese Simplified"
        *
        *  \~english
        *   Language name, for example: "Chinese Simplified" for Chinese Simplified
        */
        public string LanguageName { get; internal set; }

        /**
        *  \~chinese
        *  语言的原生名称，如中文简体为"中文 (简体)"
        *
        *  \~english
        *
        *  Language native name, for example: "中文 (简体)" for Chinese Simplified
        */
        public string LanguageNativeName { get; internal set; }

        [Preserve]
        internal SupportLanguage() { }

        [Preserve]
        internal SupportLanguage(string jsonString) : base(jsonString) { }

        [Preserve]
        internal SupportLanguage(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            LanguageCode = jsonObject["code"];
            LanguageName = jsonObject["name"];
            LanguageNativeName = jsonObject["nativeName"];
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("code", LanguageCode);
            jo.AddWithoutNull("name", LanguageName);
            jo.AddWithoutNull("nativeName", LanguageNativeName);

            return jo;
        }
    }
}