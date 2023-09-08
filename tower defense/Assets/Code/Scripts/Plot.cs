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

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = tower.GetComponent<Turret>();
    }


}
