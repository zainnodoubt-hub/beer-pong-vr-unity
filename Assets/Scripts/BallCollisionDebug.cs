using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionDebug : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BALL COLLIDED WITH: " + collision.gameObject.name);
    }
}