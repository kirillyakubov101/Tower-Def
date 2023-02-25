using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace TowersNoDragons.Sounds
{
    public class SoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource audioSource;

        [Header("Background Clips")]
        [SerializeField] private AudioClip[] mainMenuClips = null;
        [SerializeField] private AudioClip[] battleClips = null;
        [SerializeField] private AudioClip[] loseClips = null;
        [SerializeField] private AudioClip[] winClips = null;

        public static SoundHandler Instance = null;

        private const string SFX_PARAM = "sfxVol";
        private const string BG_PARAM = "bgVol";

        private Coroutine current;

        private void Awake()
		{
			if(Instance == null) { Instance = this; }
			else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
            //current = StartCoroutine(PlayTracks(mainMenuClips));
		}

		private void OnEnable()
		{
            SceneManager.sceneLoaded += UpdateTracks;
        }

		private void OnDisable()
		{
            SceneManager.sceneLoaded -= UpdateTracks;
        }

		private void UpdateTracks(Scene scene, LoadSceneMode mode)
		{
            PlayTracks(scene.buildIndex);
        }

        private void PlayTracks(int sceneNumber)
		{
            if(current != null)
			{
                StopCoroutine(current);
            }

            switch(sceneNumber)
			{
                //Main menu
                case 0:
                    current = StartCoroutine(PlayTracksProcess(mainMenuClips));
                    break;

                case 1:
                    current = StartCoroutine(PlayTracksProcess(battleClips));
                    break;
            }
           
        }

		private IEnumerator PlayTracksProcess(AudioClip[] clips)
		{
            int index = -1;

            while (true)
			{
                if(!audioSource.isPlaying)
				{
                    index = (index + 1) % clips.Length;
                    audioSource.clip = clips[index];
                    audioSource.Play();
                }
               
                yield return null;
			}
		}


		//background volume of the game
		public void SetBGVolume(float vol)
        {
            audioMixer.SetFloat(BG_PARAM, Mathf.Log10(vol) * 20);
        }

        //sfx volume of the game
        public void SetSoundEffectsVolume(float vol)
        {
            audioMixer.SetFloat(SFX_PARAM, Mathf.Log10(vol) * 20);
        }
    }
}


