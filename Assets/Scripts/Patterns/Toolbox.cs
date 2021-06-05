using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class Toolbox : Singleton<Toolbox>
{
    private Dictionary<Type, System.Object> mTools;
    private string LogFileName;

    protected Toolbox()
    {
        mTools = new Dictionary<Type, object>();
    }

    void Awake()
    {
        //LeanTween.init(500);

        DontDestroyOnLoad(gameObject);

        Component[] components = GetComponents(typeof(Component));

        foreach (var component in components)
        {
            if (component is Transform)
                continue;

            if (component == this)
                continue;

            Register(component);
        }

        DateTime now = DateTime.UtcNow;
        LogFileName = now.ToString();
        LogFileName = LogFileName.Replace("/", ".");
        LogFileName = LogFileName.Replace(":", ".");
        LogFileName = LogFileName.Replace(" ", "_");
        LogFileName.Insert(0, "ApexLog_");
    }

    public void Register<T>(T thing)
    {
        if (thing != null)
        {
            mTools[typeof(T)] = thing;
        }
    }
    public bool IsRegistered<T>()
    {
        return mTools.ContainsKey(typeof(T));
    }
    public T GetComponentThatImplementsInterface<T>()
    {
        var type = typeof(T);
        if (!mTools.ContainsKey(type))
        {
            T result = Instance.gameObject.GetComponentInChildren<T>();
            if (result == null)
            {
                Debug.LogError($"Did not find: {type}");
            }
            Register<T>(result);
        }
        if (!mTools.ContainsKey(type))
            return default(T);
        return (T)mTools[typeof(T)];
    }
    public T GetComponentThatImplements<T>(bool pCreateIfMissing = true) where T : Component
    {
        var type = typeof(T);
        if (!mTools.ContainsKey(type))
        {
            T result = Instance.gameObject.GetComponent<T>();

            if (result == null)
            {
                if (pCreateIfMissing)
                {
                    result = Instance.gameObject.AddComponent<T>();
                }
            }

            Register<T>(result);
            return result;
        }

        return (T)mTools[typeof(T)];
    }

    public void LogTofile(string pText, string pFileName = null)
    {
        string fullFileName = Application.persistentDataPath + "/" + (pFileName != null ? pFileName : "ApexLog_" + LogFileName) + ".txt";
        var fileWriter = File.AppendText(fullFileName);
        fileWriter.WriteLine(pText);
        fileWriter.Close();
    }

    static public string GetCultureString()
    {
        string culture;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.French:
                culture = "fr-FR";
                break;

            case SystemLanguage.Italian:
                culture = "it-IT";
                break;

            case SystemLanguage.German:
                culture = "de-DE";
                break;

            case SystemLanguage.Spanish:
                culture = "es-ES";
                break;

            case SystemLanguage.Portuguese:
                culture = "pt-BR";
                break;

            //case SystemLanguage.Russian:
            //    culture = "ru-RU";
            //    break;

            case SystemLanguage.Russian:
            case SystemLanguage.English:
            default:
                culture = "en-US";
                break;
        }

        return culture;
    }

    static public int TextToInt(string pText)
    {
        if (string.IsNullOrEmpty(pText))
            return 0;

        int value = 0;
        try { value = int.Parse(pText); } catch (System.Exception) { }
        return value;
    }

    float time_last_press = 0;
    public bool ButtonPressedRecently()
    {
        return ((Time.unscaledTime - time_last_press) < 0.5f);
    }

    public void TrackButtonPress()
    {
        time_last_press = Time.unscaledTime;
    }

    public bool TryButtonPress()
    {
        if (ButtonPressedRecently())
            return false;

        TrackButtonPress();
        return true;
    }
}
