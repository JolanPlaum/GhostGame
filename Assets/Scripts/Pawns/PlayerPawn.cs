using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPawn : MonoBehaviour
{
    protected MovementBehavior _movementBehavior;
    protected JumpBehavior _jumpBehavior;
    protected WorldSwitchingBehavior _worldSwitchingBehavior;

    private PlayerController _controller;
    public PlayerController Controller
    {
        get { return _controller; }
        set { _controller = value; }
    }

    // Get the behaviors from this gameobject
    protected virtual void Awake()
    {
        _movementBehavior = GetComponent<MovementBehavior>();
        _jumpBehavior = GetComponent<JumpBehavior>();
        _worldSwitchingBehavior = GetComponent<WorldSwitchingBehavior>();
    }

    // Functions that should be called by a controller
    public virtual void Move(Vector2 input)
    {
        if (_movementBehavior == null) return;

        Vector3 direction = input.x * Vector3.right + input.y * Vector3.forward;
        _movementBehavior.DesiredMovementDirection = direction;
        _movementBehavior.DesiredRotationDirection = direction;
    }
	public virtual void Jump()
    {
        if (_jumpBehavior == null) return;

        _jumpBehavior.Jump();
    }
	public virtual void WorldSwitch() { }
	public virtual void Action1() { }
	public virtual void Action2() { }
}
