using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;


public class PlayableCharacter : NetworkBehaviour, IInteractable
{
    [SerializeField] private NetworkObject networkPlayerObject;
    private NetworkCharacterControllerPrototype _cc;
    //private NetworkMecanimAnimator _ma;
    private Animator animator;
    private RaycastHit raycastHit;
    [SerializeField] private LayerMask[] layerMasks;

    public ERole characterRole = ERole.Hider;

    private bool hasScent;

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
        }

        Debug.DrawRay(transform.position, transform.forward*3, Color.red, 0.5f);
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 3f))
        {
            IInteractable interactiveObject = raycastHit.collider.gameObject.GetComponent<IInteractable>();
            if (interactiveObject != null && Input.GetKeyDown(KeyCode.Space))
            {
                interactiveObject.Interact(characterRole, raycastHit);
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

    public void SetRole(ERole role) => characterRole = role;

    public void Interact(ERole myRole, RaycastHit raycastHit)
    {
        if (myRole == ERole.Seeker)
        {
            switch(raycastHit.collider.gameObject.layer)
            {
                case (int)ERole.AI:
                    break;

                case (int)ERole.Hider:
                    break;
            }
        }
        else if (myRole == ERole.Hider)
        {
            switch (raycastHit.collider.gameObject.layer)
            {
                case (int)ERole.Hider:
                    ThrowScent();
                    break;
            }
        }
    }

    // Object.HasInputAuthority ¿œ ∂ß∏∏
    public void ThrowScent()
    {
        CatchScent_RPC();
        hasScent = false;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void CatchScent_RPC(RpcInfo info = default)
    {
        hasScent = true;
        Application.Quit();
    }
}
