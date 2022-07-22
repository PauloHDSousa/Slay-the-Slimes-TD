using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActionsManager : MonoBehaviour
{
    [Space(10)]
    [Header("SFX")]
    [SerializeField]
    AudioClip onHoverSound;
    [SerializeField]
    AudioClip onClickSound;

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

    void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(onClickSound);
        SceneManager.LoadScene(scene);
    }

    public void OnHoverSound()
    {
        audioSource.PlayOneShot(onHoverSound);
    }
}
