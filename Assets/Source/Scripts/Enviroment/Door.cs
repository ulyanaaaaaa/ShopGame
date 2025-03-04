using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
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

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ToggleDoor();
                    }
                }
            }
        }
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