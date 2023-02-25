using System.Collections;
using TowersNoDragons.Pathing;
using TowersNoDragons.Waves;
using UnityEngine;
using TowersNoDragons.Core;
using System.Collections.Generic;

namespace TowersNoDragons.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Wave[] waves = null;
        [SerializeField] private Path path = null; //should choose a path closest to the spawner
        [SerializeField] private float spawnCooldown = 1f;

        private const float MIN_SPAWN_TIME = 0.5f;
        private const float SPAWN_TIME_INCREMENT = 0.005f;

        private Wave.EnemiesToSpawn[] EnemiesToSpawns;
        private int currentWaveIndex = 0;
        private int maxWaveIndex;
        private WavesManager wavesManager = null;
        private Dictionary<string, int> upcomingWave = new Dictionary<string, int>(); 
		

		private void Start()
        {
            wavesManager = WavesManager.Instance;
            this.FillUpcomingEnemiesInfo();
            maxWaveIndex = waves.Length; //get waves amount
            this.TotalEnemiesCount(); //win loss handler notification
           
        }


		private void FillUpcomingEnemiesInfo()
		{
            upcomingWave.Clear();

            foreach (var ele in waves[currentWaveIndex].GetEnemiesToSpawns())
			{
                string enemyName = ele.GetEnemy().ToString();

                if (upcomingWave.ContainsKey(enemyName))
				{
                    upcomingWave[enemyName] += ele.AmountToSpawn;
                }
				else
				{
                    upcomingWave.Add(enemyName, ele.AmountToSpawn);
                }
            }

            wavesManager.PrintUpcomingWaveInfo(upcomingWave);
        }

        public void SpawnEnemy()
        {
            if(currentWaveIndex >= maxWaveIndex) { return; } //max wave reached
           
            wavesManager.PauseSpawning(); //stop spawning and displaying timer
            EnemiesToSpawns = waves[currentWaveIndex].GetEnemiesToSpawns(); //get the enemies to spawn from the current wave index
            StartCoroutine(SpawnProcess());
        }

        private IEnumerator SpawnProcess()
		{
            int count = 0;
            while(count < EnemiesToSpawns.Length)
			{
                yield return SpawnEnemyType(EnemiesToSpawns[count]);
                count++;
			}

            this.currentWaveIndex++;
            wavesManager.KeepSpawning(); //wave is done spawning, restart the timer count
         
            if(this.currentWaveIndex != maxWaveIndex)
			{
                this.FillUpcomingEnemiesInfo();
            }
            

        }

        private IEnumerator SpawnEnemyType(Wave.EnemiesToSpawn enemiesToSpawn)
		{
            int enemiesSpawned = 0;
            
            while (enemiesSpawned < enemiesToSpawn.AmountToSpawn)
            {
                var instance = Instantiate(enemiesToSpawn.GetEnemy(), transform.position,Quaternion.identity);
                instance.AssignPath(this.path.GetPath());
                enemiesSpawned++;

                spawnCooldown -= SPAWN_TIME_INCREMENT;
                spawnCooldown = Mathf.Clamp(spawnCooldown, MIN_SPAWN_TIME, spawnCooldown);

                yield return new WaitForSeconds(spawnCooldown);
            }
        }

		private void TotalEnemiesCount()
		{
			foreach(var ele in waves)
			{
                foreach(var element in ele.GetEnemiesToSpawns())
				{
                    WinLossHandler.Instance.TotalAmountOfEnemies += element.AmountToSpawn;
                }
            }
		}

	}
}


