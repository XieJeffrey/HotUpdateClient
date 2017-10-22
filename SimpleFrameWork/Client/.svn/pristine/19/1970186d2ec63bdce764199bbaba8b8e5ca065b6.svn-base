using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class GenFileMD5
{
    private static string androidPath = Application.streamingAssetsPath + "/Android/version.txt";
    public static string iosPath = Application.streamingAssetsPath + "/IOS/version.txt";
    [MenuItem("Assets/GenBundleMD5  % _G")]
    static void GenBundleMD5()
    {
        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
        string orginPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(arr[0]);
        string fileContent = "";
        if (Selection.activeObject.name.CompareTo("Android") == 0 || Selection.activeObject.name.CompareTo("IOS") == 0)
        {
            if (Directory.Exists(orginPath))
            {
                string[] fileInfo = Directory.GetFiles(orginPath);
                for (int i = 0; i < fileInfo.Length; i++)
                { 
                    if (Selection.activeObject.name=="Android"&&(fileInfo[i].Contains("meta") || fileInfo[i].Contains("version")))
                        continue;
                    if (Selection.activeObject.name == "IOS" && (fileInfo[i].Contains("meta") || fileInfo[i].Contains("version")||fileInfo[i].Contains("CodeUpdate")))
                        continue;
                    int lastIndex = fileInfo[i].LastIndexOf('\\') + 1;
                    string fileName = fileInfo[i].Substring(lastIndex, fileInfo[i].Length - lastIndex);
                    fileContent += fileName + "|" + Util.md5file(fileInfo[i]) + "\r\n";
                }
                if (Selection.activeObject.name.CompareTo("Android") == 0)
                {                   
                    File.WriteAllText(androidPath, fileContent);
                }
                if (Selection.activeObject.name.CompareTo("IOS") == 0)
                {
                    File.WriteAllText(iosPath, fileContent);                   
                }
            }
        }
    }
}
