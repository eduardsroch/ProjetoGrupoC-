using System;
using System.Collections.Generic;

namespace ProjetoSolo;
public class Medico
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string CRM { get; set; }
    private List<Atendimento> atendimentos;

    public Medico()
    {
        atendimentos = new List<Atendimento>();
    }

public void IniciarAtendimento(Paciente paciente, string suspeita)
    {
        foreach (Atendimento atendimento in atendimentos)
        {
            if (atendimento.Paciente == paciente && atendimento.Fim == default(DateTime))
            {
                throw new InvalidOperationException("Este paciente já está em atendimento.");
            }
        }

        Atendimento novoAtendimento = new Atendimento
        {
            Inicio = DateTime.Now,
            SuspeitaInicial = suspeita,
            MedicoResponsável = this,
            Paciente = paciente
        };

        atendimentos.Add(novoAtendimento);
    }

public void FinalizarAtendimento(Paciente paciente, string diagnóstico)
    {
        Atendimento atendimentoEmAndamento = atendimentos.Find(atendimento => atendimento.Paciente == paciente && atendimento.Fim == default(DateTime));

        if (atendimentoEmAndamento != null)
        {
            atendimentoEmAndamento.Fim = DateTime.Now;
            atendimentoEmAndamento.DiagnosticoFinal = diagnóstico;
        }
        else
        {
            throw new InvalidOperationException("Não há atendimento em andamento para este paciente com este médico.");
        }
    }

    public List<Atendimento> ObterAtendimentosConcluídos()
    {
        return atendimentos.FindAll(atendimento => atendimento.Fim != default(DateTime));
    }

}
