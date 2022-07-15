using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    Color defaultColor;

    Renderer _renderer;

    string tagBuilded = "Builded";
    string tagUnBuilded = "UnBuildable";
    string tagBuildable = "Buildable";

    CurrencyManager currencyManager;
    BuildManager buildManager;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        defaultColor = _renderer.material.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.tag == tagBuilded)
            return;

        currencyManager = FindObjectOfType<CurrencyManager>();
        buildManager = FindObjectOfType<BuildManager>();


        if (!currencyManager.CanBuy(buildManager.GetWeaponPrice()))
        {
            _renderer.material.color = notEnoughMoneyColor;
            return;
        }

        gameObject.tag = tagBuildable;
        _renderer.material.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Remove the hover color then return;
        _renderer.material.color = defaultColor;
        if (gameObject.tag == tagBuilded)
            return;
        gameObject.tag = tagUnBuilded;
    }
}
