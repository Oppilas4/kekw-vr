using System;
using UnityEngine;

namespace Kekw.Animation
{
    /// <summary>
    /// Animates clock time indicator arms.
    /// Sets clock to current time and counts from there.
    /// </summary>
    public class ClockAnimation : MonoBehaviour
    {
        /// <summary>
        /// Hour pointer model
        /// </summary>
        [SerializeField]
        Transform hourHands;

        /// <summary>
        /// Minute pointer model
        /// </summary>
        [SerializeField]
        Transform minuteHands;

        /// <summary>
        /// Clock rotation direction
        /// </summary>
        [SerializeField]
        float minuteRevertor, hourRevertor;


        // Update is called once per frame
        void Update()
        {
            DateTime currentTime = DateTime.Now;
            float second = currentTime.Second;
            float minute = currentTime.Minute;
            float hour = currentTime.Hour;

            float secondAngle = 360 * (second/60);
            float minuteAngle = 360*(minute/60)+(secondAngle/60);
            float hourAngle=360*(hour/12)+(minuteAngle/12);

            minuteHands.localRotation = Quaternion.Euler(minuteRevertor*minuteAngle, 0, 0);
            hourHands.localRotation = Quaternion.Euler(hourRevertor*hourAngle, 0, 0);
        }
    }
}