using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.Events;

public class PlayerModel : NetworkBehaviour
{
    // Networked ��Ʈ��ũ �� ���ϴ� ������ ����ϱ� (��Ʈ��ũ���� ����ȭ�� �����͸� ����)
    // ���� - �����ʹ� ��������(�Ӽ�)���� �ۼ��ؾ�����

    // OnChangedRender : ��Ʈ��ũ �����Ͱ� ����Ǿ��� �� ȣ��Ǵ� �Լ��� ����
    //                                      || �� �Ʒ� ���� ��
    // OnChangedRender(nameof(OnChangedHP)) ������ ���� �� ���� �ȿ� �� �Լ��� ����ȴ�.
    [Networked, OnChangedRender(nameof(OnChangedHP))] public int hp { get; set; }

    public UnityAction<int> OnChangedHpEvent;

    public void OnChangedHP() // => OnChangedHpEvent?.Invoke(hp);
    {
        OnChangedHpEvent?.Invoke(hp);
    }
}
