using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public bool[] isItemReached;
    public float healthPoints;
    public int sceneIndex;
    public Vector2 playerPosition;
    public SerializableDictionary<string, bool> itemsCollected;

    public GameData()
    {
        healthPoints = 100;
        isItemReached = new bool[5];
        sceneIndex = 1;
        playerPosition = Vector2.zero;
        itemsCollected = new SerializableDictionary<string, bool>();
    }
}
