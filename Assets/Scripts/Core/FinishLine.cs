using UnityEngine;
using UnityEngine.Events;

namespace TowersNoDragons.Core
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCrossed;

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            onCrossed.Invoke();
        }
    }
}

