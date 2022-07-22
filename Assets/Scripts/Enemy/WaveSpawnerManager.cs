using TMPro;
using UnityEngine;
using System.Collections;

public class WaveSpawnerManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform spawPoint;

    [Space(1)]
    [Header("Time Management")]
    [SerializeField] float timeBetweenWaves = 3f;
    [SerializeField] float timeBetweenEnemies = .8f;

    [Space(2)]
    [Header("UI")]
    [SerializeField] TextMeshProUGUI tmpWaveCountDownTimer;
    [SerializeField] Animator tmpWaveMessageAnim;
    [SerializeField] TextMeshProUGUI tmpWaveMessage;


    [Space(3)]
    [Header("Wave configuration")]
    [SerializeField] int maxWaves = 5;


    float countDown = 5.5f;
    float waveMessagecountDown = 3f;
    int waveNumber = 1;

    bool fadeText = false;
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            if (!fadeText)
            {
                tmpWaveMessageAnim.SetTrigger("FadeText");
                fadeText = true;
            }

            if (waveMessagecountDown <= 0)
            {
                tmpWaveCountDownTimer.gameObject.SetActive(true);

                if (countDown <= 0)
                {
                    StartCoroutine(SpawnWave());
                    countDown = timeBetweenWaves;
                    waveMessagecountDown = timeBetweenWaves;
                    tmpWaveCountDownTimer.gameObject.SetActive(false);
                }
                countDown -= Time.deltaTime;
                tmpWaveCountDownTimer.text = Mathf.Round(countDown).ToString();
            }
            else
            {
                tmpWaveMessage.text = $"Next wave incoming {waveNumber}-{maxWaves}";
                waveMessagecountDown -= Time.deltaTime;
            }
        }
        else
            tmpWaveCountDownTimer.text = "";
    }

    IEnumerator SpawnWave()
    {
        fadeText = false;
        waveNumber++;
        for (int i = 0; i <= waveNumber; i++)
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
