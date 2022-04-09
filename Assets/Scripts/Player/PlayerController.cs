using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _controller;

        #region Variables: Movement
        private Vector3 _move;
        [SerializeField]
        private float _MOVE_SPEED = 15f;
        #endregion

        #region Variables: Inputs
        private DefaultInputActions _inputActions;
        private InputAction _moveAction;
        #endregion

        void Awake()
        {
            _inputActions = new DefaultInputActions();
            _controller = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            _moveAction = _inputActions.Player.Move;
            _moveAction.Enable();
        }

        void OnDisable()
        {
            _moveAction.Disable();
        }

        void Update()
        {
            _move = _moveAction.ReadValue<Vector2>();
            _controller.Move(
                new Vector3(_move.x, 0f, _move.y) * 
                Time.deltaTime * 
                _MOVE_SPEED
            );
        }
    }
}
