using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public static MenuSoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
