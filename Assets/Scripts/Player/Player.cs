using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, UserInput.IPlayer1Actions, UserInput.IPlayer2Actions
{
    [SerializeField] private PlayerTribe tribe;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [Header("SFX")]
    [SerializeField] private SoundData jumpSFX;

    private UserInput input;
    private CharacterController2D controller;

    public PlayerTribe Tribe => tribe;

    void Awake()
    {
        InitInputActions();

        controller= GetComponent<CharacterController2D>();
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
        if (tribe == PlayerTribe.Human)
        {
            controller.Move(input.Player1.Move.ReadValue<Vector2>() * moveSpeed);
        }
        else
        {
            controller.Move(input.Player2.Move.ReadValue<Vector2>() * moveSpeed);
        }
    }

    private void InitInputActions()
    {
        input = new();


        if (tribe == PlayerTribe.Human)
        {
            input.Player1.SetCallbacks(this);
        }
        else
        {
            input.Player2.SetCallbacks(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        //controller.Move(inputVector * moveSpeed);

        if (context.phase == InputActionPhase.Performed)
        {
            if (controller.IsGrounded && inputVector.y > 0f)
            {
                controller.Jump(jumpPower);

                SoundManager.Instance.Bulider().WidthSoundData(jumpSFX).Play();
            }
        }
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