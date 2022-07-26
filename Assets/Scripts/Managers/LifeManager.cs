using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class LifeManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] List<GameObject> lifes;
    [SerializeField] ParticleSystem onHitNexusVFX;
    [SerializeField] AudioClip onHitNexusSFX;
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

        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy.IsDead())
            return;

        var currentLife = lifes.LastOrDefault();
        currentLife.SetActive(false);
        lifes.Remove(currentLife);
        Destroy(other.gameObject);


        Instantiate(onHitNexusVFX, other.transform.position, Quaternion.identity);

        if (RemainingLifes() == 0)
        {
            tmpHeader.text = "Game Over";
            audioSource.PlayOneShot(gameOverSound);
            gameOverPanel.SetActive(true);
            PauseGame();
        }
        else
        {
            audioSource.PlayOneShot(onHitNexusSFX);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    public int RemainingLifes()
    {
        return lifes.Count;
    }
}
