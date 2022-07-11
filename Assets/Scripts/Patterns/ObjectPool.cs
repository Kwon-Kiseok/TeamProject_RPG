using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.DP
{

    // 오브젝트 풀은 변경 예정

    public class ObjectInfo
    {
        public GameObject prefab;
        public int maxCount;
        public Transform trParent;
    }

    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField]
        private ObjectInfo[] objectInfos = null;

        public Queue<GameObject> objectQueue = new();

        private void Start()
        {
            objectQueue = InsertQueue(objectInfos[0]);
        }

        private Queue<GameObject> InsertQueue(ObjectInfo @object)
        {
            Queue<GameObject> queue = new();
            for(int i = 0; i < objectInfos.Length; i++)
            {
                GameObject clone = Instantiate(@object.prefab, transform.position, Quaternion.identity);
                clone.SetActive(false);
                if(@object.trParent != null)
                {
                    clone.transform.SetParent(@object.trParent);
                }
                else
                {
                    clone.transform.SetParent(this.transform);
                }

                queue.Enqueue(clone);
            }

            return queue;
        }
    }
}