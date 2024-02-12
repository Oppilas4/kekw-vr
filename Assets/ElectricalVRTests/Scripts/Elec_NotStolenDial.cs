using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_NotStolenDial : MonoBehaviour
{
    [SerializeField] Transform linkedDial;
    [SerializeField] private Axis rotationAxis = Axis.Z;
    [SerializeField] private int snapRotationAmount = 25;
    [SerializeField] private float angleTolerance;
    [SerializeField] private bool limitRotation = false;  // New field for enabling/disabling rotation limitation
    [SerializeField] private float maxRotationAngle = 90f;  // New field for rotation limit
    [SerializeField] private bool restrictAntiClockwise = false;  // New field for restricting anticlockwise rotation

    private XRBaseInteractor interactor;
    private float startAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;
    private Quaternion originalRotation;  // New class variable to store the original rotation
    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();

    private void OnEnable()
    {
        grabInteractor.selectEntered.AddListener(GrabbedBy);
        grabInteractor.selectExited.AddListener(GrabEnd);

        originalRotation = linkedDial.localRotation;
    }
    private void OnDisable()
    {
        grabInteractor.selectEntered.RemoveListener(GrabbedBy);
        grabInteractor.selectExited.RemoveListener(GrabEnd);
    }
    public enum Axis
    {
        X,
        Y,
        Z
    }
    private void GrabEnd(SelectExitEventArgs arg0)
    {
        shouldGetHandRotation = false;
        requiresStartAngle = true;
    }
    private void GrabbedBy(SelectEnterEventArgs arg0)
    {
        interactor = GetComponent<XRGrabInteractable>().interactorsSelecting.FirstOrDefault() as XRBaseInteractor;
        if (interactor != null)
        {
            shouldGetHandRotation = true;
            startAngle = 0f;
        }
    }
    void Update()
    {
        if (shouldGetHandRotation)
        {
            var rotationAngle = GetInteractorRotation(); //gets the current controller angle
            GetRotationDistance(rotationAngle);
        }
    }
    public float GetInteractorRotation() => interactor.GetComponent<Transform>().eulerAngles.z;

    #region TheMath!
    private void GetRotationDistance(float currentAngle)
    {
        if (!requiresStartAngle)
        {
            var angleDifference = Mathf.Abs(startAngle - currentAngle);

            if (angleDifference > angleTolerance)
            {
                if (angleDifference > 270f) //checking to see if the user has gone from 0-360 - a very tiny movement but will trigger the angletolerance
                {
                    float angleCheck;

                    if (startAngle < currentAngle)
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);

                        if (angleCheck < angleTolerance)
                            return;
                        else
                        {
                            RotateDialClockwise();
                            startAngle = currentAngle;
                        }
                    }
                    else if (startAngle > currentAngle)
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);

                        if (angleCheck < angleTolerance)
                            return;
                        else
                        {
                            RotateDialAntiClockwise();
                            startAngle = currentAngle;
                        }
                    }
                }
                else
                {
                    if (startAngle < currentAngle)
                    {
                        RotateDialAntiClockwise();
                        startAngle = currentAngle;
                    }
                    else if (startAngle > currentAngle)
                    {
                        RotateDialClockwise();
                        startAngle = currentAngle;
                    }
                }
            }
        }
        else
        {
            requiresStartAngle = false;
            startAngle = currentAngle;
        }
    }
    #endregion
    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;
    private void RotateDialClockwise()
    {
        // Check for rotation limit
        if (limitRotation)
        {
            float clampedAngle = Mathf.Clamp(linkedDial.localEulerAngles[(int)rotationAxis] + snapRotationAmount, 0f, maxRotationAngle);
            linkedDial.localEulerAngles = GetEulerAnglesWithAxis(clampedAngle);
        }
        else
        {
            linkedDial.localEulerAngles = GetEulerAnglesWithAxis(linkedDial.localEulerAngles[(int)rotationAxis] + snapRotationAmount);
        }

        if (TryGetComponent<IElecDial>(out IElecDial dial))
            dial.DialChanged(linkedDial.localEulerAngles[(int)rotationAxis]);
    }
    private void RotateDialAntiClockwise()
    {
        // Check for rotation limit
        if (limitRotation)
        {
            float clampedAngle = Mathf.Clamp(linkedDial.localEulerAngles[(int)rotationAxis] - snapRotationAmount, 0f, maxRotationAngle);

            // Check for restricting anticlockwise rotation
            if (restrictAntiClockwise && clampedAngle > linkedDial.localEulerAngles[(int)rotationAxis])
                clampedAngle = linkedDial.localEulerAngles[(int)rotationAxis];

            linkedDial.localEulerAngles = GetEulerAnglesWithAxis(clampedAngle);
        }
        else
        {
            float newAngle = linkedDial.localEulerAngles[(int)rotationAxis] - snapRotationAmount;

            // Check for restricting anticlockwise rotation
            if (restrictAntiClockwise && newAngle > linkedDial.localEulerAngles[(int)rotationAxis])
                newAngle = linkedDial.localEulerAngles[(int)rotationAxis];

            linkedDial.localEulerAngles = GetEulerAnglesWithAxis(newAngle);
        }
        if (TryGetComponent<IElecDial>(out IElecDial dial))
            dial.DialChanged(linkedDial.localEulerAngles[(int)rotationAxis]);
    }
    // Helper method to construct Vector3 with updated axis value
    private Vector3 GetEulerAnglesWithAxis(float updatedValue)
    {
        Vector3 eulerAngles = linkedDial.localEulerAngles;
        eulerAngles[(int)rotationAxis] = updatedValue;
        return eulerAngles;
    }
}