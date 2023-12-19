using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public AllInfo saveInfo;
    
    void Start()
    {
        instance = this;
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            saveInfo = JsonUtility.FromJson<AllInfo>(json);
            
            
            PlayerMovement.Instance.transform.position = new Vector3(saveInfo.x, saveInfo.y, saveInfo.z);
            GameManager.instance.money = saveInfo.money;
            
            GameObject Chest1 = GameObject.Find("Chest01");
            Chest1.GetComponent<ChestOpen>().isOpened = saveInfo.chest;
        }
    }

    public void Save()
    {
        Vector3 position = PlayerMovement.Instance.GetCheckpoint();
        saveInfo.x = position.x;
        saveInfo.y = position.y;
        saveInfo.z = position.z;

        int coins = GameManager.instance.money;
        saveInfo.money = coins;

        GameObject Chest1 = GameObject.Find("Chest01");
        bool chestOpen01 = Chest1.GetComponent<ChestOpen>().isOpened;
        saveInfo.chest = chestOpen01;

        Debug.Log(Application.persistentDataPath + "/data.save");
        string json = JsonUtility.ToJson(saveInfo);
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);

    }

    public void DeleteFile()
    {
        string json = Application.persistentDataPath + "/data.save";
    
        // check if file exists
        if (!File.Exists(json))
        {
            SceneManager.LoadScene("ClaireDebug");
        }
        else
        {
            File.Delete(json);
            SceneManager.LoadScene("ClaireDebug");
        }
    }
}