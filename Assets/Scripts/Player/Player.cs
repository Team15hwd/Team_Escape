using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerTribe tribe;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [Header("SFX")]
    [SerializeField] private SoundData jumpSFX;


    private UserInput input;
    private CharacterController2D controller;
    private Animator animator;
    private SpriteRenderer sprRenderer;

    [SerializeField]
    private PlayerState playerState;
    public PlayerState PlayerState
    {
        get { return playerState; }
        set
        {
            playerState = value;
            //해시로 바꾸기
            animator.SetInteger("StateID", (int)playerState);
        }
    }

    void Awake()
    {
        InitInputActions();

        controller = GetComponent<CharacterController2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        sprRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (tribe == PlayerTribe.Human)
        {
            input.Player1.Jump.started += (val) => Jump();
        }
        else
        {
            input.Player2.Jump.started += (val) => Jump();
        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        StateControl();

        Vector2 moveDirection = Vector2.zero;

        moveDirection = tribe == PlayerTribe.Human ?
            input.Player1.Move.ReadValue<Vector2>() :
            input.Player2.Move.ReadValue<Vector2>();

        Move(moveDirection);
    }

    private void Move(Vector2 direction)
    {
        controller.Move(direction * moveSpeed);
    }

    private void Jump()
    {
        if (controller.canJump && controller.IsGrounded)
        {
            controller.Jump(jumpPower);
            SoundManager.Instance.Bulider().WidthSoundData(jumpSFX).Play();
        }
    }

    private void StateControl()
    {
        if (controller.IsGrounded)
        {
            PlayerState = controller.Velocity.x - controller.externalVelocity.x != 0 ?
                PlayerState.Move : PlayerState.Idle;
        }
        else
        {
            PlayerState = PlayerState.Jump;
        }

        if (Mathf.Abs(controller.Velocity.x) > 0.1f)
        {
            sprRenderer.flipX = controller.Velocity.x < 0 ? true : false;
        }
    }

    private void InitInputActions()
    {
        input = new();
    }
}


public enum PlayerTribe
{
    Human, //1P
    Alien //2P
}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Ladder,
    Die
}