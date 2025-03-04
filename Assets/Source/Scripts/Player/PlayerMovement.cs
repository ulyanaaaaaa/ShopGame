using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector3 moveDirection)
    {
        if (moveDirection.magnitude > 0)
        {
            Vector3 localMoveDirection = transform.TransformDirection(moveDirection);
            Vector3 offset = localMoveDirection.normalized * _speed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + offset);
        }
    }
}