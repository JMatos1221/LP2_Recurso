using System;

namespace LP2_Recurso
{
    public class Poisson
    {
        private double gridValue;
        private double lambda;
        const int STEP = 500;
        private Random rnd;

        public Poisson(int xDim, int yDim)
        {
            gridValue = xDim * yDim / 3.0;
            rnd = new Random();
        }

        public int Next(double rateExp)
        {
            double lambdaLeft = gridValue * Math.Pow(10, rateExp);
            double p = 1;
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