using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public EnemySpawner spawner;
    public Transform mapParent;

    private GameObject currentMap;
    private Path currentPath;

    private StageSO currentStage;
    private int currentWaveIndex = 0;
    private bool isWaveRunning = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            LoadStage("Stage_001");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartNextWave();
        }
    }

    public void LoadStage(string stageID)
    {
        currentStage = DataManager.Instance.GetData<StageSO>(stageID);
        currentWaveIndex = 0;
        isWaveRunning = false;

        LoadMap(currentStage.MapPrefabID);
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

    public void StartNextWave()
    {
        if(isWaveRunning)
        {
            Debug.Log("웨이브 진행중");
            return;
        }

        if(currentWaveIndex >= currentStage.WaveIDs.Count)
        {
            Debug.Log("모든 웨이브 종료");
            return;
        }

        string waveID = currentStage.WaveIDs[currentWaveIndex];
        WaveSO wave = DataManager.Instance.GetData<WaveSO>(waveID);

        if(wave == null)
        {
            Debug.LogError($"WaveSO {waveID}를 찾을수 없습니다.");
            return;
        }

        StartCoroutine(RunWave(wave));
        currentWaveIndex++;
    }
    private IEnumerator RunWave(WaveSO wave)
    {
        isWaveRunning = true;

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

        isWaveRunning = false;
    }
}
