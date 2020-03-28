using RHGameCore.Api;
using UnityEngine;

namespace RHGameCore.Tools
{
    public static class Logger
    {

        public static void Log(string tag, string message)
        {
            if (!RHCore.API.Configurations.AllowLogs) return;

            Debug.Log("<color=green><b>[" + tag + "]: </b></color>" + message);
        }

        public static void LogWarning(string tag, string message)
        {
            if (!RHCore.API.Configurations.AllowWarnings) return;

            Debug.Log("<color=yellow><b>[" + tag + "]: </b></color>" + message);
        }

        public static void LogError(string tag, string message)
        {
            if (!RHCore.API.Configurations.AllowErrors) return;

            Debug.Log("<color=red><b>[" + tag + "]: </b></color>" + message);
        }

    }
}