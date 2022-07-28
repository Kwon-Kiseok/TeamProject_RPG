using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.Inventory;
using HOGUS.Scripts.Character;

public class DropItem : MonoBehaviour, IUpdatableObject
{
    private float rotateSpeed = 15f;

    public List<BaseItem> itemTableList = new();
    public BaseItem item;
    private bool isTaken = false;

    public bool IsTaken { get { return isTaken; } set { isTaken = value; } }

    public void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
        item = itemTableList[Random.Range(0, itemTableList.Count)];
    }

    public void OnFixedUpdate(float deltaTime)
    {
        transform.Rotate(Vector3.up * rotateSpeed * deltaTime);
    }

    public void OnUpdate(float deltaTime)
    {
        if(IsTaken)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<AttackCollision>() != null)
                return;
            other.gameObject.GetComponent<Player>().joystick.takeButtonGO.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<AttackCollision>() != null)
                return;
            other.gameObject.GetComponent<Player>().joystick.takeButtonGO.SetActive(false);
        }
    }
}
