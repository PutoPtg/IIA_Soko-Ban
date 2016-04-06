using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Profundidade necessária da árvore (n) para resolução = 63    
*/

public class AprofundamentoProgressivo : SearchAlgorithm
{

    private Stack<SearchNode> openStack = new Stack<SearchNode>();
    private HashSet<object> closedSet = new HashSet<object>();
    public int n;
    private SearchNode startPoint;

    void Start()
    {
        problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
        SearchNode start = new SearchNode(problem.GetStartState(), 0);
        startPoint = start;
        /* Push do nó inicial para a pilha */
        openStack.Push(start);
    }

    protected override void Step()
    {
        if (openStack.Count > 0)
        {
            SearchNode cur_node = openStack.Pop();
            closedSet.Add(cur_node.state);

            // Se a profundidade do nó for n faz reset
            if (problem.IsGoal(cur_node.state) || cur_node.depth == n)
            {
                if (problem.IsGoal(cur_node.state))
                {
                    solution = cur_node;
                    finished = true;
                    running = false;
                }
                else
                {
                    // RESET - Incrementa n, limpa Stack e mete nó inicial
                    solution = null;
                    n++;
                    openStack.Clear();
                    openStack.Push(startPoint);
                }       
            }
            else {
                Successor[] sucessors = problem.GetSuccessors(cur_node.state);
                foreach (Successor suc in sucessors)
                {
                    if (!closedSet.Contains(suc.state))
                    {
                        SearchNode new_node = new SearchNode(suc.state, suc.cost + cur_node.g, suc.action, cur_node);
                        openStack.Push(new_node);
                    }
                }
            }
        }
        else
        {
            finished = true;
            running = false;
        }
    }
}

