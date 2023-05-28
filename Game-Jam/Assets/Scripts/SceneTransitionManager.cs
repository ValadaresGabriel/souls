using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private GameObject hideScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(SceneTransition(scene));
    }

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    IEnumerator SceneTransition(string scene)
    {
        hideScene.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);

        hideScene.SetActive(false);
    }

    IEnumerator Transition()
    {
        hideScene.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        hideScene.SetActive(false);
    }
}
