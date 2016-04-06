using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PesquisaSofrega : SearchAlgorithm {

    private Map mapa;
    private Stack<SearchNode> openStack = new Stack<SearchNode>();
    private HashSet<object> closedSet = new HashSet<object>();
    private List<Vector2> crates = new List<Vector2>();
    private int numCrates;

    // Use this for initialization
    void Start () {
        crates = mapa.GetCrates();
        numCrates = crates.Count;

        problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
        SearchNode start = new SearchNode(problem.GetStartState(), 0);
        /* Push do nó inicial para a pilha */
        openStack.Push(start);
    }

    protected override void Step()
    {

    }
}
