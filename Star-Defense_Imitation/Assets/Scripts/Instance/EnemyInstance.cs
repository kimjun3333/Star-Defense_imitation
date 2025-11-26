using System;

[Serializable]
public class EnemyInstance
{
    public EnemySO Definition; //원본SO
    public float CurrentHealth; //현재 체력
    public float CurrentSpeed; //현재 속도

    public int CurrentWaypointIndex; //경로 이동용

    public EnemyInstance(EnemySO so)
    {
        Definition = so;

        CurrentHealth = so.Health;
        CurrentSpeed = so.Speed;
        CurrentWaypointIndex = 0;
    }
}
