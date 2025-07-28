using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // Singleton instance
    public List<Item> items = new List<Item>();
    public int space = 20; // Inventory Capacity
    public GameObject inventorySlotPrefab;
    public RectTransform itemsParent;
    public GameObject _inventoryUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        _inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryUI.SetActive(true);
        }
    }

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);

        UpdateUI();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);
            InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
            slotComponent.AddItem(item);
        }
    }
}
