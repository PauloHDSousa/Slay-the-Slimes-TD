using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanBuyWeaponManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] int weaponPrice;
    [SerializeField] bool isDamageTamashi;
    [SerializeField] bool isPoisonTamashi;

    Button button;
    CurrencyManager currencyManager;
    BuildManager buildManager;
    void Start()
    {

        button = GetComponent<Button>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        buildManager = FindObjectOfType<BuildManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamageTamashi && buildManager.damageTamashiCreated)
        {
            button.interactable = false;
            return;
        }
        else if (isPoisonTamashi && buildManager.poisonTamashiCreated)
        {
            button.interactable = false;
            return;
        }


        button.interactable = currencyManager.CanBuy(weaponPrice);
    }
}
