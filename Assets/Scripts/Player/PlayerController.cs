using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _controller;
        [SerializeField] private Animator _animator;

        #region Variables: Movement
        private Vector3 _move;
        [SerializeField]
        private float _MOVE_SPEED = 15f;
        private bool _running;
        #endregion

        #region Variables: Inputs
        private DefaultInputActions _inputActions;
        private InputAction _moveAction;
        #endregion

        #region Variables: Animation
        private int _animRunningParamHash;
        private Transform _animatorTransform;
        #endregion

        void Awake()
        {
            _inputActions = new DefaultInputActions();
            _controller = GetComponent<CharacterController>();
            _animatorTransform = _animator.transform;

            _running = false;
            // pre-computing the integer hashed equivalent of the “Running” parameter and caching it
            _animRunningParamHash = Animator.StringToHash("Running");
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
            if (_move.sqrMagnitude > 0.01f)
            {
                if (!_running)
                {
                    _running = true;
                    _animator.SetBool(_animRunningParamHash, true);
                }
                Vector3 v = new Vector3(_move.x, 0f, _move.y);
                // Because of how the FBX model is configured, I need to reverse the 3D version of my _move vector
                _animatorTransform.rotation = Quaternion.LookRotation(-v, Vector3.up);
                _controller.Move(
                    v * 
                    Time.deltaTime * 
                    _MOVE_SPEED
                );
            }
            else if (_running)
            {
                _running = false;
                _animator.SetBool(_animRunningParamHash, false);
            }
        }
    }
}
