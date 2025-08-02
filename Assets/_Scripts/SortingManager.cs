using UnityEngine;
using System.Collections.Generic;

public class SortingManager : MonoBehaviour
{
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    // This would be called by an InputManager or GameManager after a drop
    public void OnVisitorGroupPlaced(TileData targetTile, List<VisitorData> placedVisitors)
    {
        targetTile.AddVisitors(placedVisitors);
        
        // Start the chain reaction logic
        ProcessChainReaction(targetTile);
    }

    private void ProcessChainReaction(TileData initialTile)
    {
        Queue<TileData> processQueue = new Queue<TileData>();
        processQueue.Enqueue(initialTile);

        while (processQueue.Count > 0)
        {
            TileData currentTile = processQueue.Dequeue();
            VisitorColor? tileColor = currentTile.TileColor;

            // We only process single-color tiles, as they are the ones that "pull"
            if (tileColor == null) continue;

            List<TileData> neighbors = gridManager.GetNeighbors(currentTile.GridPosition);

            // Sort neighbors to prioritize pulling from those with more matching visitors
            neighbors.Sort((a, b) => 
                b.Visitors.FindAll(v => v.Color == tileColor).Count
                .CompareTo(a.Visitors.FindAll(v => v.Color == tileColor).Count)
            );

            foreach (var neighbor in neighbors)
            {
                if (neighbor.IsEmpty || neighbor.IsFull || neighbor.TileColor == null) continue;

                List<VisitorData> visitorsToPull = neighbor.Visitors.FindAll(v => v.Color == tileColor);

                if (visitorsToPull.Count > 0)
                {
                    // Remove visitors from neighbor
                    neighbor.Visitors.RemoveAll(v => v.Color == tileColor);

                    // Add visitors to current tile
                    currentTile.AddVisitors(visitorsToPull);

                    // The neighbor has changed, so it might need processing
                    if (!processQueue.Contains(neighbor))
                    {
                        processQueue.Enqueue(neighbor);
                    }
                }
            }

            // Check if the current tile is now full
            if (currentTile.IsFull && currentTile.TileColor != null)
            {
                CarriageManager.Instance.OnTileFilled(currentTile);
                // The tile is now empty and doesn't need further processing in this chain
            }
        }
    }
}
