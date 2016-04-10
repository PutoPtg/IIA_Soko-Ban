using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PesquisaAast : SearchAlgorithm
{
    private List<Vector2> crates;
    private List<SearchNode> listaSemHeap = new List<SearchNode>();
    private HashSet<object> closedSet = new HashSet<object>();
    private int crt = 0;
    private List<Vector2> goals;
    //private List<Vector2> crates = new List<Vector2>();

    // Use this for initialization
    void Start()
    {
        //crates = mapa.GetCrates();
        //numCrates = crates.Count;
        
        problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
        Map mapa = GameObject.Find("Map").GetComponent<Map>();
        goals = mapa.GetGoals();
        crates = mapa.GetCrates();
        crt = crates.Count;
        SearchNode start = new SearchNode(problem.GetStartState(), 0, crt);
        /* Push do nó inicial para a lista */
        listaSemHeap.Add(start);
    }

    protected override void Step()
    {
        if (listaSemHeap.Count > 0)
        {
            // Cur_node é igual ao primeiro elemento da lista
            // Depois é removido
            SearchNode cur_node = listaSemHeap[0];
            listaSemHeap.RemoveAt(0);
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
                        SearchNode new_node = new SearchNode(suc.state, suc.cost + cur_node.g, crt, suc.action, cur_node);
                        crt = GoalRemaining(new_node.state);
                        new_node.f = new_node.g + crt;
                        listaSemHeap.Add(new_node);
                    }
                }
            }
            // Ordenamento
            insertionSort();
            //print();
        }
        else
        {
            finished = true;
            running = false;
        }
    }

    // Ordenar pelo valor de "g" pois é o que mantém o custo atual
    public void insertionSort()
    {
        SearchNode temp;
        int j;

        for (int i = 1; i < listaSemHeap.Count; i++)
        {
            temp = listaSemHeap[i];
            j = i - 1;

            while (j >= 0 && listaSemHeap[j].f > temp.f)
            {
                listaSemHeap[j + 1] = listaSemHeap[j];
                j--;
            }
            listaSemHeap[j + 1] = temp;
        }

    }

    private int GoalRemaining(object state)
    {
        SokobanState s = (SokobanState)state;
        int remainingGoals = goals.Count;

        foreach (Vector2 crate in s.crates)
        {
            if (goals.Contains(crate))
            {
                remainingGoals--;
            }
        }
        return remainingGoals;
    }

    // Função para debug
    public void print()
    {
        for (int i = 1; i < listaSemHeap.Count; i++)
        {
            Debug.LogWarning(listaSemHeap[i].g);
        }

        Debug.LogWarning("---------------------");
    }
}

