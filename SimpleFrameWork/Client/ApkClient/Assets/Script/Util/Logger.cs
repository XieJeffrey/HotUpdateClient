using System.Collections;

public class Logger
{
    public static void Log(string format, params object[] msg)
    {
#if DEBUG
        if (msg.Length == 0)
            UnityEngine.Debug.Log(format);
        else
            UnityEngine.Debug.LogFormat(format, msg);
#endif
    }

    public static void Error(string format, params object[] msg)
    {
#if DEBUG
        if (msg.Length == 0)
            UnityEngine.Debug.LogError(format);
        else
            UnityEngine.Debug.LogFormat(format, msg);
#endif
    }
}
