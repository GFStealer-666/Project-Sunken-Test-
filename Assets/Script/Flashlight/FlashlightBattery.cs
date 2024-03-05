using System;
using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    private FlashLightSetting currentDifficultyFlashlightSetting;
    [SerializeField] private float maxBatteryLife;
    [SerializeField] private float currentBatteryLife;
    [SerializeField] private float batteryDrainPerSecond;
    [SerializeField] private float batteryPercentage ;

    [SerializeField] private bool IsDrainBattery = false;

    public float CurrentBatteryLife 
    {
        get => currentBatteryLife;
        private set
        {
            currentBatteryLife = value;
            OnBatteryPowerChange?.Invoke();
        }
    }
    public float CurrentBatteryPercentage 
    {
        get => batteryPercentage;
        private set
        {
            batteryPercentage = value;
            OnBatteryPercentageChange?.Invoke(batteryPercentage);
        }
    }
    [SerializeField] private float minPeakBattery, minModerateBattery , minGlimmerBattery;
    public enum BatteryLifeLevel {Peak , Moderate , Glimmer , Depleted};
    public BatteryLifeLevel currentBatteryLifeLevel;
    public static FlashlightBattery Instance;
    public static event Action<float> OnBatteryPercentageChange;
    public static event Action OnBatteryPowerChange;
    private void OnEnable() {FlashlightManager.OnGameDifficultySelectFlashlight += SetFlashlightSetting;}
    private void OnDisable() {FlashlightManager.OnGameDifficultySelectFlashlight -= SetFlashlightSetting;}
    private void Awake() 
    {
        Instance = this;
    }
    private void SetFlashlightSetting(FlashLightSetting flashLightSetting)
    {
        currentDifficultyFlashlightSetting = flashLightSetting;

        maxBatteryLife = currentDifficultyFlashlightSetting.maxBatteryLife;
        currentBatteryLife = currentDifficultyFlashlightSetting.startBatteryLife;
        batteryDrainPerSecond = currentDifficultyFlashlightSetting.batteryDrainPerSecond;

        minPeakBattery = currentDifficultyFlashlightSetting.lightModerateIntensity - 0.01f;
        minModerateBattery = currentDifficultyFlashlightSetting.lightGlimmerIntensity - 0.01f;

    }
    private void Update() 
    {
        if (IsDrainBattery == false) return;

        if(FlashlightState.Instance.CurrentFlashlightState != FlashlightState.FlashlightStateEnum.Off 
        && FlashlightState.Instance.CurrentFlashlightState != FlashlightState.FlashlightStateEnum.Depleted)
        {
            DrainBattery();
            SetBatteryState();
        }
        
    }
    private void DrainBattery()
    {
        Debug.Log("IS IT HERE?");
        //if(FlashlightState.Instance.CurrentFlashlightState == FlashlightState.FlashlightStateEnum.Depleted) return;
        CurrentBatteryLife -= batteryDrainPerSecond * Time.deltaTime;
        if(CurrentBatteryLife <= 0) 
        {
            FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Depleted; 
            CurrentBatteryLife = 0;
        }
    }
    private void SetBatteryState()
    {
        CurrentBatteryPercentage = currentBatteryLife / maxBatteryLife;
        switch(batteryPercentage)
        {
            case float n when n <= 1.0f && n >= minPeakBattery : 
                FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Peak;
                currentBatteryLifeLevel = BatteryLifeLevel.Peak;
                break;
            case float n when n <= minPeakBattery && n >= minModerateBattery : 
                FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Moderate;
                currentBatteryLifeLevel = BatteryLifeLevel.Moderate;
                break;
            case float n when n <= minModerateBattery && n >= minGlimmerBattery : 
                FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Glimmer;
                currentBatteryLifeLevel = BatteryLifeLevel.Glimmer;
                break;
            case float n when n == 0.0f : 
                FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Depleted;
                currentBatteryLifeLevel = BatteryLifeLevel.Depleted;
                break;
            default :
                FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Off;
                break;
        }
        
    }
    private void RechargeBattery(float amount)
    {
        CurrentBatteryLife += amount;
        SetBatteryState();
    }
    public BatteryLifeLevel GetBatteryLifeLevel()
    {
        return currentBatteryLifeLevel;
    }

    public float GetCurrentBatteryLife() => CurrentBatteryLife;


}
