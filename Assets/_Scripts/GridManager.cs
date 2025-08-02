using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform gridParent;

    private Dictionary<Vector2Int, TileData> grid;

    void Start()
    {
        CreateGrid(GameManager.Instance.currentLevelData);
    }

    public void CreateGrid(LevelData levelData)
    {
        grid = new Dictionary<Vector2Int, TileData>();
        for (int x = 0; x < levelData.gridWidth; x++)
        {
            for (int y = 0; y < levelData.gridHeight; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                // Using odd-q vertical layout for hex grid positioning
                float xPos = x * 1.0f;
                float yPos = y * 0.75f;
                if (y % 2 != 0)
                {
                    xPos += 0.5f;
                }
                Vector3 worldPosition = new Vector3(xPos, yPos, 0);
                
                GameObject tileGO = Instantiate(tilePrefab, worldPosition, Quaternion.identity, gridParent);
                tileGO.name = $"Tile ({x},{y})";
                
                grid.Add(gridPos, new TileData(gridPos));
            }
        }
    }

    public TileData GetTileData(Vector2Int gridPosition)
    {
        grid.TryGetValue(gridPosition, out TileData tileData);
        return tileData;
    }

    public List<TileData> GetNeighbors(Vector2Int gridPosition)
    {
        List<Vector2Int> neighborCoords = GetNeighborCoordinates(gridPosition);
        List<TileData> neighbors = new List<TileData>();

        foreach (var coord in neighborCoords)
        {
            if (grid.TryGetValue(coord, out TileData neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    private List<Vector2Int> GetNeighborCoordinates(Vector2Int gridPosition)
    {
        List<Vector2Int> neighborCoords = new List<Vector2Int>();
        int x = gridPosition.x;
        int y = gridPosition.y;

        // Directions for odd-q vertical layout
        Vector2Int[] directions;
        if (y % 2 != 0) // Odd rows
        {
            directions = new Vector2Int[]
            {
                new Vector2Int(0, 1), new Vector2Int(0, -1), // N, S
                new Vector2Int(-1, 0), new Vector2Int(1, 0), // W, E
                new Vector2Int(1, 1), new Vector2Int(1, -1)  // NE, SE
            };
        }
        else // Even rows
        {
            directions = new Vector2Int[]
            {
                new Vector2Int(0, 1), new Vector2Int(0, -1),   // N, S
                new Vector2Int(-1, 0), new Vector2Int(1, 0),   // W, E
                new Vector2Int(-1, 1), new Vector2Int(-1, -1) // NW, SW
            };
        }

        foreach (var dir in directions)
        {
            neighborCoords.Add(new Vector2Int(x + dir.x, y + dir.y));
        }

        return neighborCoords;
    }
}
