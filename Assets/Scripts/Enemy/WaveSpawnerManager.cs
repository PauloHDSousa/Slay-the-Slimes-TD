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
    [SerializeField] Transform second_spawPoint;

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
    [SerializeField] AudioClip successSFX;

    [Space(4)]
    [Header("Wave configuration")]
    [SerializeField] int maxWaves = 6;
    [SerializeField] int currentMap = 0;

    GameObject enemyPrefab;

    float countDown = 5.5f;
    float waveMessagecountDown = 3.5f;
    int waveNumber = 1;


    bool fadeText = false;
    public bool allWavesSumoned = false;
    bool dataSaved = false;

    CurrencyManager currencyManager;
    LifeManager lifeManager;
    AudioSource audioSource;

    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(dataSaved)
            return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            if (allWavesSumoned && !dataSaved)
            {
                dataSaved = true;
                lifeManager = FindObjectOfType<LifeManager>();

                int remainingLifes = lifeManager.RemainingLifes();
                int stars = 0;
                if (remainingLifes == 10)
                {
                    stars = 3;
                    stars_3.SetActive(true);
                }
                else if (remainingLifes >= 6)
                {
                    stars = 2;
                    stars_2.SetActive(true);
                }
                else
                {
                    stars = 1;
                    stars_1.SetActive(true);
                }

                SaveMapStars(stars);
                currencyManager.UpdatePassiveGold(0);
                mapFinishedModal.SetActive(true);
                audioSource.PlayOneShot(successSFX);
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

        //Summon the boss!
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

        if(second_spawPoint != null)
            Instantiate(enemyPrefab, second_spawPoint.position, second_spawPoint.rotation);
    }

    GameObject GetCurrentEnemyByWave()
    {
        if (waveNumber <= (maxWaves / 3))   //Level one Enemies
            return enemyPrefab = level1_enemyPrefabs[Random.Range(0, level1_enemyPrefabs.Length)];
        else if (waveNumber <= (maxWaves / 2))  //Level 2 Enemies
            return enemyPrefab = level2_enemyPrefabs[Random.Range(0, level2_enemyPrefabs.Length)];

        return enemyPrefab = level3_enemyPrefabs[Random.Range(0, level3_enemyPrefabs.Length)];
    }
    
    void SaveMapStars(int stars)
    {
        PlayerPrefsManager prefs = new PlayerPrefsManager();
        if (currentMap == 0)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsTutorial, stars);
        else if (currentMap == 1)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap1, stars);
        else if (currentMap == 2)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap2, stars);
        else if (currentMap == 3)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap3, stars);
        else if (currentMap == 4)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap4, stars);
        else if (currentMap == 5)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap5, stars);
        else if (currentMap == 6)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap6, stars);
        else if (currentMap == 7)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap7, stars);
        else if (currentMap == 8)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap8, stars);
        else if (currentMap == 9)
            prefs.SaveInt(PlayerPrefsManager.PrefKeys.StarsMap9, stars);

    }
}
