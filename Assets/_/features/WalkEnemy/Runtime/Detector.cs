using UnityEngine;

namespace WalkEnemy.Runtime
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Patrouille scriptEnemy;

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
