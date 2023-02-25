using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using TowersNoDragons.Sounds;

namespace TowersNoDragons.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        //Main menu
        [Header("Main menu")]
        [SerializeField] private string loadLevel;
        [SerializeField] private string loadTutorial;

        //Volume
        [Header("Volume")]
        [SerializeField] private TMP_Text bgVolumeValue = null;
        [SerializeField] private Slider bgVolumeSlider = null;
        [SerializeField] private TMP_Text sfxVolumeValue = null;
        [SerializeField] private Slider sfxVolumeSlider = null;
        [SerializeField] private float defaultVol = 0.5f;


        //Graphics
        [Header("Graphics")]
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private TMP_Text brightnessTextValue = null;
        [SerializeField] private float defaultBrightness = 1;

        //Resolution
        [Header("Resolution")]
        [SerializeField] private TMP_Dropdown resDropDown;

        //Quality
        private int qualityLevel;
        private float brightnessLevel;
        private Resolution[] resolutions;

        private void Start()
		{
			InitResolution();

		}

		private void InitResolution()
		{
			resolutions = Screen.resolutions;
			resDropDown.ClearOptions();


			List<string> options = new List<string>();

			int currResIndex = 0;

			for (int i = 0; i < resolutions.Length; i++)
			{
				string option = resolutions[i].width + " x " + resolutions[i].height;
				options.Add(option);

				if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
				{
					currResIndex = i;
				}
			}

			resDropDown.AddOptions(options);
			resDropDown.value = currResIndex;
			resDropDown.RefreshShownValue();
		}

		public void SetReslution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void PlayButton()
        {
            SceneManager.LoadScene(loadLevel);
        }

        public void TutorialButton()
        {
            SceneManager.LoadScene(loadTutorial);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void Update_Background_Music_Text_Slider(float vol)
		{
            bgVolumeValue.text = vol.ToString("0.0");
        }

        public void Update_SFX_Music_Text_Slider(float vol)
        {
            sfxVolumeValue.text = vol.ToString("0.0");
        }


        public void ResetToDefault()
        {
            bgVolumeSlider.value = defaultVol;
            bgVolumeValue.text = defaultVol.ToString("0.0");
            SoundHandler.Instance.SetBGVolume(defaultVol);


            sfxVolumeSlider.value = defaultVol;
            sfxVolumeValue.text = defaultVol.ToString("0.0");
            SoundHandler.Instance.SetSoundEffectsVolume(defaultVol);
            //ApplyChanges(); //TODO: SAVE AND LOAD

        }

        public void SetBrightness(float brightness)
        {
            brightnessLevel = brightness;
            brightnessTextValue.text = brightness.ToString("0.0");
        }

        public void SetQuality(int qualityIndex)
        {
            qualityLevel = qualityIndex;
        }

        public void GraphicsApply()
        {
            PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);//save brightness

            PlayerPrefs.SetInt("masterQuality", qualityLevel);//save quality

        }
    }
}


