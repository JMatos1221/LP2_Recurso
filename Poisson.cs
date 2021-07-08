using System;

namespace LP2_Recurso
{
    public class Poisson
    {
        private double gridValue;
        private double lambda;
        private Random rnd;

        public Poisson(int xDim, int yDim)
        {
            gridValue = xDim * yDim / 3.0;
            rnd = new Random();
        }

        public int Next(float rateExp)
        {
            int k = 0;
            double l = Math.Pow(Math.E, -lambda);
            double p = 1;

            lambda = gridValue * Math.Pow(10, rateExp);

            do
            {
                k += 1;

                double u = rnd.NextDouble();

                p *= u;
            } while (p > l);

            return k - 1;
        }
    }
}