using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourHandler : MonoBehaviour
{
    [HideInInspector]
    public List<StateMachineAction> Actions = new List<StateMachineAction>();
}
