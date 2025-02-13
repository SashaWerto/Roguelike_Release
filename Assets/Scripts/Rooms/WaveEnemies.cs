using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WaveNull", menuName = "Enemies/Wave")]
public class WaveEnemies : ScriptableObject
{
    public List<Wave> waves;
}
[Serializable]
public class Wave
{
    public List<EnemyConfig> enemies;
}
[Serializable]
public class EnemyConfig
{
    public GameObject enemyPrefab;
    public int count;
}