using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private int numberOfRooms = 20;
    [SerializeField] private LayerMask roomLayer;
    [SerializeField] private Vector3 checkVolumeSize = new Vector3(9.5f, 5f, 9.5f);
    [SerializeField] private Transform mapContainer;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        foreach (Transform child in mapContainer)
        {
            Destroy(child.gameObject);
        }

        if (roomPrefabs.Count == 0)
        {
            Debug.LogError("Error: The Room Prefab list is empty!");
            return;
        }

        GameObject previousRoom = Instantiate(roomPrefabs[0], mapContainer.position, Quaternion.identity, mapContainer);

        for (int i = 1; i < numberOfRooms; i++)
        {
            // 1. Find the parent object of all exits from the last room
            Transform exitPointsParent = previousRoom.transform.Find("ExitPoints");

            if (exitPointsParent.childCount == 0)
            {
                Debug.Log($"Room #{i - 1} has no exits. Stopping generation.");
                break;
            }

            // 2. Randomly select one of the exits
            int randomIndex = Random.Range(0, exitPointsParent.childCount);
            Transform exitPoint = exitPointsParent.GetChild(randomIndex);

            // 3. Randomly select a new room type
            GameObject newRoomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

            // 4. Calculate the target rotation and position for the new room
            Quaternion targetRotation = Quaternion.LookRotation(-exitPoint.forward, Vector3.up);
            Vector3 entryPointOffset = newRoomPrefab.transform.Find("EntryPoint").localPosition;
            Vector3 rotatedEntryPointOffset = targetRotation * entryPointOffset;
            Vector3 targetPosition = exitPoint.position - rotatedEntryPointOffset;

            // 5. Collision Detection
            if (Physics.CheckBox(targetPosition, checkVolumeSize / 2f, targetRotation, roomLayer))
            {
                Debug.Log($"Overlap detected at room #{i}. Stopping generation.");
                break; // exit the for loop
            }

            GameObject newRoom = Instantiate(newRoomPrefab, targetPosition, targetRotation, mapContainer);

            previousRoom = newRoom;
        }

    }
}