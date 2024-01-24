using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveDial : MonoBehaviour
{
    [SerializeField] Transform linkedDial;
    [SerializeField] private Axis rotationAxis = Axis.Z;
    [SerializeField] private int snapRotationAmount = 25;
    [SerializeField] private float angleTolerance;

    private XRBaseInteractor interactor;
    private float startAngle;
    private bool requiresStartAngle = true;
    private bool shouldGetHandRotation = false;
    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();

    private void OnEnable()
    {
        grabInteractor.selectEntered.AddListener(GrabbedBy);
        grabInteractor.selectExited.AddListener(GrabEnd);
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
        switch ((int)rotationAxis)
        {
            case 0:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x + snapRotationAmount,
                                                          linkedDial.localEulerAngles.y,
                                                          linkedDial.localEulerAngles.z);
                break;
            case 1:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x,
                                                          linkedDial.localEulerAngles.y + snapRotationAmount,
                                                          linkedDial.localEulerAngles.z);
                break;
            case 2:
            default:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x,
                                                          linkedDial.localEulerAngles.y,
                                                          linkedDial.localEulerAngles.z + snapRotationAmount);
                break;
        }

        if (TryGetComponent<IDial>(out IDial dial))
            dial.DialChanged(linkedDial.localEulerAngles[(int)rotationAxis]);
    }

    private void RotateDialAntiClockwise()
    {
        switch ((int)rotationAxis)
        {
            case 0:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x - snapRotationAmount,
                                                          linkedDial.localEulerAngles.y,
                                                          linkedDial.localEulerAngles.z);
                break;
            case 1:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x,
                                                          linkedDial.localEulerAngles.y - snapRotationAmount,
                                                          linkedDial.localEulerAngles.z);
                break;
            case 2:
            default:
                linkedDial.localEulerAngles = new Vector3(linkedDial.localEulerAngles.x,
                                                          linkedDial.localEulerAngles.y,
                                                          linkedDial.localEulerAngles.z - snapRotationAmount);
                break;
        }

        if (TryGetComponent<IDial>(out IDial dial))
            dial.DialChanged(linkedDial.localEulerAngles[(int)rotationAxis]);
    }



}