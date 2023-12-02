using System;

namespace ProjetoSolo;
public class Exame
{
    public string Título { get; set; }
    public float Valor { get; set; }
    public string Descrição { get; set; }
    public string Local { get; set; }

    public Exame(string título, float valor, string descrição, string local)
    {
        Título = título;
        Valor = valor;
        Descrição = descrição;
        Local = local;
    }

}
