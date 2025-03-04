using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Vector3 _openRotation; 
    [SerializeField] private Transform _transform; 
    [SerializeField] private float _openSpeed; 
    
    private Vector3 _closedRotation;
    private bool _isOpen;

    private void Start()
    {
        _closedRotation = _transform.eulerAngles;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ToggleDoor();
    }
    
    private void ToggleDoor()
    {
        if (!_isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(_openRotation));
        _isOpen = true;
    }

    private void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(_closedRotation));
        _isOpen = false;
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        while (Vector3.Distance(_transform.eulerAngles, targetRotation) > 0.01f)
        {
            _transform.eulerAngles = Vector3.Lerp(_transform.eulerAngles, targetRotation, Time.deltaTime * _openSpeed);
            yield return null;
        }
        _transform.eulerAngles = targetRotation;
    }
}