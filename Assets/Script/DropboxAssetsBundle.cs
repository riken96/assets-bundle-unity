using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace UnityTutorial.AssetsBundle
{
    public class DropboxAssetsBundle : MonoBehaviour
    {
        public AssetBundle myloadedAssetBundle;
        string url = "https://www.dropbox.com/scl/fi/uc1mne4dnjj0u44gp1lj2/game1?rlkey=q7o7t83n5bsfuhn1oabpxr0vo&dl=1";
        public string bundleName;
        public string dateAndTime
        {
            get
            {
                return PlayerPrefs.GetString("Time", "");
            }
            set
            {
                PlayerPrefs.SetString("Time", value);
            }
        }


        public void btnclick(string URL)
        {
            Chackdata(URL);
            url = URL;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {

                myloadedAssetBundle.Unload(false);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {

                Loaddata();
            }
        }

        public void Chackdata(string URL)
        {
            if (dateAndTime == null)
            {
                dateAndTime = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                if (dateAndTime != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    dateAndTime = DateTime.Now.ToString("yyyy-MM-dd");

                    StartCoroutine(DownloadAssetBundl(URL));

                }
                else
                {
                    if (!File.Exists(Application.persistentDataPath + "/game1.unity3d"))
                    {
                        StartCoroutine(DownloadAssetBundl(URL));
                    }
                    else
                    {
                        Loaddata();
                    }
                }
            }
        }

        public IEnumerator DownloadAssetBundl(string URL)
        {

            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(URL);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                myloadedAssetBundle = bundle;
                Debug.LogError("Downlaoded");
                string[] scenes = myloadedAssetBundle.GetAllScenePaths();

                foreach (string sceneName in scenes)
                {
                    SceneManager.LoadScene(sceneName);
                    Debug.Log(sceneName);
                }

                myloadedAssetBundle.Unload(false);
            }

        }

        public IEnumerator SaveDatas()
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                Debug.LogError(Application.persistentDataPath);
                File.WriteAllBytes(Application.persistentDataPath + "/" + "game1.unity3d", www.downloadHandler.data);
                Debug.LogError("SaveData");
            }
        }

        public void Loaddata()
        {
            string bundlePath = Application.persistentDataPath + "/" + "game1.unity3d";

            // Load the asset bundle
            AssetBundle assetBundle = AssetBundle.LoadFromFile(bundlePath);
            Debug.Log("LoadData");
            if (assetBundle != null)
            {
                myloadedAssetBundle = assetBundle;
                Debug.Log(assetBundle.name);
                string[] scenes = myloadedAssetBundle.GetAllScenePaths();


                foreach (string sceneName in scenes)
                {
                    SceneManager.LoadScene(sceneName);
                    Debug.Log(sceneName);
                }

                myloadedAssetBundle.Unload(false);
                /*// Example: Load an asset from the bundle (assuming it's a prefab)
                GameObject loadedPrefab = assetBundle.LoadAsset<GameObject>("YourPrefabName");

                // Use the loaded asset as needed

                // Remember to unload the asset bundle when done
                assetBundle.Unload(false);*/
            }
            else
            {
                Debug.LogError("Failed to load asset bundle");
            }
        }

    }
}
