using System.IO;
using UnityEngine;

public class ProgressSave
{
    private readonly string _path;

    public ProgressSave(string fileName = "progress.json")
    {
        _path = Path.Combine(Application.persistentDataPath, fileName);
    }

    public PlayerProgress Load()
    {
        if (!File.Exists(_path))
            return new PlayerProgress();

        var json = File.ReadAllText(_path);
        PlayerProgress dto = JsonUtility.FromJson<PlayerProgress>(json);

        return dto ?? new PlayerProgress();
    }

    public void Save(PlayerProgress dto)
    {
        if (dto == null)
            dto = new PlayerProgress();

        if (dto.ownedUpgrades == null)
            dto.ownedUpgrades = new System.Collections.Generic.List<string>();

        if (dto.shopOfferIds == null)
            dto.shopOfferIds = new System.Collections.Generic.List<string>();

        var json = JsonUtility.ToJson(dto, true);
        File.WriteAllText(_path, json);
    }
}