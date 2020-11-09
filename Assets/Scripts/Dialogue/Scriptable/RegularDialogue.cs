using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regular Dialogue", menuName = "Regular Dialogue")]
public class RegularDialogue : Dialogue
{
    [SerializeField] protected string[] _sentences;
    public string[] Sentences { get { return _sentences; } set { value = _sentences; } }

    public override string[] GetSentences()
    {
        return _sentences;
    }
}
