using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class Turret : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI sellText;

    [Header("Attribute")]
    [SerializeField] public float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;     //attack per second
    [SerializeField] private int upgradeCost = 100;
    [SerializeField] private int sellValue;

    [Header("Audio")]
    [SerializeField] AudioSource hitSoundEffect;
    [SerializeField] AudioSource upgradeSoundEffect;
    [SerializeField] AudioSource sellSoundEffect;

    private float bpsBase;
    private float targetingRangeBase;
    private Plot plot;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start()
    {
        //upgrade
        bpsBase = bps;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(UpgradeTurret);
        sellButton.onClick.AddListener(SellTurret);
    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                hitSoundEffect.Play();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot()
    {
        // Debug.Log("Shoot");
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y,
        target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // turretRotationPoint.rotation = targetRotation;
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation,
        rotationSpeed * Time.deltaTime);
    }

    public void UpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void UpgradeTurret()
    {
        int cost = CalculateCost();
        int nextUpgradeCost = CalculateNextUpgradeCost();
        if (cost > LevelManager.main.gold) return;

        LevelManager.main.SpendCurrency(cost);

        level++;

        bps = CalculateBPS();
        targetingRange = CalculateRange();
        upgradeSoundEffect.Play();
        CloseUpgradeUI();

        if (costText != null)
        {
            costText.text = "$" + nextUpgradeCost.ToString(); // cost ui 
        }
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.5f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(level, 0.75f));
    }

    private int CalculateNextUpgradeCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(level + 1, 0.75f)); // to display next upgrade cost
    }

    public int CalculateSell()
    {
        return Mathf.RoundToInt(upgradeCost * 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    public void SetPlot(Plot plotReference)
    {
        plot = plotReference;
    }

    public void SellTurret()
    {
        int sellValue = CalculateSell();
        LevelManager.main.IncreaseCurrency(sellValue);

        if (sellText != null)
        {
            sellText.text = "$" + sellValue.ToString();
        }

        plot.ResetPlot();
        StartCoroutine(playAudioAndDestroy());
    }

    IEnumerator playAudioAndDestroy()
    {
        sellSoundEffect.Play();
        while (sellSoundEffect.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }

}


