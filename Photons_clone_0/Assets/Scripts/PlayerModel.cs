using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.Events;

public class PlayerModel : NetworkBehaviour
{
    // Networked 네트워크 상 변하는 변수에 사용하기 (네트워크에서 동기화할 데이터를 선정)
    // 조건 - 데이터는 프로터피(속성)으로 작성해야하함

    // OnChangedRender : 네트워크 데이터가 변경되었을 때 호출되는 함수를 지정
    //                                      || 위 아래 같은 말
    // OnChangedRender(nameof(OnChangedHP)) 변수가 변할 때 가로 안에 들어간 함수가 실행된다.
    [Networked, OnChangedRender(nameof(OnChangedHP))] public int hp { get; set; }

    public UnityAction<int> OnChangedHpEvent;

    public void OnChangedHP() // => OnChangedHpEvent?.Invoke(hp);
    {
        OnChangedHpEvent?.Invoke(hp);
    }
}
