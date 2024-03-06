using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _gravityForce = 0.6f;
    [SerializeField] private Inputs inputs;
    [SerializeField] private float rotateTime = 10f;
    [SerializeField] private bool _canMove = true;
    private Vector3 _moveDir;
    
    private CharacterController _characterController;
    
    public PlayerStateMachine _stateMachine;
    public PlayerMovingState _movingState;
    public PlayerFallingState _fallingState;
    


    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();
        _movingState = new PlayerMovingState(this, _stateMachine);
        _fallingState = new PlayerFallingState(this, _stateMachine);
        
        _characterController = GetComponent<CharacterController>();
        LevelController.RegisterPlayer(gameObject);
    }
    
    private void Start()
    {
        _stateMachine.Initialize(_movingState);
    }

    private void OnDestroy()
    {
        LevelController.UnregisterPlayer();
    }
    
    private void Update()
    {       
        UpdateMovement();   
    }

    private void ApplyGravity()
    {
        _canMove = _characterController.isGrounded;
        _characterController.Move(Physics.gravity * Time.deltaTime * _gravityForce);
    }
    
    private void UpdateMovement()
    {
        _canMove = _characterController.isGrounded;
        
        _moveDir = CalculateMoveDir();
        Vector3 gravityDir = Physics.gravity * Time.deltaTime * _gravityForce;
        _characterController.Move(gravityDir);
        float moveDistance = _moveSpeed * Time.deltaTime;
        
        var rotateDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
        transform.forward = Vector3.Slerp(rotateDirection, _moveDir, Time.deltaTime * rotateTime);
        
        _characterController.Move(_moveDir * moveDistance);
    }
    
    public Vector3 CalculateMoveDir()
    {
        if (!_canMove) return Vector3.zero;
        Vector2 movementInputVector = inputs.GetMovementVectorNormalized();
        float Xdir = movementInputVector.x;
        float Ydir = movementInputVector.y;

        Vector3 moveDir = new Vector3(Xdir, 0, Ydir);
        
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;
        
        return moveDir.normalized;
    }
}
