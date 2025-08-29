using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    
    public static readonly int idReload = Animator.StringToHash("Reload");
    public Gun gun;

    private Vector3 gunInitPosition;
    private Quaternion gunInitRotation;

    private Rigidbody gunRb;
    private Collider gunCollider;

    private Animator animator;
    private PlayerInput input;


    public Transform gunPivot;
    public Transform leftHanmdMount;
    public Transform rightHanmdMount;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        gunRb = gun.GetComponent<Rigidbody>();
        gunCollider = gun.GetComponent<Collider>();

        gunInitPosition = gun.transform.localPosition;
        gunInitRotation = gun.transform.localRotation;
    }

    private void OnEnable()
    {
        gunRb.isKinematic = true;
        gunCollider.enabled = false;
        gun.transform.localPosition = gunInitPosition;
        gun.transform.localRotation = gunInitRotation;
    }

    private void OnDisable()
    {
        gunRb.linearVelocity = Vector3.zero;
        gunRb.angularVelocity = Vector3.zero;

        gunRb.isKinematic = false;
        gunCollider.enabled = true;
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

    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // 왼손
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHanmdMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHanmdMount.rotation);

        // 오른손
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHanmdMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHanmdMount.rotation);

    }
}
