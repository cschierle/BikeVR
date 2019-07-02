using UnityEngine;

public class SpinningScript : MonoBehaviour
{
    public float RotateSpeed = 200f;

    private RectTransform _rectComponent;

    private void Start()
    {
        _rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectComponent.Rotate(0f, 0f, RotateSpeed * Time.deltaTime);
    }
}
