

namespace CsharpPermutation
{
    public class Seeded
    {
        public void SeededRandom(int[] array)
        {
            Random random = new Random(123);
            int[] result = new int[7];

            // Fisher-Yates shuffle with seed
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            for (int i = 0; i < 7; i++)
            {
                result[i] = array[i];
            }

            Console.WriteLine("Seed: 123");
            Console.WriteLine("Language: C#");
            Console.WriteLine("PRNG type: Knuth Subtractive Random Number Generator");
            Console.WriteLine("Seed Type: 32-bit signed integer");
            Console.WriteLine("First 7 numbers: " + string.Join(", ", result));
            Console.WriteLine("observation: The numbers will repeat every time I run Main.");
        }
    }
}
