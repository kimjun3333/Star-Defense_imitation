/// <summary>
/// 플레이어 자원이 변경됐을때 EventManager을 통해 전달하는 데이터 구조
/// </summary>
public struct ResourceChangedPayload
{
    public ResourceType type;
    public int value;

    public ResourceChangedPayload(ResourceType type, int value)
    {
        this.type = type;
        this.value = value;
    }
}
