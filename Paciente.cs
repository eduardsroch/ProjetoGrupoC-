using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoSolo;

public class Paciente
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string Sexo { get; set; }
    private List<string> sintomas;

    public Paciente()
    {
        sintomas = new List<string>();
    }

    public void AdicionarSintoma(string novoSintoma)
    {
        sintomas.Add(novoSintoma);
    }

    public List<string> ObterSintomas()
    {
        return sintomas;
    }

    public bool PossuiSintoma(string sintoma)
    {
        return sintomas.Contains(sintoma);
    }
    public int ObterIdade()
    {
        DateTime dataAtual = DateTime.Now;
        int idade = dataAtual.Year - DataNascimento.Year;
        if (dataAtual.Month < DataNascimento.Month || (dataAtual.Month == DataNascimento.Month && dataAtual.Day < DataNascimento.Day))
        {
            idade--;
        }
        return idade;
    }
}
