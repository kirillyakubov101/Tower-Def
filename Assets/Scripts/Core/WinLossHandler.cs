using UnityEngine;
using TMPro;

namespace TowersNoDragons.Core
{
    public class WinLossHandler : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private int lives = 10;
        [SerializeField] private int enemiesEliminated = 0;
        [SerializeField] private int totalAmountOfEnemies;
        [SerializeField] private TMP_Text livesText = null;

        public static WinLossHandler Instance = null;

		public int TotalAmountOfEnemies { get => totalAmountOfEnemies; set => totalAmountOfEnemies = value; }

		private void Awake()
		{
            if (Instance != null) { Destroy(gameObject); }
            else
            {
                Instance = this;
            }
        }

		private void Start()
		{
            RefreshLives();
        }

		//Enemy crossed the finish line
		public void EnemyDetected()
        {
            lives--;
            RefreshLives();
            enemiesEliminated++;
            ProcessWinLoss();
        }

        //Enemy died to a tower
        public void OnTowerKill()
        {
            enemiesEliminated++;
            ProcessWinLoss();
        }

        private void GameLoss()
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }

        private void GameWin()
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }

        private void RefreshLives()
        {
            livesText.text = lives.ToString();
        }

        private void ProcessWinLoss()
        {
            if (lives <= 0)
            {
                GameLoss();
            }

            else if (enemiesEliminated == totalAmountOfEnemies)
            {
                GameWin();
            }
        }
    }
}

