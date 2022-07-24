using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip onHover;
    [SerializeField] AudioClip OnSceneLoad;
    [SerializeField] int onHoverFontSize;

    float defaultSize;
    AudioSource audioSource;
    string scene;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        LoadScene("Menu");
    }

    public void LoadCredits()
    {
        Time.timeScale = 1f;
        LoadScene("Credits");
    }

    void LoadScene(string _scene)
    {
        scene = _scene;
        audioSource.PlayOneShot(OnSceneLoad);
        Invoke("Load", 0.5f);
        
    }

    public void Load()
    {
        SceneManager.LoadScene(scene);

    }
    #endregion

}
