using TowersNoDragons.AI;
using TowersNoDragons.AttackTypes;
using UnityEngine;

namespace TowersNoDragons.Projectiles
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float damage = 20f;
        [SerializeField] private float projectileSpeed = 50f;
        [SerializeField] private DamageTypes damageType = DamageTypes.Magical;
        [SerializeField] private Rigidbody rigid = null;
        //[SerializeField] private Transform target; //TODO: Remove serialzed field after testing.
        private Vector3 targetLastpos;
        private Vector3 targetDirection;

        private const string enemyTag = "Enemy";

        private void Start()
        {
            targetDirection = targetLastpos - transform.position;
            targetDirection.y += 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(enemyTag))
            {
                other.GetComponent<Enemy>().TakeDamage(damage,damageType);
                print("I attack");
                Destroy(gameObject);
            }
            else
            {
                print("Bruh");
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            
            rigid.AddForce(projectileSpeed * targetDirection);
        }

        public void SetTargetPosition(Vector3 newpos)
        {
            targetLastpos = newpos;
        }

    }
}