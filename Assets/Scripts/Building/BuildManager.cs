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
    [SerializeField] TextMeshProUGUI upgradeOrSellWeaponLevel;
    [SerializeField] GameObject upgradeButton;

    [SerializeField] Image selectedWeaponUI;
    [SerializeField] TextMeshProUGUI tmpUpgradePrice;
    [SerializeField] TextMeshProUGUI tmpSellPrice;
    [SerializeField] Material selectedWeaponMaterial;



    [Header("SFX")]
    [Space(4)]
    [SerializeField] AudioClip cantUpgradeSFX;

    Camera mainCamera;
    //Weapon build data
    GameObject currentWeapon;
    GameObject previewWeapon;
    int weaponPrice;
    Node currentNode;
    WeaponInfo weaponInfo;
    //Weapon upgrade Data
    GameObject upgradeCurrentWeapon;
    GameObject upgradeNode;
    int sellPrice;
    int upgradePrice;
    TowerShoot currentTowerShot;
    TowerIA currentTowerIA;
    //Curency
    CurrencyManager currencyManager;

    //Tags
    string tagBuilded = "Builded";
    string tagShowingUI = "ShowingUI";

    //SFX
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.canceled += Build;

    }

    private void OnDisable()
    {
        mouseClickAction.canceled += Build;
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
        //Can only build in buildable area, this is important to deny build 2 towers at the same place
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit, range, layerMask) && hit.collider && hit.collider.CompareTag(buildableTag))
        {
            if (currentWeapon == null || !currencyManager.CanBuy(weaponPrice))
                return;

            //Buy weapon
            currencyManager.BuyItem(weaponPrice);

            if (currentNode != null)
                currentNode.DestroyPreview();


            hit.collider.tag = tagBuilded;

            var instantiatedWeapon = Instantiate(currentWeapon,
                new Vector3(hit.collider.transform.position.x,
                            hit.collider.transform.position.y + 0.05f,
                            hit.collider.transform.position.z), currentWeapon.transform.rotation);

            instantiatedWeapon.transform.parent = hit.collider.transform;

            weaponInfo = instantiatedWeapon.GetComponent<WeaponInfo>();

            Instantiate(weaponInfo.GetParticlesVFX(), hit.collider.transform.position, Quaternion.identity);
        }
        else if (hit.collider != null && hit.collider.tag == tagBuilded)
        {
            currentTowerIA = hit.collider.GetComponentInChildren<TowerIA>();
            if (currentTowerIA == null)
                return;

            var uiPos = new Vector3(hit.collider.transform.position.x,
                         hit.collider.transform.position.y + currentTowerIA.GetWeaponSize(),
                         hit.collider.transform.position.z - 1.2f);

            upgradeOrSellUI.transform.position = uiPos;
            upgradeOrSellUI.SetActive(true);
            hit.collider.tag = tagShowingUI;

            currentTowerShot = hit.collider.GetComponentInChildren<TowerShoot>();
            upgradeCurrentWeapon = currentTowerIA.gameObject;

            if (currentTowerIA.CurrentWeaponLevel == 3)
            {
                upgradeButton.SetActive(false);
                upgradeOrSellWeaponLevel.text = $"Max Level";
                upgradeOrSellWeaponLevel.fontSize = 32;
            }
            else
                upgradeOrSellWeaponLevel.text = $"Level {currentTowerIA.CurrentWeaponLevel}";

            sellPrice = currentTowerIA.GetSellPrice();
            upgradePrice = currentTowerIA.GetUpgradePrice();

            tmpSellPrice.text = sellPrice.ToString();
            tmpUpgradePrice.text = upgradePrice.ToString();
            upgradeNode = hit.collider.gameObject;
        }
        else if (hit.collider != null && hit.collider.tag == tagShowingUI)
        {
            upgradeOrSellUI.SetActive(false);
            hit.collider.tag = tagBuilded;
        }
    }

    #region Weapons

    //Buy - Check for price latter.
    public void SetWeapon(GameObject _weapon)
    {
        if (currentWeapon == null || currentWeapon != _weapon)
            currentWeapon = _weapon;
    }

    public void SetWeaponPrice(int price)
    {
        if (!currencyManager.CanBuy(price))
            audioSource.PlayOneShot(cantUpgradeSFX);

        weaponPrice = price;
    }

    public void SetPreviewWeapon(GameObject _previewWeapon)
    {
        if (previewWeapon == null || previewWeapon != _previewWeapon)
            previewWeapon = _previewWeapon;
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
        upgradeNode.tag = buildableTag;

        Instantiate(weaponInfo.GetParticlesVFX(), upgradeCurrentWeapon.transform.position, Quaternion.identity);
    }

    public void UpgradeWeapon()
    {
        if (upgradePrice != 0 && currentTowerIA != null && !currencyManager.CanBuy(upgradePrice))
        {
            audioSource.PlayOneShot(cantUpgradeSFX);
            return;
        }

        currencyManager.BuyItem(upgradePrice);
        currentTowerShot.UpgradeTower();
        upgradeOrSellUI.SetActive(false);
        currentTowerIA.CurrentWeaponLevel += 1;
    }

    public void SetCurrentNode(Node _node)
    {
        currentNode = _node;
    }
    #endregion
}
