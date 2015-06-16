using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerPrefsDataProvider: DataIoInterface
{
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public void SetInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }

    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public void SetFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }

    public bool GetBool(string val)
    {
        return PlayerPrefs.GetInt(val) == 1;
    }

    public void SetBool(string key, bool val)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(val));
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SetString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }

    public bool hasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}

