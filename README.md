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
