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

        private void Update()
        {
            CheckEndGame();

            // scene test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ++GameManager.Instance.Index;
                if(GameManager.Instance.Index == 3)
                {
                    GameManager.Instance.IsGameOver = true;
                    return;
                }

                SceneManager.LoadScene(GameManager.Instance.Index);
                Debug.Log(GameManager.Instance.Index);

            }

            for (int i = 0; i < Instance.updatableObjects.Count; ++i)
            {
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