using System;
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
            //_rb.linearVelocity += new Vector3(_moveDirection.x,0, _moveDirection.y) * moveSpeed * Time.deltaTime;
            transform.position += transform.forward * _moveDirection.y * moveSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0,_moveDirCam * _Sensibility,0);
        }

        #endregion


        #region Utils

        public void MovePlayer(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
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
        private float _moveDirCam;
        private Vector3 _moveDirection;
        private Rigidbody _rb;
        
        #endregion
    }
}
