using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StageManager : Singleton<StageManager>, IInitializable
{
    public EnemySpawner spawner;
    public Transform mapParent;

    private GameObject currentMap;
    private Path currentPath;

    private StageSO currentStage;
    private int currentWaveIndex = 0;
    private bool isWaveRunning = false;

    public async Task Init()
    {
        LoadStage("Stage_001");

        await Task.CompletedTask;

    }

    private void Update()
    {
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

        EventManager.Instance.Trigger(
            EventType.WaveStarted,
            new WaveStartedPayload(currentWaveIndex + 1, currentStage.WaveIDs.Count)
            );

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
                yield return new WaitForSeconds(0.5f); //Wave Interval 넣기 적 나오는 텀
            }
        }

        yield return new WaitUntil(() => EnemyManager.Instance.IsAllEnemyDead());

        isWaveRunning = false;
    }

  
}
