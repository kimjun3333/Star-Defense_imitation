using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 초기화 매니저
/// </summary>
public class InitializeManager : Singleton<InitializeManager>
{
    protected override async void Awake()
    {
        base.Awake();

        Debug.Log("초기화 시작");
        await AddressableLoader.Instance.Init(); //어드레서블 데이터 불러오기
        await GoogleLoader.Instance.Init(); //시트 데이터 덮어쓰기
        AddressableLoader.Instance.LinkAllSprites(); //스프라이트 ID 비교후 매핑
        await DataManager.Instance.Init(); //어드레서블 데이터 DataManager에 넘겨주기
        await PoolingManager.Instance.Init(); //풀링 생성 
    }
}
