using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    //link currency to UI
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    private void OnGUI() {
        currencyUI.text = LevelManager.main.gold.ToString();
    }

    public void SetSelected() {
        
    }
}
