﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Wheel;
    public GameObject WheelAxis;
    public GameObject Handle;
    public GameObject HandleAxis;
    public GameObject Bike;
    public Text scoreText;

    public float respawn_offset;
    public float MaxRotation = 30f;
    public float Speed = 5f;
    public float TurnSpeed = 60f;
    public float TurnFactor = 2.5f;

    [HideInInspector] public int score;
    [HideInInspector] public int fading;

    private Rigidbody _rigidBody;

    private Quaternion Rot;

    private int end;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();

        Rot = Bike.transform.rotation;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
            float wheelAngle = (Wheel.transform.localEulerAngles.y > 180) ? Wheel.transform.localEulerAngles.y - 360 : Wheel.transform.localEulerAngles.y;

            if (InputManager.GetAxis("Horizontal") > 0)
            {
                if (wheelAngle < MaxRotation)
                {
                    float deltaAngle = TurnSpeed * Time.deltaTime;
                    if (wheelAngle + deltaAngle > MaxRotation)
                    {
                        deltaAngle = MaxRotation - wheelAngle;
                    }

                    // Turn wheel to the right
                    Wheel.transform.RotateAround(WheelAxis.transform.position, Vector3.up, deltaAngle);
                    Handle.transform.RotateAround(HandleAxis.transform.position, Vector3.up, deltaAngle);
                }
            }
            else if (InputManager.GetAxis("Horizontal") < 0)
            {
                if (wheelAngle > -MaxRotation)
                {
                    float deltaAngle = TurnSpeed * Time.deltaTime;
                    if (wheelAngle - deltaAngle < -MaxRotation)
                    {
                        deltaAngle = MaxRotation + wheelAngle;
                    }

                    // Turn wheel to the left
                    Wheel.transform.RotateAround(WheelAxis.transform.position, Vector3.down, deltaAngle);
                    Handle.transform.RotateAround(HandleAxis.transform.position, Vector3.down, deltaAngle);
                }
            }
            else
            {
                // Rotate wheel back to original angle
                if (wheelAngle < 0f)
                {
                    float deltaAngle = TurnSpeed * Time.deltaTime;
                    if (wheelAngle + deltaAngle > 0f)
                    {
                        deltaAngle = 0f - wheelAngle;
                    }
                    Wheel.transform.RotateAround(WheelAxis.transform.position, Vector3.up, deltaAngle);
                    Handle.transform.RotateAround(HandleAxis.transform.position, Vector3.up, deltaAngle);
                }
                else if (wheelAngle > 0f)
                {
                    float deltaAngle = TurnSpeed * Time.deltaTime;
                    if (wheelAngle - deltaAngle < 0f)
                    {
                        deltaAngle = wheelAngle;
                    }
                    Wheel.transform.RotateAround(WheelAxis.transform.position, Vector3.down, deltaAngle);
                    Handle.transform.RotateAround(HandleAxis.transform.position, Vector3.down, deltaAngle);
                }
            }

            wheelAngle = (Wheel.transform.localEulerAngles.y > 180) ? Wheel.transform.localEulerAngles.y - 360 : Wheel.transform.localEulerAngles.y;
        if (fading == 0)
        {
            // Needs to be modified for speed metric
            if (InputManager.GetAxis("Vertical") > 0)
            {
                // Turn wheel forward
                Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, wheelAngle, 0f) * TurnFactor * Time.deltaTime);
                _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotation);

                Vector3 movementVec = gameObject.transform.forward * InputManager.GetAxis("Vertical") * Speed * Time.deltaTime;
                _rigidBody.MovePosition(_rigidBody.position + movementVec);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn") || other.gameObject.CompareTag("Mailbox") || other.gameObject.CompareTag("EndFence") || other.gameObject.CompareTag("Cars"))
        {
            if (other.gameObject.CompareTag("EndFence"))
                end = 1;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        //fade out
        fading = 1;
        ani.SetInteger("fading", 1);
        yield return new WaitForSecondsRealtime(0.4f);
        //reset Bike
        Bike.transform.rotation = Rot;
        if (end == 1)
        {
            Bike.transform.position = new Vector3(0, Bike.transform.position.y, Bike.transform.position.z + 3);
        }
        else
        {
            if (Bike.transform.position.z % 100 < respawn_offset && Bike.transform.position.z % 100 > -15)
            {
                Bike.transform.position = new Vector3(0, Bike.transform.position.y, Bike.transform.position.z + 1);
            }
            else
            {
                Bike.transform.position = new Vector3(0, Bike.transform.position.y, Bike.transform.position.z - respawn_offset);
            }
        }
        //fade in
        ani.SetInteger("fading", 0);
        yield return new WaitForSecondsRealtime(0.5f);
        fading = 0;
        end = 0;
    }
}
