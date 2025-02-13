using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Events : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] private AudioClip attachedClip;

    public void PlayAttachedClip()
    {
        Sound_Manager.Instance.PlayShot(attachedClip);
    }
    public void PlayShot(AudioClip clip)
    {
        Sound_Manager.Instance.PlayShot(clip);
    }
}
