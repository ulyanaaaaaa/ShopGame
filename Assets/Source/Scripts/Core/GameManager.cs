using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _joystickCanvasPrefab;
    [SerializeField] private GameObject _userPrefabClone;
    
    private void Awake()
    {
        JoystickSetup();
    }
    
    private void JoystickSetup()
    {
        var joystickCanvas = Instantiate(_joystickCanvasPrefab, transform.position, Quaternion.identity);
        Joystick joystick = joystickCanvas.GetComponentInChildren<Joystick>();
        _userPrefabClone.GetComponent<Player>().Joystick = joystick;
        joystickCanvas.gameObject.SetActive(true);
    }
}
