using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    //Scriptable Object로 설정 저장 가능하게 바꾸기
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
    public Vector2 inputVelocity = Vector2.zero;

    private Vector2 groundNormal = Vector2.up;
    private Vector2 blockNormal = Vector2.zero;

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

    public bool canJump = true;

    [ContextMenu("Out Controller")]
    public void UseOutOfControl()
    {
        isOutOfControl = !isOutOfControl;

        if (isOutOfControl)
        {
            //string값을 const or layermask 로 수정
            gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
            rid.velocity = Vector2.zero;
            verticalVelocity = Vector2.zero;
            rid.gravityScale = 0f;
        }
        else
        {
            gameObject.layer = myLayerCache;
            rid.gravityScale = gravityCache;
        }
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
        if (isOutOfControl)
        {
            rid.velocity = externalVelocity;
            return;
        }

        UpdateGrouning();
        UpdateBlocking();
        UpdateVelocity();
    }

    //player 클래스로 옮기기
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TriggerController tc))
        {
            tc.TriggerEnter(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TriggerController tc))
        {
            tc.TriggerExit(this);
        }
    }
    //

    public void Move(Vector2 movePos)
    {
        horizontalVelocity.x = movePos.x;

        inputVelocity = movePos;
    }

    public void Jump(float jumpPower)
    {
        if (canJump && isGrounded)
        {
            canJump = false;

            if (!isOutOfControl)
            {
                verticalVelocity.y = jumpPower;
            }
        }
    }

    private void UpdateGrouning()
    {
        isGrounded = false;
        isSteepSloped = false;
        isSloped = false;

        groundNormal = Vector2.down;

        var capsuleHeight = col.size.y * 0.5f;
        var capsuleRadius = col.size.x * 0.5f;
        var castOrigin = (Vector2)transform.position + (Vector2.down * capsuleHeight) + (Vector2.up * (capsuleRadius));
        var size = new Vector2(col.size.x - skinWidth, col.size.x) * transform.localScale;

        //0.05 상수값으로 수정;
        var capsuleCast = Physics2D.CapsuleCast(castOrigin, size, col.direction, 0f, Vector2.down, skinWidth + 0.1f, targetLayers);

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
                else
                {
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                }
            }
        }

        if (rid.velocity.y <= 0f)
        {
            canJump = true;
        }
    }

    private void UpdateBlocking()
    {
        isBlocked = false;
        blockNormal = Vector2.zero;

        var sign = Mathf.Sign(horizontalVelocity.x);

        //상수값으로 변경
        var capsuleCast = Physics2D.CapsuleCast(transform.position, col.bounds.size * (Vector2)transform.localScale, col.direction, 0f, Vector2.right * sign,
            skinWidth + 0.01f, targetLayers);

        if (capsuleCast)
        {
            isBlocked = true;

            var hit = Physics2D.Raycast(new Vector2(transform.position.x, capsuleCast.point.y), Vector2.right * sign, skinWidth + 0.01f, targetLayers);

            if (hit)
            {
                blockNormal = capsuleCast.normal;
            }
        }

        //capsuleCast = Physics2D.CapsuleCast(transform.position, col.bounds.size * (Vector2)transform.localScale, col.direction, 0f, -Vector2.right,
        //    skinWidth + 0.01f, targetLayers);

        //if (capsuleCast)
        //{
        //    isBlocked = true;
        //}
    }

    private void UpdateVelocity()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3)rid.velocity);

        rid.gravityScale = gravityCache;

        if (isGrounded && !isSloped)
        {
            rid.velocity = new Vector2(inputVelocity.x, 0f);
        }
        else if (isSloped && !isSteepSloped)
        {
            var projection = ProjectiOnPlane(inputVelocity, groundNormal);
            rid.velocity = projection;

            rid.gravityScale = 0f;
        }
        else if (isSteepSloped)
        {
            rid.velocity = ProjectiOnPlane(rid.velocity, groundNormal);
        }
        else if (isBlocked && !isGrounded)
        {
            if (blockNormal != Vector2.zero)
            {
                var projection = ProjectiOnPlane(horizontalVelocity, blockNormal);

                rid.velocity = new Vector2(projection.x, rid.velocity.y);
            }
        }
        else
        {
            rid.velocity = new Vector2(inputVelocity.x, rid.velocity.y);
        }

        rid.velocity = rid.velocity + externalVelocity;

        //jumped 임시코드
        if (verticalVelocity.y > 0f)
        {
            rid.velocity = new Vector2(rid.velocity.x + externalVelocity.x, verticalVelocity.y);
        }

        externalVelocity = Vector2.zero;
        verticalVelocity = Vector2.zero;
    }

    private Vector2 ProjectiOnPlane(Vector2 vel, Vector2 normal)
    {
        var mag = vel.magnitude;

        return Vector3.ProjectOnPlane(vel, normal).normalized * mag;
    }
}
