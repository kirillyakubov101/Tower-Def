/*
 * The arher tower is just a stand for the actual archer.
 */

using UnityEngine;

namespace TowersNoDragons.Towers
{
	public class ArcherTower : Tower
	{
		[SerializeField] private Archer[] archers = null;

		protected override void AttackTarget()
		{
			//tell archer to rotate towards the target and assign a target
			foreach(var ele in archers)
			{
				if (!ele.gameObject.activeSelf) { continue; }
				ele.Attack(target);
			}
			
		}

		protected override void StopAttacking()
		{
			foreach (var ele in archers)
			{
				if (!ele.gameObject.activeSelf) { continue; }
				ele.StopAttacking();
			}
		}

		public override void UpgradeTower()
		{
			base.UpgradeTower();
			archers[0].MoveExistingArcher(); //make room for a new archer
			archers[1].gameObject.SetActive(true); //second archer
			StopAttacking();
		}
	}
}

