using System;
using UnityEngine;
using ChatSDK;

public class ConnectionDelegate : IConnectionDelegate
{ 

    public void OnConnected()
    {
        Debug.Log("Client connected.");
    }

    public void OnDisconnected(int info)
    {
        Debug.Log($"Client disconnected with {info}.");
    }
}
