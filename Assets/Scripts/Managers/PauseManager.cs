using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] InputAction pauseAction;
    [SerializeField] GameObject pauseModal;
    [SerializeField] TextMeshProUGUI tmpHeader;
    [SerializeField] AudioClip pauseSound;

    bool isPaused = false;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += TooglePause;
    }

    private void OnDisable()
    {
        pauseAction.performed -= TooglePause;
        pauseAction.Disable();
    }

    private void TooglePause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;
        pauseModal.SetActive(isPaused);
        audioSource.PlayOneShot(pauseSound);

        if (isPaused)
        {
            tmpHeader.text = "Paused";
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }
}
