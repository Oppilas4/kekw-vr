namespace Kekw.Common
{
    /// <summary>
    /// Common behaviour between all components that require or respond to user input.
    /// </summary>
    interface IButtonBinder
    {
        /// <summary>
        /// Method is used to bind inputs to actions.
        /// </summary>
        public void BindInputs();

        /// <summary>
        /// Method is used to unbind inputs from actions like in OnDestroy.
        /// </summary>
        public void UnBindInputs();
    }
}
