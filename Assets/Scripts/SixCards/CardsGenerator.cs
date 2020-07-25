
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CardsGenerator
    {
        private int maxV;
        private int seed;

        public CardsGenerator(int _maxV)
        {
            maxV = _maxV;
            Random.seed = (int)System.DateTime.Now.Ticks;
        }

        public int[,] InitField()
        {
            var field = new int [3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = Random.Range(1, maxV);
                }
            }

            //Main hero is 0
            field[1, 1] = 0;
            return field;
        }
    }
}