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
#XpPerLevel:
```
delegate float XpPerLevel(int level);
```
function returns experience needed for certain level
if the level is max, it must return 0


