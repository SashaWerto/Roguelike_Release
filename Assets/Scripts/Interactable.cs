using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("References")]
    public UnityEvent interactEvent;
    public bool canBeInteractedOnce;
    [Header("GFX")]
    [SerializeField] private GameObject interactIcon;
    [Header("Sound")]
    [SerializeField] private AudioClip clip;
    [Header("Options")]
    public float interactDelay;

    private float currentDelay;
    private bool interacted;
    private float hideTimer;
    [HideInInspector] public bool isActivated;
    [HideInInspector] public Action OnInteract;
    private void Start()
    {
        HideIcon();
        currentDelay = interactDelay;
    }
    private void Update()
    {
        hideTimer -= Time.deltaTime;
        currentDelay -= Time.deltaTime;
        if(hideTimer <= 0) HideIcon();
    }
    public void ShowIcon()
    {
        if (canBeInteractedOnce && interacted || !interactIcon) return;
        interactIcon.SetActive(true);
        hideTimer = 0.05f;        
    }
    private void HideIcon()
    {
        if(!interactIcon) return;
        interactIcon.SetActive(false);
    }
    public void CallInteract()
    {
        isActivated = true;
        interactEvent?.Invoke();
        interacted = true;
    }
    public void Interact()
    {
        if (canBeInteractedOnce && interacted || currentDelay > 0) return;
        SetDelay();
        interacted = true;
        isActivated = !isActivated;
        OnInteract?.Invoke();
        interactEvent?.Invoke();
        if (clip)
        {
            Sound_Manager.Instance.PlayShot(clip);
        }
    }
    public void SetDelay()
    {
        currentDelay = interactDelay;
    }
    public void SwitchObj(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void SwitchPauseAudio(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
