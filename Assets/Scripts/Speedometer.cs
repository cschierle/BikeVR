using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private IBLEManager _bleManager;
    public float Speed { get; set; }

    private int _prevValue;
    private int _currentValue;

    public float _minimalSpeed = 1f;
    private float _lastSpeed;

    public float _accTime = 1f;
    public float _decTime = 1f;

    public float _minimalRevolutionCount = 5f;
    public float _resetTime = 1f;
    private float _revolutionCount;

    private float _deltaTime;
    private float _prevTime;
    private float _timer;

    private States _state;

    public enum States
    {
        STILL,
        ACC,
        PED,
        DEC
    }

    void Start()
    {
        Speed = 0f;
        _prevValue = 0;
        _state = States.STILL;

#if UNITY_EDITOR
        _bleManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<IBLEManager>();
#elif UNITY_ANDROID && !UNITY_EDITOR
        _bleManager = GameObject.FindGameObjectWithTag("BLEManager").GetComponent<IBLEManager>();
#endif
    }

    void Update()
    {
        var now = Time.time;
        _currentValue = _bleManager.GetCharacteristic<IRCharacteristic>().Value;

        if (_currentValue > _prevValue)
        {
            _deltaTime = now - _prevTime;
            _prevTime = now;
            _revolutionCount++;

            if (_revolutionCount == 1)
            {
                _timer = 0;
                _lastSpeed = Speed;
                _state = States.ACC;
            }
            else if (_revolutionCount > _minimalRevolutionCount && Speed == _minimalSpeed)
            {
                _state = States.PED;
            }
        }


        if (now - _prevTime > _resetTime && (_state == States.PED || _state == States.ACC))
        {
            // no sensor value within last _resetTime seconds
            _revolutionCount = 0;
            _state = States.DEC;
            _timer = 0;
            _lastSpeed = Speed;
        }

        switch (_state)
        {
            case States.ACC:
                {
                    _timer += Time.deltaTime;
                    if (_timer > _accTime) _timer = _accTime;
                    Speed = Mathf.Lerp(_lastSpeed, _minimalSpeed, _timer / _accTime);
                    break;
                }
            case States.PED:
                {
                    Speed = _minimalSpeed + AddBonusSpeed(_deltaTime);
                    break;
                }
            case States.DEC:
                {
                    _timer += Time.deltaTime;
                    if (_timer > _decTime) _timer = _decTime;
                    Speed = Mathf.Lerp(_lastSpeed, 0f, _timer / _decTime);

                    if (Speed < 0.01)
                    {
                        _state = States.STILL;
                        _timer = 0;
                    }

                    break;
                }
            case States.STILL:
                {
                    Speed = 0f;
                    break;
                }
        }

        _prevValue = _currentValue;
    }

    public void ResetSpeed()
    {
        Speed = 0f;
        _timer = 0f;
        _state = States.STILL;
        _revolutionCount = 0;
    }

    private float AddBonusSpeed(float delta)
    {
        // 0.5s is the average time in between two sensor readings
        return Math.Max(0.5f - delta, 0f) * 5f;
    }
}
