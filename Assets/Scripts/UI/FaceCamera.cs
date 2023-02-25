/*
 * Basic script to make UI elements face the camera at all times
 */
using UnityEngine;

namespace TowersNoDragons.UI
{
	public class FaceCamera : MonoBehaviour
	{
		private void LateUpdate()
		{
			transform.LookAt(
				transform.position + Camera.main.transform.rotation * Vector3.forward,
				Camera.main.transform.rotation * Vector3.up
				);
		}
	}

	 
}


