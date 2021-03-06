﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLEManagerMock : MonoBehaviour, IBLEManager
{
    public float Interval = 0.5f;

    private List<Characteristic> _characteristics;
    private IRCharacteristic _irCharacteristic;

    private int _value;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _irCharacteristic = new IRCharacteristic();

        _value = 0;

        _characteristics = new List<Characteristic>(
            new Characteristic[] { _irCharacteristic }
            );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _timer += Time.deltaTime;
            if (_timer > Interval)
            {
                _timer -= Interval;
                _value++;
                _irCharacteristic.UpdateValue(BitConverter.GetBytes(_value));
            }
        }
    }
    
    public T GetCharacteristic<T>() where T : Characteristic
    {
        return _characteristics.Find(c => c.GetType() == typeof(T)) as T;
    }
}
