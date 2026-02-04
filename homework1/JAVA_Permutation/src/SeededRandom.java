import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class SeededRandom {

    /**
     *
     * @param nums
     * @return
     */
    public void SeededRandom(List<Integer> nums){

        int[] result = new int[7];
        Collections.shuffle(nums, new Random(123));


        for (int i=0; i<7;i++){
            result[i]= nums.get(i);
        }

        System.out.println("Seed: 123");
        System.out.println("Language: JAVA");
        System.out.println("PRNG type: Linear Congruential Generator (LCG)");
        System.out.println("Seed Type: 48-bit seed");
        System.out.println("First 7 numbers: " + Arrays.toString(result));
        System.out.println("observation: The numbers will repeat everytime I run Main.");


    }
}
