using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlatform : TriggerController
{
    [SerializeField] private float climbSpeed = 5f;

    private List<CharacterController2D> players = new();

    public override void TriggerEnter(CharacterController2D cc)
    {
        players.Add(cc);
    }

    public override void TriggerExit(CharacterController2D cc)
    {
        if (cc.IsOutOfControl)
        {
            cc.UseOutOfControl();
            cc.inputVelocity = Vector2.zero;
        }
        players.Remove(cc);
    }

    void Update()
    {
        foreach (var iter in players)
        {
            if (!iter.IsOutOfControl && iter.inputVelocity.y != 0f)
            {
                iter.UseOutOfControl();
            }
            else if (iter.IsOutOfControl)
            {
                var moveVec = iter.inputVelocity.normalized;

                if (moveVec.y != 0f)
                {
                    iter.transform.position = new Vector3(transform.position.x, iter.transform.position.y, iter.transform.position.z);
                }

                if (iter.transform.position.y < transform.position.y)
                {
                    iter.externalVelocity = new Vector2(moveVec.x, moveVec.y) * climbSpeed;
                }
                else
                {
                    iter.externalVelocity = new Vector2(0f, moveVec.y) * climbSpeed;
                }
            }
        }
    }
}
