import java.util.*;

public class TopoSortResult
{
    private boolean cycle;
    private ArrayList<String> order;

    public TopoSortResult(boolean isCycle, ArrayList<String> order)
    {
        this.cycle = isCycle;
        this.order = order;
    }

    public boolean isCycle()
    {
        return cycle;
    }

    public ArrayList<String> getOrder()
    {
        return order;
    }
}
