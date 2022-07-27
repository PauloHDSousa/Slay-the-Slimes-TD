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
    [SerializeField] AudioClip onSellSFX;
    [SerializeField] AudioClip onUpgradeSFX;
    [SerializeField] AudioClip buildStartSFX;
    [SerializeField] AudioClip buildEndSFX;
    [SerializeField] AudioClip poisonBuildEndSFX;
    [SerializeField] AudioClip damageBuildEndSFX;


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
    Image currentWeaponCanvas;
    Color deafultWeaponCanvasColor;
    //Curency
    CurrencyManager currencyManager;

    //Tags
    string tagBuilded = "Builded";
    string tagShowingUI = "ShowingUI";

    //SFX
    AudioSource audioSource;
    //Tamashis
    public bool damageTamashiCreated = false;
    public bool poisonTamashiCreated = false;

    bool hoveringUI = false;

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
        if (hoveringUI)
            return;

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

            if (weaponInfo.GetIsDamageTamashi())
            {
                damageTamashiCreated = true;
                currentWeapon = null;
                previewWeapon = null;
                weaponPrice = 0;
                currentNode.DestroyPreview();

            }
            else if (weaponInfo.GetIsPoisonTamashi())
            {
                poisonTamashiCreated = true;
                currentWeapon = null;
                previewWeapon = null;
                weaponPrice = 0;
                currentNode.DestroyPreview();
            }
            if (weaponInfo.GetIsPoisonTamashi())
            {
                audioSource.PlayOneShot(poisonBuildEndSFX);
                ResetCurrentCanvasWeaponColor();
            }
            else if (weaponInfo.GetIsDamageTamashi())
            {
                audioSource.PlayOneShot(damageBuildEndSFX);
                ResetCurrentCanvasWeaponColor();
            }
            else
                audioSource.PlayOneShot(buildEndSFX);


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

            upgradeButton.SetActive(true);

            if (currentTowerIA.CurrentWeaponLevel == 3)
            {
                upgradeButton.SetActive(false);
                upgradeOrSellWeaponLevel.text = $"Max Level";
                upgradeOrSellWeaponLevel.fontSize = 32;
            }
            else
            {
                upgradeOrSellWeaponLevel.text = $"Level {currentTowerIA.CurrentWeaponLevel}";
                upgradeOrSellWeaponLevel.fontSize = 22;
            }

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

        audioSource.PlayOneShot(buildStartSFX);
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

    public void SetCurrentCanvasWeapon(Image canvasImage)
    {
        if (currentWeaponCanvas != null)
            ResetCurrentCanvasWeaponColor();

        currentWeaponCanvas = canvasImage;
        deafultWeaponCanvasColor = currentWeaponCanvas.color;
        var color = currentWeaponCanvas.color;
        color.a = 255;
        currentWeaponCanvas.color = color;
    }

    void ResetCurrentCanvasWeaponColor()
    {
        if (currentWeaponCanvas != null)
            currentWeaponCanvas.color = deafultWeaponCanvasColor;
    }

    public GameObject GetWeaponPrefabPreview()
    {
        return previewWeapon;
    }
    public int GetWeaponPrice()
    {
        return weaponPrice;
    }

    //Upgrade and Sell

    public void HoverUIButtons()
    {
        hoveringUI = true;
    }

    public void ExitUIButtons()
    {
        hoveringUI = false;
    }


    public void SellWeapon()
    {
        hoveringUI = false;
        Destroy(upgradeCurrentWeapon);
        currencyManager.WonGold(sellPrice);
        upgradeOrSellUI.SetActive(false);
        upgradeNode.tag = buildableTag;

        audioSource.PlayOneShot(onSellSFX);
        Instantiate(weaponInfo.GetParticlesVFX(), upgradeCurrentWeapon.transform.position, Quaternion.identity);
    }

    public void UpgradeWeapon()
    {
        hoveringUI = false;
        if (upgradePrice != 0 && currentTowerIA != null && !currencyManager.CanBuy(upgradePrice))
        {
            audioSource.PlayOneShot(cantUpgradeSFX);
            return;
        }

        upgradeNode.tag = tagBuilded;
        audioSource.PlayOneShot(onUpgradeSFX);
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
