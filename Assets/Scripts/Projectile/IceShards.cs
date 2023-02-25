using TowersNoDragons.AI;
using UnityEngine;

namespace TowersNoDragons.Projectiles
{
	public class IceShards : MonoBehaviour
	{
		[SerializeField] private float debuffPercent = 0.5f;
		[SerializeField] private float slowTime = 2f;
		[SerializeField] private float radius = 3f;
		[SerializeField] private LayerMask layerMask = new LayerMask();
		[SerializeField] private Collider[] enemyCollided;
		[SerializeField] private Transform centerOfImpact = null;


		public void CaptureEnemiesInRange()
		{
			this.ClearEnemiesArray();
			var count = Physics.OverlapSphereNonAlloc(centerOfImpact.position, radius, enemyCollided, layerMask);
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					if (enemyCollided[i] == null) { continue; }
					enemyCollided[i].GetComponent<DebuffHandler>().SlowDown(slowTime, debuffPercent);

				}
			}
		}

		//keep the collided array clean and updated
		private void ClearEnemiesArray()
		{
			for (int i = 0; i < enemyCollided.Length; i++)
			{
				enemyCollided[i] = null;
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(centerOfImpact.position, radius);
		}

	}
}


