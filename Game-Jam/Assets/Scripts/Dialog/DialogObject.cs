using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObject : MonoBehaviour
{
    [SerializeField] private bool isDialog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && isDialog == false)
        {
            DialogManager.Instance.StartDialog();
            isDialog = true;
        }
    }
}
