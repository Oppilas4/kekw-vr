using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_Haptics : MonoBehaviour
{
    List<XRBaseController> Controllers;
    public float Duration;
    [Range(0f, 1f)]
    public float Amplitude;
    void Start()
    {
        Controllers = FindObjectsOfType<XRBaseController>().ToList();
    }

    [ContextMenu("Vibration")]
    public void SendHapticsTest()
    {
        foreach (var VR in Controllers)
        {
            VR.SendHapticImpulse(Amplitude, Duration);
        }
    }
    public void SendHaptics(float Amplitude,float Duration)
    {
        foreach (var VR in Controllers)
        {
            VR.SendHapticImpulse(Amplitude, Duration);
        }
    }
}
