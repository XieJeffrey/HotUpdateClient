using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportAssetBundles
{
    private string subUrl = "/Asset/Streaming";
    [MenuItem("Assets/(Android)Build AssetBundle From Selection - Track dependencies %_e")]
    static void ExportResource()
    {
        string playform = "Android";

        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
        string orginPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(arr[0]);
        orginPath = orginPath.Replace(Application.dataPath, "");

        string targetFile = Application.streamingAssetsPath + "/" + playform + "/" + Selection.activeObject.name + ".bundle";

        string targetPath = targetFile.Replace(Selection.activeObject.name, "");
        if (Directory.Exists(targetPath) == false)
        {
            Directory.CreateDirectory(targetPath);
        }

        // Build the resource file from the active selection.
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, targetFile, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        Selection.objects = selection;

    }

    [MenuItem("Assets/(IOS)Build AssetBundle From Selection - Track dependencies %_r")]
    static void ExportResource1()
    {
        string playform = "IOS";

        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
        string orginPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(arr[0]);
        orginPath = orginPath.Replace(Application.dataPath, "");

        string targetFile = Application.streamingAssetsPath + "/" + playform + "/" + Selection.activeObject.name + ".bundle";

        string targetPath = targetFile.Replace(Selection.activeObject.name, "");
        if (Directory.Exists(targetPath) == false)
        {
            Directory.CreateDirectory(targetPath);
        }

        // Build the resource file from the active selection.
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, targetFile, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iOS);
        Selection.objects = selection;

    }


}