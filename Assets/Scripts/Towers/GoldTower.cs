using UnityEngine;
using UnityEngine.UI;
using TowersNoDragons.Economy;

namespace TowersNoDragons.Towers
{
	public class GoldTower : Tower
	{
		
        [SerializeField] private int goldAmount = 500;
		[SerializeField] private Image collectGoldBtn = null;
		[SerializeField] private float goldGenerateTimer = 10f;
		
        private float timer = 0f;


		private void Awake()
		{
			base.isGoldTower = true;
		}

		public void GoldToAdd()
        {
            EconomyHandler.Instance.AddGold(goldAmount);
			timer = 0f;
			collectGoldBtn.gameObject.SetActive(false);

		}

        private void Update()
        {
            timer += Time.deltaTime;
			Mathf.Clamp(timer, 0f, goldGenerateTimer);
            if (timer >= goldGenerateTimer)
            {
				collectGoldBtn.gameObject.SetActive(true);
			}
        }

		protected override void AttackTarget()
		{
			//
		}

		protected override void StopAttacking()
		{
			//
		}

		public override void UpgradeTower()
		{
			base.UpgradeTower();
			goldAmount += 50;
		}
	}
}


