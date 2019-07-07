using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Wheel;
    public GameObject WheelAxis;
    public GameObject Handle;
    public GameObject HandleAxis;
    public GameObject Bike;
    public GameObject NewsPaperSpawn;
    public GameObject NewsPaperPrefab;
    public GameObject MainCamera;
    public Text scoreText;

    public bool CanThrowNewspaper = true;

    public float respawn_offset;
    public float MaxRotation = 30f;
    public float SpeedFactor = 7.5f;
    public float TurnSpeed = 60f;
    public float TurnFactor = 2.5f;
    public float ThrowDeadAngle = 0.3f;

    [HideInInspector] public int score;
    [HideInInspector] public int delivered;
    [HideInInspector] public int fading;

    private Rigidbody _rigidBody;
    private Quaternion Rot;
    private Animator ani;
    private Speedometer _speedometer;

    private GameControlls gameControlls;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        _speedometer = GameObject.FindGameObjectWithTag("Speedometer").GetComponent<Speedometer>();

        Rot = Bike.transform.rotation;

        delivered = 0;
        UpdateScore(0);

        if (SceneManager.GetActiveScene().name.Equals("PaperboyScene"))
        {
            gameControlls = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlls>();
        }
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
            if (InputManager.GetAxis("Vertical") > 0 || _speedometer.Speed > 0f)
            {
                // Turn wheel forward
                Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, wheelAngle, 0f) * TurnFactor * Time.deltaTime);
                _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotation);

                Vector3 movementVec = gameObject.transform.forward * SpeedFactor * _speedometer.Speed * Time.deltaTime;
                _rigidBody.MovePosition(_rigidBody.position + movementVec);
            }

            if (CanThrowNewspaper && InputManager.GetFire1ButtonDown() && MainCamera.transform.rotation.x < ThrowDeadAngle)
            {
                // spawn and throw newspaper
                Rigidbody newspaper = Instantiate(NewsPaperPrefab, NewsPaperSpawn.transform.position, NewsPaperSpawn.transform.rotation).GetComponent<Rigidbody>();
                newspaper.transform.Rotate(0f, 0f, 90f);
                // effect size of applied force controlled by newspaper's drag property in inspector
                newspaper.AddForce(NewsPaperSpawn.transform.forward * 1000);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn") || other.gameObject.CompareTag("Mailbox") || other.gameObject.CompareTag("Cars"))
        {
            var position = new Vector3(0f, 0f, 0f);

            if (Bike.transform.position.z % 100 < respawn_offset && Bike.transform.position.z % 100 > -15)
            {
                position = new Vector3(0, Bike.transform.position.y, Bike.transform.position.z + 1);
            }
            else
            {
                position = new Vector3(0, Bike.transform.position.y, Bike.transform.position.z - respawn_offset);
            }

            ResetToPosition(position);
        }
        else if (other.gameObject.CompareTag("EndFence"))
        {
            ResetToPosition(new Vector3(0, Bike.transform.position.y, Bike.transform.position.z + 3));
        }
        else if (other.gameObject.CompareTag("Respawnother"))
        {
            var position = new Vector3(0f, 0f, 0f);
            position = new Vector3(-5.5f, Bike.transform.position.y, Bike.transform.position.z + 1);
            ResetToPosition(position);
        }
    }

    public void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        delivered++;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(Ending());
        }
    }
    public void ResetToPosition(Vector3 position)
    {
        StartCoroutine(Fade(position));
    }

    IEnumerator Fade(Vector3 position)
    {
        //fade out
        fading = 1;
        ani.SetInteger("fading", 1);
        yield return new WaitForSecondsRealtime(0.4f);

        //reset Bike
        Bike.transform.rotation = Rot;
        Bike.transform.position = position;
        _speedometer.ResetSpeed();

        //fade in
        ani.SetInteger("fading", 0);
        yield return new WaitForSecondsRealtime(0.5f);
        fading = 0;
    }

    IEnumerator Ending()
    {
        //fade out
        fading = 1;
        ani.SetInteger("fading", 1);
        yield return new WaitForSecondsRealtime(0.4f);

        GameObject[] delete = GameObject.FindGameObjectsWithTag("Scenes");
        for (int j = 0; j < delete.Length; j++)
        {
            Destroy(delete[j]);
        }

        gameControlls.DisplayEndScreen(score, delivered);
        ani.SetInteger("fading", 0);
    }

}
