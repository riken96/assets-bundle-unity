using System;
using UnityEditor;
using UnityEngine;

namespace UnityTutorial.AssetsBundle
{
    public class CreateAssetsBundle
    {
        [MenuItem("Assets/Create Asset Bundles")]
        private static void BuildAllAssetBundle()
        {
            string assetsBundleDiractoryPath = Application.dataPath + "/../AssetsBundles";

            try
            {
                BuildPipeline.BuildAssetBundles(assetsBundleDiractoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}

