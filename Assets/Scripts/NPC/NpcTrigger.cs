using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCTrigger : RenderObject
{
    public Action TriggerDialogue;

    protected override void Start()
    {
        base.Start();
    }

    void OnMouseDown()
    {
        TriggerDialogue();
    }
}
