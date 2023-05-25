using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogConstructor : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogMessage;

    public void SetDialogMessage(string message)
    {
        dialogMessage.text = message;
    }
}
