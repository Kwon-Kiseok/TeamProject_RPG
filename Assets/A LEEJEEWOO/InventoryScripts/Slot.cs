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
}