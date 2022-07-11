using System;

namespace HOGUS.Scripts.DP
{
    public class Singleton<T> where T : class, new()
    {
        // Lazy -> ���� ��� ������ �ƴ�, ������ �� �������� ��
        // Lazy �̱������� �ν��Ͻ� ������ ���� ��Ƽ ������ ȯ�� �� thread-safety ����
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

        // �ν��Ͻ��� ����������� Ȯ��
        public static bool Exist()
        {
            return instance != null && instance.IsValueCreated;
        }

        // �ν��Ͻ� �����̷� �ʱ�ȭ
        public static void ClearInstance()
        {
            instance = null;
        }
    }
}