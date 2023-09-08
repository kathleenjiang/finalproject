using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildManager : MonoBehaviour
{

    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;

    private void Awake() {
        main = this;
    }

    private void Start() { 
    }

    public Tower GetSelectedTower() {
        // return towerPrefabs[selectedTower];
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower) {
        selectedTower = _selectedTower;
    }

}
