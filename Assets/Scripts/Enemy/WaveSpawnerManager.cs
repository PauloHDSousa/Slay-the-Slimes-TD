using TMPro;
using UnityEngine;
using System.Collections;

public class WaveSpawnerManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform spawPoint;

    [Space()]
    [Header("Time Management")]
    [SerializeField] float timeBetweenWaves = 5.5f;
    [SerializeField] float timeBetweenEnemies = .8f;

    [Space()]
    [Header("UI")]
    [SerializeField] TextMeshProUGUI tmpWaveCountDownTimer;


    float countDown = 5.5f;
    int waveNumber = 0;

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            if (countDown <= 0)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }
            countDown -= Time.deltaTime;
            tmpWaveCountDownTimer.text = Mathf.Round(countDown).ToString();
        }
        else
            tmpWaveCountDownTimer.text = "";
    }

    IEnumerator SpawnWave()
    {

        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawPoint.position, spawPoint.rotation);
    }
}
