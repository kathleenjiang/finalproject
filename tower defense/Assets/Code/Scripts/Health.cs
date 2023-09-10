using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject deathPrefab;

    [Header("Audio")]
    [SerializeField] public AudioSource deathSoundEffect;


    private bool isDestroyed = false;

    private void Start()
    {
        InitializeHealthSlider();
    }

    private void InitializeHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = hitPoints;
            healthSlider.value = hitPoints;
        }
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (healthSlider != null)
        {
            healthSlider.value = hitPoints;
        }

        if (hitPoints <= 0 && !isDestroyed)
        {
            GameObject deathEffect = Instantiate(deathPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 1f);
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            StartCoroutine(playAudioAndDestroy()); // wait for the audio to finish playing before destroying go

            // Destroy(gameObject);
        }
    }

    IEnumerator playAudioAndDestroy()
    {
        deathSoundEffect.Play();
        while (deathSoundEffect.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
