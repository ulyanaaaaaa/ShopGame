using UnityEngine;

public class MobileController : IControlable
{
    private PlayerMovement _userMovement;
    private Joystick _joystick;

    public MobileController(PlayerMovement userMovement, Joystick joystick)
    {
        _userMovement = userMovement;
        _joystick = joystick;
    }

    public void Controller()
    {
        Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        
        if (moveDirection.magnitude > 0.5f)
        {
            _userMovement.MovePlayer(moveDirection);
        }
    }
}