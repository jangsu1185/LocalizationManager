using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class LocalizationManager : Singleton<LocalizationManager>
{
    [SerializeField]
    int langIndex;

    [SerializeField]
    SystemLanguage[] langList;
    [SerializeField]
    string[] langLocalList;

    Dictionary<string, string> texts = new Dictionary<string, string>();

    public override void Awake()
    {
        base.Awake();

        InitLanguage();

        ReadLanguageFile(langIndex);

    }

    void InitLanguage()
    {
        string lang = PlayerPrefs.GetString("Language", Application.systemLanguage.ToString());
        langIndex = -1;

        string filePath = Path.Combine(Application.streamingAssetsPath, "LanguageText.tsv");
        FileInfo fileInfo = new FileInfo(filePath);
        string str = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            str = reader.ReadLine();

            string[] column = str.Split('\t');
            langList = new SystemLanguage[column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                if (column[i] == lang)
                    langIndex = i;
                langList[i] = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), column[i]);
            }

            str = reader.ReadLine();
            langLocalList = new string[column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                langLocalList[i] = column[i];
            }

            reader.Close();
        }
        else
        {
            Debug.Log("There is no file");
        }

        if (langIndex == -1) 
        {
            Debug.Log("There is no System Language");
            langIndex = 0;
        }
    }

    bool ReadLanguageFile(int index) 
    {
        texts.Clear();
        string filePath = Path.Combine(Application.streamingAssetsPath, "LanguageText.tsv");

        FileInfo fileInfo = new FileInfo(filePath);
        string str = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            reader.ReadLine();
            reader.ReadLine();
            while ((str=reader.ReadLine())!=null) 
            {
                string[] column = str.Split('\t');
                texts.Add(column[0], column[index]);
            }
            reader.Close();
        }
        else
        {
            Debug.Log("There is no file");
            return false;
        }

        return true;
    }

    public void SetLanguage(int index) 
    {
        langIndex = index;
        PlayerPrefs.SetString("Language", langList[index].ToString());
        ReadLanguageFile(index);
    }

    public string GetText(string key) 
    {
        string str;
        if (!texts.TryGetValue(key, out str)) 
        {
            str = key;
        }
        return str;
    }
}
