using UnityEngine;
using TowersNoDragons.Input;
using UnityEngine.EventSystems;
using TowersNoDragons.UI;

namespace TowersNoDragons.Core
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = new LayerMask(); //ground + selectables
        [SerializeField] Texture2D buildTowerCursorTexture = null;
        [SerializeField] Texture2D defaultCursorTexture = null;
 
        private ISelectable selectedGameObject = null;
        private Ray ray;
        private RaycastHit hit;
        private bool hitGround = false;
        private bool isDefaultCursor = true;



        private void Update()
		{
            if (EventSystem.current.IsPointerOverGameObject()) { return; } //over UI element

            //Mouse is over the tower and te user clicks on it
            if (selectedGameObject != null && InputController.Instance.MouseClick() && selectedGameObject is TowerUI && !hitGround)
            {
                selectedGameObject.Select();
            }

            //if the mouse points at the ground and is clicked
            else if (InputController.Instance.MouseClick() && hitGround)
            {
                if (selectedGameObject != null)
				{
                    selectedGameObject.Deselect();
                    
                }

                selectedGameObject = null;
                
            }

			
		}

		private void FixedUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; } //over UI element

            ray = Camera.main.ScreenPointToRay(InputController.Instance.GetMousePosition());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
				//if the mouse points at the ground
				if (hit.collider.CompareTag("Ground"))
				{
                    if(!isDefaultCursor)
					{
                        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
                        isDefaultCursor = true;
                    }
                    hitGround = true;
					return;
				}

                ISelectable hitObject = hit.collider.gameObject.GetComponent<ISelectable>();

                 //Mouse is over the same selected object
                if (selectedGameObject == hitObject) { return; }

                else if (selectedGameObject == null)
                {
                    selectedGameObject = hitObject;

                    if (!(hitObject is TowerUI))
					{
                        selectedGameObject = hitObject;
                        selectedGameObject.Select();
                    }
                }

                else if (selectedGameObject!= null)
				{
                    selectedGameObject.Deselect();
                    selectedGameObject = hitObject;
                    if (!(hitObject is TowerUI))
					{
                        selectedGameObject.Select();
                    }
                }

                //if the mouse points at any Selectable object and not at the ground
                hitGround = false;

                if (isDefaultCursor)
                {
                    Cursor.SetCursor(buildTowerCursorTexture, Vector2.zero, CursorMode.Auto);
                    isDefaultCursor = false;
                }

            }
        }

       
	}
}


