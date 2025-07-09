using System;
using Ragdoll.Runtime;
using UnityEngine;

namespace MovePlayer.Runtime
{
    public class KillTriigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

           ActivatorRagdoll lol = other.GetComponent<ActivatorRagdoll>();
           
           lol._IsTouched = true;
           
        }
    }
}
