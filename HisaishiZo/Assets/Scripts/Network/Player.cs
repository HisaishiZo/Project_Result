using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype _cc;
    private NetworkMecanimAnimator _ma;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _ma = GetComponent<NetworkMecanimAnimator>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
            //_ma.SetTrigger(0);
        }
    }
}