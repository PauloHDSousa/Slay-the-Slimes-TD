using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] Color upgradeSellColor;

    Color defaultColor;

    Renderer _renderer;
    Transform _position;

    string tagBuilded = "Builded";
    string tagShowingUI = "ShowingUI";
    string tagBuildable = "Buildable";

    CurrencyManager currencyManager;
    BuildManager buildManager;
    GameObject previewWeapon;

    private void Start()
    {
        _position = GetComponent<Transform>();
        _renderer = GetComponent<Renderer>();
        defaultColor = _renderer.material.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (gameObject.tag == tagBuilded || gameObject.tag == tagShowingUI)
        {
            _renderer.material.color = upgradeSellColor;
            return;
        }

        buildManager = FindObjectOfType<BuildManager>();
        currencyManager = FindObjectOfType<CurrencyManager>();


        if (!currencyManager.CanBuy(buildManager.GetWeaponPrice()))
        {
            _renderer.material.color = notEnoughMoneyColor;
            return;
        }

        gameObject.tag = tagBuildable;
        _renderer.material.color = hoverColor;
        var prefabPreviewWeapon = buildManager.GetWeaponPrefabPreview();

        if (prefabPreviewWeapon == null)
            return;

        previewWeapon = Instantiate(prefabPreviewWeapon,
            new Vector3(_position.transform.position.x,
                        _position.transform.position.y + 0.05f,
                        _position.transform.position.z), prefabPreviewWeapon.transform.rotation);

        buildManager.SetCurrentNode(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Remove the hover color and the preview Weapon then return;
        if (previewWeapon != null)
            Destroy(previewWeapon);

        _renderer.material.color = defaultColor;
        if (gameObject.tag == tagBuilded)
            return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DestroyPreview();
    }

    public void DestroyPreview()
    {
        Destroy(previewWeapon);
        _renderer.material.color = defaultColor;
    }
}
