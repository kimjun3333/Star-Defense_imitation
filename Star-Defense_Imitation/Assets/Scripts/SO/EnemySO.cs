using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : BaseSO
{
    public float Health;
    public float Speed;

    public UnitType UnitType;
    public ResourceType RewardType; //골드 Or 미네랄
    public int Reward;
}
