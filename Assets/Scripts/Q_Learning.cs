using System;
using UnityEngine;

public class Q_Learning 
{
    float[,] EstadoAcaoMatriz; // Matriz de aprendizado (Tabela Q): EstadoAtual x Açoes Possiveis
    float[,] EstadoAcaoRecompensa;     // Gabarito de reforço para o aprendizado
    float[,] EstadoAcaoPunicao; // Gabarito da puniçao para o aprendizado
    int estadoContador;  // Número de estados (linhas) na tabela Q
    int acaoContador; // Número de ações possíveis (colunas) na tabela Q
    float taxaAprendizado; // Taxa de aprendizado [0:1]

    public Q_Learning(int States, int Actions, float[,] Reinforcement, float[,] Punishment, float LearnRate)
    {
        EstadoAcaoMatriz = new float[States, Actions];
        EstadoAcaoRecompensa = Reinforcement;
        EstadoAcaoPunicao = Punishment;
        taxaAprendizado = LearnRate;
        estadoContador = States;
        acaoContador = Actions;
    }
    public float MaximoQ(int State)
    {

        float valorMaximo = EstadoAcaoMatriz[State, 0];

        for (int i = 0; i < acaoContador; i++)
        {
            if (valorMaximo < EstadoAcaoMatriz[State, i])
                valorMaximo = EstadoAcaoMatriz[State, i];
        }

        return valorMaximo;
    }

    public void Treinamento(int Episodes)
    {
        int acao;
        int estado;

        System.Random rand = new System.Random();

        for (int i = 0; i < Episodes; i++)
        {
            estado  = rand.Next(0, estadoContador); 
            acao = rand.Next(0, acaoContador); 
            EstadoAcaoMatriz[estado, acao] = ((1 - taxaAprendizado) *
                                                 EstadoAcaoMatriz[estado, acao] +
                                                 taxaAprendizado *
                                                 (EstadoAcaoRecompensa[estado, acao] +
                                                 EstadoAcaoPunicao[estado, acao] *
                                                 MaximoQ(estado)));
        }

        NomralizarQ(); 
    }

    public void NomralizarQ()
    {
        float maxValue = EstadoAcaoMatriz[0, 0];

        for (int i = 0; i < estadoContador; i++)
        {
            for (int j = 0; j < acaoContador; j++)
            {
                if (maxValue < EstadoAcaoMatriz[i, j])
                    maxValue = EstadoAcaoMatriz[i, j];
            }
        }

        for (int i = 0; i < estadoContador; i++)
        {
            for (int j = 0; j < acaoContador; j++)
            {
                EstadoAcaoMatriz[i, j] /= maxValue; 
            }
        }
    }
    public int Executar(int Estado)
    {
        float maxValue = EstadoAcaoMatriz[Estado, 0];
        int idx = 0;

        for (int i = 1; i < acaoContador; i++)
        {
            if (maxValue < EstadoAcaoMatriz[Estado, i])
            {
                idx = i;
                maxValue = EstadoAcaoMatriz[Estado, i];
            }
        }
        Debug.Log("Best action for state " + Estado + " is the action " + idx + " with a gain of " + maxValue);
        return idx;
    }
}
