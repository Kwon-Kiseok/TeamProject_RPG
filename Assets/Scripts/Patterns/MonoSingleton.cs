using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모노비헤이비어가 필요한 매니저 생성 시 사용할 모노싱글톤

namespace HOGUS.Scripts.DP
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static object lockObject = new();
        private static T instance = null;
        private static bool IsQuitting = false;

        public static T Instance
        {
            get
            {
                // 쓰레드 안전화, 한번에 한 쓰레드만 lock 블럭을 실행하도록 한다.
                lock (lockObject)
                {
                    // 비활성화가 되었다면 새로 생성
                    if (IsQuitting)
                    {
                        return null;
                    }

                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }

        private void OnDisable()
        {
            IsQuitting = true;
            instance = null;
        }

        private void OnApplicationQuit()
        {
            IsQuitting = true;
            instance = null;
        }

        private void OnDestroy()
        {
            IsQuitting = true;
            instance = null;
        }
    }
}