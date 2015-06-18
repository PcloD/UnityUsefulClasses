using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * This class incapsulates user level, experience and health(or whatever your player has). 
 * Template parameter provides class that will be used for save/load of data. Must implement DataIoInterface
 */
class PlayerStats<T> where T : DataIoInterface, new()
{
    public delegate float DataPerLevel(int level);
    public delegate void LevelUpCallback(int level);

    public event LevelUpCallback levelUpEvent;

    public const string LEVEL_KEY = "player_level_key";
    public const string CURRENT_XP_KEY = "player_current_xp_key";

    public float CurrentXp { get; private set; }
    public float MaxXp { get; private set; }

    /* Player's life || fuel || energy etc
     */
    public float CurrentLife { get; private set; }
    public float MaxLife { get; private set; }

    public int Level { get; private set; }

    //================================ PRIVATE

    /* If enabled - level and xp will be saved after each xp increment
     */
    private bool autosave;

    /* Enables life refund after level-up
     */
    private bool refundLifeAfterLevelUp = true;

    private DataIoInterface dataProvider;

    /* Pointer to functiom that returns experience for level that is passed to it
     * returns 0 if there is no next level
     */
    private DataPerLevel xpPerLevelFunc;

    /* Returns max life for current level
     */
    private DataPerLevel currentLevelHealth;


    //================================ METHODS

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
        Level = getLevelSaved();
        MaxXp = xpPerLevelFunc(Level);
        CurrentXp = getCurrentXpSaved();
        MaxLife = currentLevelHealth(Level);
        CurrentLife = MaxLife;
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

    private void incLevel()
    {
        //TODO(vlad): make this work if xp is enough for a couple of levels (multiple level-ups in the same time)

        //check amount of xp for next level. if 0 - no more levels and no need to increase one
        float newMaxXp = xpPerLevelFunc(Level + 1);

        if (newMaxXp != 0)
        {
            ++Level;

            CurrentXp -= MaxXp;
            MaxXp = newMaxXp;

            float newMaxLife = currentLevelHealth(Level);
            //assuming each level your life increases
            if (refundLifeAfterLevelUp)
            {
                float healthDelta = newMaxLife - MaxLife;
                incLife(healthDelta);
            }
            MaxLife = newMaxLife;

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

    //=================== INTERFACE

    public PlayerStats(DataPerLevel xpFunc, DataPerLevel healthFunc, bool autos = true)
    {
        dataProvider = new T();

        xpPerLevelFunc = xpFunc;
        currentLevelHealth = healthFunc;
        autosave = autos;

        setInitialValues();
        loadValues();

        if (!autosave)
        {
            Debug.LogWarning("PlayerState: autosave is off");
        }
    }

    public float getLevelProgress()
    {
        return CurrentXp / MaxXp;
    }

    public float getHealthLevel()
    {
        return CurrentLife / MaxLife;
    }

    public void incXp(float value)
    {
        if (value < 0)
        {
            Debug.LogError("XP amount must be a positive number");
            return;
        }

        CurrentXp += value;
        if (CurrentXp >= MaxXp)
        {
            incLevel();
        }

        if (autosave)
        {
            save();
        }
    }

    public void decLife(float amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Life amount must be a positive number");
            return;
        }

        CurrentLife -= amount;
        if (CurrentLife < 0)
        {
            CurrentLife = 0;
        }
    }

    public void incLife(float amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Life amount must be a positive number");
            return;
        }

        CurrentLife += amount;
        if (CurrentLife > MaxLife)
        {
            CurrentLife = MaxLife;
        }
    }

    public bool isAlive()
    {
        return CurrentLife > 0;
    }

    public void enableAutoSave()
    {
        autosave = true;
    }

    public void disableAutoSave()
    {
        autosave = false;
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

    public void enableLifeRefund()
    {
        refundLifeAfterLevelUp = true;
    }

    public void disableLifeRefund()
    {
        refundLifeAfterLevelUp = false;
    }
}
