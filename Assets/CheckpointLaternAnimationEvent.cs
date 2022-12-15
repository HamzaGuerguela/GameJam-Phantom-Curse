using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLaternAnimationEvent : MonoBehaviour
{
    #region Inspector

    

    #endregion

    #region Unity Event Functions

    

    #endregion

    public void CheckpointAnimationEvent()
    {
        FindObjectOfType<AudioManager>().CheckpointSound();
    }
}
