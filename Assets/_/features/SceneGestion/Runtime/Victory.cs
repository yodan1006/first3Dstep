using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneGestion.Runtime
{
    public class Victory : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (((1<< other.gameObject.layer) & layerMaskPlayer) != 0) 
            {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
            }
        }
        
        [SerializeField] private LayerMask layerMaskPlayer;
    }
}
