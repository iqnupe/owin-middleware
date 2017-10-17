using System;

namespace DevUpMiddleware.Devup
{
    public class Over9000NumberMaker : INumberMaker
    {
        private Random rng = new Random();

        public int MakeNumber()
        {
            return rng.Next(9001, int.MaxValue);
        }
    }
}