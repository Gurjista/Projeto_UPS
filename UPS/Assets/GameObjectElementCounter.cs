using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectElementCounter : MonoBehaviour
{
    public List<GameObject> prefabs;

    void Start()
    {
        // Get all the prefabs inside the container
        foreach (Transform child in transform)
        {
            // Check if the child is a prefab
            if (child.gameObject.scene.rootCount == 0)
            {
                // Add the prefab to the list
                prefabs.Add(child.gameObject);
            }
        }

        // Print the number of prefabs to the console
        Debug.Log("Number of prefabs: " + prefabs.Count);
    }
}