namespace MusicCityMiddleware.Middleware
{
    public class OddNumberMaker : INumberMaker
    {
        private int lastNumber = -1;

        public int MakeNumber()
        {
            lastNumber += 2;
            return lastNumber;
        }
    }
}