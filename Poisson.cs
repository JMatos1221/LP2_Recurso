using System;

namespace LP2_Recurso
{
    public class Poisson
    {
        /// Value calculated with grid size
        private double gridValue;
        /// Lambda value for the Poisson algorithm
        private double lambda;
        /// Constant value for the Poisson algorithm
        const int STEP = 500;
        /// Random instance
        private Random rnd;

        /// <summary>
        /// Poisson constructor, with grid dimension
        /// </summary>
        /// <param name="xDim">Grid Width</param>
        /// <param name="yDim">Grid Height</param>
        public Poisson(int xDim, int yDim)
        {
            /// Calculates the grid constant value for lambda calculation
            gridValue = xDim * yDim / 3.0;
            /// Initializes the random instance
            rnd = new Random();
        }

        /// <summary>
        /// Calculates the number of events with the given rate
        /// </summary>
        /// <param name="rateExp">Rate exponetial</param>
        /// <returns>Number of events that will occur 
        /// in the next frame</returns>
        public int Next(double rateExp)
        {
            /// Calculates the lambda with the given exponential rate
            double lambdaLeft = gridValue * Math.Pow(10, rateExp);
            /// Value to multiply by a random double to loop the method
            double p = 1;
            /// Number of events, returned variable -1
            int k = 0;

            do
            {
                k += 1;
                p *= rnd.NextDouble();

                while (p < 1 && lambdaLeft > 0)
                {
                    if (lambdaLeft > STEP)
                    {
                        p *= Math.Pow(Math.E, STEP);
                        lambdaLeft -= STEP;
                    }
                    else
                    {
                        p *= Math.Pow(Math.E, lambdaLeft);
                        lambdaLeft = 0;
                    }
                }

            } while (p > 1);

            return k - 1;
        }
    }
}