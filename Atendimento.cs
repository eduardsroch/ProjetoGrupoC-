using System;
using System.Collections.Generic;

namespace ProjetoSolo;
public class Atendimento
{
    private string suspeita;

    public DateTime Inicio { get; set; }
    public string SuspeitaInicial { get; set; }
    public List<(Exame, string)> ListaExamesResultado { get; set; }
    public float Valor { get; set; }
    public DateTime Fim { get; set; }
    public Medico MedicoResponsável { get; set; }
    public Paciente Paciente { get; set; }
    public string DiagnosticoFinal { get; set; }

    public Atendimento(Medico medico, Paciente paciente)
    {
        ListaExamesResultado = new List<(Exame, string)>();
        Fim = DateTime.MinValue;
    }

    public Atendimento(Medico medico, Paciente paciente, string suspeita) : this(medico, paciente)
    {
        this.suspeita = suspeita;
    }

    public void IniciarAtendimento(string suspeita, Medico médico, Paciente paciente)
    {
        if (Fim != DateTime.MinValue)
        {
            throw new InvalidOperationException("O atendimento já foi finalizado.");
        }

        Inicio = DateTime.Now;
        SuspeitaInicial = suspeita;
        MedicoResponsável = médico;
        Paciente = paciente;
    }

    public void AdicionarExame(Exame exame, string resultado)
    {
        ListaExamesResultado.Add((exame, resultado));
    }

    public void FinalizarAtendimento(string diagnóstico)
    {
        if (Fim != DateTime.MinValue)
        {
            throw new InvalidOperationException("O atendimento já foi finalizado.");
        }

        Fim = DateTime.Now;
        DiagnosticoFinal = diagnóstico;
    }

}
