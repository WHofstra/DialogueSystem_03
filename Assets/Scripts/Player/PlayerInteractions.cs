using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractions : RenderObject
{
    public Action TriggerDialogue;

    protected override void Start()
    {
        base.Start();
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
                TriggerDialogue();
            }
        }
    }
}
