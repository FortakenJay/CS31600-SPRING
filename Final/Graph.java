import java.util.*;

public class Graph
{
    private ArrayList<String> vertices;
    private HashMap<String, ArrayList<String>> adjList;

    public Graph()
    {
        vertices = new ArrayList<>();
        adjList = new HashMap<>();
    }

    public void addVertex(String v)
    {
        if (!adjList.containsKey(v))
        {
            vertices.add(v);
            adjList.put(v, new ArrayList<>());
        }
    }

    public void addEdge(String from, String to)
    {
        adjList.get(from).add(to);
    }

    public ArrayList<String> getVertices()
    {
        return vertices;
    }

    public ArrayList<String> neighborsOf(String v)
    {
        return adjList.get(v);
    }
}
