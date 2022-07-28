using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper
{
    private PlayerData _playerData;

    private static PlayerHelper _playerHelper;
    public PlayerHelper()
    {
        _playerData = new PlayerData();
        ResetPlayerData();
    }
    
    public static PlayerHelper Instance
    {
        get
        {
            if (_playerHelper == null)
            {
                _playerHelper = new PlayerHelper();
            }

            return _playerHelper;
        }
    }

    public PlayerData Player => _playerData;
    private void Save()
    {
        SaveSystem.Save(_playerData);
    }
    public void UpdatePlayerData(object data)
    {
        _playerData = data as PlayerData;
    }
    public void ResetPlayerData()
    {
        _playerData.Build();

        Save();
    }

    public void UpdateLevel(int amount)
    {
        _playerData.Level += amount;

        Save();
    }
    
}
