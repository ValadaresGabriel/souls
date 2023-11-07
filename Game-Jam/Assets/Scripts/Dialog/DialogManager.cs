using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TranscendenceStudios;
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

    public bool isDialogPlaying;

    private Coroutine displayDialogMessageCoroutine;

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
        if (displayDialogMessageCoroutine != null) return;

        if (dialogPanel.activeSelf == false)
        {
            dialogPanel.SetActive(true);
            isDialogPlaying = true;
        }

        currentDialog = dialogDatas[dialogIndex];

        dialogConstructor.SetDialogOwner(currentDialog.dialogOwner);
        displayDialogMessageCoroutine = StartCoroutine(DisplayTextByTime(currentDialog.message));
        // dialogConstructor.SetDialogMessage(currentDialog.message);

        if (currentDialog.audioVoice != null)
        {
            audioVoiceSource.clip = currentDialog.audioVoice;
            audioVoiceSource.Play();
        }
    }

    public void NextDialog()
    {
        if (displayDialogMessageCoroutine != null) return;

        if (audioVoiceSource != null && audioVoiceSource.clip != null && audioVoiceSource.isPlaying) return;

        if (isDialogPlaying == false || dialogPanel.activeSelf == false) return;

        dialogIndex++;

        if (!currentDialog.isDialogContinue)
        {
            dialogPanel.SetActive(false);
            isDialogPlaying = false;

            if (currentDialog.goToTimeInTimeline)
            {
                if (TimelineManager.Instance != null)
                {
                    TimelineManager.Instance.GoToTimeInTimeline(currentDialog.timeToGoToTimeline);
                }
            }

            if (currentDialog.enableGameObjectOfScene)
            {
                if (EnableGameObjectManager.Instance != null)
                {
                    GameObject gameObjectToEnable = EnableGameObjectManager.Instance.gameObjectsToEnable[EnableGameObjectManager.Instance.index++];

                    gameObjectToEnable.SetActive(true);
                }
            }
            return;
        }

        if (dialogIndex < dialogDatas.Count)
        {
            PlayDialog();
        }
    }

    private IEnumerator DisplayTextByTime(string dialogMessage)
    {
        string text = "";
        foreach (char c in dialogMessage.ToCharArray())
        {
            text += c;
            dialogConstructor.SetDialogMessage(text);
            yield return new WaitForSeconds(.01f);
        }

        StopCoroutine(displayDialogMessageCoroutine);
        displayDialogMessageCoroutine = null;
    }

}
