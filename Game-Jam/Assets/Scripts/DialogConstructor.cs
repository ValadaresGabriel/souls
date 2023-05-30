using UnityEngine;
using TMPro;

public class DialogConstructor : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogMessage;

    [SerializeField] private TMP_Text dialogOwner;

    public void SetDialogMessage(string message)
    {
        dialogMessage.text = message;
    }

    public void SetDialogOwner(string message)
    {
        dialogOwner.text = message;
    }
}
