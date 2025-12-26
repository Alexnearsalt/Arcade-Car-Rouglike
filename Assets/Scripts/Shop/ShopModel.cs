using UnityEngine;
using System;
using System.Collections.Generic;

public class ShopModel
{
    public event Action OnOffersChanged;
    public event Action OnPurchasesLeftChanged;

    private readonly List<ShopOffer> _offers = new List<ShopOffer>();
    private int _purchasesLeft;

    public IReadOnlyList<ShopOffer> Offers => _offers;
    public int PurchasesLeft => _purchasesLeft;
    public bool IsLocked => _purchasesLeft <= 0;

    public void ResetShop(List<ShopOffer> offers, int purchasesLeft)
    {
        _offers.Clear();
        if (offers != null)
            _offers.AddRange(offers);

        _purchasesLeft = purchasesLeft;

        OnOffersChanged?.Invoke();
        OnPurchasesLeftChanged?.Invoke();
    }

    public void RemoveOffer(ShopOffer offer)
    {
        if (_offers.Remove(offer))
            OnOffersChanged?.Invoke();
    }

    public void DecreasePurchases()
    {
        if (_purchasesLeft <= 0) return;

        _purchasesLeft--;
        OnPurchasesLeftChanged?.Invoke();
    }
}