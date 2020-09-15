using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueSystem : MonoBehaviour
{
    public Action Activate;
    public Action<string[]> Options;
    public Action<string, string> ChangeToNext;

    [SerializeField] PlayerInteractions _player;
    [SerializeField] NPCTrigger[] _npcs;

    void Start()
    {
        if (_player != null) {
            _player.TriggerDialogue += PutInQueue;
        }

        if (_npcs != null)
        {
            for (int i = 0; i < _npcs.Length; i++) {
                _npcs[i].TriggerDialogue += PutInQueue;
            }
        }
    }

    void PutInQueue(string npcName, Dialogue[] dialogueSet)
    {
        StopAllCoroutines();
        Activate();
        StartCoroutine(ActivationCoroutine(1.0f, npcName, dialogueSet[0].GetSentences()[0], dialogueSet[0].GetOptions()));
    }

    IEnumerator ActivationCoroutine(float seconds, string npcName, string sentence, string[] options)
    {
        yield return new WaitForSeconds(seconds);

        if (sentence != null) {
            ChangeToNext(npcName, sentence);
        }

        if (options[0] != null) {
            Options(options);
        }
    }
}
