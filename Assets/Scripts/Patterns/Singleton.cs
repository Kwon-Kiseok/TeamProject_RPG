namespace HOGUS.Scripts.DP
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (!Exist())
                {
                    instance = new();
                }
                return instance;
            }
        }

        // 인스턴스가 만들어졌는지 확인
        public static bool Exist()
        {
            return instance != null;
        }

        // 인스턴스 생성이력 초기화
        public static void ClearInstance()
        {
            instance = null;
        }
    }
}