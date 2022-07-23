using HOGUS.Scripts.Data;
using HOGUS.Scripts.DP;
using HOGUS.Scripts.Object.Item;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Reference by : https://velog.io/@starkshn/Data-Manager
// Addressable 관련 : https://seonbicode.tistory.com/52

namespace HOGUS.Scripts.Manager
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager : Singleton<DataManager>
    {
        // Temp Example Dict
        // 필요한 데이터의 딕셔너리가 존재할 경우 Data.Contents에서 추가 후 Dictionary 생성해서 사용
        public Dictionary<int, BaseItem> itemDict { get; private set; } = new();

        private TextAsset jsAsset;

        public void Init()
        {
            Debug.Log("Load and Init Data");
            itemDict = LoadJson<ItemData, int, BaseItem>("ItemData").MakeDict();
            
        }

        public void Save()
        {
            if (jsAsset != null)
                File.WriteAllText(Application.persistentDataPath + "/ItemData_Backup.json", jsAsset.text);
            Debug.Log("Save Data");
        }

        Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            //TextAsset textAsset = Resources.Load<TextAsset>($"Datas/{path}");

            var jsHandle = Addressables.LoadAssetAsync<TextAsset>($"Data/{path}");
            jsHandle.WaitForCompletion();   // 동기 처리
            // do work
            TextAsset textAsset = JsonHandle_Complete(jsHandle, path);

            // Release
            Addressables.Release(jsHandle);

            return JsonUtility.FromJson<Loader>(textAsset.text);
        }

        private TextAsset JsonHandle_Complete(AsyncOperationHandle<TextAsset> handle, string path)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                jsAsset = handle.Result;
                Debug.Log(jsAsset);
                return jsAsset;
            }

            return null;
        }
    }
}