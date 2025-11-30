using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Repair,
    Buff
}

public class TileController : MonoBehaviour
{
    [Header("타일 타입 설정")]
    public TileType tileType;

    [Header("타일별 스프라이트")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite repairSprite;
    [SerializeField] private Sprite buffSprite;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private TowerController currentTower;
    public TowerController CurrentTower => currentTower;

    [Header("설치 여부")]
    public bool hasTower = false;
    public bool CanPlaceTower
    {
        get
        {
            if (tileType == TileType.Repair) return false;

            if (hasTower) return false;

            return true;
        }
    }
    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        switch (tileType)
        {
            case TileType.Normal:
                sprite.sprite = normalSprite;
                break;

            case TileType.Repair:
                sprite.sprite = repairSprite;
                break;

            case TileType.Buff:
                sprite.sprite = buffSprite;
                break;
        }
    }

    public float GetBuffMult()
    {
        return tileType == TileType.Buff ? 1.3f : 1f;
    }

    public void Repair()
    {
        if(tileType == TileType.Repair)
        {
            tileType = TileType.Normal;
            Debug.Log("타일 타입 -> Normal");
            UpdateSprite();
        }
    }

    private void OnMouseDown()
    {
        EventManager.Instance.Trigger(
        EventType.TileClicked,
        new TileClickedPayload(this)
        );
    }

    public void TryPlaceRandomTower()
    {
        if(!CanPlaceTower)
        {
            Debug.Log("설치 불가능한 타일");
            return;
        }

        TowerSO so = GetRandomTowerSO();
        if(so == null)
        {
            Debug.LogError("타워 뽑기 실패");
            return;
        }

        TowerController tower = TowerManager.Instance.BuildTower(so, transform.position);

        tower.ownerTile = this;
        currentTower = tower;
        hasTower = true;

        Debug.Log($"타워 설치 완료 : {so.ID}");
    }
    public void SetTower(TowerController tower)
    {
        currentTower = tower;
        hasTower = (tower != null);
    }

    public void RemoveTower()
    {
        if (currentTower != null)
            currentTower.ownerTile = null;

        currentTower = null;
        hasTower = false;
    }

    private TowerSO GetRandomTowerSO()
    {
        List<TowerSO> towers = DataManager.Instance.GetAllDataOfType<TowerSO>();
        if (towers == null || towers.Count == 0)
            return null;

        int idx = Random.Range(0, towers.Count);
        return towers[idx];
    }
}
