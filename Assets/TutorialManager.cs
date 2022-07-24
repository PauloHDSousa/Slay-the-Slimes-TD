using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Space(1)]
    [Header("UI")]
    [SerializeField] TextMeshProUGUI tmpTutorial;

    [SerializeField] TextMeshProUGUI tmpBlaster;
    [SerializeField] TextMeshProUGUI tmpCannonTower;
    [SerializeField] TextMeshProUGUI tmpIceTower;

    [SerializeField] TextMeshProUGUI tmpDamageTamashi;
    [SerializeField] TextMeshProUGUI tmpPoisonTamashi;


    [Space(1)]
    [Header("Configuration")]
    [SerializeField] WaveSpawnerManager waveSpawnerManager;


    bool blasterBuilded = false;
    bool canonBuilded = false;
    bool iceTowerBuilded = false;
    bool damageTamashiBuilded = false;
    bool poisonTamashiBuilded = false;

    bool secondTutorialPartStarted = false;
    bool tutorialFinished = false;

    void Start()
    {
        tmpTutorial.gameObject.SetActive(true);
        tmpTutorial.text = "First, let's build every weapon in the game";
    }
    private void Update()
    {
        if(secondTutorialPartStarted)
            return;

        if(blasterBuilded && canonBuilded && iceTowerBuilded && damageTamashiBuilded && poisonTamashiBuilded)
        {
            tutorialFinished = true;
            Invoke("ContinueTutorial", 6f);
            secondTutorialPartStarted = true;
        }
    }

    public void ContinueTutorial()
    {
        DisableAllTexts();
        tmpTutorial.gameObject.SetActive(true);
        tmpTutorial.text = "You can upgrade or sell your weapons by clicking in then";
        Invoke("ContinueTutorialSummon", 6f);
    }

    public void ContinueTutorialSummon()
    {
        DisableAllTexts();
        tmpTutorial.gameObject.SetActive(true);
        tmpTutorial.text = "You can only have one poison and only one damage tamashi!  You can't sell or upgrade then!";
        Invoke("ContinueTutorialGold", 8f);
    }

    public void ContinueTutorialGold()
    {
        DisableAllTexts();
        tmpTutorial.gameObject.SetActive(true);
        tmpTutorial.text = "You are going to win gold by killing slimes and passively through time";
        Invoke("ContinueTutorialSpeed", 6f);
    }

    public void ContinueTutorialSpeed()
    {
        tmpTutorial.text = "if you want, you can increase the game speed by clicking in the button under your current gold amount";
        Invoke("ContinueTutorialPause", 6.5f);
    }

    public void ContinueTutorialPause()
    {
        tmpTutorial.text = "Press ESC at any time to pause the game";
        Invoke("LastMessage", 5f);
    }
    public void LastMessage()
    {
        tmpTutorial.text = "You are good to go, protect the nexus and good luck";
        Invoke("StartGame", 5f);
    }

    public void StartGame()
    {
        tmpTutorial.gameObject.SetActive(false);
        waveSpawnerManager.gameObject.SetActive(true);
    
    }

    public void BlasterMessage()
    {
        if(tutorialFinished)
            return;

        DisableAllTexts();
        tmpBlaster.gameObject.SetActive(true);
        blasterBuilded = true;
    }

    public void CannonMessage()
    {
        if (tutorialFinished)
            return;

        DisableAllTexts();
        tmpCannonTower.gameObject.SetActive(true);
        canonBuilded = true;
    }

    public void IceTowerMessage()
    {
        if (tutorialFinished)
            return;

        DisableAllTexts();
        tmpIceTower.gameObject.SetActive(true);
        iceTowerBuilded = true;
    }

    public void DamageTamashiMessage()
    {
        if (tutorialFinished)
            return;

        DisableAllTexts();
        tmpDamageTamashi.gameObject.SetActive(true);
        damageTamashiBuilded = true;
    }

    public void PoisonTamashiMessage()
    {
        if (tutorialFinished)
            return;

        DisableAllTexts();
        tmpPoisonTamashi.gameObject.SetActive(true);
        poisonTamashiBuilded = true;
    }

    void DisableAllTexts()
    {
        tmpTutorial.gameObject.SetActive(false);
        tmpBlaster.gameObject.SetActive(false);
        tmpCannonTower.gameObject.SetActive(false);
        tmpIceTower.gameObject.SetActive(false);
        tmpDamageTamashi.gameObject.SetActive(false);
        tmpPoisonTamashi.gameObject.SetActive(false);
    }
}
