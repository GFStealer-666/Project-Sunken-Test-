using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    // SET EACH VARIABLE BASE ON DIFFICULTY
    [SerializeField] private FlashLightSetting easyFlashlight , normalFlashlight , hardFlashlight , nightmareFlashlight;
    GameDifficulty gameDifficultyState;
    public static event Action<FlashLightSetting> OnGameDifficultySelectFlashlight;
    private void OnEnable() {GameDifficultyManager.OnGameDifficultySelect += SetFlashlightDifficult;}
    
    private void OnDisable() {GameDifficultyManager.OnGameDifficultySelect -= SetFlashlightDifficult;}

    private void Awake() {
        HelloTest();
    }
    public void HelloTest()
    {
        OnGameDifficultySelectFlashlight?.Invoke(easyFlashlight);
    }
    private void SetFlashlightDifficult(GameDifficulty selectedDiffculty)
    {
        gameDifficultyState = selectedDiffculty;
        switch(gameDifficultyState)
        {
            case GameDifficulty.Easy :
            {
                OnGameDifficultySelectFlashlight?.Invoke(easyFlashlight);
                break;
            }
            case GameDifficulty.Normal :
            {
                OnGameDifficultySelectFlashlight?.Invoke(normalFlashlight);
                break;
            }
            case GameDifficulty.Hard :
            {
                OnGameDifficultySelectFlashlight?.Invoke(hardFlashlight);
                break;
            }
            case GameDifficulty.Nightmare :
            {
                OnGameDifficultySelectFlashlight?.Invoke(nightmareFlashlight);
                break;
            }
            default : { Debug.Log("Error Occur"); break;}
        }
    }







}
