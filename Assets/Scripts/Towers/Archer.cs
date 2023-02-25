using TowersNoDragons.AI;
using TowersNoDragons.Projectiles;
using UnityEngine;

namespace TowersNoDragons.Towers
{
	public class Archer : MonoBehaviour
	{
		[Tooltip("How fast the archer rotates towards the enemy")]
		[SerializeField] private float rotationSpeed = 2f;
		[SerializeField] private Arrow projectilePrefab = null;
		[SerializeField] private Transform shootingPoint = null;
		[SerializeField] private AudioSource audioSource = null;

		private Enemy target = null;
		private Vector3 targetDirection = new Vector3();
		private Quaternion rotationTarget = new Quaternion();

		//animation
		private Animator animator;

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
			if(target == null) { return; }
			if (audioSource != null) { audioSource.Play(); }
			
			var instance = Instantiate(projectilePrefab, shootingPoint); //create an arrow
			instance.AssignTarget(target.transform);
			instance.transform.parent = null;
		}

		//make room for a new archer when upgraded
		public void MoveExistingArcher()
		{
			Vector3 makeRoomOffset = transform.position;
			makeRoomOffset.x -= 1f;
			transform.position = makeRoomOffset;
		}


	}
}


