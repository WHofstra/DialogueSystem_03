using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractions : RenderObject
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.NPC))
        {
            if (col.GetComponent<NPCDialogue>() != null) {
                npcDialogue = col.GetComponent<NPCDialogue>();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.NPC))
        {
            if (col.gameObject.transform.position.y >= transform.position.y) {
                sprRenderer.sortingOrder = 2;
            }
            else {
                sprRenderer.sortingOrder = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (npcDialogue != null) {
                    TriggerDialogue(npcDialogue.Name, npcDialogue.DialogueSet);
                }
            }
        }
    }
}
