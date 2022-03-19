using UnityCRUD.Scripts.Data.ScriptableObjects;
using UnityCRUD.Scripts.Views;
using UnityEngine;

namespace UnityCRUD.Scripts.Controllers
{
    public static class CrudLogger
    {
        private static string logPrefix = "[-CRUD-]: ";
        public static void Log(string message, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.Log(logPrefix + message);
        }
        public static void Log(object message, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.Log(logPrefix + message);
        }
        public static void Log(string message, Object context, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.Log(logPrefix + message, context);
        }
        public static void LogWarning(string message, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.LogWarning(logPrefix + message);
        }
        public static void LogWarning(string message, Object context, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.LogWarning(logPrefix + message, context);
        }
        public static void LogError(string message, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.LogError(logPrefix + message);
        }
        public static void LogError(string message, Object context, ServerSettings.DebuglevelEnum debuglevel = ServerSettings.DebuglevelEnum.Verbose)
        {
            if(MainView.Instance.serverSettings.DebugLog && debuglevel >= MainView.Instance.serverSettings.Debuglevel)
                Debug.LogError(logPrefix + message, context);
        }
    }
}