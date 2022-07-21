using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] LayerMask layerMask;
    [SerializeField] [Range(15, 25)] int range;
    [SerializeField] string buildableTag;

    [Header("UI")]
    [Space(2)]
    [SerializeField] GameObject upgradeOrSellUI;
    [SerializeField] Image selectedWeaponUI;
    [SerializeField] TextMeshProUGUI tmpUpgradePrice;
    [SerializeField] TextMeshProUGUI tmpSellPrice;
    [SerializeField] Material selectedWeaponMaterial;

    [Header("VFX")]
    [Space(2)]
    [SerializeField] ParticleSystem onSellVFX;

    Camera mainCamera;
    //Weapon build data
    GameObject currentWeapon;
    GameObject previewWeapon;
    int weaponPrice;
    //Weapon upgrade Data
    GameObject upgradeCurrentWeapon;
    int sellPrice;
    int upgradePrice;
    TowerShoot currentTowerShot;
    //Curency
    CurrencyManager currencyManager;

    private void Awake()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.performed += Build;
    }

    private void OnDisable()
    {
        mouseClickAction.performed -= Build;
        mouseClickAction.Disable();
    }

    private void Update()
    {
        if (currentWeapon == null)
            return;

        if (!currencyManager.CanBuy(upgradePrice))
            return;
    }

    private void Build(InputAction.CallbackContext ctx)
    {
        if (currentWeapon == null)
            return;

        if (!currencyManager.CanBuy(weaponPrice))
            return;

        //Can only build in buildable area, this is important to deny build 2 towers at the same place
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit, range, layerMask) && hit.collider && hit.collider.CompareTag(buildableTag))
        {
            //Buy weapon
            currencyManager.BuyItem(weaponPrice);

            hit.collider.tag = "Builded";

            var instantiatedWeapon = Instantiate(currentWeapon,
                new Vector3(hit.collider.transform.position.x,
                            hit.collider.transform.position.y + 0.05f,
                            hit.collider.transform.position.z), currentWeapon.transform.rotation);

            instantiatedWeapon.transform.parent = hit.collider.transform;
            var vfx = Instantiate(onSellVFX, hit.collider.transform.position, Quaternion.identity);
            Destroy(vfx, 1);

        }
        else if (hit.collider != null && hit.collider.tag == "Builded")
        {
            var uiPos = new Vector3(hit.collider.transform.position.x,
                            hit.collider.transform.position.y + 1f,
                            hit.collider.transform.position.z - 1.2f);

            upgradeOrSellUI.transform.position = uiPos;
            upgradeOrSellUI.SetActive(true);
            hit.collider.tag = "ShowingUI";


            var towerIA = hit.collider.GetComponentInChildren<TowerIA>();
            currentTowerShot = hit.collider.GetComponentInChildren<TowerShoot>();
            upgradeCurrentWeapon = towerIA.gameObject;

            sellPrice = towerIA.GetSellPrice();
            upgradePrice = towerIA.GetUpgradePrice();

            tmpSellPrice.text = sellPrice.ToString();
            tmpUpgradePrice.text = upgradePrice.ToString();
        }
        else if (hit.collider != null && hit.collider.tag == "ShowingUI")
        {
            upgradeOrSellUI.SetActive(false);
            hit.collider.tag = "Builded";
        }
    }

    #region Weapons

    //Buy - Check for price latter.
    public void SetWeapon(GameObject _weapon)
    {
        if (currentWeapon == null)
            currentWeapon = _weapon;
        else if (currentWeapon == _weapon)
            currentWeapon = null;
    }

    public void SetWeaponPrice(int price)
    {
        weaponPrice = price;
    }

    public void SetPreviewWeapon(GameObject _previewWeapon)
    {
        if (previewWeapon == null)
            previewWeapon = _previewWeapon;
        else if (previewWeapon == _previewWeapon)
            previewWeapon = null;
    }

    public GameObject GetWeaponPrefabPreview()
    {
        return previewWeapon;
    }
    public int GetWeaponPrice()
    {
        return weaponPrice;
    }

    //Upgrade
    public void SellWeapon()
    {
        Destroy(upgradeCurrentWeapon);
        currencyManager.WonGold(sellPrice);
        upgradeOrSellUI.SetActive(false);

        var vfx = Instantiate(onSellVFX, upgradeCurrentWeapon.transform.position, Quaternion.identity);
        Destroy(vfx, 1);

    }
    public void UpgradeWeapon()
    {
        currentTowerShot.UpgradeTower();
        upgradeOrSellUI.SetActive(false);
    }
    #endregion
}
