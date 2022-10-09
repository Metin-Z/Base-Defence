using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    public static BaseComponent Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
