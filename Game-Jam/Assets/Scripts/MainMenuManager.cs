using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject creditsMenu;

    public void Play()
    {
        SceneTransitionManager.Instance.LoadScene("Beginning");
    }

    public void MenuCreditsController()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
