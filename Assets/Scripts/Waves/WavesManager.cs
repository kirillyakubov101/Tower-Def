using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace TowersNoDragons.Waves
{
	public class WavesManager : MonoBehaviour
	{
		[SerializeField] private PlayableDirector playableDirector = null;
		[SerializeField] private float startTime = 15f; //seconds IT MUST BE THE SAME AS THE SIGNAL EMITTER!
		[SerializeField] private float defaultTime = 10f; //seconds IT MUST BE THE SAME AS THE SIGNAL EMITTER!
		[SerializeField] private Slider waveSlider = null;

		[SerializeField] private TMP_Text textAmountPrefab = null;
		[SerializeField] private GameObject textContainer = null;

		public static WavesManager Instance = null;

		private Coroutine current;

		private void Awake()
		{
			if (Instance != null) { Destroy(gameObject); }
			else
			{
				Instance = this;
			}
		}

		private void Start()
		{
			current = StartCoroutine(Timer(startTime));
		}

		public void PrintUpcomingWaveInfo(Dictionary<string,int> upcomingWave)
		{
			textContainer.SetActive(true); //show panel
			foreach (var ele in upcomingWave)
			{
				var instance = Instantiate(textAmountPrefab, textContainer.transform);
				instance.transform.SetParent(textContainer.transform);
				instance.text = $"- {ele.Key} X {ele.Value}";
			}
		}

		//Clear the list of upcoming enemies before the next wave
		private void ClearUpcomingWaveInfo()
		{
			foreach(Transform ele in textContainer.transform)
			{
				Destroy(ele.gameObject);
			}

			textContainer.SetActive(false); //hide the panel
		}


		public void KeepSpawning()
		{
			current = StartCoroutine(Timer(defaultTime));
			playableDirector.Resume();
		}

		public void PauseSpawning()
		{
			playableDirector.Pause();
			ClearUpcomingWaveInfo();
			StopCoroutine(current);
			//current = null;
		}

		private IEnumerator Timer(float maxTime)
		{
			float timer = maxTime;
			float sliderVal = 0f;
			waveSlider.maxValue = maxTime - 1f;
			while ((int)timer > 0)
			{
				//waveTimer.text = timeWaveTextConst + ((int)timer).ToString();
				timer -= Time.deltaTime;
				sliderVal += Time.deltaTime;
				waveSlider.value = sliderVal;
				yield return null;
			}
		}

		
	}
}


