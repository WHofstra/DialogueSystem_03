using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : RenderObject
{
    protected override void Start()
    {
        base.Start();
        npcDialogue = GetComponent<NPCDialogue>();
    }

    void OnMouseDown()
    {
        if (npcDialogue != null) {
            TriggerDialogue(npcDialogue.Name, npcDialogue.DialogueSet);
        }
    }
}
