using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject cupRoot;
    public int scoreValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        if (gameManager != null)
            gameManager.AddScore(scoreValue);

        if (cupRoot != null)
            cupRoot.SetActive(false);   // this must be here
    }
}