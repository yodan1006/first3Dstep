using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovePlayer.Runtime
{
    public class Move : MonoBehaviour
    {
        #region Publics

        

        #endregion


        #region Unity Api

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _IsGrounded = Physics.CheckSphere(_GroundCheck.transform.position, _radiusCheck, _groundLayer);
            _animator.SetBool("isJumping", !_IsGrounded);
            //_rb.linearVelocity += new Vector3(_moveDirection.x,0, _moveDirection.y) * moveSpeed * Time.deltaTime;
            transform.position += transform.forward * _moveDirection.y * moveSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0,_moveDirCam * _Sensibility,0);
        }

        #endregion


        #region Utils

        public void MovePlayer(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
            if (context.performed)
            {
                if (_moveDirection.y > 0)
                {
                  _animator.SetFloat("Speed", 0.4f);
                  _animator.SetFloat("axez", 1);
                }
                else if (_moveDirection.y < 0)
                {
                     _animator.SetFloat("Speed", -0.4f);
                     _animator.SetFloat("axez", -1);
                }
            }

            if (context.canceled)
            {
                _animator.SetFloat("Speed", 0);
                _animator.SetFloat("axez", 0);
            }
        }

        public void SprintPlayer(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                moveSpeed = moveSpeed * 2;
                _animator.SetBool("IsRuning",true);
            }
            if (context.canceled)
            {
                moveSpeed *= 0.5f;
                _animator.SetBool("IsRuning",false);
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            float positionY = transform.position.y;

            if (context.performed && _IsGrounded)
            {
                _animator.SetBool("isJumping",true);
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                
            }
            
        }

        public void MoveCamera(InputAction.CallbackContext context)
        {
            _moveDirCam = context.ReadValue<Vector2>().x;
        }

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates
        
        [SerializeField]private float moveSpeed = 5f;
        [Range(0, 100)] [SerializeField] private float _Sensibility;
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _IsJumping;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _IsGrounded;
        [SerializeField] private GameObject _GroundCheck;
        [SerializeField] private float _radiusCheck;
        [SerializeField] private LayerMask _groundLayer;
        private float _moveDirCam;
        private Vector3 _moveDirection;
        private Rigidbody _rb;

        #endregion
    }
}
