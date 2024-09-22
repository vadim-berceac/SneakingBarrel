using UnityEngine;

public class CameraController : MonoBehaviour
{
    // basic coordiantes 
    // Vector3(11.4899998,15,-10.6000004)
    // Quaternion(0.3460913,-0.354817897,0.143355697,0.856606185)
    [SerializeField] private Transform _transformToFollow;
    private Vector3 _newPosition;
    private Vector3 _startCameraPosition;

    private void Awake()
    {
        _startCameraPosition = transform.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_transformToFollow == null)
        {
            return;
        }
        _newPosition = _transformToFollow.position + _startCameraPosition;
        transform.position = Vector3.Slerp(transform.position, _newPosition, 0.03f);
    }
}
