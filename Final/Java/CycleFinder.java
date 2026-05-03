import java.util.*;

public class CycleFinder
{
    private Graph graph;
    private HashMap<String, Integer> state;
    private ArrayList<String> dfsStack;

    public CycleFinder(Graph graph)
    {
        this.graph = graph;
        this.state = new HashMap<>();
        this.dfsStack = new ArrayList<>();
        for (String v : graph.getVertices())
        {
            state.put(v, 0);
        }
    }

    public TopoSortResult find()
    {
        for (String v : graph.getVertices())
        {
            if (state.get(v) == 0)
            {
                ArrayList<String> cycle = dfs(v);

                // dfs returns non null when a cycle is detected. 

                if (cycle != null)
                {
                    return new TopoSortResult(true, cycle);
                }
            }
        }
        return null;
    }

    private ArrayList<String> dfs(String v)
    {
        state.put(v, 1);
        dfsStack.add(v);
        for (String n : graph.neighborsOf(v))
        {
            if (state.get(n) == 1)
            {
                int idx = dfsStack.indexOf(n);
                ArrayList<String> cycle = new ArrayList<>(dfsStack.subList(idx, dfsStack.size()));

                // grab the last occurrence of current node (the cycle) and the following nodes which eventually cycle back to the current node.

                cycle.add(n);
                return cycle;
            }
            if (state.get(n) == 0)
            {
                ArrayList<String> cycle = dfs(n);

                // continue to dfs on the neighbors of v 

                if (cycle != null)
                {
                    return cycle;
                }
            }
        }
        state.put(v, 2);

        // current node is completed, we've gone through all of it's neighbors.

        dfsStack.remove(dfsStack.size() - 1);
        return null;
    }
}
