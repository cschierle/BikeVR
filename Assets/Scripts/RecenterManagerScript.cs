using UnityEngine;

public class RecenterManagerScript : MonoBehaviour
{
    public float HoldDuration = 2f;

    private float _timer;
    private bool _recentered = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (InputManager.GetRecenterButtonDown())
        {
            _timer = Time.time;
        }
        else if (InputManager.GetRecenterButton())
        {
            if (Time.time - _timer > HoldDuration && !_recentered)
            {
#if UNITY_EDITOR
                Debug.Log("Recenter " + Time.time);
#elif UNITY_ANDROID
                GvrCardboardHelpers.Recenter();
#endif
                _recentered = true;
            }
        }
        else if (InputManager.GetRecenterButtonUp())
        {
            _recentered = false;
        }
    }
}
