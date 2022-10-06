using SimpleJSON;

namespace AgoraChat
{

    /**
    * \~chinese
    * 翻译功能支持的语言列表。
    *
    * \~english
    * The list of supported languages for translation.
    * 
    */
    public class SupportLanguage
    {
        /**
        *  \~chinese
        *  微软翻译目标语言对应的 code。
        *
        *  \~english
        *  The code of the target language in Microsoft Translation Service.
        */
        public string LanguageCode { get; internal set; }

        /**
        *  \~chinese
        *  语言名称，如中文简体为 "Chinese Simplified"。
        *
        *  \~english
        *  The language name, for example: "Chinese Simplified" for Chinese Simplified.
        */
        public string LanguageName { get; internal set; }

        /**
        *  \~chinese
        *  语言的原生名称，如中文简体为 "中文 (简体)"。
        *
        *  \~english
        *
        *  The native name of the language, for example: "中文 (简体)" for Chinese Simplified.
        */
        public string LanguageNativeName { get; internal set; }


        internal SupportLanguage()
        {

        }

        internal SupportLanguage(string jsonString) {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull)
                {
                    LanguageCode = jn["code"].Value;
                    LanguageName = jn["name"].Value;
                    LanguageNativeName = jn["nativeName"].Value;
                }
            }
        }
    }
}

