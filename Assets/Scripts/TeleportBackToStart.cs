using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TeleportBackToStart : MonoBehaviour
{
    [Header("References")]
    public Transform xrOrigin;      // XR Origin (Action-based)
    public Transform startPosition; // The StartPosition transform by the table

    [Header("Input")]
    [Tooltip("Which hand's B/Y button to use (usually RightHand).")]
    public XRNode handNode = XRNode.RightHand;

    private bool wasButtonPressedLastFrame = false;

    void Update()
    {
        if (xrOrigin == null || startPosition == null)
            return;

        // Get the device (controller) for the chosen hand
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);

        // On Quest: B button on right controller = secondaryButton
        bool isPressed;
        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed))
        {
            // Detect the button "down" event (pressed this frame, not last frame)
            if (isPressed && !wasButtonPressedLastFrame)
            {
                TeleportToStart();
            }

            wasButtonPressedLastFrame = isPressed;
        }
    }

    void TeleportToStart()
    {
        // Keep height/orientation exactly like StartPosition
        xrOrigin.position = startPosition.position;
        xrOrigin.rotation = startPosition.rotation;
    }
}