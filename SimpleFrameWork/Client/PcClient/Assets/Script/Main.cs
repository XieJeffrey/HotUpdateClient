using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Text;
using System.IO;

public class Main : MonoBehaviour
{
    public static MonoBehaviour mono;
    public static Assembly assembly;

    void Awake()
    {
        gameObject.AddComponent<OutLog>();
    }

    void Start()
    {
        Application.logMessageReceived += OutLog.HandleLog;
        var files = Directory.GetFiles(Application.dataPath);
        mono = this;
        Logger.Log("=====================MainImpl is awake===========");
        if (GameSetup.IsUpdate)
            UpdateHelper.CheckAppVersion(OnCheckAppVersion);
        else
            GameInit();
    }

    void OnCheckAppVersion(bool result)
    {
        if (result)
        {
            int localVersion = PlayerPrefs.GetInt("AppVersion", 0);
            Logger.Log("--------检查资源版本----> local:{0}   app:{1}", localVersion, GameSetup.Version);
            if (localVersion == 0 || GameSetup.Version > localVersion)
            {
                //为首次安装或者安装包的资源高于本地版本资源的，将安装包的资源解压的本地去
                UpdateHelper.CopyResToLocal(OnCopyResFinish);
            }
            else
            {
                //执行资源检查并更新
                UpdateHelper.UpdateBundleRes(OnUpdateBundleRes);
            }

        }
        else
        {
            Logger.Error("----app版本过低,请更新app------");
            Application.Quit();
        }
    }


    /// <summary>
    /// 安装包资源解压完成
    /// </summary>
    void OnCopyResFinish()
    {
        Logger.Log("---------------资源解压完毕-------------------");
        UpdateHelper.UpdateBundleRes(OnUpdateBundleRes);
    }

    void OnUpdateBundleRes()
    {
        Logger.Log("----------------资源更新完成-------------------");
        LoadDLL();
        GameInit();
    }

    /// <summary>
    /// 加载脚本资源
    /// </summary>
    void LoadDLL()
    {
#if IOS
        return;
#endif
        string path = ""; ;
        if (GameSetup.IsUpdate)
            path = Application.persistentDataPath + "/Resource/CodeUpdate.bundle";
        else
            path = Application.streamingAssetsPath + "/Android/CodeUpdate.bundle";


        if (File.Exists(path))
        {
            FileStream stream = File.OpenRead(path);
            byte[] dllByte = new byte[stream.Length];
            int numByteToRead = (int)stream.Length;
            int numByteRead = 0;
            while (numByteToRead > 0)
            {
                int n = stream.Read(dllByte, numByteRead, numByteToRead);
                if (n == 0)
                    break;
                numByteRead += n;
                numByteToRead -= n;
            }
            assembly= Assembly.Load(dllByte);
            Logger.Log("--------------------DLL加载成功-----------------");

           
        }
    }


    /// <summary>
    /// 初始化进入游戏
    /// </summary>
    void GameInit()
    {       
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i].Name == "ResourceManager")
            {
                Debug.Log("Create");
                assembly.CreateInstance(("ResourceManager"));
            }
        }
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= OutLog.HandleLog;
    }

}
