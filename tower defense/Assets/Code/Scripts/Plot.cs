using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject tower;
    public Turret turret;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        // Debug.Log("Build tower here" + name);
        Debug.Log(turret == null);

        if (UIManager.main.IsHoveringUI()) return;
        
        if (tower != null) {
            turret.UpgradeUI();
            return;
        } 
        //if no tower in plot
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.gold) {
            Debug.Log("Not enough gold");
            return;
        }

        // Reset plot's tower and turret before building a new one
        ResetPlot();

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = tower.GetComponent<Turret>();

    }

    public void ResetPlot() {
        tower = null;
        turret = null;
    }

}
