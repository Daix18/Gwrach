using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Vidas : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void ChangeMaxHealth(float MaxHealth)
    {
        slider.maxValue = MaxHealth;
            
    }
    public void SetMaxHealth(float CurrentHealth) 
    {
        slider.value = CurrentHealth;
    }
    public void InitializeHealthBar(float CurrentHealth, float MaxHealt)
    {
        ChangeMaxHealth(MaxHealt);
        SetMaxHealth(CurrentHealth);
    }
}
