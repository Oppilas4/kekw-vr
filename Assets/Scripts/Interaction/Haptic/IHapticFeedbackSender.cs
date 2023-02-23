namespace Kekw.Interaction
{
    /// <summary>
    /// Components that want to send haptic feedback implement this interface.
    /// </summary>
    public interface IHapticFeedbackSender
    {
        /// <summary>
        /// Called when haptic feed. Decide in this method wether to send left, right or both controllers forces and times.
        /// </summary>
        public void SendHapticFeedback(float amplitude, float duration);
    } 
}
