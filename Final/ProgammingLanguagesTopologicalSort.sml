(* SUPER IMPORTANT KNOWLEDGE *)

(* 
  Higher-Order Functions
  ('a -> bool)
  If the Type Signature is in this form, it is Higher-Order, and works weirder
  These functions take in a FUNCTION as a parameter, not just a variable of sorts

  fun doMath (f, x) = f x;
  This is a functiopn named doMath which takes in a tuple of a function f, and        number x, and applies the function f to said number x.
*)

(* 
  Datatype var, which ensure that "result is one of only two POSSIBLE results.       Those being an Order of string lists, or a Cycle of string lists".
*)
datatype result = Order of string list | Cycle of string list

(* 
  An Alias, defined as a list of string, string list, so essentially a
    List of (String, [String])
    where [String] is a list of Strings.
*)
type graph = (string * string list) list

(* Keyword for private-functionality *)
local
  
(* 
  Declares a function getNeighbors where g is a graph variable input, and finds     some value to put in u.
*)
    fun getNeighbors (g: graph) u =

(*
  https://smlfamily.github.io/Basis/list.html
  https://smlfamily.github.io/Basis/option.html#SIG:OPTION.option:TY:SPEC
  
  Case because List.find returns an Option, meaning Some or None.
  List.find j L
  j = (fn (v, _)) => v = u)
    (v, _) specifies SML to assign the first value to v, but to not assign the         second value. As we know that the graph is a list with only two values,           this works. This is due to the _ being a placeholder for a value.
      This is also known as Pattern Matching, and the _ is a Wildcard Pattern
    => splits the input values, and the actual comparison for the lambda               function.
    v = u specifies the comparison. So in this case, is the first node that we         pull the same as the one we want to compare to?
  L = g.
    The second part of the List.find function, and this simply specifies the list       to look in.
*)
        case List.find (fn (v, _) => v = u) g of
  
(* 
  Specifies the case for List.find, which again, returns an Option, with Some or     None.
  SOME (_, ns) => ns specifies that ns should recieve the value of the next           item in the returned object from List.find. Again, this is because _ is a         placeholder for a value.
  NONE => [] is because if List.find finds nothing, we should return an empty         list for type safety.
  
  Essentially, this looks for all nodes in the graph that start with u, and grab     grabs all the connected nodes in the DAG.
*)
            SOME (_, ns) => ns
          | NONE => []

(* 
  This code extracts the cycle specifically, honestly its really weird and ill     look into it further
*)
    fun extractCycle [] _ = []
      | extractCycle (x::xs) target = 
        if x = target then [x] else x :: (extractCycle xs target)

(* 
  Again, g is a graph variable input variable for this function.

  fun dfs [] is the empty list handling.
    [], visited, path, and acc are function parameters.
    (visited, acc) is the output, a tuple of visited and acc.
    This both handles the base-case and edge-case of nothing being in the list.
  
  | dfs (u::us) visited path acc is the main handler, and first splits the given     list into the head u, and the rest into the tail us.
    It also has visited, path, and acc as function parameters.
*)
in
    fun topologicalSort (g: graph) =
        let
            fun dfs [] visited path acc = (visited, acc)
              | dfs (u::us) visited path acc =

(* 
  https://smlfamily.github.io/Basis/list.html
  
  List.exists f L
    This declared function goes through all elements of L, and compares it to f
    In our case, this functions loops through all of visited, and checks if the       current node is already in the visited list.
  f = (fn v => v = u) visited
    This function uses the fn because it specifies the value for visited.             Essentially, v represents each value in visited, which is then compared to u.     This is a Higher-Order function, and returns a boolean for the if statement.
    If this is TRUE, then it will call DFS on the resulting list.
    If this is FALSE, then it checks if it exists in the path, and if it does,         then it raises a failure state. If it's not in the visited set but already       in the path, that means that it's currently in a loop.
*)
                if List.exists (fn v => v = u) visited then
                    dfs us visited path acc
                else if List.exists (fn v => v = u) path then
(* Manually building the cycle string since SOSML handles strings strictly *)
                    raise Fail (String.concatWith " -> " (rev (u :: extractCycle path u)))

(*
  If its not in either the visited or path lists, then it must be a new node.
  To deal with a new node, we set the neighbors, then create a new "set" of depth   to go through, as this recurses back through our DFS code for each new node       found, until we reach the final node, where the base-case will be hit, and       it'll start recursing back up with the list being filled with the last-found     nodes.
    
  val neighbors = getNeighbors g u
  val (newVisited, newAcc) = dfs neighbors visited (u::path) acc
    This is the main driver of the code. It uses the neighbors of the current         node, and passes it back through the dfs code, while updating the path such       that we know which nodes have been passed through.
    
  By calling dfs us (u::newVisited) path (u::newAcc), we build the actual           accumulator which tells us the fastest path by dfs. 
*)
                else
                    let
                        val neighbors = getNeighbors g u
                        val (newVisited, newAcc) = dfs neighbors visited (u::path) acc
                    in
                        dfs us (u::newVisited) path (u::newAcc)
                    end

(*
  allNodes is a list that takes all the nodes in the graph, and makes sure that     dfs does not miss any.
    
  Since our dfs implementation returns (visited, acc), we grab the second value     via #2, and wraos it in Order, as that's how we determine if it worked, or       theres a cycle (implemented at the top).
    
  The handle statement is there so when the dfs code throws the "raise Fail", it   catches it, and labels it a cycle, as that is the only failure point in the       program.
*)
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