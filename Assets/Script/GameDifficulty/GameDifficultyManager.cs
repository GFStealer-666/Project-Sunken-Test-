using System;
using Unity.VisualScripting;
using UnityEngine;

public enum GameDifficulty {Easy , Normal , Hard , Nightmare}
public class GameDifficultyManager : MonoBehaviour
{
    public GameDifficulty currentGameDifficulty = GameDifficulty.Normal;
    public static event Action<GameDifficulty> OnGameDifficultySelect;
    public GameDifficulty CurrentGameDifficulty
    {
        get => currentGameDifficulty;
        set
        {
            currentGameDifficulty = value;
            OnGameDifficultySelect?.Invoke(currentGameDifficulty);
        }
    }
    public void OnEasySelect()
    {
        CurrentGameDifficulty = GameDifficulty.Easy;
    }
    public void OnNormalSelect()
    {
        CurrentGameDifficulty = GameDifficulty.Normal;
    }
    public void OnHardSelect()
    {
        currentGameDifficulty = GameDifficulty.Hard;
    }
    public void OnNightmareSelect()
    {
        currentGameDifficulty = GameDifficulty.Nightmare;
    }
}
