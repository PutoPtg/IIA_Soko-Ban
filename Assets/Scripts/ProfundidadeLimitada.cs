using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Semelhante ao Breath First mas é usada uma Stack em vez de Queue 
   Estrutura LIFO
   Operações de Push e Pop      
*/

public class ProfundidadeLimitada : SearchAlgorithm
{

    private Stack<SearchNode> openStack = new Stack<SearchNode>();
    private HashSet<object> closedSet = new HashSet<object>();
    // Para especificar no Unity qual o nivelMax da profundidade
    public int nivelMax;
    // Para contar quantos niveis de profundidade já foram visitados
    private int contaProf = 0;

    void Start()
    {
        problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
        SearchNode start = new SearchNode(problem.GetStartState(), 0);
        /* Push do nó inicial para a pilha */
        openStack.Push(start);
    }

    protected override void Step()
    {
        // Caso o nivelMax seja 0
        if(nivelMax == 0)
        {
            if (openStack.Count > 0)
            {
                SearchNode cur_node = openStack.Pop();
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
        // Caso o nivelMax não seja 0
        else
        {
            // Se ainda não tiver sudo atingida a profundidade
            if (openStack.Count > 0 && contaProf < nivelMax)
            {
                SearchNode cur_node = openStack.Pop();
                closedSet.Add(cur_node.state);

                if (problem.IsGoal(cur_node.state))
                {
                    solution = cur_node;
                    finished = true;
                    running = false;
                }
                else {
                    Successor[] sucessors = problem.GetSuccessors(cur_node.state);
                    // Como está a ver sucessores, a profundidade aumenta em 1
                    contaProf++;
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
        
}

