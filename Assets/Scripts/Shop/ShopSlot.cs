using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private ShopOffer _offer;
    private ShopController _controller;

    public void Bind(ShopOffer offer, CarUpgrade upgrade, ShopController controller)
    {
        _offer = offer;
        _controller = controller;

        icon.sprite = upgrade.Icon;
        nameText.text = upgrade.DisplayName;
        descriptionText.text = upgrade.Description;
        priceText.text = upgrade.Price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyClicked);
    }

    private void OnBuyClicked()
    {
        var success = _controller.TryBuy(_offer);
        if (success)
            Destroy(gameObject);
    }
}