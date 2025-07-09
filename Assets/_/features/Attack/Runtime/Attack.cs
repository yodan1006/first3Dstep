using UnityEngine;
using UnityEngine.InputSystem;

namespace Attack.Runtime
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        public void Attacking(InputAction.CallbackContext context)
        {
            if (context.started)
                _collider.enabled = true;
            if (context.canceled)
                _collider.enabled = false;
        }
    }
}
