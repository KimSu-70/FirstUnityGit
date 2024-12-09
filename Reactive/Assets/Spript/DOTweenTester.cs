using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenTester : MonoBehaviour
{

    [SerializeField] Material material;

    private void Start()
    {
        material = GetComponent<Material>();
    }

    private void Update()
    {
        // 왼쪽 마우스 버튼을 클릭 했을 때
        if (Input.GetMouseButtonDown(0) == false)
            return;

        // 화면상의 마우스 위치를 기준으로 레이
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 레이가 닿은 위치로 이동
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            MoveTo(hitInfo.point);
        }
    }

    public void MoveTo(Vector3 des)
    {
        //transform.DOMove(des, 1f)
        //    .SetEase(Ease.Linear)
        //    .SetDelay(0.5f)
            //.OnStart(() => Debug.Log("Started"))
            //.OnComplete(() => Debug.Log("Completed"));

        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DORotate(Vector3.zero, 0.1f))
            .Join(transform.DOMove(des, 1f))
            .Append(transform.DOShakeRotation(1f));
    }
}
