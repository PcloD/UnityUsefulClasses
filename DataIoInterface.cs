using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


interface DataIoInterface
{
    int GetInt(string key);
    void SetInt(string key, int val);

    float GetFloat(string key);
    void SetFloat(string key, float val);

    bool GetBool(string val);
    void SetBool(string key, bool val);

    string GetString(string key);
    void SetString(string key, string val);

    bool hasKey(string key);
}

