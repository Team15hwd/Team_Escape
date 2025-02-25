using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerController : MonoBehaviour
{
    public abstract void TriggerEnter(CharacterController2D cc);

    public abstract void TriggerExit(CharacterController2D cc);
}
