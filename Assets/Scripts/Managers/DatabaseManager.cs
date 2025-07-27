using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    private PlayerDatabase _playerDatabase;
    private string filePath => Path.Combine(Application.persistentDataPath, "player_data.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadDatabase();
    }


    /// <summary>
    /// Найти или создать новый профиль игрока.
    /// Новому игроку даём Badge = count+1 и RankId = 0.
    /// </summary>
    public PlayerData GetOrCreatePlayer(string nickname)
    {
        if (_playerDatabase == null || _playerDatabase.players == null)
        {
            LoadDatabase();
        }

        var existing = _playerDatabase.players
            .Find(p => string.Equals(p.Nickname, nickname, StringComparison.OrdinalIgnoreCase));

        if (existing != null)
            return existing;

        var newBadge = _playerDatabase.players.Count + 1;
        var pd = new PlayerData
        {
            Nickname = nickname,
            RankId = 0,
            BadgeNumber = newBadge
        };

        _playerDatabase.players.Add(pd);
        SaveDatabase();
        return pd;
    }


    private void LoadDatabase()
    {
        try
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _playerDatabase = JsonUtility.FromJson<PlayerDatabase>(json);
            }

            if (_playerDatabase == null)
            {
                _playerDatabase = new PlayerDatabase();
            }

            if (_playerDatabase.players == null)
            {
                _playerDatabase.players = new List<PlayerData>();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load DB: {e.Message}");
            _playerDatabase = new PlayerDatabase();
        }

        SaveDatabase();
    }


    public void SaveDatabase()
    {
        try
        {
            var json = JsonUtility.ToJson(_playerDatabase, true);
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save DB: {e.Message}");
        }
    }
}
