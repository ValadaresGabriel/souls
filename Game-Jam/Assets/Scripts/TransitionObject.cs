using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObject : MonoBehaviour
{
    [SerializeField] private string newSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.LoadScene(newSceneName);
        }
    }
}
