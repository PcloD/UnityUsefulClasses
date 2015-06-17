using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Class that incapsulates user level and experience. 
 * Template parameter provides class that will be used for save/load of data
 */
class PlayerStats<T> where T: DataIoInterface, new()
{
    public delegate float XpPerLevel(int level);
    public delegate void LevelUpCallback(int level);

    public event LevelUpCallback levelUpEvent;

    public const string LEVEL_KEY       = "player_level_key";
    public const string CURRENT_XP_KEY  = "player_current_xp_key";

    public float    CurrentXp   { get; private set; }
    public float    MaxXp       { get; private set; }
    public int      Level       { get; private set; }

    private bool    autosave;

    private DataIoInterface dataProvider;
    /*
     * Pointer to functiom that returns experience for level that is passed to it
     * returns 0 if there is no next level
     */
    private XpPerLevel xpPerLevelFunc;

    public PlayerStats(XpPerLevel xpFunc, bool autos = true)
    {
        dataProvider = new T();

        xpPerLevelFunc = xpFunc;
        autosave = autos;

        setInitialValues();
        loadValues();

        if (!autosave)
        {
            Debug.LogWarning("PlayerState: autosave is off");
        }
    }

    private void setInitialValues()
    {

        if (!dataProvider.hasKey(LEVEL_KEY))
        {
            dataProvider.SetInt(LEVEL_KEY, 1);
        }

        if (!dataProvider.hasKey(CURRENT_XP_KEY))
        {
            dataProvider.SetInt(CURRENT_XP_KEY, 0);
        }
    }

    private void loadValues()
    {
        Level       = getLevelSaved();
        MaxXp = xpPerLevelFunc(Level);
        CurrentXp = getCurrentXpSaved();
    }

    private int getLevelSaved()
    {
        return dataProvider.GetInt(LEVEL_KEY);
    }

    private float getCurrentXpSaved()
    {
        return dataProvider.GetFloat(CURRENT_XP_KEY);
    }

    private void saveLevel()
    {
        dataProvider.SetInt(LEVEL_KEY, Level);
    }

    private void saveCurrentXp()
    {
        dataProvider.SetFloat(CURRENT_XP_KEY, CurrentXp);
    }

    public float getLevelProgress()
    {
        return CurrentXp / MaxXp;
    }

    public void incXp(float value)
    {
        CurrentXp += value;
        if (CurrentXp >= MaxXp)
        {
            //check amount of xp for next level. if 0 - no more levels and no need to increate one
            float newMaxXp = xpPerLevelFunc(Level + 1);

            if (newMaxXp != 0)
            {
                ++Level;
                CurrentXp -= MaxXp;
                MaxXp = newMaxXp;

                if (levelUpEvent != null)
                {
                    levelUpEvent(Level);
                }
            }
            else
            {
                CurrentXp = MaxXp;
            }
        }

        if (autosave)
        {
            save();
        }

    }

    public void setAutoSave(bool state)
    {
        autosave = state;
    }

    public void save()
    {
        saveLevel();
        saveCurrentXp();
    }

    public void clear()
    {
        Level = 1;
        CurrentXp = 0;
        MaxXp = xpPerLevelFunc(Level);
        save();
    }
}
