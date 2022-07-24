using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI tmpCurrentPlayerGold;

    [Header("Configuration")]
    [SerializeField] int startGold;
    [SerializeField] int passiveGoldIncome = 5;
    [SerializeField] float passiveGoldTime = 1.5f;

    int currentGold;
    private void Start()
    {
        currentGold = startGold;
        UpdateUI();
        InvokeRepeating("PassiveGold", passiveGoldTime, passiveGoldTime);
    }
    #region CurrencyManagement
    public void WonGold(int gold)
    {
        currentGold += gold;
        UpdateUI();
    }

    public void BuyItem(int gold)
    {
        currentGold -= gold;
        UpdateUI();
    }

    public bool CanBuy(int gold)
    {
        return currentGold >= gold;
    }
    #endregion

    void PassiveGold()
    {
        currentGold += passiveGoldIncome;
        UpdateUI();
    }

    void UpdateUI()
    {
        tmpCurrentPlayerGold.text = currentGold.ToString();
    }

    public void UpdatePassiveGold(int _passiveGoldIncome)
    {
        passiveGoldIncome = _passiveGoldIncome;
    }
}
