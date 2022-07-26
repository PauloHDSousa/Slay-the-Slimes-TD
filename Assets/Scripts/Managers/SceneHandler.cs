using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    private void Start()
    {
        

        // SCALE
        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        //LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
        //    fader.gameObject.SetActive(false);
        //});
    }
}