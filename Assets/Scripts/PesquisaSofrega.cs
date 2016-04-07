using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PesquisaSofrega : SearchAlgorithm {

    private Map mapa;
    private List<SearchNode> listaSemHeap = new List<SearchNode>();
    private Stack<SearchNode> stack = new Stack<SearchNode>();
    private HashSet<object> closedSet = new HashSet<object>();
    private List<Vector2> crates = new List<Vector2>();
    private int numCrates;

    // Use this for initialization
    void Start () {
        //crates = mapa.GetCrates();
        //numCrates = crates.Count;

        problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
        SearchNode start = new SearchNode(problem.GetStartState(), 0);
        /* Push do nó inicial para a pilha */
        listaSemHeap.Add(start);
    }

    protected override void Step()
    {
        if (listaSemHeap.Count > 0)
        {
            // Cur_node é igual ao primeiro elemento da lista
            // Depois é removido
            SearchNode cur_node = listaSemHeap[0];
            //listaSemHeap.RemoveAt(0);
            closedSet.Add(cur_node.state);

            if (problem.IsGoal(cur_node.state))
            {
                solution = cur_node;
                finished = true;
                running = false;
            }
            else {
                Successor[] sucessors = problem.GetSuccessors(cur_node.state);
                foreach (Successor suc in sucessors)
                {
                    if (!closedSet.Contains(suc.state))
                    {
                        SearchNode new_node = new SearchNode(suc.state, suc.cost + cur_node.g, suc.action, cur_node);
                        listaSemHeap.Add(new_node);
                    }
                }
            }
            // Ordenamento
            listaSemHeap.Sort();        // Está a dar BODEGA
        }
        else
        {
            finished = true;
            running = false;
        }
    }
}

