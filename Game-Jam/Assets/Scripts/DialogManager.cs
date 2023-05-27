using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    private AudioSource audioVoiceSource;

    private DialogData currentDialog;

    [SerializeField] private GameObject dialogPanel;

    [SerializeField] private DialogConstructor dialogConstructor;

    public List<DialogData> dialogDatas;

    public int dialogIndex = 0;

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
