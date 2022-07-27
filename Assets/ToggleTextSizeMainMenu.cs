using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleTextSizeMainMenu : MonoBehaviour
{

    TextMeshProUGUI tmpTitle;
    void Start()
    {
        tmpTitle = GetComponent<TextMeshProUGUI>();
        GoUp();
    }
    void GoUp()
    {

        LeanTween.value(tmpTitle.gameObject, 130f, 145f, 1.5f).setOnUpdate((float val) =>
        {
            tmpTitle.fontSize = val;
        }).setOnComplete(() =>
        {
            GoDown();
        });
    }

    void GoDown()
    {

        LeanTween.value(tmpTitle.gameObject, 145f, 130f, 1.5f).setOnUpdate((float val) =>
        {
            tmpTitle.fontSize = val;
        }).setOnComplete(() =>
        {
            GoUp();
        });
    }
}
