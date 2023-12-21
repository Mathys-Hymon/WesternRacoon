using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public AllInfo saveInfo;

    private void Awake()
    {
        Instance = this;
        Load();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            saveInfo = JsonUtility.FromJson<AllInfo>(json);
            
            PlayerMovement.Instance.transform.position = new Vector3(saveInfo.x, saveInfo.y, saveInfo.z);
            GameManager.Instance.money = saveInfo.money;
            
            GameObject Chest1 = GameObject.Find("Chest01");
            Chest1.GetComponent<ChestOpen>().isOpened = saveInfo.chest;
            
            CameraScript.Instance.NewCameraBoundary(new Vector2(saveInfo.cameraPosX, saveInfo.cameraPosY));
            CameraScript.Instance.transform.position = new Vector3(saveInfo.x, CameraScript.Instance.transform.position.y, saveInfo.z);
            
            string mySavedScene = PlayerPrefs.GetString("sceneName");
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("sceneName", SceneManager.GetActiveScene().name);
        
        Vector3 position = PlayerMovement.Instance.GetCheckpoint();
        saveInfo.x = position.x;
        saveInfo.y = position.y;
        saveInfo.z = position.z;

        int coins = GameManager.Instance.money;
        saveInfo.money = coins;

        Vector2 cameraPosition = CameraScript.Instance.GetBoundaries();
        saveInfo.cameraPosX = cameraPosition.x;
        saveInfo.cameraPosY = cameraPosition.y;
        
        
        
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
}
