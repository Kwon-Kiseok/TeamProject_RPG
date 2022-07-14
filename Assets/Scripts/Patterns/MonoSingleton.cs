using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모노비헤이비어가 필요한 매니저 생성 시 사용할 모노싱글톤
// protected ClassName() {} 선언으로 비 싱글톤 생성자 사용 방지
namespace HOGUS.Scripts.DP
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        private static object lockObject = new ();
        private static bool applicationIsQuit = false;

        public static T Instance
        {
            get
            {
                if (applicationIsQuit)
                    return null;

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        // 인스턴스 존재 여부 확인
                        instance = (T)FindObjectOfType(typeof(T));

                        // 생성되지 않았다면 인스턴스 생성
                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        }
                    }
                }
                return instance;
            }
        }

        protected void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnDestroy()
        {
            applicationIsQuit = true;
        }
    }
}