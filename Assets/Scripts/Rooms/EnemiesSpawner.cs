using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("References")]
    public List<Transform> spawnPoints;
    [Header("Properties")]
    public List<WaveEnemies> waves;
    [Header("Prefabs")]
    public GameObject eggPrefab;
    private WaveEnemies currentWave;

    private void OnEnable()
    {
        if(waves.Count <= 0) return;
        InitiateWave();
        RoomManager.OnRoomCleared += SpawnWave;
    }

    private void OnDisable()
    {
        if(waves.Count <= 0) return;
        RoomManager.OnRoomCleared -= SpawnWave;
    }

    public void InitiateWave()
    {
        currentWave = Instantiate(waves[Random.Range(0,waves.Count)]);
    }
    public void SpawnWave()
    {
        if (currentWave.waves.Count <= 0)
        {
            switch (LanguageChanger.Instance.languageType)
            {
                case TranslationType.russian:
                    UI_Messages.Instance.ShowMessage("Комната зачищена", "Продолжу движение в глубь бездны..", 4f);
                    break;
                case TranslationType.english:
                    UI_Messages.Instance.ShowMessage("Room cleared", "I'm gonna keep going into the abyss.", 4f);
                    break;
            }
            
        }
        if(currentWave.waves.Count <= 0 || waves.Count <= 0) return;
        for (int i = 0; i < currentWave.waves[0].enemies.Count; i++)
        {
            for (int j = 0; j < currentWave.waves[0].enemies[i].count; j++)
            {
                var rndPoint = Random.Range(0, spawnPoints.Count);
                var createdEgg = Instantiate(eggPrefab, spawnPoints[rndPoint].position + new Vector3(Random.Range(0.001f,0.005f),Random.Range(0.001f,0.005f),0), Quaternion.identity);
                createdEgg.TryGetComponent<Egg>(out var egg);
                egg.objToSpawn = currentWave.waves[0].enemies[i].enemyPrefab;
                RoomManager.Instance.TransferInRoom(createdEgg);
            }
        }
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                UI_Messages.Instance.ShowMessage("Волна наступает!", "Приготовиться!", 4f);
                break;
            case TranslationType.english:
                UI_Messages.Instance.ShowMessage("The wave is coming!", "Get ready!", 4f);
                break;
        }
        currentWave.waves.Remove(currentWave.waves[0]);
        RoomManager.Instance.CheckForEnemies();
    }
}
