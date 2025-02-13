using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRadius = 0.5f;
    public static Action OnInteract;
    private void OnEnable()
    {
        Inputs.OnInteractDownAction += Interact;
    }
    private void OnDisable()
    {
        Inputs.OnInteractDownAction -= Interact;
    }
    private void Update()
    {
        DetectNearInteraction();
    }
    public void DetectNearInteraction()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(interactPoint.position, interactRadius, Vector2.zero, 0);
        List<GameObject> nearObjects = new List<GameObject>();
        foreach (var nearObj in hits)
        {
            nearObjects.Add(nearObj.collider.gameObject);
        }
        nearObjects.Sort(CompareDistanceToMe);
        foreach (var hit in nearObjects)
        {
            if (hit.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.ShowIcon();
                break;
            }
        }
    }
    public void Interact()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(interactPoint.position, interactRadius, Vector2.zero, 0);
        List<GameObject> nearObjects = new List<GameObject>();
        foreach (var nearObj in hits)
        {
            nearObjects.Add(nearObj.collider.gameObject);
        }
        nearObjects.Sort(CompareDistanceToMe);
        foreach (var hit in nearObjects)
        {
            if (hit.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.Interact();
                OnInteract?.Invoke();
                break;
            }
        }
    }
    int CompareDistanceToMe(GameObject a, GameObject b) {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}
