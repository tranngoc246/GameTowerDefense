using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Ins;

    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";

    private void Awake()
    {
        if (SaveManager.Ins != null) Destroy(gameObject);
        SaveManager.Ins = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    protected virtual string GetSaveName()
    {
        return SaveManager.SAVE_1;
    }

    protected virtual void LoadSaveGame()
    {
        string stringSave = SaveSystem.GetString(this.GetSaveName());
        Debug.Log("LoadSaveGame: " + stringSave);
    }

    protected virtual void SaveGame()
    {
        Debug.Log("SaveGame");
        string stringSave = "aaaaaaa";
        SaveSystem.SetString(this.GetSaveName(), stringSave);
    }
}
