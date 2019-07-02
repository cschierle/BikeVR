using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public GameObject Player;
    
    private float checkPointStreet = 90.0f;

    private PlayerController playerController;
    private Vector3 carSpawnPointLR = new Vector3(-37f, 1f, 98.5f);
    private Vector3 carSpawnPointRL = new Vector3(37f, 1f, 103.5f);

    public enum CarDirection
    {
        LeftToRight,
        RightToLeft
    }
    
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();

        SpawnNewCar(CarDirection.LeftToRight);
        SpawnNewCar(CarDirection.RightToLeft);
    }
    
    void Update()
    {
        
    }

    public void EndTutorial()
    {

    }

    public void CollisionWithCar()
    {
        playerController.ResetToPosition(new Vector3(0f, Player.transform.position.y, checkPointStreet));
    }

    public void SpawnNewCar(CarDirection carDirection)
    {
        switch (carDirection)
        {
            case CarDirection.LeftToRight:
                {
                    Instantiate(CarPrefab, carSpawnPointLR, Quaternion.Euler(0f, 90f, 0f));
                    break;
                }
            case CarDirection.RightToLeft:
                {
                    Instantiate(CarPrefab, carSpawnPointRL, Quaternion.Euler(0f, 270f, 0f));
                    break;
                }
        }
    }
}
