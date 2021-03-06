# UnityUsefulClasses
Some useful classes for unity

#### AudioManager

Add AudioManager to your game(only to first scene, as it is a singleton), then add to it sounds and corresponding keys.

Exapmle usage:

```c#
public static void playBackgroundMusic()
{
  if (!AudioManager.GetInstance().isSoundPlaying("background"))
  {
    AudioManager.GetInstance().playSoundByKey("background", true);
  }
}

public static void playShootSound()
{
    AudioManager.GetInstance().playSoundByKey("shootSound");
}

```

#### PlayerStats

Class provides level and experience functionality. 
Constructor:

```c#
public PlayerStats(DataPerLevel xpFunc, DataPerLevel healthFunc, bool autosave = true);
```
##### DataPerLevel:
```c#
public delegate float DataPerLevel(int level);
```
Returns value based on level. 

DataPerLevel xpFunc: Returns max xp for level passed to it. If the level is max, it must return 0.
DataPerLevel healthFunc: Returns max life for level passed to it

Example usage:
```c#
playerStats = new PlayerStats<PlayerPrefsDataProvider>(
    (int lvl) =>
    {
        float[] expArray = { 100, 500, 1000, 1500, 2500, 4000 };
        int levelIndex = lvl - 1;

        if (levelIndex < expArray.Length)
        {
            return expArray[levelIndex];
        }

        return 0;
    },
    (int lvl) => {
        const float startLife = 60;
        const float lifePerLevel = 10;

        return startLife + energyPerLevel * lvl;
    });
```

###AdsManager unity import
1. Add this file to your project Assets/Plugin
2. Download [Heyzap SDK](https://developers.heyzap.com/docs/unity_sdk_setup_and_requirements) for Unity and [Android SDK for Unity](https://github.com/googleads/googleads-mobile-plugins/releases). Be aware of first-pass compilation.
3. Change your ads keys ``` heyzappKey``` and ``` admobKey``` for independent Admob integration
4. Go to Heyzap and [create a game](https://developers.heyzap.com/dashboard/games/add_game) than [create mediation](https://developers.heyzap.com/dashboard/mediation)
5. To test whether everything works correctly see [Step 7](https://developers.heyzap.com/docs/unity_sdk_setup_and_requirements)


