using UnityEngine;
[CreateAssetMenu(fileName = "FlashLightSetting", menuName = "Project Sunken (Test)/FlashLightSetting", order = 0)]
public class FlashLightSetting : ScriptableObject 
{
    public float lightRadius; 
    public float lightPeakIntensity;
    public float lightModerateIntensity;
    public float lightGlimmerIntensity;
    public float maxBatteryLife;
    public float startBatteryLife;
    public float batteryDrainPerSecond;
}