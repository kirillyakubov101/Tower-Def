/*
 * 
 * A script to clean a dead enemy but not remove it
 * all right away
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TowersNoDragons.AI
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private float vanishSpeed = 2f;
        [SerializeField] private Collider enemyCollider = null;

        private Enemy enemyScript = null;
        private EnemyEventHandler eventHandler;
       

		private void Awake()
		{
            enemyScript = GetComponent<Enemy>();
            eventHandler = GetComponent<EnemyEventHandler>();

        }

        public void EnemyDeathProcess()
		{
            Destroy(enemyCollider);
            enemyScript.StopAgent();
            StartCoroutine(ProcessDeath());

        }

        private IEnumerator ProcessDeath()
		{
           float timer = 0f;
           yield return new WaitForSeconds(2.5f);
           while(timer < 4f)
		   {
                transform.Translate(Vector3.down * vanishSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
		   }

            Destroy(gameObject);
        }
	}
}


