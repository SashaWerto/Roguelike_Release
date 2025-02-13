using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blood : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer blood;
    [Header("Preferences")]
    public List<Sprite> bloodSprites;
    private void OnEnable()
    {
        Initiate();
    }
    public void Initiate()
    {
        int rnd = Random.Range(0, bloodSprites.Count);
        blood.sprite = bloodSprites[rnd];
        blood.transform.localScale = new Vector3(Random.Range(0.7f, 1.5f), Random.Range(0.5f, 1.1f), 1f);
    }
}
