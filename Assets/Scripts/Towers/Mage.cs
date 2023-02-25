using TowersNoDragons.AI;
using TowersNoDragons.Projectiles;
using UnityEngine;

namespace TowersNoDragons.Towers
{
    public class Mage : MonoBehaviour
    {
		[Tooltip("How fast the mage rotates towards the enemy")]
		[SerializeField] private float rotationSpeed = 2f;
		[SerializeField] private ParticleSystem iceShardsVFX = null;

		//animation
		private Animator animator;
		private Enemy target = null;
		private Vector3 targetDirection = new Vector3();
		private Quaternion rotationTarget = new Quaternion();
		private Vector3 spawnVector;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (this.target == null) { return; }

			RotateTowardsTarget();
		}

		public void Attack(Enemy target)
		{
			this.target = target;
			animator.SetTrigger("attack");
		}

		public void StopAttacking()
		{
			this.target = null;
		}

		private void RotateTowardsTarget()
		{
			targetDirection = target.transform.position - transform.position;
			targetDirection.y = 0f;
			rotationTarget = Quaternion.LookRotation(targetDirection);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
		}

		//Animation Event
		private void AttackTarget()
		{
			if (target == null) { return; }
			spawnVector = target.transform.position;
			spawnVector.y = 14f;

			iceShardsVFX.transform.position = spawnVector;
			iceShardsVFX.Stop();
			iceShardsVFX.Play();
			iceShardsVFX.GetComponent<IceShards>().CaptureEnemiesInRange();

			target = null;
		}

	}
}


