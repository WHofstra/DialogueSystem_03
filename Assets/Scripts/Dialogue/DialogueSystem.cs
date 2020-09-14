using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueSystem : MonoBehaviour
{
    public Action Activate;

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

    void PutInQueue()
    {
        Activate();
    }
}
