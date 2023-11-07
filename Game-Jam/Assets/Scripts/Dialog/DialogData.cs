using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog", order = 0)]
public class DialogData : ScriptableObject
{
    [Space(2)]
    [Tooltip("Quem está falando")]
    public string dialogOwner;

    [Tooltip("O texto")]
    [TextArea(4, 4)]
    public string message;

    [Tooltip("Áudio de dublagem")]
    public AudioClip audioVoice;

    [Tooltip("Flag que dirá se o diálogo continuará ou não")]
    public bool isDialogContinue = true;

    public bool goToTimeInTimeline = false;
    public float timeToGoToTimeline;

    public bool enableGameObjectOfScene = false;
}
