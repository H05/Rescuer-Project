using UnityEngine;
using System.IO;
using LitJson;

public class JSONReader : MonoBehaviour {
    private JsonData Items;

    private string Path;
    private string JsonData;
    private string[] TextLine;

    private int LevelID;
    private int LineCount;
 
    private void Awake(){LevelID = 0;}

    private void Start ()
    {
        JsonData = File.ReadAllText(Application.dataPath + "/Resources/TextLines/TextLine.json");
        Items = JsonMapper.ToObject(JsonData);
        LineCount = Items["TextLines"][LevelID].Count - 1;

        TextLineManager.TextLine = new string[LineCount];
        TextLine = new string[LineCount];

        GetDataFromJSON();
    }

    private void GetDataFromJSON()
    {
       for (int i = 0; i < LineCount; i++)
       {
            TextLine[i] = Items["TextLines"][LevelID][i + 1].ToString();
       }
        TextLineManager.TextLine = TextLine;
    }
}
