using System.Collections;
using UnityEngine;

namespace TowersNoDragons.AI
{
    public class DebuffHandler : MonoBehaviour
    {
		private Enemy currentEnemy;
		private EnemyEventHandler eventHandler;
		private float startingSpeed = 0f;
		private Coroutine current;
        private bool isSlowedByIceShards = false;

		private void Awake()
		{
			currentEnemy = GetComponent<Enemy>();
			eventHandler = GetComponent<EnemyEventHandler>();
		}


		public void SlowDown(float slowTime, float debuffPercent)
		{
			if(currentEnemy == null) { return; }
			eventHandler.OnSlowEffect(true);

			if (!isSlowedByIceShards)
			{
				current = StartCoroutine(SlowDownDebuffProcess(slowTime,debuffPercent));
			}
			else
			{
				StopCoroutine(current);
				current = StartCoroutine(SlowDownDebuffProcess(slowTime, debuffPercent));
			}
		}

		private IEnumerator SlowDownDebuffProcess(float slowTime, float debuffPercent)
		{
			startingSpeed = currentEnemy.Speed;
			float timer = 0f;
			isSlowedByIceShards = true;
			currentEnemy.Agent.speed = startingSpeed;
			currentEnemy.Agent.speed *= debuffPercent;

			while (timer < slowTime)
			{
				timer += Time.deltaTime;
				yield return null;
			}

			if(currentEnemy.Agent)
			{
				currentEnemy.Agent.speed = startingSpeed;
			}
			
			isSlowedByIceShards = false;
			eventHandler.OnSlowEffect(false);
		}


		public bool IsSlowedByIceShards { get => isSlowedByIceShards; }
	}
}


