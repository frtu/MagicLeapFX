using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text debugText = null;

    [Inject]
    public void Setup(
        DiagnosticsManager diagnosticsManager, 
        UIManager uiManager) 
    {
        Debug.Log("Setup has been called");
        
        uiManager.BindUI(debugText);
        diagnosticsManager.Status();
    }
}
