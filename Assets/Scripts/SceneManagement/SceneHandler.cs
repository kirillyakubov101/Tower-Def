using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowersNoDragons.SceneManagement
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject firstMessagePanel;
        [SerializeField] [Range(0, 1)] private int setTimeScaleOnStart;

        private int currentSceneIndex;

        void Start()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Time.timeScale = setTimeScaleOnStart;

            if(firstMessagePanel != null) { firstMessagePanel.SetActive(true); }
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResumeTime()
        {
            Time.timeScale = 1;
        }

        public void PauseTime()
        {
            Time.timeScale = 0;
        }
    }
}

