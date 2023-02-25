using UnityEngine;

namespace TowersNoDragons.AI
{
	public class Vampire : Enemy
	{
		[SerializeField] private float hpRegen = 5f;
		[SerializeField] private ParticleSystem regenVFX = null;

		private void Update()
		{
			RegenVampire();
		}

		private void RegenVampire()
		{
			base.hp += hpRegen;
			base.hp = Mathf.Clamp(base.hp, 0f, base.maxHp);
			base.eventHandler.OnUI_HP_Update(base.hp / base.maxHp);
			
			if(base.hp < base.maxHp)
			{
				if(!regenVFX.isPlaying)
				{
					regenVFX.Play();
				}
			}
			else
			{
				regenVFX.Stop();
			}
		}

		public override string ToString()
		{
			return "Physical";
		}
	}
}

