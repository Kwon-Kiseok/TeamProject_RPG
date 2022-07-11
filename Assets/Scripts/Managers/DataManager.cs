using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


using HOGUS.Scripts.DP;
using HOGUS.Scripts.Data;

// Reference by : https://velog.io/@starkshn/Data-Manager
// Addressable ���� : https://seonbicode.tistory.com/52

namespace HOGUS.Scripts.Manager
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager : Singleton<DataManager>
    {
        // Temp Example Dict
        // �ʿ��� �������� ��ųʸ��� ������ ��� Data.Contents���� �߰� �� Dictionary �����ؼ� ���
        public Dictionary<int, Item> itemDict { get; private set; } = new();

        public void Init()
        {
            itemDict = LoadJson<ItemData, int, Item>("ItemData").MakeDict();
        }

        Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            //TextAsset textAsset = Resources.Load<TextAsset>($"Datas/{path}");

            var jsHandle = Addressables.LoadAssetAsync<TextAsset>($"Datas/{path}");
            jsHandle.WaitForCompletion();   // ���� ó��
            // do work
            TextAsset textAsset = JsonHandle_Complete(jsHandle, path);

            // Release
            Addressables.Release(jsHandle);

            return JsonUtility.FromJson<Loader>(textAsset.text);
        }

        private TextAsset JsonHandle_Complete(AsyncOperationHandle<TextAsset> handle, string path)
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                var jsAsset = handle.Result;
                File.WriteAllText(path, jsAsset.text);
                return jsAsset;
            }

            return null;
        }
    }
}