using UnityEngine;
using TMPro;
using System;

namespace TowersNoDragons.Economy
{
    public class EconomyHandler : MonoBehaviour
    {
        public static EconomyHandler Instance;
        public event Action OnGoldChange;

        [SerializeField] private int playerGold = 1000;
        [SerializeField] private TMP_Text playerGoldTxt = null;

        private void Awake()
        {
            Instance = this;
        }

		private void Start()
		{
            RefreshGold();
        }

		private void RefreshGold()
        {
            playerGoldTxt.text = playerGold.ToString();
        }

        public void AddGold(int toAdd)
        {
            playerGold += toAdd;
            OnGoldChange?.Invoke();
            RefreshGold();
        }

        public void SubtractGold(int toReduce)
        {
            playerGold -= toReduce;
            OnGoldChange?.Invoke();
            RefreshGold();
        }

        public int GetCurrentGold()
        {
            return playerGold;
        }

    }
}

