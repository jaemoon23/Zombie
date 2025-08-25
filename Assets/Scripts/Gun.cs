using System.Collections;
using System.Data;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //  총의 상태
    public enum State
    {
        Ready,
        Empty,
        Reloading,
    }

    private State currentState = State.Ready;

    public State CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;

            switch (currentState)
            {
                case State.Ready:
                    break;
                case State.Empty:
                    break;
                case State.Reloading:
                    break;
            }
        }
    }

    public GunData gunData;

    public ParticleSystem muzzleEffect; // 총구 화염 이펙트
    public ParticleSystem shellEffect;  // 탄피 배출 이펙트

    private LineRenderer lineRenderer;  //  총알 궤적을 그리기 위한 컴포넌트
    private AudioSource audioSource;    // 총 소리 재생 컴포넌트

    public Transform firePosition;  // 총알 발사 위치

    public int ammoRemain;  // 남은 총알
    public int magAmmo;     // 현재 탄창에 남아있는 총알

    private float lastFireTime; //  마지막 총알 발사 시점

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;   
        magAmmo = gunData.magCapacity;  

        lastFireTime = 0f;
        CurrentState = State.Ready;


    }

    private void Update()
    {
        // 상태에 따른 행동
        switch (currentState)
        {
            case State.Ready:
                UpdateReady();
                break;
            case State.Empty:
                UpdateEmpty();
                break;
            case State.Reloading:
                UpdateReloading();
                break;
        }
    }

    // 각 상태에 따른 행동 메서드
    private void UpdateReady()
    {
        
    }

    private void UpdateEmpty()
    {

    }

    private void UpdateReloading()
    {

    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(gunData.shootClip);

        muzzleEffect.Play();    // 화염 이펙트 플레이
        shellEffect.Play();     // 탄피 이펙트 플레이
        lineRenderer.enabled = true;    // lineRenderer 컴포넌트 활성

        lineRenderer.SetPosition(0, firePosition.position);  // lineRenderer 포지션 변경

        // lineRenderer 1번째 인덱스 포지션 설정 라인의 마지막 위치
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.2f);  // 1초 기다렸다가 실행 코루틴 비동기 코딩

        lineRenderer.enabled = false;   // lineRenderer 컴포넌트 비활성

    }

    public void Fire()
    {
        // 총알 발사 가능 상태 && 마지막 발사 시점에서 timeBetFire 이상 시간이 흘렀다면
        if (currentState == State.Ready && Time.time > (lastFireTime + gunData.timeBetFire))    
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 hitPosition = Vector3.zero; // 맞은 위치
        RaycastHit hit; // 충돌 정보

        // Ray(시작 위치, 방향)
        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, gunData.fireDistance))
        {
            // 무언가 맞았다면, 맞은 위치 저장
            hitPosition = hit.point;
            var target =  hit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            // 아무것도 맞지 않았다면, 최대 사거리 위치
            hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        }

        // 총알 발사 이펙트 재생
        StartCoroutine(CoShotEffect(hitPosition));

        --magAmmo;
        Debug.Log($"지금 탄창 {magAmmo}");
        if (magAmmo == 0)
        {
            CurrentState = State.Empty; // 총알이 다 떨어졌다면 상태를 Empty로 변경
        }
    }

    // 탄창 교체
    public bool Reload()
    {
        if (ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(CoReloading());
        return true;
    }

    private IEnumerator CoReloading()
    {
        CurrentState = State.Reloading;

        yield return new WaitForSeconds(gunData.reloadTime);

        magAmmo = gunData.magCapacity;
        ammoRemain -= magAmmo;
        CurrentState = State.Ready;
        Debug.Log($"재장전 후 탄창: {magAmmo}");
        Debug.Log($"내가 가진 총알: {ammoRemain}");


    }
}
