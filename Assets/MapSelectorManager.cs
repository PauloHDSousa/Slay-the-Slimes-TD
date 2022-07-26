using UnityEngine;
using static PlayerPrefsManager;

public class MapSelectorManager : MonoBehaviour
{

    [Header("Tutorial")]
    [SerializeField] GameObject tutorial_OneStarPanel;
    [SerializeField] GameObject tutorial_TwoStarPanel;
    [SerializeField] GameObject tutorial_ThreeStarPanel;

    [Header("1 Map")]
    [SerializeField] GameObject firstMap_OneStarPanel;
    [SerializeField] GameObject firstMap_TwoStarPanel;
    [SerializeField] GameObject firstMap_ThreeStarPanel;

    [Header("2 Map")]
    [SerializeField] GameObject secondMap_OneStarPanel;
    [SerializeField] GameObject secondMap_TwoStarPanel;
    [SerializeField] GameObject secondMap_ThreeStarPanel;

    [Header("3 Map")]
    [SerializeField] GameObject thirdMap_OneStarPanel;
    [SerializeField] GameObject thirdMap_TwoStarPanel;
    [SerializeField] GameObject thirdMap_ThreeStarPanel;

    [Header("4 Map")]
    [SerializeField] GameObject fourthMap_OneStarPanel;
    [SerializeField] GameObject fourthMap_TwoStarPanel;
    [SerializeField] GameObject fourthMap_ThreeStarPanel;

    [Header("5 Map")]
    [SerializeField] GameObject fifthMap_OneStarPanel;
    [SerializeField] GameObject fifthMap_TwoStarPanel;
    [SerializeField] GameObject fifthMap_ThreeStarPanel;

    [Header("6 Map")]
    [SerializeField] GameObject sixMap_OneStarPanel;
    [SerializeField] GameObject sixMap_TwoStarPanel;
    [SerializeField] GameObject sixMap_ThreeStarPanel;

    [Header("7 Map")]
    [SerializeField] GameObject sevenMap_OneStarPanel;
    [SerializeField] GameObject sevenMap_TwoStarPanel;
    [SerializeField] GameObject sevenMap_ThreeStarPanel;

    [Header("8 Map")]
    [SerializeField] GameObject eightMap_OneStarPanel;
    [SerializeField] GameObject eightMap_TwoStarPanel;
    [SerializeField] GameObject eightMap_ThreeStarPanel;

    [Header("9 Map")]
    [SerializeField] GameObject nineMap_OneStarPanel;
    [SerializeField] GameObject nineMap_TwoStarPanel;
    [SerializeField] GameObject nineMap_ThreeStarPanel;

    PlayerPrefsManager prefs = new PlayerPrefsManager();

    void Start()
    {
        LoadTutorialStars();
        LoadFirstMapStars();
        LoadSecondMapStars();
        Load3MapStars();
        Load4MapStars();
        Load5MapStars();
        Load6MapStars();
        Load7MapStars();
        Load8MapStars();
        Load9MapStars();
    }

    // Update is called once per frame
    void LoadTutorialStars()
    {
        if (prefs.HasKey(PlayerPrefsManager.PrefKeys.StarsTutorial))
        {
            int tutorialStars = prefs.GetInt(PlayerPrefsManager.PrefKeys.StarsTutorial);
            if (tutorialStars == 3)
                tutorial_ThreeStarPanel.SetActive(true);
            else if (tutorialStars == 2)
                tutorial_TwoStarPanel.SetActive(true);
            else
                tutorial_OneStarPanel.SetActive(true);
        }
    }
    void LoadFirstMapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap1, firstMap_OneStarPanel, firstMap_TwoStarPanel, firstMap_ThreeStarPanel);
    }

    void LoadSecondMapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap2, secondMap_OneStarPanel, secondMap_TwoStarPanel, secondMap_ThreeStarPanel);
    }

    void Load3MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap3, thirdMap_OneStarPanel, thirdMap_TwoStarPanel, thirdMap_ThreeStarPanel);
    }

    void Load4MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap4, fourthMap_OneStarPanel, fourthMap_TwoStarPanel, fourthMap_ThreeStarPanel);
    }

    void Load5MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap5, fifthMap_OneStarPanel, fifthMap_TwoStarPanel, fifthMap_ThreeStarPanel);
    }

    void Load6MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap6, sixMap_OneStarPanel, sixMap_TwoStarPanel, sixMap_ThreeStarPanel);
    }

    void Load7MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap7, sevenMap_OneStarPanel, sevenMap_TwoStarPanel, sevenMap_ThreeStarPanel);
    }

    void Load8MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap8, eightMap_OneStarPanel, eightMap_TwoStarPanel, eightMap_ThreeStarPanel);
    }

    void Load9MapStars()
    {
        LoadStars(PlayerPrefsManager.PrefKeys.StarsMap9, nineMap_OneStarPanel, nineMap_TwoStarPanel, nineMap_ThreeStarPanel);
    }

    void LoadStars(PrefKeys prefKey, GameObject oneStart, GameObject twoStars, GameObject threeStars)
    {
        if (prefs.HasKey(prefKey))
        {
            int stars = prefs.GetInt(prefKey);
            if (stars == 3)
                threeStars.SetActive(true);
            else if (stars == 2)
                twoStars.SetActive(true);
            else
                oneStart.SetActive(true);
        }
    }
}
