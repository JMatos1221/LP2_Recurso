namespace LP2_Recurso
{
    /// <summary>
    /// Console View Interface
    /// </summary>

    public interface IConsoleView
    {
        /// <summary>
        /// Abstract method to print the grid
        /// </summary>
        /// <param name="grid">Grid to print</param>
        void Print(Grid grid);

        /// <summary>
        /// Abstract method to update the grid
        /// </summary>
        /// <param name="grid">Grid to update</param>
        void Update(Grid grid);

        /// <summary>
        /// Abstract method to read the user input
        /// </summary>
        void GetInput();

        /// <summary>
        /// View end actions
        /// </summary>
        /// <param name="grid">Grid to get dimension from</param>
        void End(Grid grid);
    }
}