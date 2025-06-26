using System;
using Unity.Cinemachine;
using UnityEngine;

namespace GestionCam.Runtime
{
    public class GestionCam : MonoBehaviour
    {
        #region Publics

        

        #endregion


        #region Unity Api

        private void Start()
        {
            _CameraRail.Priority = 10;
            _CameraPlayer.Priority = 5;
        }

        #endregion


        #region Utils

        

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates

        [SerializeField] public CinemachineCamera _CameraRail;
        [SerializeField] public CinemachineCamera _CameraPlayer;

        #endregion
    }
}
