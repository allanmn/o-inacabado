using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ForgeSaveData
{
    public ForgeSaveData(int weaponStageIndex, int armorStageIndex, int armorCurrentStageChance, int weaponCurrentStageChance)
    {
        this.weaponStageIndex = weaponStageIndex;
        this.armorStageIndex = armorStageIndex;
        this.weaponCurrentStageChance = weaponCurrentStageChance;
        this.armorCurrentStageChance = armorCurrentStageChance;
    }

    [SerializeField]
    public int weaponStageIndex;

    [SerializeField]
    public int armorStageIndex;

    [SerializeField]
    public int armorCurrentStageChance;

    [SerializeField]
    public int weaponCurrentStageChance;
}

public class ForgeData : MonoBehaviour
{
    public ForgeSaveData data;

    public Weapon weapon;

    public Armor armor;

    public bool reset;

    private string persistentPath = "";

    private string file = "";

    public static event System.Action onLoadComplete;

    // Start is called before the first frame update
    void Start()
    {
        SetPaths();

        SetData();
    }

    public void SetData()
    {
        if (Directory.Exists(persistentPath)  && File.Exists(persistentPath + file) && !reset)
        {
            data = LoadData();
        } else
        {
            int armorChance = armor.GetNextStage() != null ? armor.GetNextStage().percentChance : 0;
            int weaponChance = weapon.GetNextStage() != null ? weapon.GetNextStage().percentChance : 0;
            data = new ForgeSaveData(weapon.currentStageIndex, armor.currentStageIndex, weaponChance, armorChance);
        }

        if (onLoadComplete != null)
        {
            StartCoroutine(loading());
            
        }

    }

    IEnumerator loading ()
    {
        yield return new WaitForSeconds(2f);

        onLoadComplete();
        onLoadComplete = null;
    }

    public void SetPaths()
    {
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SavedData";
        file = Path.AltDirectorySeparatorChar + "ForgeData.json";
    }

    public void SaveData()
    {
        string savePath = persistentPath + file;

        int armorChance = armor.GetNextStage() != null ? armor.GetNextStage().percentChance : 0;
        int weaponChance = weapon.GetNextStage() != null ? weapon.GetNextStage().percentChance : 0;

        data = new ForgeSaveData(weapon.currentStageIndex, armor.currentStageIndex, armorChance, weaponChance);

        string json = JsonUtility.ToJson(data);

        if (!Directory.Exists(persistentPath))
        {
            Directory.CreateDirectory(persistentPath);
        }

        using StreamWriter writer = new StreamWriter(savePath);

        writer.Write(json);
    }

    public ForgeSaveData LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath + file);
        string json = reader.ReadToEnd();

        data = JsonUtility.FromJson<ForgeSaveData>(json);

        return data;
    }
}
