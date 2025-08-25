using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]    // 어트리뷰트
public class GunData : ScriptableObject
{
    public AudioClip shootClip;
    public AudioClip reloadClip;

    public float damage = 25f;  // 공격력
    public int startAmmoRemain = 100;   // 시작 총알
    public int magCapacity = 25;    // 탄창 용량

    public float timeBetFire = 0.12f;   // 연사 속도
    public float reloadTime = 1.8f; // 재장전 시간

    public float fireDistance = 50f;    // 사정거리
}
