using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : RenderObject
{
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
        }
    }
}
