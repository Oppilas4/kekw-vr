
namespace Kekw.Mission
{
    /// <summary>
    /// Interface that all mission managers implement.
    /// </summary>
    interface IMissionManager
    {
        /// <summary>
        /// Mission managers must provide OnMIssionStart method. What happens on mission start?
        /// </summary>
        public void OnMissionStart();
        /// <summary>
        /// Mission managers must provide OnMissionStop method. What happens on mission stop?
        /// </summary>
        public void OnMissionStop();

        /// <summary>
        /// What happens on mission fail.
        /// </summary>
        public void OnMissionFail();

        /// <summary>
        /// What happens when user completes mission succesfully.
        /// </summary>
        public void OnMissionSuccess();
    }
}
