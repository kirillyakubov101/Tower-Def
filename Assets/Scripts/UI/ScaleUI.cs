/*
 * Script to scale the HP bars based on the scroll ratio
 */
using TowersNoDragons.Cameras;
using UnityEngine;

namespace TowersNoDragons.UI
{
	public class ScaleUI : MonoBehaviour
	{
		[SerializeField] private float maxRectWidth = 4f;
		[SerializeField] private float minRectWidth = 1.5f;
		[SerializeField] private float maxRectHeight = 1.2f;
		[SerializeField] private float minRectHeight = 0.9f;

		private Vector2 rectStats = Vector2.zero;
		CameraController cameraController;
		RectTransform rectTransform;

		private void Awake()
		{
			cameraController = FindObjectOfType<CameraController>();
			rectTransform = GetComponent<RectTransform>();
		}

		private void LateUpdate()
		{
			DynamiclyScaleHPbar();
		}

		/// <summary>
		/// Scales the UI based on the scroll amount
		/// </summary>
		private void DynamiclyScaleHPbar()
		{
			rectStats.x = cameraController.ZoomRatio * (maxRectWidth - minRectWidth) + minRectWidth;
			rectStats.y = cameraController.ZoomRatio * (maxRectHeight - minRectHeight) + minRectHeight;

			rectTransform.sizeDelta = rectStats;
		}

	}
}


