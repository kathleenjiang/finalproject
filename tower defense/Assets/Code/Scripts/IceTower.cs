using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IceTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject slowEffectPrefab;

    [Header("Attribute")]
    [SerializeField] public float targetingRange = 5f;
    [SerializeField] private float attackSpeed = 4f;    //attacks per second
    [SerializeField] private float freezeTime = 1f;

    [Header("Audio")]
    [SerializeField] AudioSource hitSoundEffect;



    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackSpeed)
        {
            Freeze();
            timeUntilFire = 0f;
        }
    }

    private void Freeze()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            hitSoundEffect.Play();
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                GameObject slowEffect = Instantiate(slowEffectPrefab, em.transform.position, Quaternion.identity);
                Destroy(slowEffect, freezeTime);


                StartCoroutine(ResetEnemySpeed(em));

            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        // Handles.color = Color.cyan;
        // Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
