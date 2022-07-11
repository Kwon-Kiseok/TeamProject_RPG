using System;

namespace HOGUS.Scripts.DP
{
    public class Singleton<T> where T : class, new()
    {
        // Lazy -> 선언 즉시 생성이 아닌, 접근할 때 생성시켜 줌
        // Lazy 싱글톤으로 인스턴스 생성에 대한 멀티 쓰레드 환경 시 thread-safety 보장
        private static Lazy<T> instance =
            new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                if(!Exist())
                {
                    var inst = new T();
                    instance = new Lazy<T>(() => inst);
                }
                return instance.Value;
            }
        }

        // 인스턴스가 만들어졌는지 확인
        public static bool Exist()
        {
            return instance != null && instance.IsValueCreated;
        }

        // 인스턴스 생성이력 초기화
        public static void ClearInstance()
        {
            instance = null;
        }
    }
}