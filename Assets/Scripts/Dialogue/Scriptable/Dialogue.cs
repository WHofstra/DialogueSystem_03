using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField] protected float _typeSpeed;
    public float TypeSpeed { get { return _typeSpeed; } set { value = _typeSpeed; } }

    virtual public string[] GetSentences()
    {
        string[] sentToSet = { null };
        return sentToSet;
    }

    virtual public string[] GetOptions()
    {
        string[] sentToSet = { null };
        return sentToSet;
    }
}
