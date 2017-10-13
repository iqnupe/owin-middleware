using System;

namespace MiddlewareDevspace.Devspace
{
    public class RandomNumberMaker : INumberMaker
    {
        private Random rng = new Random();

        public int MakeNumber()
        {
            return rng.Next(10, 21);
        }
    }
}