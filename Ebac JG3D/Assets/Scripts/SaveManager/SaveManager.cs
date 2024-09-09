using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastCheckpoint = 0;
        _saveSetup.playerName = "Rafael";
    }

    private void Start() {
        Invoke(nameof(Load),.1f);
    }

    public void ChangeCheckpoint(int checkpointIndex)
    {
        _saveSetup.lastCheckpoint = checkpointIndex;
        Save();
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Itens.itemManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.health = Itens.itemManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastCheckpoint = level;
        SaveItems();
        Save();
    }
    #endregion

    private void LoadFile(int lastCheckpoint)
    {
        CheckPointData.Instance.salvaIndex(lastCheckpoint);
    }

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path,json);
    }
    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if(File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastCheckpoint;

            LoadFile(lastLevel);

        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded.Invoke(_saveSetup);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }
}

[System.Serializable]

public class SaveSetup
{
    public int lastCheckpoint;
    public float coins;
    public float health;
    public string playerName;
}
