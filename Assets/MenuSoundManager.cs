using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public static MenuSoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            return;
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
