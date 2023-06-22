using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System.Linq;

public class PlayableCharacter : NetworkBehaviour, IInteractable
{
    [SerializeField] private NetworkObject playerObject;
    private NetworkCharacterControllerPrototype _cc;
    //private NetworkMecanimAnimator _ma;
    private Animator animator;
    private RaycastHit raycastHit;
    [SerializeField] private LayerMask[] layerMasks;

    private ERole role;

    private bool hasScent;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        //_ma = GetComponent<NetworkMecanimAnimator>();
        animator = GetComponent<Animator>();

        if (playerObject.Runner.IsClient)
        {
            role = ERole.Hider;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }

        Debug.DrawRay(transform.position, transform.forward*3, Color.red, 0.5f);
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 3f))
        {
            IInteractable interactiveObject = raycastHit.collider.gameObject.GetComponent<IInteractable>();
            if (interactiveObject != null && Input.GetKeyDown(KeyCode.Space))
            {
                interactiveObject.Interact(role, raycastHit);
            }
        }

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void Interact(ERole myRole, RaycastHit raycastHit)
    {
        if (myRole == ERole.Seeker)
        {
            //if (layerMasks. == raycastHit.collider.lay)
            //{

            //}
        }

        switch (myRole)
        {
            case ERole.AI:
                break;
            case ERole.Hider:
                break;
            case ERole.Seeker:
                break;
        }
    }

    public void ThrowScent()
    {

    }

    [Rpc()]
    public void CatchScent()
    {

    }
}
