using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public EnemySpawner spawner;
    public Transform mapParent;

    private GameObject currentMap;

    private Path currentPath;

    public void StartStage(StageSO stage)
    {
        LoadMap(stage.MapPrefabID);
        StartCoroutine(RunStage(stage));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            var stage = DataManager.Instance.GetData<StageSO>("Stage_001");
            StartStage(stage);
        }
    }

    private void LoadMap(string prefabID)
    {
        if (currentMap != null) 
            Destroy(currentMap);

        GameObject mapPrefab = DataManager.Instance.GetPrefab(prefabID);

        if (mapPrefab == null) return;

        currentMap = Instantiate(mapPrefab, mapParent);
        currentPath = currentMap.GetComponentInChildren<Path>();
    }

    private IEnumerator RunStage(StageSO stage)
    {
        foreach(string waveID in stage.WaveIDs)
        {
            WaveSO wave = DataManager.Instance.GetData<WaveSO>(waveID);

            if (wave == null)
            {
                Debug.LogError($"WaveSO {waveID}를 찾지못합니다.");
                continue;
            }

            yield return StartCoroutine(RunWave(wave));
        }
    }

    private IEnumerator RunWave(WaveSO wave)
    {
        foreach(var spawnInfo in wave.Enemies)
        {
            EnemySO enemy = DataManager.Instance.GetData<EnemySO>(spawnInfo.EnemyID);

            if(enemy == null)
            {
                Debug.Log($"EnemySO {spawnInfo.EnemyID} 못찾음");
                continue;
            }

            for(int i = 0; i < spawnInfo.Count; i++)
            {
                spawner.SpawnEnemy(enemy, currentPath.Waypoints);
                yield return new WaitForSeconds(0.5f); //Wave Interval 넣기
            }

        }
    }
}
