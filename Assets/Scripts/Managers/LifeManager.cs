using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class LifeManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] List<GameObject> lifes;

    [Header("GameOver")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI tmpHeader;
    [SerializeField] AudioClip gameOverSound;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        var currentLife = lifes.LastOrDefault();
        currentLife.SetActive(false);
        lifes.Remove(currentLife);
        Destroy(other.gameObject);

        if(lifes.Count == 0)
        {
            tmpHeader.text = "Game Over";
            audioSource.PlayOneShot(gameOverSound);
            gameOverPanel.SetActive(true);
            PauseGame();
        } 
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
