using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private IControlable _controlable;
    private PlayerMovement _playerMovement;
    
    [Inject] 
    private Joystick _joystick;
    
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