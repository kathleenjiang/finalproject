using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public Vector3 pointB;

    private Vector3 pointA;
    private bool isMovingRight = true;

    private IEnumerator Start()
    {
        pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            FlipSprite(); // Flip the sprite when changing direction
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            FlipSprite(); // Flip the sprite when changing direction
        }
    }

    private void FlipSprite()
    {
        // Flip the sprite on the X-axis
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        isMovingRight = !isMovingRight;
    }

    private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
