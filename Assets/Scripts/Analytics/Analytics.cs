using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Analytic Storage", menuName = "Analytics/Storage"), System.Serializable]
public class Analytics : ScriptableObject
{
    [SerializeField] public AnalyticStorage analyticStorage = new AnalyticStorage();

    protected string sessionDataProjectFilePath = "/StreamingAssets";


    protected string sessionDataFileName = "data.json";

    public void addAnalytic(float timeStamp, string room, Vector3 point)
	{
		analyticStorage.visionTrackingData.Add(new Analytic(timeStamp, room, point));
	}


	public int getCount()
	{
		return analyticStorage.visionTrackingData.Count;
	}

	public void clearData()
	{
		analyticStorage.visionTrackingData.Clear();
	}

    public void SetFileName()
    {
        int i = 0;

        DirectoryInfo d = new DirectoryInfo(Application.dataPath + sessionDataProjectFilePath);
        FileInfo[] fis = d.GetFiles();
        foreach(FileInfo f in fis)
        {
            if(f.Extension.Contains("json"))
            {
                i++;
            }
        }

        sessionDataFileName = "data" + i + ".json";
    }

    public void ExportData()
    {
        //export the data
        SetFileName();
        string dataAsJson = JsonUtility.ToJson(analyticStorage);
        string filePath = Application.dataPath + sessionDataProjectFilePath;

        File.WriteAllText(filePath, dataAsJson);

    }

    public void LoadData(int i)
    {
        //import data
        string goalFilePathName = "data" + i + ".json";
        string filePath = Path.Combine(Application.streamingAssetsPath, goalFilePathName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            AnalyticStorage newData = JsonUtility.FromJson<AnalyticStorage>(dataAsJson);

            analyticStorage = newData;
        }else
        {
            Debug.LogError("session data unloadable");
        }
    }

    public void LoadData(string i)
    {
        //import data
        string goalFilePathName = i;
        string filePath = Path.Combine(Application.streamingAssetsPath, goalFilePathName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            AnalyticStorage newData = JsonUtility.FromJson<AnalyticStorage>(dataAsJson);

            analyticStorage = newData;
        }
        else
        {
            Debug.LogError("session data unloadable");
        }
    }
}

[System.Serializable]
public class AnalyticStorage
{
    [SerializeField] public List<Analytic> visionTrackingData = new List<Analytic>();
       
}

[System.Serializable]
public struct Analytic
{
	public float TimeStamp;
	public string Room;
	public Vector3 Point;

	public Analytic(float timeStamp, string roomName, Vector3 point)
	{
		TimeStamp = timeStamp;
		Room = roomName;
		Point = point;
	}
}