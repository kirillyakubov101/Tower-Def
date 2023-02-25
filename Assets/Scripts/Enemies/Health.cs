/*The main logic for the hp logic of every enemy.
 * The final health points are sent here to decide the faith of the enemy
 * and update its UI accordingly
 */

using UnityEngine;

namespace TowersNoDragons.AI
{
	public class Health : MonoBehaviour
	{
		private EnemyEventHandler eventHandler;
		private Enemy currentEnemy;
		private bool isDead = false;	

		private void Awake()
		{
			eventHandler = GetComponent<EnemyEventHandler>();
			currentEnemy = GetComponent<Enemy>();
		}

		//Event notification for the UI
		public void UpdateHealth(float newHealth, float percentDamage)
		{
			eventHandler.OnUI_HP_Update(percentDamage);

			if (newHealth <= 0 && !isDead)
			{
				isDead = true;
				eventHandler.OnDeathEvent(currentEnemy.GetBounty());
			}

		}


	}
}

