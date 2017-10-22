using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;


public class Util
{

    private static string tmpKey = "";//等有了用户ID之后用用户ID替换这个值

    /// <summary>
    /// 检查当前版本
    /// </summary>
    /// <returns></returns>
    public static bool CheckViersion()
    {      
        return false;
    }

    /// <summary>
    /// 是否安装之后首次打开
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstUserApp()
    {
        return true;
    }

    #region 数据本地保存
    public void SetString(string key, string value)
    {
        key = key + "_" + tmpKey;
        PlayerPrefs.SetString(key, value);
    }

    public string GetString(string key)
    {
        key = key + "_" + tmpKey;
        return PlayerPrefs.GetString(key, "");
    }

    public void SetInt(string key, int value)
    {
        key = key + "_" + tmpKey;
        PlayerPrefs.SetInt(key, value);
    }

    public int GetInt(string key, int value)
    {
        key = key + "_" + tmpKey;       
        return PlayerPrefs.GetInt(key, 0);
    }
    #endregion

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }

    /// <summary>
    /// 当前是否有网络连接
    /// </summary>
    public static bool IsNet
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 当前是否wifi环境
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }

    /// <summary>
    /// 当前是否是手机网络
    /// </summary>
    public static bool Is4G
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork;
        }
    }
    
}
