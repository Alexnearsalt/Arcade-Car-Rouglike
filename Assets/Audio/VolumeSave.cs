using System.IO;
using UnityEngine;

public class VolumeSave : MonoBehaviour
{
    //C:\Users\<Пользователь>\AppData\LocalLow\DefaultCompany\Arcade Car Rouglike
    private static string Path => System.IO.Path.Combine(Application.persistentDataPath, "volume.json");
    
    public static void Save(VolumeData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);
    }

    public static VolumeData Load()
    {
        if (!File.Exists(Path))
            return null;

        var json = File.ReadAllText(Path);
        return JsonUtility.FromJson<VolumeData>(json);
    }
}
