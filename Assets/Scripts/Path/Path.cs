/*
 * Script to store all the waypoints for the path
 * and return it when WAVE asks to assign a path for each spawned Enemy
 */
using UnityEngine;

namespace TowersNoDragons.Pathing
{
	public class Path : MonoBehaviour
	{
		[SerializeField] private Transform[] waypoints = null;

		public Transform[] GetPath()
		{
			return waypoints;
		}
	}
}


