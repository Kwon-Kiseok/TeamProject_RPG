using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Manager
{
    public interface IUpdatableObject
    {
        void OnEnable();
        void OnDisable();
        void OnUpdate();
    }

    // 최적화 관련 : https://gist.github.com/Curookie/e863c94689268eff1d91de6d4a16fab1

    [HelpURL("https://blog.unity.com/kr/technology/1k-update-calls")]
    public class UpdateManager : MonoSingleton<UpdateManager>
    {
        List<IUpdatableObject> updatableObjects = new();

        // Update
        private void Update()
        {
            CheckEndGame();           

            for (int i = 0; i < Instance.updatableObjects.Count; ++i)
            {
                // updatableObjects로 등록된 객체들의 업데이트를 수행해줌
                Instance.updatableObjects[i].OnUpdate();
            }
        }

        public void RegisterUpdatableObject(IUpdatableObject @object)
        {
            if(!Instance.updatableObjects.Contains(@object))
            {
                Instance.updatableObjects.Add(@object);
            }
        }

        public void DeregisterUpdatableObject(IUpdatableObject @object)
        {
            if(Instance.updatableObjects.Contains(@object))
            {
                Instance.updatableObjects.Remove(@object);
            }
        }

        private void CheckEndGame()
        {
            if (GameManager.Instance.IsGameOver)
            {
                DataManager.Instance.Save();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                return;
            }
        }
    }
}