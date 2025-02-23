using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    [Header("Controller Setting")]
    [SerializeField] private float accel = 50f;
    [SerializeField] private float skinWidth = 0.01f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float detectingDistance = 1f;

    [Header("Slope")]
    [SerializeField][Range(0f, 89f)] private float maxSlope = 60f;

    private Rigidbody2D rid;
    private CapsuleCollider2D col;

    private Vector2 horizontalVelocity = Vector2.zero;
    private Vector2 verticalVelocity = Vector2.zero;
    public Vector2 externalVelocity = Vector2.zero;

    private Vector2 groundNormal = Vector2.up;
    private float gravityCache = float.MaxValue;
    private int myLayerCache;

    //캐릭터의 상태 플래그
    private bool isGrounded;
    private bool isSloped;
    private bool isSteepSloped;
    private bool isBlocked;
    private bool isOutOfControl = false;
    private bool isOutOfPhysics = false;

    public bool IsGrounded => isGrounded;
    public bool IsSloped => isSloped;
    public bool IsSteepSloped => isSteepSloped;
    public bool IsBlocked => isBlocked;
    public bool IsOutOfControl => isOutOfControl;
    public bool IsOutOfPhysics => isOutOfPhysics;

    [ContextMenu("Out Controller")]
    public void UseOutOfControl()
    {
        isOutOfControl = !isOutOfControl;

        if (isOutOfControl)
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");

        }
        else
        {
            gameObject.layer = myLayerCache;
        }
    }

    public void UseOutOfPhysics()
    {
        isOutOfPhysics = !isOutOfPhysics;
    }

    void Awake()
    {
        rid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        rid.bodyType = RigidbodyType2D.Dynamic;
        gravityCache = rid.gravityScale;
        myLayerCache = gameObject.layer;
    }

    void FixedUpdate()
    {
        UpdateGrouning();
        UpdateBlocking();
        UpdateVelocity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Physics2D.queriesStartInColliders = this;
        }
    }

    public void Move(Vector2 movePos)
    {
        horizontalVelocity.x = movePos.x;
    }

    public void Jump(float jumpPower)
    {
        verticalVelocity.y = jumpPower;
    }

    private void UpdateGrouning()
    {
        isGrounded = isSloped = isSteepSloped = false;
        groundNormal = Vector2.up;

        var capsuleHeight = col.size.y * 0.5f;
        var capsuleRadius = col.size.x * 0.5f;
        var castOrigin = rid.position + (Vector2.down * capsuleHeight) + (Vector2.up * (capsuleRadius));
        var size = new Vector2(col.size.x - skinWidth, col.size.x);

        //0.05 상수값으로 수정;
        var capsuleCast = Physics2D.CapsuleCast(castOrigin, size, col.direction, 0f, Vector2.down, skinWidth + 0.05f, targetLayers);

        if (capsuleCast)
        {
            Debug.DrawLine(castOrigin, capsuleCast.point, Color.green);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(capsuleCast.point.x, castOrigin.y), Vector2.down, skinWidth + capsuleRadius + 0.01f, targetLayers);

            if (hit)
            {
                groundNormal = hit.normal;
                isGrounded = true;

                if (groundNormal != Vector2.up)
                {
                    isSloped = true;

                    if (Vector2.Angle(groundNormal, Vector2.up) > maxSlope)
                    {
                        isSteepSloped = true;
                    }
                }
            }
        }
    }

    private void UpdateBlocking()
    {
        isBlocked = false;

        var sign = Mathf.Sign(horizontalVelocity.x);

        //상수값으로 변경
        var capsuleCast = Physics2D.CapsuleCast(rid.position, col.bounds.size, col.direction, 0f, (Vector2.right * sign).normalized,
            skinWidth + 0.01f, targetLayers);

        if (capsuleCast)
        {
            isBlocked = true;
            Debug.Log("123123");
        }
    }

    private void UpdateVelocity()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3)rid.velocity);

        if (isOutOfControl)
        {
            rid.velocity = Vector2.zero;
            rid.gravityScale = 0f;
        }
        else if (isBlocked && !isGrounded)
        {
            rid.gravityScale = gravityCache;
            rid.velocity = new Vector2(0f, rid.velocity.y) + verticalVelocity;
        }
        else if (isSloped && !isSteepSloped)
        {
            rid.velocity = ProjectiOnPlane(horizontalVelocity, groundNormal) + verticalVelocity;
            rid.gravityScale = 0f;
        }
        else if (isSteepSloped)
        {
            rid.gravityScale = gravityCache;
            rid.velocity = ProjectiOnPlane(rid.velocity, groundNormal) + verticalVelocity;
        }
        else
        {
            rid.gravityScale = gravityCache;
            rid.velocity = new Vector2(horizontalVelocity.x, rid.velocity.y) + verticalVelocity;
        }

        rid.velocity = rid.velocity + externalVelocity;

        externalVelocity = Vector2.zero;
        verticalVelocity = Vector2.zero;
    }

    private Vector2 ProjectiOnPlane(Vector2 vel, Vector2 normal)
    {
        var mag = vel.magnitude;

        return Vector3.ProjectOnPlane(vel, normal).normalized * mag;
    }
}
