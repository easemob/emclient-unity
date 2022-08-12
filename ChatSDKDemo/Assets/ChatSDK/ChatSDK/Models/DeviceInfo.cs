﻿using SimpleJSON;

namespace ChatSDK
{
    /**
     * \~chinese
     * 多设备登录信息类。
     *
     * \~english
     * The multi-device information class.
     */
    public class DeviceInfo

    {
        /**
         * \~chinese
         * 其他登录的设备信息。
         * 
         * 可以通过设备信息区分设备类型。
         *
         * \~english
         * The information of other login devices.
         * 
         * With the device information, you can distinguish different types of devices.
         */
        public string Resource { get; private set; }

        /**
         * \~chinese
         * 设备的 UUID（唯一标识码）。
         *
         * \~english
         * The UUID of the device.
         * 
         */
        public string DeviceUUID { get; private set; }

        /**
         * \~chinese
         * 设备名称。
         *
         * \~english
         * The device name.
         */
        public string DeviceName { get; private set; }


        internal DeviceInfo(string jsonString)
        {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Resource = jo["resource"];
                    DeviceUUID = jo["deviceUUID"];
                    DeviceName = jo["deviceName"];
                }
            }
        }
    }
}