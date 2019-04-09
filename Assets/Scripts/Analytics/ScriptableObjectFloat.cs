using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float_", menuName = "Variables/FloatVariable")]
public class ScriptableObjectFloat : ScriptableObject
{
    [SerializeField] public float value;
}
