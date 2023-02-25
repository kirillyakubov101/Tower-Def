/*
 * The Crystal Tower renderes a line from its origin to the target position.
 * Due to the cooldown of shooting for every tower OnUpdate, the cooldown for attacks of the Crystal Tower
 * Should always be set to 0!!!
 */

using TowersNoDragons.AttackTypes;
using UnityEngine;

namespace TowersNoDragons.Towers
{
	public class CrystalTower : Tower
	{
		[Header("Crystal Tower Prefs")]
		[SerializeField] private MeshRenderer crystalTowerMeshRenderer = null;
		[SerializeField] private LineRenderer lineRenderer = null;
		[SerializeField] private float damage = 3f;
		[SerializeField] private DamageTypes damageType = DamageTypes.Magical;
		[SerializeField] private AudioSource audioSource = null;

		[Header("Upgrades")]
		[SerializeField] private Material levelTwoLaserColor = null;
		[SerializeField] private float levelTwoDamage = 1f;
		
		[Header("VFX")]
		[SerializeField] private ParticleSystem hitEffectVFX = null;
		[SerializeField] private ParticleSystemRenderer hitEffectVFXRenderer = null;

		protected override void AttackTarget()
		{
			if(base.target == null) { StopAttacking(); }
			if(audioSource != null && !audioSource.isPlaying)
			{
				audioSource.Play();
			}
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0, lineRenderer.transform.position); //start pos
			lineRenderer.SetPosition(1, new Vector3(base.target.transform.position.x, (base.target.transform.position.y + base.enemyOffset), base.target.transform.position.z)); //target pos /end pos
			hitEffectVFX.transform.position = new Vector3(base.target.transform.position.x,(base.target.transform.position.y + base.enemyOffset), base.target.transform.position.z);
			if (!hitEffectVFX.isEmitting) { hitEffectVFX.Play(); }
			
			base.target.TakeDamage(damage * Time.deltaTime, damageType);
		}

		protected override void StopAttacking()
		{
			lineRenderer.enabled = false;
		}

		public override void UpgradeTower()
		{
			base.UpgradeTower();

			//Crytal tower implementation
			this.damage += levelTwoDamage;
			lineRenderer.material = levelTwoLaserColor;
			var newMats = crystalTowerMeshRenderer.materials; //Create and assign a new array of materials
			newMats[4] = levelTwoLaserColor;				  //the color of the crystal
			crystalTowerMeshRenderer.materials = newMats;     //Create and assign a new array of materials
			hitEffectVFXRenderer.material = levelTwoLaserColor;
		}
	}
}


