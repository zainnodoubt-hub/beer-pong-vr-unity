using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BallThrowGuide : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lineRenderer;
    public Transform aimOrigin;       // usually the throwing hand/controller

    [Header("Trajectory Preview")]
    public int linePoints = 30;
    public float timeBetweenPoints = 0.05f;
    public float previewSpeed = 5f;   // how strong the preview arc looks

    [Header("Input")]
    [Tooltip("Which hand's A/X button to use (usually RightHand for A).")]
    public XRNode handNode = XRNode.RightHand;

    private XRGrabInteractable grab;
    private Rigidbody rb;
    private bool isHeld = false;
    private bool wasThrowButtonPressedLastFrame = false;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    void OnEnable()
    {
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
            grab.selectExited.AddListener(OnReleased);
        }
    }

    void OnDisable()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;

        // While held, lock physics so it follows the hand cleanly.
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (lineRenderer != null)
            lineRenderer.enabled = true;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        // Normal release without A: just drop
        isHeld = false;

        if (lineRenderer != null)
            lineRenderer.enabled = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            // No extra velocity -> it will just fall
        }
    }

    void Update()
    {
        if (!isHeld)
            return;

        // 1) Update the guide line while held
        if (lineRenderer != null)
            DrawTrajectory();

        // 2) Check for A button press to auto-throw
        HandleThrowInput();
    }

    void HandleThrowInput()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);

        // On Quest, A button = primaryButton on the right controller
        bool isPressed;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed))
        {
            // Detect button "down" event
            if (isPressed && !wasThrowButtonPressedLastFrame)
            {
                ForceThrow();
            }

            wasThrowButtonPressedLastFrame = isPressed;
        }
    }

    void ForceThrow()
    {
        if (!isHeld || grab == null || rb == null)
            return;

        // Mark as no longer held so Update() stops drawing guide
        isHeld = false;

        // Hide the guide line
        if (lineRenderer != null)
            lineRenderer.enabled = false;

        // Disable grab temporarily so XR stops controlling it
        grab.enabled = false;

        // Enable physics and apply our custom throw velocity
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.maxLinearVelocity = 100f;       // REMOVE the velocity clamp
        rb.velocity = GetPreviewVelocity();
        rb.angularVelocity = Vector3.zero;

        // Re-enable grabbing after a short delay
        StartCoroutine(ReenableGrabAfterDelay(0.2f));
    }

    IEnumerator ReenableGrabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        grab.enabled = true;
    }

    Vector3 GetPreviewVelocity()
    {
        // Use controller forward direction for the trajectory
        Vector3 dir;
        if (aimOrigin != null)
            dir = aimOrigin.forward;
        else
            dir = transform.forward;

        dir = dir.normalized;
        return dir * previewSpeed;
    }

    void DrawTrajectory()
    {
        Vector3 startPos = transform.position;
        Vector3 startVel = GetPreviewVelocity();

        lineRenderer.positionCount = linePoints;

        for (int i = 0; i < linePoints; i++)
        {
            float t = i * timeBetweenPoints;
            // s = ut + 1/2 at^2
            Vector3 point = startPos + startVel * t + 0.5f * Physics.gravity * t * t;
            lineRenderer.SetPosition(i, point);
        }
    }
}