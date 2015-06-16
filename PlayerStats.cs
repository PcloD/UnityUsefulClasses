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

    public const string LEVEL_KEY       = "player_level_key";
    public const string CURRENT_XP_KEY  = "player_current_xp_key";

    private float   maxXp;
    private float   currentXp;
    private int     level;
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
        level       = getLevelSaved();
        maxXp       = xpPerLevelFunc(level);
        currentXp   = getCurrentXpSaved();
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
        dataProvider.SetInt(LEVEL_KEY,level);
    }

    private void saveCurrentXp()
    {
        dataProvider.SetFloat(CURRENT_XP_KEY, currentXp);
    }

    public float getLevelProgress()
    {
        return currentXp / maxXp;
    }

    public void incXp(float value)
    {
        currentXp += value;
        if (currentXp >= maxXp)
        {
            //check amount of xp for next level. if 0 - no more levels and no need to increate one
            float newMaxXp = xpPerLevelFunc(level + 1);

            if (newMaxXp != 0)
            {
                ++level;
                currentXp -= maxXp;
                maxXp = newMaxXp;
            }
            else
            {
                currentXp = maxXp;
            }
        }

        if (autosave)
        {
            save();
        }

    }

    public int getLevel()
    {
        return level;
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

    public void clearStats()
    {
        level = 1;
        currentXp = 0;
        save();
    }
}
