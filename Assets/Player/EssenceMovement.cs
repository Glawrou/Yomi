using UnityEngine;

public class EssenceMovement : MonoBehaviour
{
    [SerializeField] private EssenceGravity _gravity;
    [SerializeField] private Stamina _stamina;
    [SerializeField] private GroundCheck _groundCheck;

    private CharacterController _characterController;

    [Space]
    [SerializeField, Range(0.001f, 0.1f)] private float _speedNormal = 0.01f;
    [SerializeField, Range(0.001f, 0.1f)] private float _speedRun = 0.018f;
    [SerializeField, Range(0.01f, 1f)] private float _forceJump = 0.1f;

    [Space]
    [SerializeField, Range(0.1f, 2f)] private float _jumpStamina = 1f;

    private bool _isRun = false;

    public void Initialization(CharacterController characterController)
    {
        _gravity.Initialization(characterController);
        _characterController = characterController;
        _stamina.SetMaxStamina();
    }

    public void UpdateHandler()
    {
        _stamina.Update(_isRun);
        _gravity.UpdateHandler();
    }

    public void MoveInputHandler(Vector2 vector)
    {
        var move = transform.right * vector.x + transform.forward * vector.y;
        _characterController.Move(move * GetSpeed());
    }

    private float GetSpeed()
    {
        if (_stamina.IsStamina)
        {
            return _isRun ? _speedRun : _speedNormal;
        }

        return _speedNormal;
    }

    public void SetRun(bool isRun)
    {
        _isRun = isRun && _stamina.IsCanUseStamina;
    }

    public void Jump()
    {
        if (!_groundCheck.IsGround)
        {
            return;
        }

        if (!_stamina.RemoveStamina(_jumpStamina))
        {
            return;
        }

        _gravity.SetVelocity(_forceJump);
    }
}
