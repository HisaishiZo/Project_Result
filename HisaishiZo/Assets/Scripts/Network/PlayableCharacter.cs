using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UIElements;


public class PlayableCharacter : NetworkBehaviour, IInteractable
{
    [SerializeField] private NetworkObject networkPlayerObject;
    private NetworkCharacterControllerPrototype _cc;
    //private NetworkMecanimAnimator _ma;
    private Animator animator;
    private RaycastHit raycastHit;
    private ParticleSystem particleSystem;
    [SerializeField] private LayerMask[] layerMasks;

    public ERole characterRole = ERole.Hider;

    private bool hasScent;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        //_ma = GetComponent<NetworkMecanimAnimator>();
        animator = GetComponent<Animator>();
        if(characterRole == ERole.Hider)
        {
            particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Play(); gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 8;
        }
    }
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    private float rotateSpeed = 1500.0f;
    public override void FixedUpdateNetwork()
    {
        if (Input.GetMouseButton(0)) // 클릭한 경우
        {
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;

            transform.eulerAngles = new Vector3(0, yRotate, 0);
        }

        if (GetInput(out NetworkInputData data))
        {
            // data.direction.Normalize();
            // _cc.Move(5 * data.direction * Runner.DeltaTime);
            _cc.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * 10f);
        }

        Debug.DrawRay(transform.position, transform.forward*3, Color.red, 10f);
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 10f))
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

        if (characterRole == ERole.Seeker && Input.GetKey(KeyCode.Space))
        {
            foreach(var item in PhotonEngineFusionManager.Instance._spawnedCharacters.Keys)
            {
                PhotonEngineFusionManager.Instance._spawnedCharacters.Remove(item);
                break;
            }
        }
    }

    public void SetRole(ERole role) => characterRole = role;

    public void Interact(ERole myRole, RaycastHit raycastHit)
    {
        if (myRole == ERole.Seeker)
        {
            animator.SetTrigger("Atk");
            switch(raycastHit.collider.gameObject.layer)
            {
                case (int)ERole.AI:
                    StartCoroutine(Sturn());
                    break;

                case (int)ERole.Hider:
                    Kill_RPC();
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

    // Object.HasInputAuthority 일 때만
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

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void Kill_RPC(RpcInfo info = default)
    {
        Application.Quit();
    }

    public IEnumerator Sturn()
    {
        yield return new WaitForSeconds(5f);
    }
}
