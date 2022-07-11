using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������̺� �ʿ��� �Ŵ��� ���� �� ����� ���̱���

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
                // ������ ����ȭ, �ѹ��� �� �����常 lock ���� �����ϵ��� �Ѵ�.
                lock (lockObject)
                {
                    // ��Ȱ��ȭ�� �Ǿ��ٸ� ���� ����
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