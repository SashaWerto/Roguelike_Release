using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YG;

public class Data_Manipulator : MonoBehaviour
{
    public VolumeSettings volumeSettings;
    public List<ItemCell> cellsToSave = new List<ItemCell>();
    private static Data_Manipulator dataManipulator;
    public static Data_Manipulator Instance => dataManipulator;
    private void Awake()
    {
        dataManipulator = this;
    }

    public void Start()
    {
        Load();
        LoadSettings();
    }
    public void Save()
    {
        PlayerData playerData = new PlayerData();
        //saveItems
        if (Inventory.Instance.cells.Count <= 0)
        {
            PlayerData loadedData = new PlayerData();
            loadedData = ConverterTool.ConvertFromString<PlayerData>(PlayerPrefs.GetString("Save"));
            playerData.items = loadedData.items;
        }
        else
        {
            for (int i = 0; i < cellsToSave.Count; i++)
            {
                ItemToSave itemToSave = new ItemToSave();
                itemToSave.item = cellsToSave[i].item;
                itemToSave.count = cellsToSave[i].count;
                itemToSave.level = cellsToSave[i].level;
                itemToSave.isEquiped = cellsToSave[i].equiped;
                playerData.items.Add(itemToSave);
            }
        }
        //saveStats
        playerData.level = Stats.Instance.level;
        playerData.exp = Stats.Instance.exp;
        playerData.skillPoints = Stats.Instance.skillPoints;
        playerData.force = Stats.Instance.force;
        playerData.armor = Stats.Instance.armor;
        playerData.health = Stats.Instance.health;
        //writePath
        var convertedClass = ConverterTool.ConvertToString(playerData);
        PlayerPrefs.SetString("Save", convertedClass);
        SaveWallet();
        SaveCloud(convertedClass);
    }
    public void Load()
    {
        if(!PlayerPrefs.HasKey("Save")) return;
        PlayerData playerData = new PlayerData();
        playerData = ConverterTool.ConvertFromString<PlayerData>(PlayerPrefs.GetString("Save"));
        //loadItems
        if (cellsToSave.Count > 0)
        {
            if (playerData.items.Count > 0)
            {
                for (int i = 0; i < cellsToSave.Count; i++)
                {
                    if (playerData.items[i].item)
                    {
                        cellsToSave[i].item = playerData.items[i].item;
                        cellsToSave[i].count = playerData.items[i].count;
                        cellsToSave[i].level = playerData.items[i].level;
                        cellsToSave[i].equiped = playerData.items[i].isEquiped;
                        cellsToSave[i].RefreshUI();
                        if (cellsToSave[i].equiped)
                        {
                            Equipment.Instance.Equip(cellsToSave[i]);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < playerData.items.Count; i++)
            {
                if (playerData.items[i].isEquiped)
                {
                    Equipment.Instance.Equip(playerData.items[i]);
                }
            }
        }
        //loadStats
        Stats.Instance.level = playerData.level;
        Stats.Instance.exp = playerData.exp;
        Stats.Instance.skillPoints = playerData.skillPoints;
        Stats.Instance.armor = playerData.armor;
        Stats.Instance.force = playerData.force;
        Stats.Instance.health = playerData.health;
        Stats.Instance.Refresh();
        LoadWallet();
        LoadSkills();
    }

    public void SaveWallet()
    {
        WalletData walletData = new WalletData();
        walletData.gold = Wallet.Instance.gold;
        walletData.emeralds = Wallet.Instance.emeralds;
        //writePath
        var convertedClass = ConverterTool.ConvertToString(walletData);
        PlayerPrefs.SetString("Wallet", convertedClass);
    }

    public void LoadWallet()
    {
        if (PlayerPrefs.HasKey("Wallet"))
        {
            WalletData walletData = new WalletData();
            walletData = ConverterTool.ConvertFromString<WalletData>(PlayerPrefs.GetString("Wallet"));
            Wallet.Instance.gold = walletData.gold;
            Wallet.Instance.emeralds = walletData.emeralds;
            Wallet.Instance.Refresh();
            Debug.Log("Wallet Loaded");
        }
    }

    public void SaveSkills()
    {
        SkillsData skillsData = new SkillsData();
        skillsData.skillsLevels = new List<int>();
        for (int i = 0; i < SkillsManager.Instance.skillCells.Count; i++)
        {
            skillsData.skillsLevels.Add(SkillsManager.Instance.skillCells[i].currentLevel);
        }
        //writePath
        var convertedClass = ConverterTool.ConvertToString(skillsData);
        PlayerPrefs.SetString("Skills", convertedClass);
        Save();
    }

    public void LoadSkills()
    {
        if (PlayerPrefs.HasKey("Skills") && SkillsManager.Instance)
        {
            SkillsData skillsData = new SkillsData();
            skillsData = ConverterTool.ConvertFromString<SkillsData>(PlayerPrefs.GetString("Skills"));
            for (int i = 0; i < SkillsManager.Instance.skillCells.Count; i++)
            {
                SkillsManager.Instance.skillCells[i].currentLevel = skillsData.skillsLevels[i];
            }
            Debug.Log("Skills Loaded");
        }
    }
    public void SaveSettings()
    {
        SettingsData settingsData = new SettingsData();
        settingsData.language = LanguageChanger.Instance.languageType;
        settingsData.musicValue = volumeSettings.musicSlider.value;
        settingsData.sfxValue = volumeSettings.audioSlider.value;
        
        var convertedClass = ConverterTool.ConvertToString(settingsData);
        PlayerPrefs.SetString("Settings", convertedClass);
        SaveCloud(convertedClass);
        Debug.Log("Settings Saved");
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Settings"))
        {
            SettingsData settingsData = new SettingsData();
            settingsData = ConverterTool.ConvertFromString<SettingsData>(PlayerPrefs.GetString("Settings"));
            LanguageChanger.Instance.languageType = settingsData.language;
            LanguageChanger.Instance.Refresh();
            volumeSettings.LoadMusicVolume(settingsData.musicValue);
            volumeSettings.LoadSfxVolume(settingsData.sfxValue);
            Debug.Log("Settings Loaded");
        }
        else
        {
            volumeSettings.LoadMusicVolume(0.5f);
            volumeSettings.LoadSfxVolume(0.5f);
            bool languageChanged = false;
            switch (YandexGame.lang)
            {
                case "ru":
                    LanguageChanger.Instance.languageType = TranslationType.russian;
                    languageChanged = true;
                    break;
                case "en":
                    LanguageChanger.Instance.languageType = TranslationType.english;
                    languageChanged = true;
                    break;
            }
            if (languageChanged == false)
            {
                LanguageChanger.Instance.languageType = TranslationType.english;
            }
            LanguageChanger.Instance.Refresh();
            Debug.Log("Default Settings");
        }
    }

    public void ClearSaves()
    {
        PlayerPrefs.DeleteKey("Save");
        PlayerPrefs.DeleteKey("TutorialPassed");
    }

    public void SaveCloud(string data)
    {
        YandexGame.savesData.playerData = data;
        YandexGame.SaveProgress();
    }
}
[Serializable]
public class PlayerData
{
    public List<ItemToSave> items = new List<ItemToSave>();
    public int level;
    public float exp;
    public int skillPoints;
    public float force;
    public float armor;
    public float health;
}
[Serializable]
public class WalletData
{
    public float gold;
    public float emeralds;
}
[Serializable]
public class SkillsData
{
    public List<int> skillsLevels;
}
[Serializable]
public class ItemToSave
{
    public Item item;
    public int count;
    public int level;
    public bool isEquiped;
}
[Serializable]
public class SettingsData
{
    public TranslationType language;
    public float musicValue;
    public float sfxValue;
}


