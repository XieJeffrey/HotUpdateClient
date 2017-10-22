using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;

public class UpdateHelper
{
    private static int copyIndex = 0;
    private static int copyFileCount = 0;

    /// <summary>
    /// 把资源从安装包复制到数据包
    /// </summary>
    /// <param name="callBack"></param>
    public static void CopyResToPersistentDataPath(Action callBack)
    {
        copyIndex = 0;
        copyFileCount = Directory.GetFiles(GameSetup.StreamingAssetPath).Length;
        if (copyFileCount == 0)
        {
            Logger.Log("StreamAsset 找不到资源");
            Application.Quit();
            return;
        }

        Main.mono.StartCoroutine(DoCopyResToPersistentDataPath(callBack));
    }

    /// <summary>
    /// 获取资源解压进度
    /// </summary>
    /// <returns></returns>
    public static float GetCopyResProgress()
    {
        if (copyFileCount == 0)
            return 0;
        else
            return (float)copyIndex / copyFileCount;
    }

    private static IEnumerator<int> DoCopyResToPersistentDataPath(Action func)
    {
        string[] files = Directory.GetFiles(GameSetup.StreamingAssetPath);

        for (int i = 0; i < files.Length; i++)
        {
            int index = files[i].LastIndexOf('\\') + 1;
            string fileName = files[i].Substring(index, files[i].Length - index);
            fileName = Application.persistentDataPath + "/" + fileName;
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.Copy(files[i], fileName);
            while (File.Exists(fileName) == false)
            {
                yield return 0;
            }
            yield return i;
        }
        func();
    }

    /// <summary>
    /// 更新游戏资源
    /// </summary>
    /// <param name="callBack"></param>
    public static void UpdateAssetBundle(Action callBack)
    {
        if (Util.IsNet == false)
        {
            return;
        }

        if (Util.IsWifi == false)
        {
            return;
        }
    }

    /// <summary>
    /// 检查app是否需要重新安装
    /// </summary>
    /// <param name="callBack"></param>
    public static void CheckAppVersion(Action<bool> callBack)
    {
        Main.mono.StartCoroutine(DoCheckAppVersion(callBack));
    }

    static IEnumerator DoCheckAppVersion(Action<bool> callBack)
    {
        string tempFile = Application.persistentDataPath + "/tempVerson.txt";
#if IOS
        string versionFile=Application.streamingAssetsPath+"/IOS/version.txt";
#else
        string versionFile = Application.streamingAssetsPath + "/Android/version.txt";
#endif
        WWW www = new WWW(GameSetup.appUrl);
        yield return www;
        if (www.isDone)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                byte[] bytes = www.bytes;
                string str = System.Text.Encoding.UTF8.GetString(bytes);
                Logger.Log("--------------检查app版本->  local:{0}   server:{1}", GameSetup.Version, int.Parse(str));
                callBack(GameSetup.Version >= int.Parse(str));
            }
            else
                Logger.Error(www.error);
        }
    }

    public static void CopyResToLocal(Action callBack)
    {
        Main.mono.StartCoroutine(DoCopyResToLocal(callBack));
    }

    static IEnumerator DoCopyResToLocal(Action callBack)
    {
        WaitForEndOfFrame weof = new WaitForEndOfFrame();
        string[] fileInfo = Directory.GetFiles(GameSetup.StreamingAssetPath);
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Contains("version"))
                continue;
            int lastIndex = fileInfo[i].LastIndexOf("\\") + 1;
            string filename = fileInfo[i].Substring(lastIndex, fileInfo[i].Length - lastIndex);
            try
            {
                Logger.Log("--------------文件复制中:{0}", filename);                 
                File.Copy(fileInfo[i], GameSetup.ResDataPath + "/" + filename,true);
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
            }
            yield return weof;
        }
        PlayerPrefs.SetInt("AppVersion", GameSetup.Version);
        callBack();
    }

    public static void UpdateBundleRes(Action callBack)
    {
        Main.mono.StartCoroutine(DoUpdateBundleRes(callBack));
    }

    static IEnumerator DoUpdateBundleRes(Action callBack)
    {
        Logger.Log("--------------资源比对更新中......");
        Dictionary<string, string> md5Dic = new Dictionary<string, string>();
        Dictionary<string, string> newMd5Dic = new Dictionary<string, string>();
        #region 获取本地资源MD5文件
        string path = GameSetup.ResDataPath + "/version.txt";
        if (File.Exists(path))
        {

#if UNITY_EDITOR          
            WWW www = new WWW("file:/" + path);
#else		   
		    WWW www = new WWW(url);
#endif
            string[] serverMD5File = new string[] { };
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (www.isDone)
                {
                    byte[] bytes = www.bytes;
                    string str = System.Text.Encoding.UTF8.GetString(bytes);                 
                    string[] md5List = System.Text.Encoding.UTF8.GetString(bytes).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < md5List.Length; i++)
                    {
                        string[] tmp = md5List[i].Split('|');
                        tmp[1] = tmp[1].Replace("\n", "");
                        md5Dic.Add(tmp[0], tmp[1].Replace("\r","").Trim());     
                    }
                }
            }
            else
            {
                Logger.Error(www.error);
            }
            #endregion

            #region 获取服务器资源MD5文件

            www = new WWW(GameSetup.resUrl);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (www.isDone)
                {
                    string tmpStr = System.Text.Encoding.UTF8.GetString(www.bytes);
                    serverMD5File = tmpStr.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < serverMD5File.Length; i++)
                    {
                        string[] tmp = serverMD5File[i].Split('|');
                        tmp[1] = tmp[1].Replace("\n", "");
                        newMd5Dic.Add(tmp[0], tmp[1].Replace("\r","").Trim());
                    }
                }
            }
            #endregion

            #region 文件比对并更新
            List<string> updateFileList = new List<string>();
            foreach (var tmp in newMd5Dic)
            {
                if (md5Dic.ContainsKey(tmp.Key))
                {                  
                    if (md5Dic[tmp.Key].CompareTo(tmp.Value) != 0)
                    {
                        updateFileList.Add(tmp.Key);
                    }
                }
                else
                    updateFileList.Add(tmp.Key);
            }

            for (int i = 0; i < updateFileList.Count; i++)
            {
                string filePath = GameSetup.ResDataPath + "/" + updateFileList[i];
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                www = new WWW(GameSetup.updateUrl + "/" + updateFileList[i]);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    if (www.isDone)
                    {
                        File.WriteAllBytes(filePath, www.bytes);
                        Logger.Log("{0} update finish....", filePath);
                    }
                }
            }

            File.WriteAllLines(GameSetup.ResDataPath + "/version.txt", serverMD5File);
            callBack();
            #endregion

        }
    }


}
