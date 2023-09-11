using UnityEngine;
using TMPro;

public class WaveCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] EnemySpawner enemySpawner; // Reference to the specific EnemySpawner instance

    private void OnGUI() {
        waveUI.text = "Wave: " + enemySpawner.currentWave.ToString() + "/8";
    }

    public void SetSelected() {
    }
}
