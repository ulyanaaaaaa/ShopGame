using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxVerticalAngle; 
    [SerializeField] private float _minVerticalAngle;

    private Vector2 _startTouchPosition;
    private bool _isDragging;

    private float _currentVerticalAngle; 
    private float _currentHorizontalAngle; 

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.position;
                    _isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (_isDragging && IsTouchingRightSide(touch.position))
                    {
                        Vector2 delta = touch.position - _startTouchPosition;
                        RotateCamera(delta);
                        _startTouchPosition = touch.position;
                    }
                    break;

                case TouchPhase.Canceled:
                    _isDragging = false;
                    break;
            }
        }
    }

    private void RotateCamera(Vector2 delta)
    {
        float rotationX = delta.x * _sensitivity;
        float rotationY = delta.y * _sensitivity;
        
        _currentVerticalAngle -= rotationY;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
        
        _currentHorizontalAngle += rotationX;
        
        transform.localEulerAngles = new Vector3(_currentVerticalAngle, _currentHorizontalAngle, 0f);
    }

    private bool IsTouchingRightSide(Vector2 touchPosition)
    {
        return touchPosition.x > Screen.width / 2;
    }
}