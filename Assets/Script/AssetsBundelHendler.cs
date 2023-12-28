using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace UnityTutorial.AssetsBundle
{
    public class AssetsBundelHendler : MonoBehaviour
    {
        public static AssetsBundelHendler instance;
        public string sceneName;
        public string url = "https://goldygamestudio.com/AllGamesData/";
        static AssetBundle assetBundle;


        void Update()
        {

        }

        private IEnumerator LoadScene(string Name)
        {
            using (WWW www = new WWW(url + Name))
            {
                if (!assetBundle)
                {
                    yield return www;
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        Debug.LogError(www.error);
                        yield break;
                    }
                }

                assetBundle = www.assetBundle;
                string[] scenes = assetBundle.GetAllScenePaths();

                foreach (string sceneName in scenes)
                {
                    LoadAssetsBundlesScenes(sceneName);
                    Debug.Log(sceneName);
                }
            }
            yield return new WaitForSeconds(1f);
        }

        public void LoadAssetsBundlesScenes(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void OnclickSceneButton(string sceneName)
        {
            Debug.Log("Done");
            StartCoroutine(LoadScene(sceneName));
        }
    }
}
