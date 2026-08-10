using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentSystem : MonoBehaviour
{
    public static EnvironmentSystem Instance { get; private set; }

    private ElementType currentEnvironment;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        currentEnvironment = ElementType.Light;
        TurnManager.Instance.ToNextTurn += ChangeEnvironment;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeEnvironment()
    {
        if(currentEnvironment == ElementType.Light)
        {
            currentEnvironment = ElementType.Dark;
        }
        else
        {
            currentEnvironment = ElementType.Light;
        }
    }

    public ElementType GetCurrentEnvironmentElement()
    {
        return currentEnvironment;
    }
}
