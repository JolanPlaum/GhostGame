using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputActionAsset _inputAsset;
	private InputAction _moveAction;
	private InputAction _jumpAction;
	private InputAction _worldSwitchAction;
	private InputAction _action1;
	private InputAction _action2;

	[SerializeField] private PlayerPawn _pawn;
	public PlayerPawn Pawn
	{
		get { return _pawn; }
		set { _pawn = value; }
	}

	// Make sure to enable/disable the input asset
	private void OnEnable()
	{
		if (_inputAsset == null) return;
		_inputAsset.Enable();
	}
	private void OnDisable()
	{
		if (_inputAsset == null) return;
		_inputAsset.Disable();
	}

	// Set all the input actions and bind/unbind callbacks
	private void Awake()
	{
		if (_inputAsset == null) return;

		_moveAction = _inputAsset.FindActionMap("Gameplay").FindAction("Movement");
		_jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
		_worldSwitchAction = _inputAsset.FindActionMap("Gameplay").FindAction("WorldSwitch");
		_action1 = _inputAsset.FindActionMap("Gameplay").FindAction("Action1");
		_action2 = _inputAsset.FindActionMap("Gameplay").FindAction("Action2");

		if (_jumpAction != null) _jumpAction.performed += HandleJumpInput;
		if (_worldSwitchAction != null) _worldSwitchAction.performed += HandleWorldSwitchInput;
		if (_action1 != null) _action1.performed += HandleAction1Input;
		if (_action2 != null) _action2.performed += HandleAction2Input;
	}
	private void OnDestroy()
	{
		if (_inputAsset == null) return;

		if (_jumpAction != null) _jumpAction.performed -= HandleJumpInput;
		if (_worldSwitchAction != null) _worldSwitchAction.performed -= HandleWorldSwitchInput;
		if (_action1 != null) _action1.performed -= HandleAction1Input;
		if (_action2 != null) _action2.performed -= HandleAction2Input;
	}

	// Initialize pawn/controller connection
	private void Start()
	{
		SwitchPawn(_pawn);
	}

	// Update is called once per frame
	private void Update()
    {
		HandleMovementInput();
    }

	// Correctly switching pawns, encapsulated in a function instead of put inside of getter
	public void SwitchPawn(PlayerPawn pawn)
	{
		// Controller can not exist without pawn
		if (pawn == null) return;

		// Don't switch if the new pawn is already being controller
		if (pawn.Controller != null) return;

		// Make sure to remove this controller from previous pawn
		_pawn.Controller = null;

		// Set this controller on the new pawn
		pawn.Controller = this;
		_pawn = pawn;
	}

	// Handle input functions
	private void HandleMovementInput()
	{
		if (_moveAction == null) return;

		Vector2 movement = _moveAction.ReadValue<Vector2>();

		_pawn.Move(movement);
	}
	private void HandleJumpInput(InputAction.CallbackContext context)
	{
		_pawn.Jump();
	}
	private void HandleWorldSwitchInput(InputAction.CallbackContext context)
	{
		_pawn.WorldSwitch();
	}
	private void HandleAction1Input(InputAction.CallbackContext context)
	{
		_pawn.Action1();
	}
	private void HandleAction2Input(InputAction.CallbackContext context)
	{
		_pawn.Action2();
	}
}
