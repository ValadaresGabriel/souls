using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] float transitionTime = 1f;
    [SerializeField] Animator animator;

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
        animator.SetTrigger("In");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // SceneManager.LoadScene(scene);

        animator.SetTrigger("Fade");
    }

    IEnumerator Transition()
    {
        animator.SetTrigger("In");

        yield return new WaitForSeconds(transitionTime);

        animator.SetTrigger("Fade");
    }
}
