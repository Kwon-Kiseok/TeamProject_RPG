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

    public Slot[] slots;
    public Transform slotHolder;

    Inventory inven;

    public GameObject SlotBtn;

    private void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();

        InventoryPanel.SetActive(false);
        StatusPanel.SetActive(false);

        SlotBtn.GetComponentInChildren<Button>().enabled = false;
    }

    public void InventoryOn()
    {
        activeInventory = true;

        if (activeInventory == true)
        {
            InventoryPanel.SetActive(true);
        }
    }

    public void StatusOn()
    {
        SlotBtn.GetComponentInChildren<Button>().enabled = true;
        activeStatus = true;

        if(activeStatus == true)
        {
            StatusPanel.SetActive(true);
        }
    }

    public void InventoryEXIT()
    {
        SlotBtn.GetComponentInChildren<Button>().enabled = false;
        activeInventory = false;

        if (activeInventory == false)
        {
            InventoryPanel.SetActive(false);
            StatusPanel.SetActive(false);
        }
    }
}
