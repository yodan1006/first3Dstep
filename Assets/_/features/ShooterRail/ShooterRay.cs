using System;
using UnityEngine;

namespace ShooterRail.Runtime
{
    public class ShooterRay : MonoBehaviour
    {
        [SerializeField] Camera cam;
        
        private void Start()
        {
            cam = Camera.main;
        }
        
        
        
    }
}
