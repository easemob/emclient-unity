using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public interface IConnectionDelegate
    {
        void OnConnected();

        void OnDisconnected(int i);
    }

}