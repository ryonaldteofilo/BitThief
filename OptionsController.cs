using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider GameVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    MusicPlayer musicPlayer;

    float defaultVolume = 0.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        GameVolumeSlider.value = PlayerPrefsController.GetGameVolume();
        MusicVolumeSlider.value = PlayerPrefsController.GetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        musicPlayer.SetVolume(MusicVolumeSlider.value);
    }

    public void SaveandExit()
    {
        PlayerPrefsController.SetMusicVolume(MusicVolumeSlider.value);
        PlayerPrefsController.SetGameVolume(GameVolumeSlider.value);
        FindObjectOfType<LevelManager>().StartMenu();
    }

    public void Default()
    {
        MusicVolumeSlider.value = defaultVolume;
        GameVolumeSlider.value = defaultVolume;
    }
}
