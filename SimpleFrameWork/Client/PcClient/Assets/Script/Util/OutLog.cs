using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class OutLog : MonoBehaviour
{ 

    static List<string> mWriteTxt = new List<string>();
    private string outpath;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
            outpath = Application.persistentDataPath + "/log.txt";
        else
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
                Directory.CreateDirectory(Application.streamingAssetsPath);
            outpath = Application.streamingAssetsPath + "/log.txt";
        }
        
        if (File.Exists(outpath))
        {
            File.Delete(outpath);
        }
        File.Create(outpath);
    }

    void Update()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中哦给你写入文件。
        if (mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            string str = "";
            foreach (string t in temp)
            {
                str += t;             
            }
            File.AppendAllText(outpath, str);
            mWriteTxt.Clear();
        }
    }

    public static void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(logString);
        mWriteTxt.Add(stackTrace);
        mWriteTxt.Add("\n");
    }
}