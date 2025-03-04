using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Button _throwButton;
    [SerializeField] private float _impulse;
    [SerializeField] private float _maxPickupDistance;

    private GameObject _currentItem;

    private void Start()
    {
        _throwButton.gameObject.SetActive(false);
        _throwButton.onClick.AddListener(ThrowItem);
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
                    if (hit.collider.TryGetComponent(out GrabbableObject grabbableObject) && _currentItem == null)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);
                        if (distance <= _maxPickupDistance)
                        {
                            PickUpItem(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        _currentItem = item;
        _currentItem.transform.SetParent(_hand);
        _currentItem.transform.localPosition = Vector3.zero;
        _currentItem.GetComponent<BoxCollider>().enabled = false;
        _currentItem.GetComponent<Rigidbody>().isKinematic = true;
        _throwButton.gameObject.SetActive(true);
    }

    private void ThrowItem()
    {
        if (_currentItem != null)
        { 
            _currentItem.GetComponent<BoxCollider>().enabled = true;
            _currentItem.GetComponent<Rigidbody>().isKinematic = false;
            _currentItem.transform.SetParent(null);
            _currentItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _impulse);
            _currentItem = null;
            _throwButton.gameObject.SetActive(false);
        }
    }
}