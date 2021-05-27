using System;
using UnityEngine;

namespace ChatSDK
{
    public abstract class AndroidObjectMirror : IDisposable
    {
        protected AndroidJavaObject AJObject { get; private set; }
        internal void FromJava(AndroidJavaObject ajo)
        {
            AJObject = ajo;
            InitFromJava(ajo);
        }

        protected virtual void InitFromJava(AndroidJavaObject ajo)
        {

        }

        public void Dispose()
        {

            if (AJObject != null)
            {
                AJObject.Dispose();
            }

        }
    }

    public class Reflection
    {
        public static T Reflect<T>(AndroidJavaObject ajo) where T : AndroidObjectMirror, new()
        {
            if (ajo == null)
            {
                return default(T);
            }
            try
            {
                var result = new T();
                result.FromJava(ajo);
                return result;
            }
            catch (System.Exception e)
            {
                Debug.LogError("failed to reflect from Java : " + e.Message);
                return default(T);
            }
        }
    }   
}