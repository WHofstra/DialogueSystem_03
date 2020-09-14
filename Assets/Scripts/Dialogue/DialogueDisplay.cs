using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] DialogueSystem _system;

    void Start()
    {
        for (int i = 0; i < 4; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_system != null)
        {
            _system.Activate += Show;
        }
    }

    void Show()
    {
        for (int i = 0; i < 4; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
