using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] LayerMask layerMask;
    [SerializeField] [Range(15, 25)] int range;
    [SerializeField] string buildableTag;

    [Header("UI")]
    [SerializeField] GameObject improveSellUI;

    Camera mainCamera;
    //Weapon data
    GameObject currentWeapon;
    int weaponPrice;
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

    private void Build(InputAction.CallbackContext ctx)
    {
        //if (currentWeapon == null)
        //    return;

        //if (!currencyManager.CanBuy(weaponPrice))
        //    return;

        //Can only build in buildable area, this is important to deny build 2 towers at the same place
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit, range, layerMask) && hit.collider && hit.collider.CompareTag(buildableTag))
        {
            //Buy weapon
            currencyManager.BuyItem(weaponPrice);

            hit.collider.tag = "Builded";
            Instantiate(currentWeapon,
                new Vector3(hit.collider.transform.position.x,
                            hit.collider.transform.position.y + 0.05f,
                            hit.collider.transform.position.z), currentWeapon.transform.rotation);

        }
        else if (hit.collider.tag == "Builded")
        {

            //improveSellUI.SetActive(true); 
            var uiPos = new Vector3(hit.collider.transform.position.x,
                            hit.collider.transform.position.y + 1.3f,
                            hit.collider.transform.position.z - 2f);

            improveSellUI.transform.position = uiPos;

        }
    }

    #region Weapons

    //Check for price latter.
    public void SetWeapon(GameObject _weapon)
    {
        currentWeapon = _weapon;
    }

    public void SetWeaponPrice(int price)
    {
        weaponPrice = price;
    }

    public int GetWeaponPrice()
    {
        return weaponPrice;
    }
    #endregion
}
