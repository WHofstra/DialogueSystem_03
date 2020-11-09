using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question")]
public class Question : Dialogue
{
    [SerializeField] protected string _sentence;
    public string Sentence { get { return _sentence; } set { value = _sentence; } }

    [SerializeField] protected string[] _options;
    public string[] Options { get { return _options; } set { value = _options; } }

    public override string[] GetSentences()
    {
        string[] sentToSet = { _sentence };
        return sentToSet;
    }

    public override string[] GetOptions()
    {
        return _options;
    }
}
