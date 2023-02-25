using UnityEngine;
using TowersNoDragons.Input;


namespace TowersNoDragons.UI
{
    public class PauseHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        private bool gameIsPaused = false;

        private void OnEnable()
        {
            InputController.Instance.OnPause += GamePause;
        }

        private void OnDisable()
        {
            InputController.Instance.OnPause -= GamePause;
        }

        private void GamePause()
        {

            if (!gameIsPaused)
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                gameIsPaused = true;
            }

            else if (gameIsPaused)
            {
                ContinueButton();
            }
        }

        public void ContinueButton()
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            gameIsPaused = false;
        }
    }
}