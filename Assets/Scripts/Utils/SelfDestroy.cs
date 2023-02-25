using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem mainParticleSystem = null;

	private float waitTime;

	private void Awake()
	{
		waitTime = mainParticleSystem.main.duration;
	}

	private void Start()
	{
		Destroy(gameObject, waitTime);
	}
}
