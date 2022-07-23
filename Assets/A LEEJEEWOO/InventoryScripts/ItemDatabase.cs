using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.DP;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.Object.Item;

public class ItemDatabase : MonoSingleton<ItemDatabase>
{
    public List<BaseItem> itemDB = new List<BaseItem>();

    //public GameObject fieldItemPrefab;
    //public Vector3[] pos;

    //private void Start()
    //{
    //    for (int i = 0; i<6; i++)
    //    {
    //        GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
    //        //go.GetComponent<Fieldtems>().SetItem(itemDB[Random.Range(0, 3)]);
    //    }
    //}
}
