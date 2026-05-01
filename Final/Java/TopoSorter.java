import java.util.*;

public class TopoSorter
{
    private Graph graph;
    private HashMap<String, Integer> state;
    private LinkedList<String> topoOrder;

    public TopoSorter(Graph graph)
    {
        this.graph = graph;
        this.state = new HashMap<>();
        this.topoOrder = new LinkedList<>();
        for (String v : graph.getVertices())
        {
            state.put(v, 0);
        }
    }

    public TopoSortResult sort()
    {
        TopoSortResult cycleResult = new CycleFinder(graph).find();
        if (cycleResult != null)
        {
            return cycleResult;
        }
        for (String v : graph.getVertices())
        {
            if (state.get(v) == 0)
            {
                dfs(v);
            }
        }
        return new TopoSortResult(false, new ArrayList<>(topoOrder));
    }

    private void dfs(String v)
    {
        state.put(v, 1);
        for (String n : graph.neighborsOf(v))
        {
            if (state.get(n) == 0)
            {
                dfs(n);
            }
        }
        state.put(v, 2);
        topoOrder.addFirst(v);
    }
}
