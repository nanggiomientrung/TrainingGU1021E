using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Toggle backgroundMusicToggle, soundEffectToggle;
    void Start()
    {
        confirmButton.onClick.AddListener(CloseSettingPopup);
        backgroundMusicToggle.onValueChanged.AddListener(delegate {
            TurnOnOffBackgroundMusic(backgroundMusicToggle);
        });
        soundEffectToggle.onValueChanged.AddListener(delegate {
            TurnOnOffSoundEffect(soundEffectToggle);
        });
    }

    public void OpenSettingPopup()
    {
        gameObject.SetActive(true);
    }

    private void CloseSettingPopup()
    {
        gameObject.SetActive(false);
    }

    private void TurnOnOffBackgroundMusic(Toggle change)
    {
        SoundController.instance.TurnOnOffBackgroundMusic(change.isOn == false);
    }

    private void TurnOnOffSoundEffect(Toggle change)
    {
        SoundController.instance.TurnOnOffSoundEffect(change.isOn == false);
    }
}
