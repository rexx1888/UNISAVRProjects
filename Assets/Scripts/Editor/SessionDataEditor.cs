using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SessionDataEditor : EditorWindow {

    protected string sessionDataProjectFilePath = "/data0.json";
    protected string sessionDataProjectPath = "/StreamingAssets";


    public AnalyticStorage sessionData;
    protected bool updateGui = false;

    [MenuItem("Window/Session Data editor")]
    static void init()
    {
        SessionDataEditor window = (SessionDataEditor)EditorWindow.GetWindow(typeof(SessionDataEditor));
        window.Show();
        
    }

    private void OnEnable()
    {
        sessionData = null;
    }

    private void OnSelectionChange()
    {
        //Repaint();
    }

    private void OnGUI()
    {
        //LoadSessionData(0);

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
          
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + sessionDataProjectPath);
        FileInfo[] fis = d.GetFiles();

        if (fis.Length > 0)
        {
            foreach (FileInfo f in fis)
            {
                if (!f.Extension.Contains("meta"))
                {
                    if (GUILayout.Button(f.Name))
                    {
                        Debug.Log(f.Name);

                        LoadSessionData(f.Name);
                    }
                }
            }
        }

        if (GUILayout.Button("Create New Data"))
        {
            sessionData = new AnalyticStorage();
            sessionDataProjectFilePath = "";
        }
    }

    protected void LoadSessionData(int i)
    {
        string goalData = sessionDataProjectFilePath;
        string filePath = Application.dataPath + goalData;
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            sessionData = JsonUtility.FromJson<AnalyticStorage>(dataAsJson);
            sessionDataProjectFilePath = goalData;
        }
       
    }

    protected void LoadSessionData(string i)
    {
        string goalData = sessionDataProjectPath + "/" + i;
        string filePath = Application.dataPath + goalData;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            sessionData = JsonUtility.FromJson<AnalyticStorage>(dataAsJson);
            sessionDataProjectFilePath = goalData;
        }
      
    }

    protected void SaveSessionData()
    {
        if(sessionDataProjectFilePath == "")SetFileName();
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

        sessionDataProjectFilePath = sessionDataProjectPath + "/data" + i + ".json";
    }


}
