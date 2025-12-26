using UnityEngine;
using System;
using System.Collections.Generic;

public class ProgressModel
{
    public event Action<int> OnCoinsChanged;
    public event Action<string, int> OnUpgradeOwned;
    public event Action OnOwnedUpgradesChanged;
    public event Action OnShopChanged;
    
    private int _coins;
    private readonly Dictionary<string, int> _ownedCounts = new Dictionary<string, int>();
    private bool _shopLocked;
    private int _shopPurchasesLeft;
    private readonly List<string> _shopOfferIds = new List<string>();

    public int Coins => _coins;
    public bool ShopLocked => _shopLocked;
    public int ShopPurchasesLeft => _shopPurchasesLeft;
    public IReadOnlyList<string> ShopOfferIds => _shopOfferIds;
    
    public void SetCoins(int value)
    {
        if (value < 0) value = 0;
        if (_coins == value) return;

        _coins = value;
        OnCoinsChanged?.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        SetCoins(_coins + amount);
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount <= 0) return true;
        if (_coins < amount) return false;

        SetCoins(_coins - amount);
        return true;
    }
    
    public int GetUpgradeCount(string upgradeId)
    {
        if (string.IsNullOrEmpty(upgradeId)) return 0;
        return _ownedCounts.TryGetValue(upgradeId, out var count) ? count : 0;
    }

    public bool HasUpgrade(string upgradeId)
    {
        return GetUpgradeCount(upgradeId) > 0;
    }
    
    public int AddOwnedUpgrade(string upgradeId)
    {
        if (string.IsNullOrEmpty(upgradeId)) return 0;

        var newCount = GetUpgradeCount(upgradeId) + 1;
        _ownedCounts[upgradeId] = newCount;

        OnUpgradeOwned?.Invoke(upgradeId, newCount);
        OnOwnedUpgradesChanged?.Invoke();

        return newCount;
    }
    
    public List<string> GetOwnedUpgradesSnapshot()
    {
        var result = new List<string>();

        foreach (var entry in _ownedCounts)
        {
            var id = entry.Key;
            var count = entry.Value;

            for (var i = 0; i < count; i++)
                result.Add(id);
        }

        return result;
    }
    
    public void ResetOwnedUpgrades(IEnumerable<string> ids)
    {
        _ownedCounts.Clear();

        if (ids != null)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id)) continue;
                _ownedCounts[id] = GetUpgradeCount(id) + 1;
            }
        }

        OnOwnedUpgradesChanged?.Invoke();
    }
    
    public void SetShopState(bool locked, int purchasesLeft, List<string> offerIds)
    {
        _shopLocked = locked;
        _shopPurchasesLeft = purchasesLeft;

        _shopOfferIds.Clear();
        if (offerIds != null) _shopOfferIds.AddRange(offerIds);

        OnShopChanged?.Invoke();
    }

    public PlayerProgress ToDto()
    {
        return new PlayerProgress
        {
            coins = _coins,
            ownedUpgrades = GetOwnedUpgradesSnapshot(),

            shopLocked = _shopLocked,
            shopPurchasesLeft = _shopPurchasesLeft,
            shopOfferIds = new List<string>(_shopOfferIds)
        };
    }

    public void FromDto(PlayerProgress dto)
    {
        dto ??= new PlayerProgress();

        SetCoins(dto.coins);
        ResetOwnedUpgrades(dto.ownedUpgrades);

        SetShopState(dto.shopLocked, dto.shopPurchasesLeft, dto.shopOfferIds);
    }
}
