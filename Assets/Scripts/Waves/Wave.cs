/*
 * Data script for the dynamic wave that can spawn different Enemy type objects in any amount and order of spawn
 */

using TowersNoDragons.AI;
using UnityEngine;

namespace TowersNoDragons.Waves
{
	[CreateAssetMenu(fileName = "Data", menuName = "Waves/New Wave")]
	public class Wave : ScriptableObject
	{
		[SerializeField] private EnemiesToSpawn[] enemiesToSpawn;

		private int totalAmount = 0;
		public int TotalAmount { get => totalAmount; }

        [System.Serializable]
		public class EnemiesToSpawn
		{
			[SerializeField] private Enemy enemyPrefab = null;
			[SerializeField] private int amountToSpawn;

            public int AmountToSpawn { get => amountToSpawn;}


			public Enemy GetEnemy()
			{
				return enemyPrefab;
			}
		}

		public EnemiesToSpawn[] GetEnemiesToSpawns()
		{
			return enemiesToSpawn;
		}

		private void CalculateTotalAmount()
        {
			totalAmount = 0;
			foreach(var ele in enemiesToSpawn)
            {
				totalAmount += ele.AmountToSpawn;
            }
        }

        private void OnEnable()
        {
			CalculateTotalAmount();
        }
    }

}

