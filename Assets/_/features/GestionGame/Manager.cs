using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GestionGame.Runtime
{
    public class Manager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);
        }
    }
}
