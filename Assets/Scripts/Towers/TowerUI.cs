using UnityEngine;
using TowersNoDragons.Towers;
using TowersNoDragons.Economy;
using TMPro;
using UnityEngine.UI;

namespace TowersNoDragons.UI
{
	public class TowerUI : MonoBehaviour, ISelectable
	{
		[SerializeField] private GameObject selectionCircleSprite = null;
		[SerializeField] private GameObject SellUpgradePanel = null;
		[SerializeField] private TMP_Text towerLvTxt = null;
		[SerializeField] private Button upgradeButton = null;
		[SerializeField] private TMP_Text upgradePriceTxt = null;

		private bool isPanelShown = false;
		private Tower currentTower;
		private const string levelTxtIntro = "Lv: ";

		private void Awake()
		{
			currentTower = GetComponentInParent<Tower>();
		}

		private void Start()
		{
			UpdateTowerLevelTxt();
			upgradePriceTxt.text = this.currentTower.GetUpgradePrice().ToString();
		}

		private void UpdateTowerLevelTxt()
		{
			if (currentTower == null) { print("Error, no tower!"); return; }
			if (currentTower.GetTowerLv() == Tower.TOWER_MAX_LEVEL) // max level tower reached
			{
				Destroy(upgradeButton.gameObject);
			}

			towerLvTxt.text = levelTxtIntro + currentTower.GetTowerLv();
		}

		public void Deselect()
		{
			if (!isPanelShown) { return; }
			isPanelShown = false;
			SellUpgradePanel.SetActive(false);
			selectionCircleSprite.SetActive(false);
		}

		public void Select()
		{
			if (isPanelShown) { return; }
			isPanelShown = true;
			SellUpgradePanel.SetActive(true);
			selectionCircleSprite.SetActive(true);
		}

		public void SellTower()
		{
			currentTower.BuildingBase.gameObject.SetActive(true);
			this.Deselect();
			EconomyHandler.Instance.AddGold(currentTower.GetTowerPrice());
			currentTower.CreateSmokeEffectOnTowerSell();
			Destroy(currentTower.gameObject);
		}

		public void UpgradeTower()
		{
			if(currentTower == null) { print("Error, no tower!"); return; }

			if (currentTower.GetUpgradePrice() <= EconomyHandler.Instance.GetCurrentGold())
			{
				EconomyHandler.Instance.SubtractGold(currentTower.GetUpgradePrice());
				currentTower.UpgradeTower();
				this.UpdateTowerLevelTxt();
			}

		
		}
	}
}


