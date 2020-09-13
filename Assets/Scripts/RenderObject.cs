using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour
{
    protected SpriteRenderer sprRenderer;

    virtual protected void Start()
    {
        sprRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
