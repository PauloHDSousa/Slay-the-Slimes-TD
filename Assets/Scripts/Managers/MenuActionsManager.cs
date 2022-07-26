using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActionsManager : MonoBehaviour
{
    [Space(1)]
    [Header("SFX")]
    [SerializeField]
    AudioClip OnSceneLoad;


    [Space(2)]
    [Header("Options")]
    [SerializeField] GameObject optiosnPanel;
    [SerializeField] GameObject mainButtonsPanel;


    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
    }

    public void GoToMenu()
    {
        LoadScene("Menu");
    }

    public void ShowOptions()
    {

        audioSource.PlayOneShot(OnSceneLoad);
        optiosnPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);
    }

    public void ShowMenu()
    {

        audioSource.PlayOneShot(OnSceneLoad);
        optiosnPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    public void Exit()
    {
        audioSource.PlayOneShot(OnSceneLoad);
        Application.Quit();
    }

    void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(OnSceneLoad);
        SceneManager.LoadScene(scene);
    }

    
}
