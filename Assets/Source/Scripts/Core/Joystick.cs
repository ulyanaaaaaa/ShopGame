using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal { get { return (_snapX) ? SnapFloat(_input.x, AxisOptions.Horizontal) : _input.x; } }
    public float Vertical { get { return (_snapY) ? SnapFloat(_input.y, AxisOptions.Vertical) : _input.y; } }
    
    public float HandleRange
    {
        get { return _handleRange; }
        set { _handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return _deadZone; }
        set { _deadZone = Mathf.Abs(value); }
    }

    [SerializeField] private float _handleRange = 1;
    [SerializeField] private float _deadZone;
    [SerializeField] private AxisOptions _axisOptions = AxisOptions.Both;
    [SerializeField] private bool _snapX;
    [SerializeField] private bool _snapY;
    [SerializeField] private RectTransform _handle;
    public RectTransform background;

    private Canvas _canvas;
    private Camera _camera;

    private Vector2 _input = Vector2.zero;

    private void Start()
    {
        HandleRange = _handleRange;
        DeadZone = _deadZone;
        _canvas = GetComponentInParent<Canvas>();
        
        if (_canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        _handle.anchorMin = center;
        _handle.anchorMax = center;
        _handle.pivot = center;
        _handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _handle.gameObject)
        {
            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _handle.gameObject)
        {
            _camera = null;
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _camera = _canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, background.position);
            Vector2 radius = background.sizeDelta / 2;
            _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            FormatInput();
            HandleInput(_input.magnitude, _input.normalized, radius, _camera);
            _handle.anchoredPosition = _input * radius * _handleRange;
        }
    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > _deadZone)
        {
            if (magnitude > 1)
                _input = normalised;
        }
        else
            _input = Vector2.zero;
    }

    private void FormatInput()
    {
        if (_axisOptions == AxisOptions.Horizontal)
            _input = new Vector2(_input.x, 0f);
        else if (_axisOptions == AxisOptions.Vertical)
            _input = new Vector2(0f, _input.y);
    }

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (_axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(_input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }