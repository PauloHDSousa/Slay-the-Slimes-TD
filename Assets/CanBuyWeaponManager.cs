using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanBuyWeaponManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] int weaponPrice;

    Button button;
    CurrencyManager currencyManager;
    void Start()
    {

        button = GetComponent<Button>();
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = currencyManager.CanBuy(weaponPrice);
    }
}
