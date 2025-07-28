using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // or other.Tag("Player")
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Picked up: " + item.itemName);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
