/*
 * The arrow should know the target position at all time
 * The arrow looks at the target and translates towards it
 * The arrow detects collision either with the target or the ground
 */

using TowersNoDragons.AI;
using TowersNoDragons.AttackTypes;
using UnityEngine;

namespace TowersNoDragons.Projectiles
{
	public class Arrow : MonoBehaviour
	{
		[SerializeField] private float projectileSpeed = 10f;
		[SerializeField] private float damage = 20f;
		[SerializeField] private DamageTypes damageType = new DamageTypes();
	    
		private Transform target; 
		private const string enemyTag = "Enemy";
		
		private void Update()
		{
			if (target.GetComponent<Enemy>()== null) { Destroy(gameObject); return; }

			transform.LookAt(target.position);
			transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
		}


		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(enemyTag) && target.GetComponent<Enemy>() != null)
			{
				other.GetComponent<Enemy>().TakeDamage(damage,damageType);
				Destroy(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
				
		}

		public void AssignTarget(Transform target)
		{
			this.target = target;
		}
	}
}


