using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{

    internal sealed class MultiDeviceListener : MonoBehaviour
    {

        internal List<IMultiDeviceDelegate> delegater;

        internal void OnContactMultiDevicesEvent(string jsonString)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }

        internal void OnGroupMultiDevicesEvent(string jsonString)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }

        internal void OndisturbMultiDevicesEvent(string jsonString)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }

    }
}