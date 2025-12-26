using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public static class GameLoadSave
{
    //C:\Users\<Пользователь>\AppData\LocalLow\DefaultCompany\Arcade Car Rouglike
    private static string path => System.IO.Path.Combine(Application.persistentDataPath, "Game.json");
    public static GameState gameState;

    public static GameState LoadState()
    {
        if (!File.Exists(path))
            return null;
        
        using (StreamReader reader = new(path))
        {
            var json = reader.ReadToEnd();
            Debug.Log("Loaded state:" + path);
            return JsonConvert.DeserializeObject<GameState>(json);
        }
    }

    public static void SaveState()
    {
        var json = JsonConvert.SerializeObject(gameState);
        using (StreamWriter writer = new(path))
        {
            writer.Write(json);
        }
    }
    
    public static void Initialize()
    {
        gameState = LoadState();
        if (gameState == null)
        {
            gameState = new GameState
            {
                tracksData = new List<TrackData>()
            };
            
            gameState.tracksData.Add(new TrackData
            {
                trackID = 1,
                isUnlocked = true,
                isTimeSet = false,
                bestLapTime = 0f,
                stars = 0
            });
            
            for (var i = 2; i <= 4; i++)
            {
                gameState.tracksData.Add(new TrackData
                {
                    trackID = i,
                    isUnlocked = false,
                    isTimeSet = false,
                    bestLapTime = 0f,
                    stars = 0
                });
            }

            SaveState();
        }
    }
}