using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Analytic Storage", menuName = "Analytics/Storage"), System.Serializable]
public class Analytics : ScriptableObject
{
    [SerializeField] public AnalyticStorage analyticStorage = new AnalyticStorage();

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

    public void ExportData()
    {
        //export the data
    }

    public void LoadData()
    {
        //import data

        string filePath = Path.Combine(Application.streamingAssetsPath, sessionDataFileName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            //AnalyticStorage newData = JsonUtility.FromJson<AnalyticStorage>;
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