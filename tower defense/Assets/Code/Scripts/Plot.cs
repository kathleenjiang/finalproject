using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    // public Tower tower;
    public Turret turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // Debug.Log("Build tower here" + name);
        // Debug.Log(turret == null);

        if (turret != null)
        {
            turret.UpgradeUI();
            return;
        }
        //if no tower in plot
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.gold)
        {
            Debug.Log("Not enough gold");
            return;
        }

        ResetPlot();

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        GameObject go = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = go.GetComponentInChildren<Turret>();
        turret.SetPlot(this);

    }

    public void ResetPlot()
    {
        turret = null;
    }

}
