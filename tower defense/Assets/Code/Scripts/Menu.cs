using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    //link currency to UI
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI healthUI;

    private void OnGUI() {
        currencyUI.text = LevelManager.main.gold.ToString();
        healthUI.text = LevelManager.main.health.ToString();
    }

    public void SetSelected() {
        
    }
}
