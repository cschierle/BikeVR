using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    public float MinSpeed = 7f;
    public float MaxSpeed = 15f;

    private float Speed;
    private TutorialManager tutorialManager;
    
    void Start()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
        tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
    }
    
    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.CollisionWithCar();
        }
        else if (other.CompareTag("RespawnLR"))
        {
            tutorialManager.SpawnNewCar(TutorialManager.CarDirection.LeftToRight);
            Destroy(gameObject);
        }
        else if (other.CompareTag("RespawnRL"))
        {
            tutorialManager.SpawnNewCar(TutorialManager.CarDirection.RightToLeft);
            Destroy(gameObject);
        }
    }
}
