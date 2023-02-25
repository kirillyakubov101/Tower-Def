/*
 * Main scipt for all the enemies.
 * Assigning basic stats, getting path and taking damage when hit.
 * Sends the event forward to the central event system.
 */

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TowersNoDragons.EnemyTypes;
using TowersNoDragons.AttackTypes;

namespace TowersNoDragons.AI
{
	public abstract class Enemy : MonoBehaviour
	{
		[SerializeField] private EnemyType enemyType = null;
		[SerializeField] private GameObject enemyBodyModel = null;

		private Transform[] path = null;

		private int currentPathIndex = 0;
		private NavMeshAgent agent = null;
		protected EnemyEventHandler eventHandler;

		//stats
		private float baseArmor;
		private float magicResistance;
		private float speed;
		protected float hp;
		protected float maxHp;

		//For debuffs
		public NavMeshAgent Agent { get => agent; }
		public float Speed { get => speed;  }

		//cache
		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
			eventHandler = GetComponent<EnemyEventHandler>();
		}

		private void Start()
		{
			IntializeStats();
		}

		//Get the designated path 
		public void AssignPath(Transform[] path)
		{
			this.path = path;
			if(this.path == null) { return; }

			StartCoroutine(PathProcess());

		}

		//getting all the waypoint and moving dynamicly towards every single one of them.
		private IEnumerator PathProcess()
		{
			bool reachedWayPoint = true;

			while(currentPathIndex < path.Length)
			{
				if(reachedWayPoint)
				{
					agent.SetDestination(this.path[currentPathIndex].position);
					reachedWayPoint = false;
				}
				else
				{
					if (agent.isOnNavMesh && agent.remainingDistance <= 0.2f)
					{
						reachedWayPoint = true;
						currentPathIndex++;
					}
				}
				yield return null;
			}
		}

		//get all the stats from the enemyType scriptable object
		private void IntializeStats()
		{
			this.agent.speed = this.enemyType.MovementSpeed;
			this.speed = this.enemyType.MovementSpeed;
			this.baseArmor = this.enemyType.BaseArmor;
			this.magicResistance = this.enemyType.MagicResistance;
			this.hp = this.enemyType.Hp;
			this.maxHp = this.hp;
		}

		/// <summary>
		/// Main function to take damage, calculate the final damage based on the armor type and attack type
		/// Send the final hp value to Health script through the central event system.
		/// </summary>
		public void TakeDamage(float damage, DamageTypes damageType)
		{
			float finalDamage;

			switch(damageType)
			{
				case DamageTypes.Magical:
					finalDamage = damage - (damage * this.magicResistance);
					break;

				case DamageTypes.Physical:
					finalDamage = damage - (damage * this.baseArmor);
					break;

				default:
					finalDamage = 0;
					break;
			}

			this.hp -= finalDamage;

			float percentDamage = this.hp / this.enemyType.Hp;

			eventHandler.OnTakeDamageEvent(amount: this.hp,damagePercent: percentDamage);
		}

		public int GetBounty()
        {
			return enemyType.Bounty;
        }

		public void StopAgent()
		{
			agent.isStopped = true;
			enemyBodyModel.layer = 0; //stop seeing enemies behind object (dead enemies underground)
			Destroy(agent);
			Destroy(this);
		}
	}
}


