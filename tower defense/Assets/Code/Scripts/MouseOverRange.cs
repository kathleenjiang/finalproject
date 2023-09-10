using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverRange : MonoBehaviour
{
    private SpriteRenderer targetingRangeCircle;

    private void Start()
    {
        targetingRangeCircle = GetComponentInChildren<SpriteRenderer>();
        targetingRangeCircle.enabled = false; //hide targeting range
    }

    private void OnMouseEnter()
    {
        // on mouse over
        targetingRangeCircle.enabled = true;
    }

    private void OnMouseExit()
    {
        targetingRangeCircle.enabled = false;
    }
}





