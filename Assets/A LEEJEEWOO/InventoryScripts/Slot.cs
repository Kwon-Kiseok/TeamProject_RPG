using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HOGUS.Scripts.Inventory;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.Enums;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;

    public BaseItem _baseItem;

    public BaseItem BaseItem
    {
        get { return _baseItem; }
        set
        {
            _baseItem = value;
            if (_baseItem != null)
            {
                image.sprite = BaseItem.sprite;

                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void ItemOnClick()
    {
        Inventory.Instance.BaseItem = BaseItem;
    }

    //public void ShowItemData()
    //{
    //    //
    //    Debug.Log("BaseItem.id: " + BaseItem.id);
    //    Debug.Log("(int)ItemType.armor: " + (int)ItemType.armor);
    //    Debug.Log("(int)ItemType.None: " + (int)ItemType.None);
    //    Debug.Log("//////////////////////////////////////////////////");

    //    if (BaseItem.id >= (int)ItemType.armor && BaseItem.id < (int)ItemType.None)
    //    {
    //        ArmorItem val = (ArmorItem)BaseItem;
            
    //        Debug.Log(val.itemName);
    //        //Debug.Log(val.defense);
    //        //Debug.Log(val.requireLevel);

    //    }
    //    else if (BaseItem.id >= (int)ItemType.weapon && BaseItem.id < (int)ItemType.armor)
    //    {
    //        WeaponItem val = (WeaponItem)BaseItem;
    //        Debug.Log(val.attackSpeed);
    //    }        
    //}

    
}