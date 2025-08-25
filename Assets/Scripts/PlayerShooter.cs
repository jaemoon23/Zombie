using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static readonly int idReload = Animator.StringToHash("Reload");

    public Gun gun;
    private Animator animator;
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();    // 다른 스크립트 참조
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (input.Fire)
        {
            gun.Fire();
        }
        else if (input.Reload)
        {
            if (gun.Reload())
            {
                animator.SetTrigger(idReload);
            }
        }
    }
}
