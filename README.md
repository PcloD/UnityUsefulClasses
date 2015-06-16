# UnityUsefulClasses
Some useful classes for unity

#### Audio manager example

```
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

```
public PlayerStats(XpPerLevel xpFunc, bool autos = true);
```
#####XpPerLevel:
```
delegate float XpPerLevel(int level);
```
Function returns experience needed for certain level. If the level is max, it must return 0. Example usage:

```
playerStats = new PlayerStats<PlayerPrefsDataProvider>((int lvl) =>
{
    float[] expArray = { 100, 500, 1000, 1500, 2500, 4000 };
    int levelIndex = lvl - 1;

    if (levelIndex < expArray.Length)
    {
        return expArray[levelIndex];
    }

    return 0;
}, false);
```

