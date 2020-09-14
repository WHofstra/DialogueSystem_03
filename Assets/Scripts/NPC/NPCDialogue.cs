using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] Dialogue[] _dialogue;

    public string Name { get { return _name; } set { value = _name; } }
}
