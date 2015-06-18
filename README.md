# UnityUsefulClasses
Some useful classes for unity

#### Audio manager example

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

#### Player Stats

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

