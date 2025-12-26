using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }
    public PlayerProgress Data { get; private set; } = new PlayerProgress();
    private HashSet<string> _owned = new HashSet<string>();
    private string SavePath => Path.Combine(Application.persistentDataPath, "progress.json");
    public int Coins => Data.coins;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        Data.coins += amount;
        Save();
    }

    public bool HasUpgrade(string upgradeId) => !string.IsNullOrEmpty(upgradeId) && _owned.Contains(upgradeId);

    public bool TryBuyUpgrade(CarUpgrade upgrade)
    {
        if (upgrade == null) return false;

        var id = upgrade.UpgradeId;
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("CarUpgrade.UpgradeId пустой");
            return false;
        }

        if (_owned.Contains(id)) return false;
        if (Data.coins < upgrade.Price) return false;

        Data.coins -= upgrade.Price;
        Data.ownedUpgrades.Add(id);
        _owned.Add(id);

        Save();
        return true;
    }

    public void Save()
    {
        try
        {
            if (Data.ownedUpgrades == null) Data.ownedUpgrades = new List<string>();
            File.WriteAllText(SavePath, JsonUtility.ToJson(Data, true));
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Save error: {e}");
        }
    }

    public void Load()
    {
        if (File.Exists(SavePath))
        {
            var json = File.ReadAllText(SavePath);
            Data = JsonUtility.FromJson<PlayerProgress>(json);
        }
        else Data = new PlayerProgress();

        if (Data.ownedUpgrades == null) Data.ownedUpgrades = new List<string>();

        _owned.Clear();
        foreach (var id in Data.ownedUpgrades)
            if (!string.IsNullOrEmpty(id)) _owned.Add(id);
    }
    private void OnApplicationQuit() { Save(); }
}
