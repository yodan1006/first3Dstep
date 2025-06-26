using System;
using UnityEngine;

namespace GestionCam.Runtime
{
    public class SwitchCam : MonoBehaviour
    {
        #region Publics

        

        #endregion


        #region Unity Api

        private void OnTriggerEnter(Collider other)
        {
            if (((1<< other.gameObject.layer) & CameraLayer) != 0)
            {
                gestionCam._CameraRail.Priority = 0;
                gestionCam._CameraPlayer.Priority = 10;
            }
        }

        #endregion


        #region Utils

        

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates
        
        [SerializeField] private GestionCam gestionCam;
        [SerializeField] private LayerMask CameraLayer;
        
        #endregion
    }
}
