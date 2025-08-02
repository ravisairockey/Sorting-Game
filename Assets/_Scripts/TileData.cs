using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public Vector2Int GridPosition { get; }
    public List<VisitorData> Visitors { get; private set; }
    public bool IsFull => Visitors.Count >= 7;
    public bool IsEmpty => Visitors.Count == 0;
    public VisitorColor? TileColor
    {
        get
        {
            if (IsEmpty) return null;
            VisitorColor firstColor = Visitors[0].Color;
            for (int i = 1; i < Visitors.Count; i++)
            {
                if (Visitors[i].Color != firstColor)
                {
                    return null; // Mixed colors
                }
            }
            return firstColor; // Single color
        }
    }

    public TileData(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
        Visitors = new List<VisitorData>();
    }

    public void AddVisitor(VisitorData visitor)
    {
        if (!IsFull)
        {
            Visitors.Add(visitor);
        }
    }

    public void AddVisitors(List<VisitorData> visitors)
    {
        foreach (var visitor in visitors)
        {
            AddVisitor(visitor);
        }
    }

    public List<VisitorData> ClearVisitors()
    {
        List<VisitorData> clearedVisitors = new List<VisitorData>(Visitors);
        Visitors.Clear();
        return clearedVisitors;
    }
}
