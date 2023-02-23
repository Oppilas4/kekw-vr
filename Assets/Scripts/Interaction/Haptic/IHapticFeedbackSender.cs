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
        /// <param name="amplitude">How much controller shakes</param>
        /// <param name="duration">How long controller will shake</param>
        public void SendHapticFeedback(float amplitude, float duration);
    }       
}
