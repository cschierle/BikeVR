using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndFence : MonoBehaviour
{
    private TutorialManager tutorialManager;
    
    void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.CollisionWithEndFence();
        }
    }
}
