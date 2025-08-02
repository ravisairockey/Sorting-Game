using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelData currentLevelData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartLevel()
    {
        // Future logic to start a level
    }

    public void WinGame()
    {
        Debug.Log("Level Won!");
        // Future logic for winning the game
    }

    public void LoseGame()
    {
        Debug.Log("Level Lost!");
        // Future logic for losing the game
    }
}
