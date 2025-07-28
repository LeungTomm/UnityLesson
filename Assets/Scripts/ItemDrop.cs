using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Item[] items;
    public Transform spawnPoint;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        float totalDroprate = 0;

        foreach (var item in items)
        {
            totalDroprate += item.dropRate;
        }

        float randomValue = Random.Range(0, totalDroprate);

        foreach (var item in items)
        {
            if (randomValue < item.dropRate)
            {
                Instantiate(item.itemPrefab, spawnPoint);
                break;
            }

            randomValue -= item.dropRate;
        }
    }
}
