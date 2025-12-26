using TMPro;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ShopController controller;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private ShopSlot slotPrefab;
    [SerializeField] private TMP_Text infoText;

    private void OnEnable()
    {
        controller ??= GetComponent<ShopController>();
        controller.OpenShop();
        
        controller.Model.OnOffersChanged += Rebuild;
        controller.Model.OnPurchasesLeftChanged += UpdateInfo;

        Rebuild();
        UpdateInfo();
    }

    private void OnDisable()
    {
        controller.Model.OnOffersChanged -= Rebuild;
        controller.Model.OnPurchasesLeftChanged -= UpdateInfo;
    }

    private void Rebuild()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        foreach (var offer in controller.Model.Offers)
        {
            var upgrade = controller.GetUpgradeById(offer.UpgradeId);
            if (upgrade == null) continue;

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Bind(offer, upgrade, controller);
        }
    }

    private void UpdateInfo()
    {
        infoText.gameObject.SetActive(controller.Model.IsLocked);

        if (controller.Model.IsLocked)
            infoText.text = "Пройди трек, чтобы обновить магазин";
    }
}
