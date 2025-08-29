using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public static readonly string PlayerDie = "Die";

    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPicupClip;

    private AudioSource audioSource;
    private Animator animator;

    private PlayerMovement movement;
    private PlayerShooter shooter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerShooter>();
        
    }

    private void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.value = Health / MaxHealth;

        movement.enabled = true;
        shooter.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnDamage(10f, Vector3.zero, Vector3.zero);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNoraml)
    {
        if (IsDead)
        {
            return;
        }
        base.OnDamage(damage, hitPoint, hitNoraml);     // 부모 함수 호출
        healthSlider.value = Health / MaxHealth;    // UI 활성
        audioSource.PlayOneShot(hitClip);   // 사운드 재생
    }

    protected override void Die()
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);
        animator.SetTrigger(PlayerDie);
        audioSource.PlayOneShot(deathClip);

        movement.enabled = false;
        shooter.enabled = false;
    }

    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + (float)amount, MaxHealth);
        healthSlider.value = Health / MaxHealth;
    }
}
