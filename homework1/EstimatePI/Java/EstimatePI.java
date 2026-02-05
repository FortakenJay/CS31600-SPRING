import java.util.Random;

public class EstimatePI
{
    public static final int[] Seeds = new int[]
    {
        660901395,
        487394134,
        198686861
    };

    public static final int[] N = new int[]
    {
        5000,
        10000,
        50000,
        100000
    };

    public static void main(String[] args)
    {
        for (var seed : Seeds)
        {
            var randToUse = new Random(seed);

            System.out.printf(
                "PRNG: Linear Congruential Generator (LCG) Seed: %d\n",
                seed
            );

            for (var n : N)
            {
                var estimatedPi = doEstimatePI(randToUse, n);
                System.out.printf("%.6f N: %d\n", estimatedPi, n);
            }

            System.out.println();
        }
    }

    public static double doEstimatePI(Random rand, int N)
    {
        long insideCount = 0;

        for (int i = 0; i < N; i++)
        {
            double x = rand.nextDouble();
            double y = rand.nextDouble();

            double dSquared = (x * x) + (y * y);

            if (dSquared <= 1.0)
            {
                insideCount += 1;
            }
        }

        return (4d * insideCount) / ((double)N);
    }
}
