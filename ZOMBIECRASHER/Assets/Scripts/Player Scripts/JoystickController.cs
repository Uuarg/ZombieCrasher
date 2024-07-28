using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class JoystickController : MonoBehaviour
{

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float resetRotationSpeed = 10f;
    private void FixedUpdate()
    {
        float horizontalInput = _joystick.Horizontal;

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {

            Quaternion targetRotation = Quaternion.Euler(0, horizontalInput * 90, 0);
            _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

            _rigidbody.linearVelocity = transform.forward * _moveSpeed;
        }
        else
        {
            Quaternion straightRotation = Quaternion.Euler(0, 0, 0);
            _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, straightRotation, Time.deltaTime * resetRotationSpeed));

            _rigidbody.linearVelocity = transform.forward * _moveSpeed;
        }
    }
}