using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WalkEnemy.Runtime
{
    public class LoseTrigger : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
