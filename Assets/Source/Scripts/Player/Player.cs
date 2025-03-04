using UnityEngine;

public class Player : MonoBehaviour
{
    private IControlable _controlable;
    private PlayerMovement _playerMovement;
    private Joystick _joystick;

    public Joystick Joystick
    {
        private get { return _joystick; }
        set
        {
            if (_joystick == null)
            {
                _joystick = value;
                _controlable = new MobileController(_playerMovement, _joystick);
            }
        }
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        
        if (_joystick != null)
        {
            _controlable = new MobileController(_playerMovement, _joystick);
        }
    }

    private void FixedUpdate()
    {
        _controlable.Controller();
    }
}