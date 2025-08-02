using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "AmusementSort/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Grid Settings")]
    public int gridWidth = 5;
    public int gridHeight = 5;

    [Header("Level Objectives")]
    public int carriagesToFill = 3;

    [Header("Visitor Settings")]
    public List<VisitorColor> availableColors;
    public List<InitialVisitorGroup> initialVisitorGroups;
}

[System.Serializable]
public class InitialVisitorGroup
{
    public VisitorColor color;
    public int count;
}
