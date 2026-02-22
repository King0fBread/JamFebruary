using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Animator _playerAnimator;

    private ItemPickupSystem _itemPickupSystem;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementVector;

    private Vector3 _originalScale;

    public InputSystem_Actions InputSystemActions;

    private float _lastDirection = 1f;
    //public event Action OnInteractPressed;

    private static MovementSystem _instance;
    public static MovementSystem Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        //Cursor.lockState = CursorLockMode.Locked;

        _itemPickupSystem = gameObject.GetComponent<ItemPickupSystem>();

        _playerAnimator = gameObject.GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _originalScale = transform.localScale;

        InputSystemActions = new InputSystem_Actions();

        InputSystemActions.Player.Move.performed += OnMove;
        InputSystemActions.Player.Move.canceled += OnMove;

        InputSystemActions.Player.Interact.started += OnInteract;
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext contect)
    {
        //OnInteractPressed?.Invoke();
        _itemPickupSystem.OnPick();
        KnockingSystem.Instance.TryToggleKnockableResident();
    }

    private void OnEnable()
    {
        InputSystemActions.Player.Enable();
    }
    private void OnDisable()
    {
        InputSystemActions.Player.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        float moveX = _movementVector.x;

        Vector2 targetPos = _rigidbody.position +
                            new Vector2(moveX * _moveSpeed * Time.fixedDeltaTime, 0);

        _rigidbody.MovePosition(targetPos);

        if (moveX != 0)
        {
            float direction = Mathf.Sign(moveX);

            transform.localScale = new Vector3(
                direction * 1f,
                1f,
                1f
            );

            _playerAnimator.CrossFade("PlayerRunning", 0, 0);
        }
        else
        {
            transform.localScale = _originalScale;

            _playerAnimator.CrossFade("PlayerIdle", 0, 0);
        }
    }
}
