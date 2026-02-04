
import java.util.ArrayList;

public class Main {

    public static void main(String[] args) {
        ArrayList<Integer> nums1 = new ArrayList<>();
        ArrayList<Integer> nums2 = new ArrayList<>();

        for(int i=0; i<50;i++){

            nums1.add(i);
            nums2.add(i);
        }

        UnseededRandom unseededRandomRandomClass = new UnseededRandom();
        unseededRandomRandomClass.UnseededRandom(nums1);

        System.out.println("-------------------------------------------------------------");
        System.out.println(" ");
        System.out.println("input an integer as your seed for the PRNG: ");


        SeededRandom seededRandomClass = new SeededRandom();
        seededRandomClass.SeededRandom(nums2);




    }
}