using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Analytic Storage", menuName = "Analytics/Storage")]
public class Analytics : ScriptableObject
{
	[SerializeField] public float value;

	

	public List<Analytic> analyticsStorage = new List<Analytic>();
}


public struct Analytic
{
	public float TimeStamp;
	public string Room;
	public Analytic(float timeStamp, string roomName)
	{
		TimeStamp = timeStamp;
		Room = roomName;
	}
}