using UnityEngine;

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

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            _move = new Vector3(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            ).normalized;
            _controller.Move(_move * Time.deltaTime * _MOVE_SPEED);
        }
    }
}
