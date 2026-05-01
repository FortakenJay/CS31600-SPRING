import java.util.*;

public class main
{
    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        Graph graph = new Graph();

        while (scanner.hasNextLine())
        {
            String line = scanner.nextLine().trim();
            if (line.isEmpty()) continue;
            String[] parts = line.split("->");
            String from = parts[0].trim();
            String to = parts[1].trim();
            graph.addVertex(from);
            graph.addVertex(to);
            graph.addEdge(from, to);
        }

        TopoSortResult result = new TopoSorter(graph).sort();

        if (result.isCycle())
        {
            System.out.println("Cycle detected: " + String.join(" -> ", result.getOrder()));
        }
        else
        {
            System.out.println("Topological order: " + String.join(" -> ", result.getOrder()));
        }
    }
}
