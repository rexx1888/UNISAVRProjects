using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SessionDataEditor : EditorWindow {

    protected string sessionDataProjectFilePath = "/StreamingAssets/data.json";
    protected string sessionDataProjectPath = "/StreamingAssets";


    public AnalyticStorage sessionData;


    [MenuItem("Window/Session Data editor")]
    static void init()
    {
        SessionDataEditor window = (SessionDataEditor)EditorWindow.GetWindow(typeof(SessionDataEditor));
        window.Show();
    }

    private void OnGUI()
    {
        if(sessionData != null)
        {
            SerializedObject seralizedObject = new SerializedObject(this);
            SerializedProperty serializedproperty = seralizedObject.FindProperty("sessionData");

            EditorGUILayout.PropertyField(serializedproperty, true);

            seralizedObject.ApplyModifiedProperties();
            
            if(GUILayout.Button("Save Data"))
            {
                SaveSessionData();
            }
            
        }

        if(GUILayout.Button("Load Data"))
        {
            LoadSessionData(0);
        }
    }

    protected void LoadSessionData(int i)
    {
        string goalData = "/StreamingAssets/data" + i + ".json";
        string filePath = Application.dataPath + goalData;

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            sessionData = JsonUtility.FromJson<AnalyticStorage>(dataAsJson);
        }
        else
        {
            sessionData = new AnalyticStorage();
        }
    }

    protected void SaveSessionData()
    {
        SetFileName();
        string dataAsJson = JsonUtility.ToJson(sessionData);
        string filePath = Application.dataPath + sessionDataProjectFilePath;

        File.WriteAllText(filePath, dataAsJson);
    }

    public void SetFileName()
    {
        int i = 0;

        DirectoryInfo d = new DirectoryInfo(Application.dataPath + sessionDataProjectPath);
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo f in fis)
        {
            if (f.Extension.Contains("json"))
            {
                i++;
            }
        }

        sessionDataProjectFilePath = "/StreamingAssets/data" + i + ".json";
    }


}
