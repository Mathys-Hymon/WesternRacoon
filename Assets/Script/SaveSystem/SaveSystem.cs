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

            if (saveInfo.activeScene != SceneManager.GetActiveScene().buildIndex)
            {
                CameraScript.Instance.CheckRoom(1);
            }
            else
            {
                PlayerMovement.Instance.transform.position = new Vector3(saveInfo.x, saveInfo.y, saveInfo.z);
                CameraScript.Instance.CheckRoom(0);
            }
            
 
            
            GameManager.Instance.money = saveInfo.money;
            
            CameraScript.Instance.NewCameraBoundary(new Vector2(saveInfo.cameraPosX, saveInfo.cameraPosY));
            CameraScript.Instance.transform.position = new Vector3(saveInfo.x, CameraScript.Instance.transform.position.y, saveInfo.z);
        }
    }

    public void Save()
    {
        Vector3 position = PlayerMovement.Instance.GetCheckpoint();
        saveInfo.x = position.x;
        saveInfo.y = position.y;
        saveInfo.z = position.z;
        
        Vector2 cameraPosition = CameraScript.Instance.GetBoundaries();
        saveInfo.cameraPosX = cameraPosition.x;
        saveInfo.cameraPosY = cameraPosition.y;

        int coins = GameManager.Instance.money;
        saveInfo.money = coins;
        
        int actualScene =  SceneManager.GetActiveScene().buildIndex;
        saveInfo.activeScene = actualScene;

        Debug.Log(Application.persistentDataPath + "/data.save");
        string json = JsonUtility.ToJson(saveInfo);
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);

    }
}
