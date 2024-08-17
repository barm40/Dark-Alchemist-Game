using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public TMP_Dropdown fScreenDropdown;
        public TMP_Dropdown resDropdown;

        private Resolution[] _resolutions;
        private int _possibleScreenModes;
        private void Start()
        {
            // populate possible screen modes
            _possibleScreenModes = 4;
        
            var currentScreenTypeIndex = 0;
        
            for (int i = 0; i < _possibleScreenModes; i++)
            {
                if ((FullScreenMode)i == Screen.fullScreenMode)
                {
                    currentScreenTypeIndex = i;
                    break;
                }
            }
            fScreenDropdown.value = currentScreenTypeIndex;
            fScreenDropdown.RefreshShownValue();
        
        
            // populate possible resolutions
            _resolutions = Screen.resolutions;
        
            resDropdown.ClearOptions();

            var currentResolutionIndex = 0;

            var usableResolutions = new List<string>();

            for (int i = 0; i < _resolutions.Length; i++)
            {
                usableResolutions.Add(_resolutions[i].width + "x" + 
                                      _resolutions[i].height + " " + 
                                      (int)_resolutions[i].refreshRateRatio.value + "Hz");

                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resDropdown.AddOptions(usableResolutions);
            resDropdown.value = currentResolutionIndex;
            resDropdown.RefreshShownValue();
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullScreen(int screenTypeIndex)
        {
            Screen.fullScreenMode = (FullScreenMode)screenTypeIndex;
        }

        public void SetResolution(int resIndex)
        {
            var res = _resolutions[resIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
        }
    }
}
