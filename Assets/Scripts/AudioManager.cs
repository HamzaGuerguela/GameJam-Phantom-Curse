using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Inspector

    [SerializeField] private Sound[] musicSounds, sfxSounds;

    [SerializeField]
    private AudioSource musicSource, sfxSource;

    #endregion

}
