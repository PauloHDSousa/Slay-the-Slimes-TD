using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip onHover;
    [SerializeField] AudioClip OnSceneLoad;
    [SerializeField] int onHoverFontSize;
    [SerializeField] RectTransform fader;

    float defaultSize;
    AudioSource audioSource;
    string scene;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void OnHoverEnter(TextMeshProUGUI text)
    {
        defaultSize = text.fontSize;
        PlayHoverSound();
        text.fontSize = onHoverFontSize;
    }

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(onHover);
    }

    public void OnHoverLeave(TextMeshProUGUI text)
    {
        text.fontSize = defaultSize;
    }

    #region Scenes

    public void LoadMaps()
    {
        LoadScene("MapSelector");
    }

    public void LoadMapSelector()
    {
        WaveSpawnerManager waveManager = FindObjectOfType<WaveSpawnerManager>();
        int currentMap = waveManager.GetCurrentMap();
        if (currentMap > 0)
            LoadScene("Map-" + (currentMap + 1));
        else
            LoadMap1();
    }
    public void LoadMenu()
    {
        LoadScene("Menu");
    }
    public void LoadTutorial()
    {
        LoadScene("Tutorial");
    }

    public void LoadCredits()
    {

        LoadScene("Credits");
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMap1()
    {
        LoadScene("Map-1");
    }

    public void LoadMap2()
    {
        LoadScene("Map-2");
    }

    public void LoadMap3()
    {
        LoadScene("Map-3");
    }

    public void LoadMap4()
    {
        LoadScene("Map-4");
    }

    public void LoadMap5()
    {
        LoadScene("Map-5");
    }

    public void LoadMap6()
    {
        LoadScene("Map-6");
    }

    public void LoadMap7()
    {
        LoadScene("Map-7");
    }

    public void LoadMap8()
    {
        LoadScene("Map-8");
    }

    public void LoadMap9()
    {
        LoadScene("Map-9");
    }


    void LoadScene(string _scene)
    {

        scene = _scene;
        audioSource.PlayOneShot(OnSceneLoad);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        Time.timeScale = 1;
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            SceneManager.LoadScene(scene);
        });
    }
    #endregion

}
