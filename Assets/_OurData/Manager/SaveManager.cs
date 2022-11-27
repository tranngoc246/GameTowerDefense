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

    protected virtual string GetSaveName(string dataName)
    {
        return SaveManager.SAVE_1 + "_" + dataName;
    }

    protected virtual void LoadSaveGame()
    {
        string jsonString = SaveSystem.GetString(this.GetSaveName("ScoreManager"));
        Debug.Log("LoadSaveGame: " + jsonString);
        ScoreManager.Ins.FromJson(jsonString);
    }

    protected virtual void SaveGame()
    {
        string jsonString = JsonUtility.ToJson(ScoreManager.Ins);
        SaveSystem.SetString(this.GetSaveName("ScoreManager"), jsonString);
        Debug.Log(jsonString);
    }
}
