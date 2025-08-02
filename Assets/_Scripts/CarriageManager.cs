using System.Collections.Generic;
using UnityEngine;

public class CarriageManager : MonoBehaviour
{
    public static CarriageManager Instance { get; private set; }

    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private Transform carriageParent;

    private Queue<GameObject> carriageQueue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetupCarriages(int count)
    {
        carriageQueue = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject carriageGO = Instantiate(carriagePrefab, carriageParent);
            // Position carriages in a line
            carriageGO.transform.position = new Vector3(i * 3.0f, -5.0f, 0);
            carriageQueue.Enqueue(carriageGO);
        }
    }

    public void OnTileFilled(TileData filledTile)
    {
        if (carriageQueue.Count > 0)
        {
            GameObject targetCarriage = carriageQueue.Peek();
            // Animate visitors moving from tile to carriage
            Debug.Log($"Filling carriage with {filledTile.TileColor} visitors.");
            
            // After filling, maybe the carriage moves away
            // and the next one comes to the front.
            // For now, we'll just dequeue it.
            carriageQueue.Dequeue();

            // Check for win condition
            if (carriageQueue.Count == 0)
            {
                GameManager.Instance.WinGame();
            }
        }
    }
}
