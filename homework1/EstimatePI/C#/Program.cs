using System;

namespace C_
{
    public class EstimatePI
    {
        public static readonly int[] Seeds = new int[]
        {
            660901395,
            487394134,
            198686861
        };

        public static readonly int[] N = new int[]
        {
            5000,
            10000,
            50000,
            100000
        };

        public static void Main(string[] args)
        {
            foreach (var seed in Seeds)
            {
                var randToUse = new Random(seed);

                Console.WriteLine(
                    $"PRNG: Subtractive Generator (Knuth) Seed: {seed}"
                );

                foreach (var n in N)
                {
                    var estimatedPi = DoEstimatePI(randToUse, n);
                    Console.WriteLine($"{estimatedPi:F6} N: {n}");
                }

                Console.WriteLine();
            }
        }

        public static double DoEstimatePI(Random rand, int N)
        {
            long insideCount = 0;

            for (int i = 0; i < N; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                double dSquared = (x * x) + (y * y);

                if (dSquared <= 1.0)
                {
                    insideCount++;
                }
            }

            return (4d * insideCount) / (double)N;
        }

        public static long[] GeneratePRandomSeeds(int count)
        {
            Random rand = new Random();

            long[] pRandomSeeds = new long[count];

            for (int i = 0; i < count; i++)
            {
                pRandomSeeds[i] = rand.NextInt64();
            }

            return pRandomSeeds;
        }
    }

}
