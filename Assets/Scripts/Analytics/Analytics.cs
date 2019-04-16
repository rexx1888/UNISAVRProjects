using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Analytic Storage", menuName = "Analytics/Storage")]
public class Analytics : ScriptableObject
{
	[SerializeField] public float value;

	
	public void addAnalytic(float timeStamp, string room, Vector3 point)
	{
		analyticsStorage.Add(new Analytic(timeStamp, room, point));
	}

	public List<Analytic> analyticsStorage = new List<Analytic>();

	public int getCount()
	{
		return analyticsStorage.Count;
	}

	public void clearData()
	{
		analyticsStorage.Clear();
	}
}


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