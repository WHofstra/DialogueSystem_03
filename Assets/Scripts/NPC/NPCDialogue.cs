using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] Dialogue[] _dialogueSet;

    public string Name { get { return _name; } set { value = _name; } }
    public Dialogue[] DialogueSet { get { return _dialogueSet; } set { value = _dialogueSet; } }
}
