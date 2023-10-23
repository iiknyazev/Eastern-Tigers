using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIScript : UIScript
{
    public static EventHandler OnStarted;

    [SerializeField] private GameObject soundManager;
    [SerializeField] private GameObject mothsEffect;
    [SerializeField] private GameObject playerPrefs;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("SoundManager") == null)
            Instantiate(soundManager);

        if (GameObject.Find("MothsEffect") == null)
            Instantiate(mothsEffect).name = "MothsEffect";

        if (GameObject.Find("PlayerPrefs") == null)
            Instantiate(playerPrefs).name = "PlayerPrefs";

        playButton.onClick.AddListener(() =>
        {
            OnStarted?.Invoke(this, EventArgs.Empty);
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene("Level_1");
        });

        quitButton.onClick.AddListener(() =>
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            PlayerPrefs.SetInt("IsTutorial", 1);
            Application.Quit();
        });
    }
}
