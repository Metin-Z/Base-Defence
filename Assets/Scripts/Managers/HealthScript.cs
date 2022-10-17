using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    Slider healthBar;
    public int health_MaxValue =550;
    public int health_Value;
    public static HealthScript Instance;
    private void Awake()
    {
        healthBar = GetComponent<Slider>();
        Instance = this;
        healthBar.maxValue = health_MaxValue;
        health_Value = health_MaxValue;
    }   
    void Update()
    {
        healthBar.value = health_Value;
    }
}
