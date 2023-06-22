using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype _cc;
    //private NetworkMecanimAnimator _ma;
    private Animator animator;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        //_ma = GetComponent<NetworkMecanimAnimator>();
        animator = GetComponent<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}