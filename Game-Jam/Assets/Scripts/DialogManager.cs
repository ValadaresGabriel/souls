using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public List<DialogData> dialogDatas;

    public AudioSource audioVoiceSource;

    [SerializeField] private GameObject dialogPanel;

    [SerializeField] private DialogConstructor dialogConstructor;

    public int dialogIndex = 0;

    private DialogData currentDialog;

    private void Start()
    {
        StartDialog();
    }

    private void Update()
    {
        if (audioVoiceSource != null && !audioVoiceSource.isPlaying)
        {
            // Aparecer setinha para continuar, apenas se der tempo
        }
    }

    public void StartDialog()
    {
        PlayDialog();
    }

    private void PlayDialog()
    {
        if (dialogPanel.activeSelf == false)
        {
            dialogPanel.SetActive(true);
        }

        currentDialog = dialogDatas[dialogIndex];

        dialogConstructor.SetDialogMessage(currentDialog.message);

        if (currentDialog.audioVoice != null)
        {
            audioVoiceSource.clip = currentDialog.audioVoice;
            audioVoiceSource.Play();
        }
    }

    public void NextDialog()
    {
        if (audioVoiceSource.isPlaying) return;

        dialogIndex++;

        if (!currentDialog.isDialogContinue)
        {
            dialogPanel.SetActive(false);
            return;
        }

        if (dialogIndex < dialogDatas.Count)
        {
            PlayDialog();
        }
    }

}
