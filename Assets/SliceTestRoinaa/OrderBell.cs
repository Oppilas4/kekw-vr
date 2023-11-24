using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kekw.Interaction;
public class OrderBell : MonoBehaviour
{
    public UnityEvent _orderReady = new UnityEvent();

    // Hand that presses button
    string _handTag = null;

    LeftHapticBroker _leftHapticBroker;
    RightHapticBroker _rightHapticBroker;

    const float _hapticForce = .5f;
    const float _hapticDuration = .25f;

    private void SendHapticFeedback(float amplitude, float duration)
    {
        if (_leftHapticBroker == null || _rightHapticBroker == null)
        {
            _leftHapticBroker = FindAnyObjectByType<LeftHapticBroker>();
            _rightHapticBroker = FindAnyObjectByType<RightHapticBroker>();
        }

        if (_handTag != null && _handTag.Equals("LeftHand"))
        {
            _leftHapticBroker.TriggerHapticFeedback(amplitude, duration);
        }

        if (_handTag != null && _handTag.Equals("RightHand"))
        {
            _rightHapticBroker.TriggerHapticFeedback(amplitude, duration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightHand") || collision.gameObject.CompareTag("LeftHand"))
        {
            _handTag = collision.gameObject.tag;
            SendHapticFeedback(_hapticForce, _hapticDuration);
            _orderReady.Invoke();
        }
    }
}
