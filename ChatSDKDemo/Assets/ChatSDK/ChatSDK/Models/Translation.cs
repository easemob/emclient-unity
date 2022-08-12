using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{

    /**
    * \~chinese
    * 翻译功能支持的语种。
    *
    * \~english
    * Support languages of translation function.
    * 
    */
    public class SupportLanguage
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

