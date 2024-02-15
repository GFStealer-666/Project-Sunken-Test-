using System.Collections;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private float currentIntensity_m;
    [SerializeField] private float lightPeakIntensity , lightModerateIntensity , lightGlimmerIntensity;
    [SerializeField] private float transitionTime = 3.0f;
    [SerializeField] private bool IsOpenable_m = true , IsOpenable_S = false;
    [SerializeField] private bool IsOpen;
    private FlashLightSetting currentDifficultyFlashlightSetting;
    [SerializeField] private Light flashLight;
    private KeyCode flashLightKeyCode;
    private void OnEnable() 
    {
        FlashlightManager.OnGameDifficultySelectFlashlight += SetFlashlightSetting;
        FlashlightBattery.OnBatteryPowerChange += UpdateFlashlightState;
    }
    private void OnDisable() 
    {
        FlashlightManager.OnGameDifficultySelectFlashlight -= SetFlashlightSetting;
        FlashlightBattery.OnBatteryPowerChange -= UpdateFlashlightState;
    }
    private void Awake() 
    {
        flashLightKeyCode = KeyCode.F;
        currentIntensity_m = 0.0f;
    }
    private void SetFlashlightSetting(FlashLightSetting flashLightSetting)
    {
        currentDifficultyFlashlightSetting = flashLightSetting;

        lightPeakIntensity = currentDifficultyFlashlightSetting.lightPeakIntensity;
        lightModerateIntensity = currentDifficultyFlashlightSetting.lightModerateIntensity;
        lightGlimmerIntensity = currentDifficultyFlashlightSetting.lightGlimmerIntensity;
    }
    private void Update() 
    {
        if(Input.GetKeyDown(flashLightKeyCode)) {ToggleFlashlight();}
        
    }

    private void UpdateFlashlightState()
    {
        switch(FlashlightState.Instance.CurrentFlashlightState)
        {
            case FlashlightState.FlashlightStateEnum.Off :
                flashLight.enabled = false;
                break;
            case FlashlightState.FlashlightStateEnum.Depleted :
                flashLight.enabled = false;
                break;
            case FlashlightState.FlashlightStateEnum.Glimmer :
                StartCoroutine(SmoothChangeLightIntensity(lightModerateIntensity, lightGlimmerIntensity, transitionTime));
                flashLight.enabled = true;
                break;
            case FlashlightState.FlashlightStateEnum.Moderate :
                StartCoroutine(SmoothChangeLightIntensity(lightPeakIntensity, lightModerateIntensity, transitionTime));
                flashLight.intensity = lightModerateIntensity;
                flashLight.enabled = true;
                break;
            case FlashlightState.FlashlightStateEnum.Peak :
                flashLight.intensity = lightPeakIntensity;
                flashLight.enabled = true;
                break;
            default :
                flashLight.enabled = false;
                break;
        }     

        if(FlashlightState.Instance.CurrentFlashlightState == FlashlightState.FlashlightStateEnum.Depleted)
        {
            flashLight.enabled = false;
            IsOpen = false;
        }
    }
    private void ToggleFlashlight()
    {
        if(IsOpen)
        {
            FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Off;
        }
        else if(!IsOpen)
        {
            switch(FlashlightBattery.Instance.GetBatteryLifeLevel())
            {
                case FlashlightBattery.BatteryLifeLevel.Peak : 
                    FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Peak;
                    break;
                case FlashlightBattery.BatteryLifeLevel.Moderate : 
                    FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Moderate;
                    break;
                case FlashlightBattery.BatteryLifeLevel.Glimmer : 
                    FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Glimmer;
                    break;
                case FlashlightBattery.BatteryLifeLevel.Depleted : 
                    FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Depleted;
                    break;
                default :
                    FlashlightState.Instance.CurrentFlashlightState = FlashlightState.FlashlightStateEnum.Moderate;
                    break;
            }
        }
        UpdateFlashlightState();
        IsOpen = flashLight.enabled;
    }

    private IEnumerator SmoothChangeLightIntensity(float currentIntensity , float newIntensity , float transitionTime)
    {
        float elapsedTime = 0f;
        while(elapsedTime < transitionTime)
        {
            float changeIntensity = Mathf.Lerp(currentIntensity , newIntensity , transitionTime);
            elapsedTime += Time.deltaTime;
            yield return changeIntensity;
            currentIntensity_m = changeIntensity;
            flashLight.intensity = changeIntensity;
        }
        yield return newIntensity;
        
        
    }
}

