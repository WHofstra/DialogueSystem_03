using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RenderObject : MonoBehaviour
{
    public Action<string, Dialogue[]> TriggerDialogue;

    protected NPCDialogue npcDialogue;
    protected SpriteRenderer sprRenderer;

    virtual protected void Start()
    {
        sprRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
