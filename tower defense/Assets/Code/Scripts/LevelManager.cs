using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    //enemy path
    public Transform[] path;

    public int gold;
    public int health;

    [SerializeField] GameObject gameOverMenu;

    [Header("Audio")]
    [SerializeField] AudioSource loseSoundEffect;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        //initial player stats
        gold = 9100;
        health = 50;
        gameOverMenu.SetActive(false);
    }

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        loseSoundEffect.Play();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void IncreaseCurrency(int amount)
    {
        gold += amount;
    }

    //if player is able to buy return, else false
    public bool SpendCurrency(int amount)
    {
        if (amount <= gold)
        {
            // buy tower
            gold -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
