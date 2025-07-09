using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WalkEnemy.Runtime;

namespace Ragdoll.Runtime
{
    public class ActivatorRagdoll : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody[] _bones;
        private Collider[] _boneColliders;
        public bool _IsTouched;
        float time;
        [SerializeField] private float _timeDespawn;
        private Patrouille _patrouille;
        private Detector _detector;
        private LoseTrigger _loseTrigger;
        [SerializeField] private GameObject disable; 

        void Awake()
        {
            _animator = GetComponent<Animator>();

            // Récupère tous les rigidbodies/colliders enfants (sauf le root)
            _bones = GetComponentsInChildren<Rigidbody>();
            _boneColliders = GetComponentsInChildren<Collider>();
            

            // Place tout en mode inactif physique
            foreach (var rb in _bones)
            {
                if (rb != GetComponent<Rigidbody>())   // garde le root éventuel tranquille
                    rb.isKinematic = true;
            }

            foreach (var col in _boneColliders)
            {
                if (col != GetComponent<Collider>())
                    col.enabled = false;
            }
        }

        private void Start()
        {
            _patrouille = GetComponent<Patrouille>();
            _detector = GetComponentInChildren<Detector>();
            _loseTrigger = GetComponentInChildren<LoseTrigger>();
        }

        void Update()
        {
            if (_IsTouched)
            {
                disable.SetActive(false);
                ActivateRagdoll();
                time += Time.deltaTime;
                _patrouille.enabled = false;
                _detector.enabled = false;
                _loseTrigger.enabled = false;
            }
        }

        // /// À appeler quand tu veux passer en ragdoll
        // public void ActivateRagdoll(InputAction.CallbackContext context)
        // {
        //     // 1. Stoppe les animations
        //     _animator.enabled = false;
        //
        //     // 2. Libère les os pour la physique
        //     foreach (var rb in _bones)
        //         rb.isKinematic = false;
        //
        //     foreach (var col in _boneColliders)
        //         col.enabled = true;
        // }
        
        public void ActivateRagdoll()
        {
            // 1. Stoppe les animations
            _animator.enabled = false;

            // 2. Libère les os pour la physique
            foreach (var rb in _bones)
                rb.isKinematic = false;

            foreach (var col in _boneColliders)
                col.enabled = true;
            if (time > _timeDespawn) Destroy(gameObject);
        }
        
        
    }
}
