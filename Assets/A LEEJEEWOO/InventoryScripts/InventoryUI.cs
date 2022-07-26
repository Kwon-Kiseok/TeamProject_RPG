using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HOGUS.Scripts.Inventory;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject StatusPanel;

    bool activeInventory = false;
    bool activeStatus = false;

    private void Start()
    {
        InventoryPanel.SetActive(false);
        StatusPanel.SetActive(false);
    }

    public void InventoryOn()
    {
        if (activeInventory)
        {
            InventoryEXIT();
            return;
        }

        activeInventory = true;
        InventoryPanel.SetActive(true);
    }

    public void StatusOn()
    {
        if (activeStatus)
        {
            activeStatus = false;
            StatusPanel.SetActive(false);
            return;
        }

        activeStatus = true;
        StatusPanel.SetActive(true);
    }

    public void InventoryEXIT()
    {
        activeInventory = false;
        InventoryPanel.SetActive(false);
        activeStatus=false;
        StatusPanel.SetActive(false);
    }
}
