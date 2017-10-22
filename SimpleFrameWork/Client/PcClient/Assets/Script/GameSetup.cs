using UnityEngine;
using System.Collections;
using System.IO;

public class GameSetup
{
    public static int Version = 1708192306;//版本号
    public static bool IsUpdate = false;
    private static string streamingAssetPath = "";
    public static string resUrl = "http://localhost/Update/Android/version.txt";//资源url
    public static string appUrl = "http://localhost/Update/Android/appversion.txt";//app版号url

#if ANDROID || UNITY_EDITOR
    public static string updateUrl = "http://localhost/Update/Android/";
#else
    public static string updateUrl = "http://localhost/Update/IOS/";
#endif


    public static string StreamingAssetPath
    {
        get
        {
            if (string.IsNullOrEmpty(streamingAssetPath))
            {
#if ANDROID || UNITY_EDITOR
                streamingAssetPath = Application.streamingAssetsPath + "/" + "Android";
#else
                 streamingAssetPath = Application.streamingAssetsPath + "/" + "IOS";
#endif
            }
            return streamingAssetPath;
        }
    }

    private static string resDataPath = "";
    public static string ResDataPath
    {
        get
        {
            if (string.IsNullOrEmpty(resDataPath))
            {
                resDataPath = Application.persistentDataPath + "/" + "Resource";
                if (Directory.Exists(resDataPath) == false)
                    Directory.CreateDirectory(resDataPath);
            }
            return resDataPath;
        }
    }

}
