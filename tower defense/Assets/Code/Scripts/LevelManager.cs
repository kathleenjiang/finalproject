using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    //enemy path
    public Transform[] path;

    public int gold;

    private void Awake() {
        main = this;
    }

    private void Start() {
        gold = 100;
    }

    public void IncreaseCurrency(int amount) {
        gold += amount;
    }

    //if player is able to buy return, else false
    public bool SpendCurrency(int amount) {
        if (amount <= gold) {
            // buy tower
            gold -= amount;
            return true;
        } else {
            Debug.Log("Insufficient Funds");
            return false;
        }
    }
}
