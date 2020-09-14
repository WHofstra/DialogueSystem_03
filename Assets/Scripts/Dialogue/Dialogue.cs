using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField] protected float _typeSpeed;
    public float TypeSpeed { get { return _typeSpeed; } set { value = _typeSpeed; } }
}
