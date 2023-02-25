using UnityEngine;
using TowersNoDragons.Input;

//CameraController
namespace TowersNoDragons.Cameras
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private float cameraMoveSpeed = 10f;
		[SerializeField] private float scrollSpeed = 10f;
		[SerializeField] private Transform cameraTransform = null;

		[Header("Movement Bounds")]
		[SerializeField] private float max_X = 500f;
		[SerializeField] private float min_X = -500f;
		[SerializeField] private float max_Z = 500f;
		[SerializeField] private float min_Z = 100f;

		[Header("Scroll Bounds")]
		[SerializeField] private float maxScrollDown = 100f;
		[SerializeField] private float maxScrollUp = 500f;

		private InputController InputController;
		private float horizontal, vertical;

		//Camera Rig
		private Vector3 direction_x = new Vector3();
		private Vector3 direction_y = new Vector3();
		private Vector3 newPosition = new Vector3();

		//Main Camera
		private Vector3 newScrollPos = new Vector3();

		//Zoom percentage
		private float zoomRatio = 0f;
		public float ZoomRatio { get => zoomRatio; }

		private void Awake()
		{
			newScrollPos = cameraTransform.localPosition;
		}

		private void Start()
		{
			InputController = InputController.Instance;
		}


		private void Update()
		{
			//Movement - Camera Rig
			ProcessInput();

			//clamp camera movement to bounds
			ClampPosition();

			//Scroll and Zoom
			ProcessScrollAmount();

		}

		private void ProcessScrollAmount()
		{
			newScrollPos.z = newScrollPos.y * -1;

			//Ratio of the scroll for the enemy UI
			zoomRatio = (newScrollPos.y - maxScrollDown) / (maxScrollUp - maxScrollDown);
		}

		private void ClampPosition()
		{
			newPosition.x = Mathf.Clamp(newPosition.x, min_X, max_X);
			newPosition.z = Mathf.Clamp(newPosition.z, min_Z, max_Z);

			//clamp camera zoom to bounds
			newScrollPos.y = Mathf.Clamp(newScrollPos.y, maxScrollDown, maxScrollUp);
		}

		private void ProcessInput()
		{
			horizontal = InputController.GetHorizontalDirection();
			vertical = InputController.GetVerticalDirection();

			direction_x = horizontal * transform.right * cameraMoveSpeed * Time.deltaTime;
			direction_y = vertical * transform.forward * cameraMoveSpeed * Time.deltaTime;

			newPosition = direction_x + direction_y + transform.position;

			//Scroll - Main Camera
			newScrollPos.y += scrollSpeed * Time.deltaTime * InputController.GetScrollDirection();
		}

		private void LateUpdate()
		{
			transform.position = newPosition;
			cameraTransform.localPosition = newScrollPos;
		}
	}
}

