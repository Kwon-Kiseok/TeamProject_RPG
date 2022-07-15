using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;

namespace HOGUS.Scripts.DP
{
    [System.Serializable]
    public class ObjectInfo
    {
        public string objectName;   // 오브젝트의 이름
        public GameObject prefab;   // 사용될 프리팹
        public int count;           // 생성될 오브젝트의 수량
    }

    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField]
        private ObjectInfo[] objectInfos = null;

        [Header("오브젝트 풀의 부모 위치")]
        Transform trParent;

        [Header("오브젝트 배열 큐")]
        public List<Queue<GameObject>> objectPoolList;

        private void Start()
        {
            GameManager.Instance.objectPool += SelectPoolList;
            GameManager.Instance.returnPool += ReturnPool;

            objectPoolList = new();
            ObjectPoolState();
        }

        private void ObjectPoolState()
        {
            if (objectPoolList != null)
            {
                for (int i = 0; i < objectInfos.Length; i++)
                {
                    objectPoolList.Add(InsertQueue(objectInfos[i]));
                }
            }
        }

        private Queue<GameObject> InsertQueue(ObjectInfo objectInfo)
        {
            Queue<GameObject> queue = new();

            for (int i = 0; i < objectInfo.count; i++)
            {
                GameObject objectClone = Instantiate(objectInfo.prefab) as GameObject;
                objectClone.SetActive(false);
                objectClone.transform.SetParent(trParent);
                queue.Enqueue(objectClone);
            }
            return queue;
        }

        // 오브젝트 풀에서 선택된 인덱스의 객체를 가져가 사용할 때
        private GameObject SelectPoolList(int idx)
        {
            return objectPoolList[idx].Dequeue();
        }
        // 사용한 인덱스의 풀 객체 반납할 때 사용
        private void ReturnPool(int idx, GameObject poolObject)
        {
            objectPoolList[idx].Enqueue(poolObject);
        }

        // test code
        // 0번 리스트에 저장된 오브젝트를 가져옴
        // GameObject damagePrefab = GameManager.Instance.objectPool.Invoke(0);
        // 0번 리스트에 오브젝트를 반납
        // GamaManager.Instance.returnPool.Invoke(0, this.gameObject);
    }
}