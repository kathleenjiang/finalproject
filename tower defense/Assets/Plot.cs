using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
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
        Debug.Log("Build tower here" + name);
        if (tower != null) return; 
        //if no tower in plot
        GameObject towerToBuild = BuildManager.main.getSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }


}
