using System;

namespace ChatSDK
{
    public interface IConnectionDelegate
    {
        void OnConnected();

        void OnDisconnected(int i);

        void OnPong();
    }

}