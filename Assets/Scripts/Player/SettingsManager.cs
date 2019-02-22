using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider soundVolumeSlider;
    public AudioSource musicSource;
    public Resolution[] resolutions;
    private GameSettings gameSettings;
    public Button applyButton;

    void OnEnable()
    {
        //gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        //
        applyButton.onClick.AddListener(delegate{OnApplyButtonClick();});
        //
        resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        //
        LoadSettings();
        //
    }
    public void OnFullscreenToggle() {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }
    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;

    }
    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = soundVolumeSlider.value;
    }
    public void OnApplyButtonClick(){
        SaveSettings();
    }
    public void SaveSettings(){
        string jasonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", jasonData);

    }
    public void LoadSettings(){
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value =gameSettings.resolutionIndex;
        soundVolumeSlider.value = gameSettings.musicVolume;
        
        
    }

}
