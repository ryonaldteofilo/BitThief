using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string GAME_VOLUME_KEY = "game volume", MUSIC_VOLUME_KEY = "music volume";

    const float MAX_VOLUME = 1f, MIN_VOLUME = 0f;

    public static void SetGameVolume(float volume)
    {
        if(volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(GAME_VOLUME_KEY, volume);
        }
    }

    public static float GetGameVolume()
    {
        return PlayerPrefs.GetFloat(GAME_VOLUME_KEY);
    }

    public static void SetMusicVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }
}
