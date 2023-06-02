using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
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
        data.skullCount = SkullManager.instance.skullCount;

        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static void LoadGame()
    {
        string savePath = Application.persistentDataPath + "/savegame.dat";
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

            SkullManager.instance.skullCount = data.skullCount;
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }
    private static GameObject FindEnemyByPosition(Vector3 position)
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            if (enemyObject.transform.position == position)
            {
                return enemyObject;
            }
        }
        return null;
    }
}
[System.Serializable]
public class GameData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public float playerHealth;
    public EnemyData[] enemies;
    public int skullCount;
    public List<EnemyData> enemyDataList;
}

[System.Serializable]
public class EnemyData
{
    public float enemyPosX;
    public float enemyPosY;
    public float enemyPosZ;
}
