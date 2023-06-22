using UnityEngine;

public interface IInteractable
{
    public void Interact(ERole myRole, RaycastHit raycastHit);
}
