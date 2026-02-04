
namespace CsharpPermutation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = new int[50];
            int[] array2 = new int[50];

            for (int i = 0; i < 50; i++)
            {
                array1[i] = i + 1;
                array2[i] = i + 1;
            }

            Console.WriteLine("Unseeded Random:");
            var unseeded = new Unseeded();
            unseeded.UnseededRandom(array1);

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(" ");

            Console.WriteLine("Seeded Random:");
            var seeded = new Seeded();
            seeded.SeededRandom(array2);
        }
    }
}