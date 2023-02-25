/*
 * Script that monitors the UI for building new towers
 */
using TowersNoDragons.Towers;
using UnityEngine;
using TowersNoDragons.Economy;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TowersNoDragons.UI
{
	public class BuildHandler : MonoBehaviour,ISelectable
	{

		[SerializeField] private GameObject BuildPanel = null;
		[SerializeField] private GameObject selectionCircleSprite = null;
		[SerializeField] private Tower archerTower = null;
		[SerializeField] private Tower crystalTower = null;
		[SerializeField] private Tower mageTower = null;
		[SerializeField] private Tower goldTower = null;

		[Header("Tower Prices and buttons")]
		[SerializeField] private Button archerTowerbtn = null;
		[SerializeField] private TMP_Text archerTowerPriceTxt = null;
		[SerializeField] private Button crystalTowerbtn = null;
		[SerializeField] private TMP_Text crystalTowerPriceTxt = null;
		[SerializeField] private Button mageTowerbtn = null;
		[SerializeField] private TMP_Text mageTowerPriceTxt = null;
		[SerializeField] private Button goldTowerbtn = null;
		[SerializeField] private TMP_Text goldTowerPriceTxt = null;


		private Dictionary<Button, int> buildButtons = new Dictionary<Button, int>(); //library of towers


		private float lowest_Y_Spawn = -10f;
		private bool isBuildPanelShown = false;
		private Vector3 spawnPos;

		//Tower prices
		private int archerTowerPrice = 0;
		private int crystalTowerPrice = 0;
		private int mageTowerPrice = 0;
		private int goldTowerPrice = 0;

		private void Start()
		{
			spawnPos = transform.position;
			spawnPos.y = lowest_Y_Spawn;

			InitPrices();
		}

		private void OnEnable()
		{
			EconomyHandler.Instance.OnGoldChange += UpdateButtonState;
		}

		private void OnDisable()
		{
			EconomyHandler.Instance.OnGoldChange -= UpdateButtonState;
		}

		private void InitPrices()
		{
			//Get tower Prices
			if(archerTower != null) { archerTowerPrice = archerTower.GetTowerPrice(); }
			if(crystalTower != null) { crystalTowerPrice = crystalTower.GetTowerPrice(); }
			if(mageTower != null) { mageTowerPrice = mageTower.GetTowerPrice(); }
			if(goldTower != null) { goldTowerPrice = goldTower.GetTowerPrice(); }
			
			//update prices on the UI
			archerTowerPriceTxt.text = archerTowerPrice.ToString();
			crystalTowerPriceTxt.text = crystalTowerPrice.ToString();
			mageTowerPriceTxt.text = mageTowerPrice.ToString();
			goldTowerPriceTxt.text = goldTowerPrice.ToString();

			//add tower buttons and their prices to the library
			buildButtons.Add(archerTowerbtn, archerTowerPrice);
			buildButtons.Add(crystalTowerbtn, crystalTowerPrice);
			buildButtons.Add(mageTowerbtn, mageTowerPrice);
			buildButtons.Add(goldTowerbtn, goldTowerPrice);
		}

		private void UpdateButtonState()
		{
			foreach(var ele in buildButtons)
			{
				if(ele.Value > EconomyHandler.Instance.GetCurrentGold())
				{
					ele.Key.interactable = false;
				}
				else 
				{
					ele.Key.interactable = true;
				}
			}
		}

		private void NotifyTowerCreationSmoke(Tower newTower)
		{
			newTower.CreateSmokeEffectOnTowerBuy();
		}

		public void BuildArcherTower()
		{
            if (archerTower.GetTowerPrice() <= EconomyHandler.Instance.GetCurrentGold())
            {
				var instance = Instantiate(archerTower, spawnPos, Quaternion.identity);
				instance.GetComponent<Tower>().AssignBuildingPlace(this);
				NotifyTowerCreationSmoke(instance);
				gameObject.SetActive(false);
				EconomyHandler.Instance.SubtractGold(archerTower.GetTowerPrice());
			}
		}

		public void BuildCrystalTower()
		{
			if(crystalTower.GetTowerPrice() <= EconomyHandler.Instance.GetCurrentGold())
            {
				var instance = Instantiate(crystalTower, spawnPos, Quaternion.identity);
				instance.GetComponent<Tower>().AssignBuildingPlace(this);
				NotifyTowerCreationSmoke(instance);
				gameObject.SetActive(false);
				EconomyHandler.Instance.SubtractGold(crystalTower.GetTowerPrice());
			}
		}

		public void BuildMageTower()
		{
			if (mageTower.GetTowerPrice() <= EconomyHandler.Instance.GetCurrentGold())
			{
				var instance = Instantiate(mageTower, spawnPos, Quaternion.identity);
				instance.GetComponent<Tower>().AssignBuildingPlace(this);
				NotifyTowerCreationSmoke(instance);
				gameObject.SetActive(false);
				EconomyHandler.Instance.SubtractGold(mageTower.GetTowerPrice());
			}
		}

		public void BuildGoldTower()
		{
			if (goldTower.GetTowerPrice() <= EconomyHandler.Instance.GetCurrentGold())
			{
				var instance = Instantiate(goldTower, spawnPos, Quaternion.identity);
				instance.GetComponent<Tower>().AssignBuildingPlace(this);
				NotifyTowerCreationSmoke(instance);
				gameObject.SetActive(false);
				EconomyHandler.Instance.SubtractGold(goldTower.GetTowerPrice());
			}
		}


		public void Select()
		{
			if (isBuildPanelShown) { return; }
			isBuildPanelShown = true;
			BuildPanel.SetActive(true);
			selectionCircleSprite.SetActive(true);
		}

		public void Deselect()
		{
			if (!isBuildPanelShown) { return; }
			isBuildPanelShown = false; ;
			BuildPanel.SetActive(false);
			selectionCircleSprite.SetActive(false);
		}
	}
}


