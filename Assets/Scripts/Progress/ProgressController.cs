using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public static ProgressController Instance { get; private set; }

    public ProgressModel Model { get; private set; }

    private ProgressSave _repo;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Model = new ProgressModel();
        _repo = new ProgressSave();

        var dto = _repo.Load();
        Model.FromDto(dto);
        
        Model.OnCoinsChanged += _ => Save();
        Model.OnOwnedUpgradesChanged += Save;
        Model.OnShopChanged += Save;
    }

    public void Save()
    {
        var dto = Model.ToDto();
        _repo.Save(dto);
    }
    
    public void AddCoins(int amount)
    {
        Model.AddCoins(amount);
    }

    public bool HasUpgrade(string upgradeId)
    {
        return Model.HasUpgrade(upgradeId);
    }

    public int GetUpgradeCount(string upgradeId)
    {
        return Model.GetUpgradeCount(upgradeId);
    }
    
    public bool TryBuyUpgrade(CarUpgrade upgrade)
    {
        if (upgrade == null) return false;

        var id = upgrade.UpgradeId;
        var price = upgrade.Price;
        if (!Model.TrySpendCoins(price))
            return false;

        Model.AddOwnedUpgrade(id);
        return true;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}