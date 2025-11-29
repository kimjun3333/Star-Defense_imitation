/// <summary>
/// 풀링이 가능한 객체일때 사용하는 인터페이스
/// </summary>
public interface IPoolable
{
    void OnSpawned();
    void OnDespawned();
}
