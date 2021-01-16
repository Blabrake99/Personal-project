using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singilton : MonoBehaviour
{
    public static Singilton Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
