using UnityEngine;
using UnityEngine.UI;

public class FlashlightBatteryUI : MonoBehaviour
{
    [SerializeField] private Image batteryBar;

    private void OnEnable() 
    {
        FlashlightBattery.OnBatteryPercentageChange += OnBatteryChange;
    }

    private void OnDisable() 
    {
        FlashlightBattery.OnBatteryPercentageChange -= OnBatteryChange;
    }

    private void OnBatteryChange(float batteryPercentage)
    {
        Debug.Log(batteryPercentage);
        batteryBar.fillAmount = batteryPercentage;
    }
}
