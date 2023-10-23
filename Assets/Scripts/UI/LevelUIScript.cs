using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIScript : UIScript
{
    public static EventHandler OnSettingsShown;
    public static EventHandler OnSettingsHidden;

    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI tutorialText1;
    [SerializeField] private TextMeshProUGUI tutorialText2;

    [SerializeField] private RectTransform tutorialRectTransform;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextTutorialButton;

    enum TutorialState
    {
        None,
        FirstScreen,
        SecondScreen,
        End
    }

    private TutorialState tutorialState = TutorialState.None;

    protected override void Awake()
    {
        base.Awake();

        levelNumberText.text += SceneManager.GetActiveScene().name.Split('_')[1];

        mainMenuButton.onClick.AddListener(() =>
        {
            OnTapDetected?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene("MainMenu");
        });

        nextTutorialButton.onClick.AddListener(() => 
        {
            if (tutorialState == TutorialState.FirstScreen)
            {
                StartCoroutine(ShowNextTutorialText());
                tutorialState = TutorialState.SecondScreen;
            }
            else if (tutorialState == TutorialState.SecondScreen)
            { 
                StartCoroutine(HideTutorial());
                tutorialState = TutorialState.End;
            }
        });

        if (PlayerPrefs.GetInt("IsTutorial") == 1)
        {
            StartCoroutine(ShowTutorial());
            tutorialState = TutorialState.FirstScreen;

            PlayerPrefs.SetInt("IsTutorial", 0);
        }
    }

    private IEnumerator ShowTutorial()
    {
        while (tutorialRectTransform.localPosition.y < 0f)
        {
            tutorialRectTransform.localPosition = new Vector3
                (
                    tutorialRectTransform.localPosition.x,
                    tutorialRectTransform.localPosition.y + 70f,
                    tutorialRectTransform.localPosition.z
                );

            yield return null;
        }
    }

    private IEnumerator HideTutorial()
    {
        while (tutorialRectTransform.localPosition.y > -1400f)
        {
            tutorialRectTransform.localPosition = new Vector3
                (
                    tutorialRectTransform.localPosition.x,
                    tutorialRectTransform.localPosition.y - 70f,
                    tutorialRectTransform.localPosition.z
                );

            yield return null;
        }
    }

    private IEnumerator ShowNextTutorialText()
    {
        while (tutorialText1.rectTransform.localPosition.y > -800f)
        {
            tutorialText1.rectTransform.localPosition = new Vector3
                (
                    tutorialText1.rectTransform.localPosition.x,
                    tutorialText1.rectTransform.localPosition.y - 80f,
                    tutorialText1.rectTransform.localPosition.z
                );

            yield return null;
        }

        while (tutorialText2.rectTransform.localPosition.y < -150f)
        {
            tutorialText2.rectTransform.localPosition = new Vector3
                (
                    tutorialText2.rectTransform.localPosition.x,
                    tutorialText2.rectTransform.localPosition.y + 30f,
                    tutorialText2.rectTransform.localPosition.z
                );

            yield return null;
        }
    }


    protected override IEnumerator ShowSettings()
    {
        yield return base.ShowSettings();

        OnSettingsShown?.Invoke(this, EventArgs.Empty);
    }

    protected override IEnumerator HideSettings()
    {
        yield return base.HideSettings();

        OnSettingsHidden?.Invoke(this, EventArgs.Empty);
    }
}
