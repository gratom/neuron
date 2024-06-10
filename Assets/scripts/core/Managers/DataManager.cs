using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Datas
{
    public class DataManager : BaseManager
    {
#pragma warning disable

        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameData gameData;
        [SerializeField] private StaticData staticData;

#pragma warning restore

        public override Type ManagerType => typeof(DataManager);

        public PlayerData PlayerData
        {
            get
            {
                if (playerData == null)
                {
                    playerData = new PlayerData();
                }
                return playerData;
            }
        }

        public GameData GameData
        {
            get
            {
                if (gameData == null)
                {
                    gameData = new GameData();
                }
                return gameData;
            }
        }

        public StaticData StaticData
        {
            get
            {
                if (staticData == null)
                {
                    staticData = new StaticData();
                }
                return staticData;
            }
        }

        private static string PlayerDataSaveLocation => "playerData";

        private static string GameDataSaveLocation => "gameData";

        #region Unity functions

        private void Awake()
        {
            Init();
        }

        private void OnApplicationQuit()
        {
            SaveAllData();
            Debug.Log("Save");
        }

        private void OnApplicationPause(bool pause)
        {
            SaveAllData();
        }

        #endregion Unity functions

        #region public functions

        public void SaveAllData()
        {
            SaveData(playerData, PlayerDataSaveLocation);
            SaveData(gameData, GameDataSaveLocation);
            PlayerPrefs.Save();
        }

        public void ResetPlayerData()
        {
            playerData = new PlayerData();
        }

        #endregion public functions

        #region private functions

        private void Init()
        {
            LoadOrCreateData(ref playerData, PlayerDataSaveLocation);
            LoadOrCreateData(ref gameData, GameDataSaveLocation);
        }

        private void LoadOrCreateData<T>(ref T value, string location) where T : new()
        {
            string data = PlayerPrefs.GetString(location);
            if (data != "")
            {
                value = JsonUtility.FromJson<T>(data);
            }
            else
            {
                value = new T();
            }
        }

        private void SaveData<T>(T value, string location)
        {
            PlayerPrefs.SetString(location, JsonUtility.ToJson(value));
        }

        #endregion private functions
    }

    [Serializable]
    public class GameData
    {
#pragma warning disable
        [SerializeField] private Setting setting;
#pragma warning restore

        public Setting Setting => setting != null ? setting : new Setting();
    }

    [Serializable]
    public class StaticData
    {
        public enum Scenes
        {
            bootScene = 0,
            mainScene = 1
        }
    }

    [Serializable]
    public class Setting
    {
    }

    [Serializable]
    public class PlayerData
    {
    }
}
