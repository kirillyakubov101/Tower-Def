/*
 * Center script for all of the events that will be triggered by the enemy's state
 */


using UnityEngine;
using UnityEngine.Events;
using TowersNoDragons.Core;
using TowersNoDragons.Economy;

namespace TowersNoDragons.AI
{
	public class EnemyEventHandler : MonoBehaviour
	{
		[SerializeField] private EnemyEvent OnTakeDamage = null;
		[SerializeField] private EnemyUiEvent OnUiUpdate = null;
		[SerializeField] private EnemyDeathEvent OnDeath = null;
		[SerializeField] private EnemySlowEventDebuff OnSlow = null;

		#region UnityEvents_Classes_Definition

		[System.Serializable]
		public class EnemyEvent : UnityEvent<float,float>
		{
		}

		[System.Serializable]
		public class EnemyUiEvent : UnityEvent<float>
		{
		}

		[System.Serializable]
		public class EnemyDeathEvent: UnityEvent<string>
		{
		}

		[System.Serializable]
		public class EnemySlowEventDebuff: UnityEvent<bool>
		{

		}
		#endregion

		public void OnTakeDamageEvent(float amount, float damagePercent)
		{
			OnTakeDamage.Invoke(amount, damagePercent);
		}


		public void OnDeathEvent(int bountyAmount)
		{
			int randomDeathIndex = Random.Range(1,3);
			OnDeath.Invoke("Death " + randomDeathIndex); //notify the visual indicator and animation about the death of the enemy
			WinLossHandler.Instance.OnTowerKill(); //notify the manager
			EconomyHandler.Instance.AddGold(bountyAmount); // notify the Economy handler of the bounty to add
		}

		public void OnSlowEffect(bool state)
		{
			OnSlow?.Invoke(state);
		}
		

		public void OnUI_HP_Update(float amount)
		{
			OnUiUpdate.Invoke(amount);
		}
	}
}


