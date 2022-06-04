using UnityEngine;

namespace Entity.Logger
{
    public static class Logger
    {
        public static void Log(this object c, object message, Object obj = null)
        {
            var typeParts = c.GetType().ToString().Split('.');
            var type = typeParts[^1];
            Debug.Log(type + " => " + message, obj);
        }
        
        public static void LogWarning(this object c, object message, Object obj = null)
        {
            var typeParts = c.GetType().ToString().Split('.');
            var type = typeParts[^1];
            Debug.LogWarning(type + " => " + message, obj);
        }
        
        public static void LogError(this object c, object message, Object obj = null)
        {
            var typeParts = c.GetType().ToString().Split('.');
            var type = typeParts[^1];
            Debug.LogError(type + " => " + message, obj);
        }
    }
}