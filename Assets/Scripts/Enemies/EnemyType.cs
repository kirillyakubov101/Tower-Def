using UnityEngine;

namespace TowersNoDragons.EnemyTypes
{
	[CreateAssetMenu(fileName = "Data", menuName = "Enemies/New Enemy")]
	public class EnemyType : ScriptableObject
	{
		[SerializeField] private float movementSpeed = 25f; //nav-agent speed
		[SerializeField] private float hp;
		[SerializeField] private int bounty = 50;

		[Range(0f, 1f)][SerializeField] private float baseArmor;
		[Range(0f,1f)][SerializeField] private float magicResistance;

		public float MovementSpeed { get => movementSpeed; }
		public float Hp { get => hp; }
		public float BaseArmor { get => baseArmor;  }
		public float MagicResistance { get => magicResistance;  }
        public int Bounty { get => bounty;}
    }
}


