using TMPro;
using UnityEngine;
using System.Collections;

public class WaveSpawnerManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject[] level1_enemyPrefabs;
    [SerializeField] GameObject[] level2_enemyPrefabs;
    [SerializeField] GameObject[] level3_enemyPrefabs;
    [SerializeField] GameObject boss_enemyPrefabs;
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
    [SerializeField] GameObject mapFinishedModal;

    [Space(3)]
    [Header("End Map")]
    [SerializeField] GameObject stars_3;
    [SerializeField] GameObject stars_2;
    [SerializeField] GameObject stars_1;

    [Space(4)]
    [Header("Wave configuration")]
    [SerializeField] int maxWaves = 6;


    GameObject enemyPrefab;

    float countDown = 5.5f;
    float waveMessagecountDown = 3.5f;
    int waveNumber = 1;


    bool fadeText = false;
    public bool allWavesSumoned = false;

    CurrencyManager currencyManager;
    LifeManager lifeManager;

    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();

    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            if (allWavesSumoned)
            {
                lifeManager = FindObjectOfType<LifeManager>();

                int remainingLifes = lifeManager.RemainingLifes();

                if (remainingLifes == 10)
                    stars_3.SetActive(true);
                else if (remainingLifes >= 6)
                    stars_2.SetActive(true);
                else
                    stars_1.SetActive(true);

                currencyManager.UpdatePassiveGold(0);
                mapFinishedModal.SetActive(true);
                return;
            }

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
                if (waveNumber == maxWaves)
                    tmpWaveMessage.text = $"the final boss is coming!";
                else
                    tmpWaveMessage.text = $"Wave incoming {waveNumber}-{maxWaves}";

                waveMessagecountDown -= Time.deltaTime;
            }
        }
        else
            tmpWaveCountDownTimer.text = "";
    }

    IEnumerator SpawnWave()
    {
        fadeText = false;
        enemyPrefab = GetCurrentEnemyByWave();

        for (int i = 0; i <= waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        if (waveNumber == maxWaves)
        {
            Instantiate(boss_enemyPrefabs, spawPoint.position, spawPoint.rotation);
            allWavesSumoned = true;
        }

        waveNumber++;

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawPoint.position, spawPoint.rotation);
    }

    GameObject GetCurrentEnemyByWave()
    {
        if (waveNumber <= (maxWaves / 3))   //Level one Enemies
            return enemyPrefab = level1_enemyPrefabs[Random.Range(0, level1_enemyPrefabs.Length)];
        else if (waveNumber <= (maxWaves / 2))  //Level 2 Enemies
            return enemyPrefab = level2_enemyPrefabs[Random.Range(0, level2_enemyPrefabs.Length)];
             
        return enemyPrefab = level3_enemyPrefabs[Random.Range(0, level3_enemyPrefabs.Length)];
    }
}
