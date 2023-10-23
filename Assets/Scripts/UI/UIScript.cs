using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static EventHandler OnTapDetected;

    [SerializeField] protected TextMeshProUGUI soundsText;
    [SerializeField] protected TextMeshProUGUI musicText;

    [SerializeField] protected Image settingsImage;

    [SerializeField] protected Button settingsButton;
    [SerializeField] protected Button backButton;

    [SerializeField] protected Toggle soundToggle;
    [SerializeField] protected Toggle musicToggle;

    [SerializeField] protected AudioMixerGroup audioMixer;

    protected virtual void Awake()
    {
        settingsButton.onClick.AddListener(() => 
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            StartCoroutine(ShowSettings());
        });

        backButton.onClick.AddListener(() => 
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            StartCoroutine(HideSettings());
        });
        
        soundToggle.onValueChanged.AddListener((bool enabled) =>
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            SoundsSettings(enabled);
        });

        musicToggle.onValueChanged.AddListener((bool enabled) =>
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            MusicSettings(enabled);
        });
    }

    protected virtual IEnumerator ShowSettings()
    {
        while (settingsImage.rectTransform.localPosition.y < 0)
        {
            settingsImage.rectTransform.localPosition = new Vector3
                (
                    settingsImage.rectTransform.localPosition.x,
                    settingsImage.rectTransform.localPosition.y + 60f,
                    settingsImage.rectTransform.localPosition.z
                );

            yield return null;
        }
    }

    protected virtual IEnumerator HideSettings()
    {
        while (settingsImage.rectTransform.localPosition.y > -1080)
        {
            settingsImage.rectTransform.localPosition = new Vector3
                (
                    settingsImage.rectTransform.localPosition.x,
                    settingsImage.rectTransform.localPosition.y - 60f,
                    settingsImage.rectTransform.localPosition.z
                );

            yield return null;
        }
    }

    protected void SoundsSettings(bool enabled)
    {
        audioMixer.audioMixer.SetFloat("SoundsVolume", enabled ? 0f : -80f);
        soundsText.text = enabled ? "Sounds: On" : "Sounds: Off";
    }

    protected void MusicSettings(bool enabled)
    {
        audioMixer.audioMixer.SetFloat("MusicVolume", enabled ? 0f : -80f);
        musicText.text = enabled ? "Music: On" : "Music: Off";
    }
}