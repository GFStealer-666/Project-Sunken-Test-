using System.Collections;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private float currentIntensity_m;
    [SerializeField] private float lightPeakIntensity , lightModerateIntensity , lightGlimmerIntensity;
    [SerializeField] private float transitionTime = 2.0f;
    [SerializeField] private bool IsOpenable_m = true , IsOpenable_S = false;
    [SerializeField] private bool IsOpen;
    [SerializeField] private bool IsStateChanging = false;
    private FlashLightSetting currentDifficultyFlashlightSetting;
    [SerializeField] private Light flashLight;
    private KeyCode flashLightKeyCode;
    private void OnEnable() 
    {
        FlashlightManager.OnGameDifficultySelectFlashlight += SetFlashlightSetting;
        FlashlightBattery.OnBatteryPowerChange += UpdateFlashlight;
    }
    private void OnDisable() 
    {
        FlashlightManager.OnGameDifficultySelectFlashlight -= SetFlashlightSetting;
        FlashlightBattery.OnBatteryPowerChange -= UpdateFlashlight;
    }
    private void Awake() 
    {
        flashLightKeyCode = KeyCode.F;
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
        if(Input.GetKeyDown(flashLightKeyCode) && IsOpenable_m) {ToggleFlashlight();}
        
    }

    private void UpdateFlashlight()
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
                if(IsStateChanging) return;
                StartCoroutine(SmoothChangeLightIntensity(currentIntensity_m, lightGlimmerIntensity, transitionTime));
                flashLight.enabled = true;
                break;
            case FlashlightState.FlashlightStateEnum.Moderate :
                if(IsStateChanging) return;
                StartCoroutine(SmoothChangeLightIntensity(currentIntensity_m, lightModerateIntensity, transitionTime));
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

        if(FlashlightBattery.Instance.CurrentBatteryLife <= 0) {IsOpenable_m = false;}
        else {IsOpenable_m = true;}
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
        UpdateFlashlight();
        IsOpen = flashLight.enabled;
    }

    private IEnumerator SmoothChangeLightIntensity(float currentIntensity , float newIntensity , float transitionTime)
    {
        float elapsedTime = 0f;
        float startIntensity = currentIntensity;
        IsStateChanging = true;
        while (elapsedTime < transitionTime)
        {

            Debug.Log(elapsedTime);
            float t = elapsedTime / transitionTime;
            float interpolatedIntensity = Mathf.Lerp(startIntensity, newIntensity, t);
            //Debug.Log("Currently changing with this intensity : " + interpolatedIntensity);
            elapsedTime += Time.deltaTime;
            currentIntensity_m = interpolatedIntensity; // Update current intensity directly with the interpolated value
            flashLight.intensity = interpolatedIntensity;
            yield return null;
        }
        IsStateChanging = false;
        flashLight.intensity = newIntensity; // Ensure that the final intensity is set correctly
        currentIntensity_m = newIntensity;     
    }
}

