using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    private Transform[] path;
    private int pathIndex = 0;
    private Transform target;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        path = LevelManager.main.path;
        if (path.Length > 0)
        {
            target = path[0]; 
        }
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            if (pathIndex == 0 || pathIndex == path.Length - 1) 
            {
                if (pathIndex == path.Length - 1) // 
                {
                    EnemySpawner.onEnemyDestroy.Invoke();
                    LevelManager.main.PlayerTakeDamage(10); // Player loses health
                    Destroy(gameObject);
                    return;
                }

                pathIndex++;
                if (pathIndex < path.Length)
                {
                    target = path[pathIndex];
                }
            }
            else
            {
                // skip to end point
                pathIndex = path.Length - 1;
                target = path[pathIndex];
            }
        }

        // Move towards the target
        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}