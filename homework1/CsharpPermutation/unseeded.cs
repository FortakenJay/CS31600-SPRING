namespace CsharpPermutation
{
    public class Unseeded
    {
        public void UnseededRandom(int[] array)
        {
            int[] result = new int[7];
            Random.Shared.Shuffle(array);

            for (int i = 0; i < 7; i++)
            {
                result[i] = array[i];
            }

            Console.WriteLine("Seed: Default");
            Console.WriteLine("Language: C#");
            Console.WriteLine("PRNG type: Knuth Subtractive Random Number Generator");
            Console.WriteLine("Seed Type: 32-bit signed integer");
            Console.WriteLine("First 7 numbers: " + string.Join(", ", result));
            Console.WriteLine("observation: The code follows a pseudo-random pattern. The sequence uses Random.Shared which is cryptographically strong and non-repeating.");
        }
    }
}
