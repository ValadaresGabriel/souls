using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectManager : MonoBehaviour
{
    public static EnableGameObjectManager Instance { get; private set; }

    public List<GameObject> gameObjectsToEnable = new();
    public int index = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
