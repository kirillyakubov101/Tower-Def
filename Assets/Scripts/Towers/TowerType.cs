using UnityEngine;

namespace TowersNoDragons.TowerTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "Towers/New Tower")]
    public class TowerType : ScriptableObject
    {
        [SerializeField] private float towerRange = 5f; //tower range
        [SerializeField] private int towerPrice = 100; //gold
        [SerializeField] private float attackDelay = 2f; //attack cooldown
        [Header("LEVEL 2 UPGRADES")]
        [SerializeField] private int upgradePrice = 100;

        //Getters
        public float GetTowerRange()
		{
            return towerRange;
		}

        public int GetTowerPrice()
        {
            return towerPrice;
        }

        public float GetAttackDelay()
        {
            return attackDelay;
        }

        public int GetUpgradePrice()
		{
            return upgradePrice;
        }
    }
}


