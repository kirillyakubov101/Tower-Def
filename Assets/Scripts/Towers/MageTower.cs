using UnityEngine;

namespace TowersNoDragons.Towers
{
	public class MageTower : Tower
	{
		[SerializeField] private Mage mage = null;


		protected override void AttackTarget()
		{
			mage.Attack(base.target);
		}

		protected override void StopAttacking()
		{
			mage.StopAttacking();
		}
	}
}


