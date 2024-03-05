using System;
using UnityEngine;

public class FlashlightState : MonoBehaviour
{
    public enum FlashlightStateEnum {Off ,Depleted ,Glimmer, Moderate, Peak};
    [SerializeField] private FlashlightStateEnum currentFlashlightState;
    public static FlashlightState Instance;
    public static event Action<FlashlightStateEnum> OnFlashlightStateChanged;  
    public FlashlightStateEnum CurrentFlashlightState
    {
        get => currentFlashlightState;
        set 
        {
            currentFlashlightState = value;
            OnFlashlightStateChanged?.Invoke(currentFlashlightState);
            
        }
    }

    private void Awake() 
    {
        Instance = this;
    }

    
}
