using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ThrowButton _throwButton;
    [SerializeField] private LeftHand _leftHandle;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_joystick).AsSingle();
        Container.BindInstance(_throwButton).AsSingle();
        Container.BindInstance(_leftHandle).AsSingle();
    }
}