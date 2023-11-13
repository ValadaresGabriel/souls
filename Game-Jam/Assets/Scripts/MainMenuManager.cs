using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] Button playButton;
    [SerializeField] GameObject playButtonGameObject;
    private TextMeshProUGUI playButtonText;

    private string levelToLoad;

    private void Awake()
    {
        playButtonText = playButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("LevelToLoad"))
        {
            playButtonText.SetText("Continue");

            if (PlayerPrefs.HasKey("HasFinishedGame"))
            {
                if (PlayerPrefs.GetInt("HasFinishedGame") == 1)
                {
                    playButtonText.SetText("Play Again");
                }
            }
            else
            {
                PlayerPrefs.SetInt("HasFinishedGame", 0);
            }
        }
    }

    public void Play()
    {
        AudioManager.Instance.PlayPlayAndDialogButtonSound();

        if (!PlayerPrefs.HasKey("LevelToLoad") || (PlayerPrefs.HasKey("HasFinishedGame") && PlayerPrefs.GetInt("HasFinishedGame") == 1))
        {
            levelToLoad = "Beginning";
        }
        else
        {
            levelToLoad = PlayerPrefs.GetString("LevelToLoad");
        }

        SceneTransitionManager.Instance.LoadScene(levelToLoad);
    }

    public void MenuCreditsController()
    {
        AudioManager.Instance.PlayButtonSoundButtonSound();
        mainMenu.SetActive(!mainMenu.activeSelf);
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void Exit()
    {
        AudioManager.Instance.PlayButtonSoundButtonSound();
        Application.Quit();
    }
}
