using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallController : MonoBehaviour
{
    [Header("Auto Reset Settings")]
    [Tooltip("If the ball goes below this height, it will reset (world Y).")]
    public float resetHeight = 0.2f;

    [Tooltip("If the ball goes farther than this from its start point, it will reset.")]
    public float maxDistanceFromStart = 5f;

    [Tooltip("Delay in seconds before resetting after it has fallen.")]
    public float resetDelay = 0.5f;

    private Rigidbody rb;
    private XRGrabInteractable grab;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float fallTimer = -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        // Remember where the ball starts in the scene
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // If the ball is currently being held, never reset it
        if (grab != null && grab.isSelected)
        {
            fallTimer = -1f;
            return;
        }

        bool shouldStartResetTimer = false;

        // Condition 1: ball fell below a certain height (e.g. hit the floor)
        if (transform.position.y < resetHeight)
        {
            shouldStartResetTimer = true;
        }

        // Condition 2: ball rolled too far away from its original spawn
        if (Vector3.Distance(transform.position, startPosition) > maxDistanceFromStart)
        {
            shouldStartResetTimer = true;
        }

        if (shouldStartResetTimer)
        {
            // Start or continue the timer
            if (fallTimer < 0f)
                fallTimer = Time.time;

            // If enough time has passed, reset
            if (Time.time - fallTimer >= resetDelay)
            {
                ResetBall();
                fallTimer = -1f;
            }
        }
        else
        {
            // If it's not in a "fallen" state, clear the timer
            fallTimer = -1f;
        }
    }

    public void ResetBall()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}