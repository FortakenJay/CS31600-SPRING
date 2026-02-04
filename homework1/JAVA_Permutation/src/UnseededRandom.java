import javax.xml.transform.Result;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class UnseededRandom {

    /**
     *
     * @param nums
     * @return
     */

    public void UnseededRandom(List<Integer> nums){
        int[] result = new int[7];
        Collections.shuffle(nums);


        for (int i=0; i<7;i++){
            result[i]= nums.get(i);
        }

        System.out.println("Seed: Default");
        System.out.println("Language: JAVA");
        System.out.println("PRNG type: Linear Congruential Generator (LCG)");
        System.out.println("Seed Type: 48-bit seed");
        System.out.println("First 7 numbers: " + Arrays.toString(result));
        System.out.println("observation: The code follows a psudo-random appearance. However by definition, Oracle states in their documentation that \"this sequence will eventually restart\" ");


    }
    
}
