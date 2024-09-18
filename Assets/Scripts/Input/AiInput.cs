using UnityEngine;

public class AiInput : MonoBehaviour, IInputSource
{
    private Vector2 _direction = Vector2.zero;
    void Start()
    {
        InvokeRepeating(nameof(RandomUnitVector), 3f, 3f);
    }

    private void RandomUnitVector()
    {
        float random = Random.Range(0f, 260f);
        _direction =  new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }
}
