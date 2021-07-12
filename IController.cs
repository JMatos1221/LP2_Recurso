namespace LP2_Recurso
{
    /// <summary>
    /// Controller Interface
    /// </summary>
    public interface IController
    {
        /// Abstract method to pause the simulation
        void TogglePause();
        /// Abstract method to quit the simulation
        void Quit();
    }
}