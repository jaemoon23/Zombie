using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleEffect;
    public ParticleSystem shellEffect;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public Transform firePosition;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CoShotEffect());
        }
    }

    private IEnumerator CoShotEffect()
    {
        muzzleEffect.Play();    // 화염 이펙트 플레이
        shellEffect.Play();     // 탄피 이펙트 플레이
        lineRenderer.enabled = true;    // lineRenderer 컴포넌트 활성
        lineRenderer.SetPosition(0, firePosition.position);  // lineRenderer 포지션 변경


        // lineRenderer 1번째 인덱스 포지션 설정 라인의 마지막 위치
        Vector3 endPos = firePosition.position + firePosition.forward * 10f;
        lineRenderer.SetPosition(1, endPos);

        yield return new WaitForSeconds(0.2f);  // 1초 기다렸다가 실행 코루틴 비동기 코딩

        lineRenderer.enabled = false;   // lineRenderer 컴포넌트 비활성

    }
}
