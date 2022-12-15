using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Audio;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    public AudioMixer audioMixer;

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
    
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        
        musicSliderText.text = ((int)(value * 100)).ToString();
        audioMixer.SetFloat("Music Volume", Mathf.Log10(value) * 20);
    }
    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        
        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(value) * 20);
    }
}
