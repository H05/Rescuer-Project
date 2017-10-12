using UnityEngine;
using UnityEditor;
using System.IO;

public class TextLineGenerator : MonoBehaviour 
{
    private string FileName = "TextLine";
    public string FileDirectory = Application.dataPath + "/Resources/TextLines/";
    public string FileFormat;
    private string FilePath;

    public int LevelIndex;
    public int NumberOfLines;

    private string[] MoodTypes;

    private void Start()
    {
        FileDirectory += "Level " + LevelIndex + "/";
        FilePath += FileDirectory + FileName;
        MoodTypes = new string[3] {"WAITING FOR HELP", "HOPING FOR HELP", "DESPERATE FOR HELP" };

        CreateFile();
    }
    private void CreateFile()
    {
        if (File.Exists(FilePath))
        {
            Debug.Log("<color=blue>TEXTLINE FILE EXISTS! UPDATING...</color>");
            File.Delete(FilePath);
        }
        else if (!AssetDatabase.IsValidFolder(FileDirectory))
        {
            Debug.Log("<color=blue>DIRECTORY DOESN'T EXIST! CREATING NEW...</color>");
            Directory.CreateDirectory(FileDirectory);
        }

        var TextFile = File.CreateText(FilePath + FileFormat);
        string LastLog = "LAST LOG: " + System.DateTime.Now;
        string spaces;

        TextFile.WriteLine(LastLog);
        TextFile.WriteLine("");
        TextFile.WriteLine("TEXT LINES FOR LEVEL " + LevelIndex + ": ");
        TextFile.WriteLine("");

        for (int i = 0; i < MoodTypes.Length; i++)
        {
            TextFile.WriteLine("MOOD TYPE: " + "\"" + MoodTypes[i] + "\"");

            for(int j = 0; j < NumberOfLines; j++)
            {
                if (j == NumberOfLines - 1) spaces = ". "; else spaces = " . ";
                TextFile.WriteLine(j + 1 + spaces + "\"" + new string(' ', 10) + "\"");
            }
            TextFile.WriteLine("");
        }

        TextFile.Close();
    }
}
