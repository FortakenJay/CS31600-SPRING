datatype result = Order of string list | Cycle of string list

type graph = (string * string list) list

local
      fun getNeighbors (g: graph) u =
          case List.find (fn (v, _) => v = u) g of
              SOME (_, ns) => ns
          | NONE => []
      fun extractCycle [] _ = []
      | extractCycle (x::xs) target = 
        if x = target then [x] else x :: (extractCycle xs target)

in
    fun topologicalSort (g: graph) =
        let
            fun dfs [] visited path acc = (visited, acc)
              | dfs (u::us) visited path acc =
                if List.exists (fn v => v = u) visited then
                    dfs us visited path acc
                else if List.exists (fn v => v = u) path then
(* Manually building the cycle string since SOSML handles strings strictly *)
                    raise Fail (String.concatWith " -> " (rev (u :: extractCycle path u)))
                else
                    let
                        val neighbors = getNeighbors g u
                        val (newVisited, newAcc) = dfs neighbors visited (u::path) acc
                    in
                        dfs us (u::newVisited) path (u::newAcc)
                    end
            val allNodes = map (fn (v, _) => v) g
        in
            Order (#2 (dfs allNodes [] [] []))
            handle Fail msg => Cycle [msg]
        end
end

(* --- Tests --- *)
val acyclicGraph = [("A", ["C"]), ("B", ["D"]), ("C", ["B"]), ("D", [])];
val cyclicGraph = [("B", ["D"]), ("D", ["F"]), ("F", ["B"])];

val result1 = topologicalSort acyclicGraph;
val result2 = topologicalSort cyclicGraph;