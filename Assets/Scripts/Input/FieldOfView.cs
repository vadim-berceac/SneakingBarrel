using UnityEngine;

internal class FieldOfView : MonoBehaviour
{
    private Vector3 _playerPosition = Vector3.zero;
    public Vector3 PlayerPosition => _playerPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        _playerPosition = other.transform.position;

        Debug.LogWarning("вижу игрока");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        _playerPosition = Vector3.zero;
    }
}