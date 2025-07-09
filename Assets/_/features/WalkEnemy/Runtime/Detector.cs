using System;
using UnityEngine;

namespace WalkEnemy.Runtime
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Patrouille scriptEnemy;
        [SerializeField] private MeshCollider collider;


        private void Awake()
        {
            collider = GetComponent<MeshCollider>();
        }

        private void Start()
        {
            collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & playerMask) != 0)
            {
                scriptEnemy.m_visionActive = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & playerMask) != 0)
            {
                scriptEnemy.m_visionActive = false;
                //scriptEnemy.m_rayCountalert = 0;
            }
        }
        
        
    }
}
