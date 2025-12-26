using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<CarUpgrade> allUpgrades = new List<CarUpgrade>();

    public ShopModel Model { get; private set; }

    private ProgressController _progress;

    private const int MaxPurchasesPerRun = 3;
    private const int SlotCount = 3;

    private void Awake()
    {
        Model = new ShopModel();
        _progress = ProgressController.Instance;
    }
    public void OpenShop()
    {
        var progress = _progress.Model;
        
        if (progress.ShopOfferIds.Count > 0)
        {
            var offers = new List<ShopOffer>();
            foreach (var id in progress.ShopOfferIds)
                offers.Add(new ShopOffer(id));

            Model.ResetShop(offers, progress.ShopPurchasesLeft);
            return;
        }
        
        GenerateNewShop();
    }

    public void GenerateNewShop()
    {
        var offers = GenerateRandomOffers();
        Model.ResetShop(offers, MaxPurchasesPerRun);
        
        var ids = new List<string>();
        foreach (var offer in offers)
            ids.Add(offer.UpgradeId);

        _progress.Model.SetShopState(
            locked: false,
            purchasesLeft: MaxPurchasesPerRun,
            offerIds: ids
        );

        _progress.Save();
    }

    public bool TryBuy(ShopOffer offer)
    {
        if (offer == null) 
            return false;

        if (Model.IsLocked) 
            return false;

        var upgrade = GetUpgradeById(offer.UpgradeId);
        if (upgrade == null) 
            return false;

        if (!_progress.TryBuyUpgrade(upgrade)) 
            return false;

        Model.RemoveOffer(offer);
        Model.DecreasePurchases();

        var offerIds = Model.Offers.Select(o => o.UpgradeId).ToList();

        _progress.Model.SetShopState(
            locked: Model.IsLocked,
            purchasesLeft: Model.PurchasesLeft,
            offerIds: offerIds
        );

        _progress.Save();

        return true;
    }
    
    public void OnTrackFinished()
    {
        GenerateNewShop();
    }

    private List<ShopOffer> GenerateRandomOffers()
    {
        var pool = allUpgrades
            .Where(u => u != null)
            .ToList();

        var result = new List<ShopOffer>();

        for (var i = 0; i < SlotCount && pool.Count > 0; i++)
        {
            var index = Random.Range(0, pool.Count);
            var upgrade = pool[index];

            result.Add(new ShopOffer(upgrade.UpgradeId));
            pool.RemoveAt(index);
        }

        return result;
    }
    
    public CarUpgrade GetUpgradeById(string id)
    {
        return allUpgrades.FirstOrDefault(u => u != null && u.UpgradeId == id);
    }
}
