using UnityEngine;
using UnityEngine.UI;

namespace TowersNoDragons.UI
{
    public class UpdateHealthUI : MonoBehaviour
    {
         [SerializeField] private GameObject HP_Container = null;
         [SerializeField] private Image hpBarImage = null;

        public void Update_HP_UI(float amount)
	    {
            hpBarImage.fillAmount = amount;
        }

        public void Show_HP_UI()
	    {
            HP_Container.SetActive(true);
        }
    }
}


