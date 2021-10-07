using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ModelAnimationController : MonoBehaviour
{
    private readonly int _keyIdle = Animator.StringToHash("Idle");
    private readonly int _keyRun = Animator.StringToHash("Run");
    private readonly int _keyHit = Animator.StringToHash("Hit");
    private readonly int _keyShoot = Animator.StringToHash("Shoot");

    enum State
    {
        Idle = 1,
        Run = 2
    }

    private Animator _mainAnimator;

    private void Awake()
    {
        _mainAnimator = GetComponent<Animator>();
    }

    public void Idle()
    {
        _mainAnimator.SetTrigger(_keyIdle);
    }

    public void Run()
    {
        _mainAnimator.SetTrigger(_keyRun);
    }

    public void Shoot()
    {
        _mainAnimator.SetTrigger(_keyShoot);
    }

    public void Hit()
    {
        _mainAnimator.SetTrigger(_keyHit);
    }

    public void Hit(Transform notUsed_, float notUsed)
    {
        _mainAnimator.SetTrigger(_keyHit);
    }

    [ContextMenu("Disable")]
    public void Disable()
    {
        _mainAnimator.enabled = false;
    }
}