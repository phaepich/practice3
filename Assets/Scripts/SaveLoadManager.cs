using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    private void Awake()
    {
        SaveGame();
    }

    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/savegame.dat";

        FileStream fileStream = new FileStream(savePath, FileMode.Create);

        GameData data = new GameData();
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.playerPosX = playerPosition.x;
        data.playerPosY = playerPosition.y;
        data.playerPosZ = playerPosition.z;
        data.playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health;
        List<EnemyData> enemyDataList = new List<EnemyData>();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            EnemyData enemyData = new EnemyData();
            Vector3 enemyPosition = enemyObject.transform.position;
            enemyData.enemyPosX = enemyPosition.x;
            enemyData.enemyPosY = enemyPosition.y;
            enemyData.enemyPosZ = enemyPosition.z;
            enemyDataList.Add(enemyData);
        }
        data.enemyDataList = enemyDataList;
        List<SkullData> skullDataList = new List<SkullData>();
        GameObject[] skullObjects = GameObject.FindGameObjectsWithTag("Skull");
        foreach (GameObject skullObject in skullObjects)
        {
            SkullData skullData = new SkullData();
            Vector3 skullPosition = skullObject.transform.position;
            Quaternion skullRotation = skullObject.transform.rotation;
            skullData.skullPosX = skullPosition.x;
            skullData.skullPosY = skullPosition.y;
            skullData.skullPosZ = skullPosition.z;
            skullData.skullRotX = skullRotation.x;
            skullData.skullRotY = skullRotation.y;
            skullData.skullRotZ = skullRotation.z;
            skullData.skullRotW = skullRotation.w;
            skullDataList.Add(skullData);
        }
        data.skullDataList = skullDataList;
        data.skullsDeliveredCount = GameObject.FindObjectOfType<SkullsManager>().skullsDeliveredCount;

        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void LoadGame()
    {
        string savePath = Application.persistentDataPath + "/savegame.dat";
        Time.timeScale = 1f;
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(savePath, FileMode.Open);
            GameData data = (GameData)formatter.Deserialize(fileStream);
            fileStream.Close();

            Vector3 playerPosition = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
            GameObject.FindGameObjectWithTag("Player").transform.position = playerPosition;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health = data.playerHealth;

            foreach (EnemyData enemyData in data.enemyDataList)
            {
                Vector3 enemyPosition = new Vector3(enemyData.enemyPosX, enemyData.enemyPosY, enemyData.enemyPosZ);
                GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemyObject in enemyObjects)
                {
                    enemyObject.transform.position = enemyPosition;
                }
            }
            foreach (SkullData skullData in data.skullDataList)
            {
                Vector3 skullPosition = new Vector3(skullData.skullPosX, skullData.skullPosY, skullData.skullPosZ);
                Quaternion skullRotation = new Quaternion(skullData.skullRotX, skullData.skullRotY, skullData.skullRotZ, skullData.skullRotW);
                // Найдите соответствующий череп по тегу, позицию и поворот и установите их значения
                GameObject skullObject = GameObject.FindGameObjectWithTag("Skull");
                skullObject.transform.position = skullPosition;
                skullObject.transform.rotation = skullRotation;
            }

            GameObject.FindObjectOfType<SkullsManager>().skullsDeliveredCount = data.skullsDeliveredCount;
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }
    public static void DeleteSaveGame()
    {
        string savePath = Application.persistentDataPath + "/savegame.dat";
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
    
}
[System.Serializable]
public class GameData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public float playerHealth;
    public int skullsDeliveredCount;
    public List<SkullData> skullDataList;
    public List<EnemyData> enemyDataList;
}

[System.Serializable]
public class EnemyData
{
    public float enemyPosX;
    public float enemyPosY;
    public float enemyPosZ;
}

[System.Serializable]
public class SkullData
{
    public float skullPosX;
    public float skullPosY;
    public float skullPosZ;
    public float skullRotX;
    public float skullRotY;
    public float skullRotZ;
    public float skullRotW;
}

