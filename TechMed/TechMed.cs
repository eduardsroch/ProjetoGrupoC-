using System;
using System.Collections.Generic;
using System.Linq;

public class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
}

public class Medico : Pessoa
{
    public string CRM { get; set; }

    public Medico(string nome, DateTime dataNascimento, string cpf, string crm)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        CRM = crm;
    }
}

public class Paciente : Pessoa
{
    public string Sexo { get; set; }
    public List<string> Sintomas { get; set; }

    public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        Sexo = sexo;
        Sintomas = new List<string>();
    }
}

public class Exame
{
    public string Titulo { get; set; }
    public float Valor { get; set; }
    public string Descricao { get; set; }
    public string Local { get; set; }

    public Exame(string titulo, float valor, string descricao, string local)
    {
        Titulo = titulo;
        Valor = valor;
        Descricao = descricao;
        Local = local;
    }
}

public class Atendimento
{
    public DateTime Inicio { get; set; }
    public string SuspeitaInicial { get; set; }
    public List<(Exame, string)> ExamesResultado { get; set; }
    public float Valor { get; set; }
    public DateTime Fim { get; set; }
    public Medico MedicoResponsavel { get; set; }
    public Paciente Paciente { get; set; }
    public string DiagnosticoFinal { get; set; }
}

public class GerenciamentoAcademia
{
    private List<Medico> medicos;
    private List<Paciente> pacientes;
    private List<Exame> exames;
    private List<Atendimento> atendimentos;

    public GerenciamentoAcademia()
    {
        medicos = new List<Medico>();
        pacientes = new List<Paciente>();
        exames = new List<Exame>();
        atendimentos = new List<Atendimento>();
    }

    public void InserirMedico(Medico medico)
    {
        medicos.Add(medico);
    }

    public void RemoverMedico(Medico medico)
    {
        medicos.Remove(medico);
    }

    public void InserirPaciente(Paciente paciente)
    {
        pacientes.Add(paciente);
    }

    public void RemoverPaciente(Paciente paciente)
    {
        pacientes.Remove(paciente);
    }

    public void InserirExame(Exame exame)
    {
        exames.Add(exame);
    }

    public void RemoverExame(Exame exame)
    {
        exames.Remove(exame);
    }

    public void IniciarAtendimento(Atendimento atendimento)
    {
        atendimentos.Add(atendimento);
    }

    public void FinalizarAtendimento(Atendimento atendimento)
    {
        atendimentos.Remove(atendimento);
    }

    public List<Medico> MedicosComIdadeEntre(int idadeMinima, int idadeMaxima)
    {
        DateTime dataAtual = DateTime.Now;
        return medicos.Where(m => (dataAtual.Year - m.DataNascimento.Year) >= idadeMinima && (dataAtual.Year - m.DataNascimento.Year) <= idadeMaxima).ToList();
    }

    public List<Paciente> PacientesComIdadeEntre(int idadeMinima, int idadeMaxima)
    {
        DateTime dataAtual = DateTime.Now;
        return pacientes.Where(p => (dataAtual.Year - p.DataNascimento.Year) >= idadeMinima && (dataAtual.Year - p.DataNascimento.Year) <= idadeMaxima).ToList();
    }

    public List<Paciente> PacientesPorSexo(string sexo)
    {
        return pacientes.Where(p => p.Sexo == sexo).ToList();
    }

    public List<Paciente> PacientesEmOrdemAlfabetica()
    {
        return pacientes.OrderBy(p => p.Nome).ToList();
    }

    public List<Paciente> PacientesComSintoma(string sintoma)
    {
        return pacientes.Where(p => p.Sintomas.Contains(sintoma)).ToList();
    }

    public List<Pessoa> AniversariantesDoMes(int mes)
    {
        return medicos.Where(m => m.DataNascimento.Month == mes).Cast<Pessoa>().Concat(pacientes.Where(p => p.DataNascimento.Month == mes)).ToList();
    }

    public List<Atendimento> AtendimentosEmAberto()
    {
        return atendimentos.Where(a => a.Fim > DateTime.Now).OrderByDescending(a => a.Inicio).ToList();
    }

    public List<Medico> MedicosPorQuantidadeAtendimentos()
    {
        return medicos.OrderByDescending(m => atendimentos.Count(a => a.MedicoResponsavel == m)).ToList();
    }

    public List<Atendimento> AtendimentosComPalavra(string palavra)
    {
        return atendimentos.Where(a => a.SuspeitaInicial.Contains(palavra) || a.DiagnosticoFinal.Contains(palavra)).ToList();
    }

    public List<Exame> Top10ExamesUtilizados()
    {
        return atendimentos.SelectMany(a => a.ExamesResultado.Select(er => er.Item1)).GroupBy(e => e).OrderByDescending(g => g.Count()).Take(10).Select(g => g.Key).ToList();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        GerenciamentoAcademia academia = new GerenciamentoAcademia();
        
        bool sair = false;
        
        while (!sair)
        {
            Console.WriteLine("Menu de Opções");
            Console.WriteLine("1. Inserir Médico");
            Console.WriteLine("2. Remover Médico");
            Console.WriteLine("3. Inserir Paciente");
            Console.WriteLine("4. Remover Paciente");
            Console.WriteLine("5. Inserir Exame");
            Console.WriteLine("6. Remover Exame");
            Console.WriteLine("7. Iniciar Atendimento");
            Console.WriteLine("8. Finalizar Atendimento");
            Console.WriteLine("9. Listar Médicos com Idade entre");
            Console.WriteLine("10. Listar Pacientes com Idade entre");
            Console.WriteLine("11. Listar Pacientes por Sexo");
            Console.WriteLine("12. Listar Pacientes em Ordem Alfabética");
            Console.WriteLine("13. Listar Pacientes com Sintoma");
            Console.WriteLine("14. Listar Aniversariantes do Mês");
            Console.WriteLine("15. Listar Atendimentos em Aberto");
            Console.WriteLine("16. Listar Médicos por Quantidade de Atendimentos");
            Console.WriteLine("17. Listar Atendimentos com Palavra");
            Console.WriteLine("18. Listar Top 10 Exames Utilizados");
            Console.WriteLine("0. Sair");
            
            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine());
            
            switch (opcao)
            {
                case 1:
                    InserirMedico(academia);
                    break;
                case 2:
                    RemoverMedico(academia);
                    break;
                case 3:
                    InserirPaciente(academia);
                    break;
                case 4:
                    RemoverPaciente(academia);
                    break;
                case 5:
                    InserirExame(academia);
                    break;
                case 6:
                    RemoverExame(academia);
                    break;
                case 7:
                    IniciarAtendimento(academia);
                    break;
                case 8:
                    FinalizarAtendimento(academia);
                    break;
                case 9:
                    ListarMedicosComIdadeEntre(academia);
                    break;
                case 10:
                    ListarPacientesComIdadeEntre(academia);
                    break;
                case 11:
                    ListarPacientesPorSexo(academia);
                    break;
                case 12:
                    ListarPacientesEmOrdemAlfabetica(academia);
                    break;
                case 13:
                    ListarPacientesComSintoma(academia);
                    break;
                case 14:
                    ListarAniversariantesDoMes(academia);
                    break;
                case 15:
                    ListarAtendimentosEmAberto(academia);
                    break;
                case 16:
                    ListarMedicosPorQuantidadeAtendimentos(academia);
                    break;
                case 17:
                    ListarAtendimentosComPalavra(academia);
                    break;
                case 18:
                    ListarTop10ExamesUtilizados(academia);
                    break;
                case 0:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
            
            Console.WriteLine();
        }
    }

    private static void InserirMedico(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Inserir Médico");
        
        Console.Write("Digite o nome do médico: ");
        string nome = Console.ReadLine();
        
        Console.Write("Digite a idade do médico: ");
        int idade = int.Parse(Console.ReadLine());
        
        Medico medico = new Medico(nome, idade);
        academia.InserirMedico(medico);
        
        Console.WriteLine("Médico inserido com sucesso!");
    }

    private static void RemoverMedico(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Remover Médico");
        
        Console.Write("Digite o nome do médico a ser removido: ");
        string nome = Console.ReadLine();
        
        Medico medico = academia.Medicos.FirstOrDefault(m => m.Nome == nome);
        
        if (medico != null)
        {
            academia.RemoverMedico(medico);
            Console.WriteLine("Médico removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Médico não encontrado.");
        }
    }

    private static void InserirPaciente(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Inserir Paciente");
        
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine();
        
        Console.Write("Digite a idade do paciente: ");
        int idade = int.Parse(Console.ReadLine());
        
        Console.Write("Digite o sexo do paciente: ");
        string sexo = Console.ReadLine();
        
        Paciente paciente = new Paciente(nome, idade, sexo);
        academia.InserirPaciente(paciente);
        
        Console.WriteLine("Paciente inserido com sucesso!");
    }

    private static void RemoverPaciente(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Remover Paciente");
        
        Console.Write("Digite o nome do paciente a ser removido: ");
        string nome = Console.ReadLine();
        
        Paciente paciente = academia.Pacientes.FirstOrDefault(p => p.Nome == nome);
        
        if (paciente != null)
        {
            academia.RemoverPaciente(paciente);
            Console.WriteLine("Paciente removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Paciente não encontrado.");
        }
    }

    private static void InserirExame(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Inserir Exame");
        
        Console.Write("Digite o nome do exame: ");
        string nome = Console.ReadLine();
        
        Exame exame = new Exame(nome);
        academia.InserirExame(exame);
        
        Console.WriteLine("Exame inserido com sucesso!");
    }

    private static void RemoverExame(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Remover Exame");
        
        Console.Write("Digite o nome do exame a ser removido: ");
        string nome = Console.ReadLine();
        
        Exame exame = academia.Exames.FirstOrDefault(e => e.Nome == nome);
        
        if (exame != null)
        {
            academia.RemoverExame(exame);
            Console.WriteLine("Exame removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Exame não encontrado.");
        }
    }

    private static void IniciarAtendimento(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Iniciar Atendimento");
        
        Console.Write("Digite o nome do médico responsável: ");
        string nomeMedico = Console.ReadLine();
        
        Console.Write("Digite o nome do paciente: ");
        string nomePaciente = Console.ReadLine();
        
        Medico medico = academia.Medicos.FirstOrDefault(m => m.Nome == nomeMedico);
        Paciente paciente = academia.Pacientes.FirstOrDefault(p => p.Nome == nomePaciente);
        
        if (medico != null && paciente != null)
        {
            Atendimento atendimento = new Atendimento(medico, paciente);
            academia.IniciarAtendimento(atendimento);
            Console.WriteLine("Atendimento iniciado com sucesso!");
        }
        else
        {
            Console.WriteLine("Médico ou paciente não encontrado.");
        }
    }

    private static void FinalizarAtendimento(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Finalizar Atendimento");
        
        Console.Write("Digite o nome do médico responsável: ");
        string nomeMedico = Console.ReadLine();
        
        Console.Write("Digite o nome do paciente: ");
        string nomePaciente = Console.ReadLine();
        
        Medico medico = academia.Medicos.FirstOrDefault(m => m.Nome == nomeMedico);
        Paciente paciente = academia.Pacientes.FirstOrDefault(p => p.Nome == nomePaciente);
        
        if (medico != null && paciente != null)
        {
            Atendimento atendimento = academia.Atendimentos.FirstOrDefault(a => a.MedicoResponsavel == medico && a.Paciente == paciente);
            
            if (atendimento != null)
            {
                academia.FinalizarAtendimento(atendimento);
                Console.WriteLine("Atendimento finalizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Atendimento não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Médico ou paciente não encontrado.");
        }
    }

    private static void ListarMedicosComIdadeEntre(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Médicos com Idade entre");
        
        Console.Write("Digite a idade mínima: ");
        int idadeMinima = int.Parse(Console.ReadLine());
        
        Console.Write("Digite a idade máxima: ");
        int idadeMaxima = int.Parse(Console.ReadLine());
        
        List<Medico> medicos = academia.MedicosComIdadeEntre(idadeMinima, idadeMaxima);
        
        if (medicos.Count > 0)
        {
            Console.WriteLine("Médicos encontrados:");
            
            foreach (Medico medico in medicos)
            {
                Console.WriteLine($"Nome: {medico.Nome}, Idade: {medico.Idade}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum médico encontrado.");
        }
    }

    private static void ListarPacientesComIdadeEntre(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Pacientes com Idade entre");
        
        Console.Write("Digite a idade mínima: ");
        int idadeMinima = int.Parse(Console.ReadLine());
        
        Console.Write("Digite a idade máxima: ");
        int idadeMaxima = int.Parse(Console.ReadLine());
        
        List<Paciente> pacientes = academia.PacientesComIdadeEntre(idadeMinima, idadeMaxima);
        
        if (pacientes.Count > 0)
        {
            Console.WriteLine("Pacientes encontrados:");
            
            foreach (Paciente paciente in pacientes)
            {
                Console.WriteLine($"Nome: {paciente.Nome}, Idade: {paciente.Idade}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum paciente encontrado.");
        }
    }

    private static void ListarPacientesPorSexo(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Pacientes por Sexo");
        
        Console.Write("Digite o sexo: ");
        string sexo = Console.ReadLine();
        
        List<Paciente> pacientes = academia.PacientesPorSexo(sexo);
        
        if (pacientes.Count > 0)
        {
            Console.WriteLine("Pacientes encontrados:");
            
            foreach (Paciente paciente in pacientes)
            {
                Console.WriteLine($"Nome: {paciente.Nome}, Sexo: {paciente.Sexo}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum paciente encontrado.");
        }
    }

    private static void ListarPacientesEmOrdemAlfabetica(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Pacientes em Ordem Alfabética");
        
        List<Paciente> pacientes = academia.PacientesEmOrdemAlfabetica();
        
        if (pacientes.Count > 0)
        {
            Console.WriteLine("Pacientes encontrados:");
            
            foreach (Paciente paciente in pacientes)
            {
                Console.WriteLine($"Nome: {paciente.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum paciente encontrado.");
        }
    }

    private static void ListarPacientesComSintoma(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Pacientes com Sintoma");
        
        Console.Write("Digite o sintoma: ");
        string sintoma = Console.ReadLine();
        
        List<Paciente> pacientes = academia.PacientesComSintoma(sintoma);
        
        if (pacientes.Count > 0)
        {
            Console.WriteLine("Pacientes encontrados:");
            
            foreach (Paciente paciente in pacientes)
            {
                Console.WriteLine($"Nome: {paciente.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum paciente encontrado.");
        }
    }

    private static void ListarAniversariantesDoMes(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Aniversariantes do Mês");
        
        Console.Write("Digite o número do mês: ");
        int mes = int.Parse(Console.ReadLine());
        
        List<Pessoa> aniversariantes = academia.AniversariantesDoMes(mes);
        
        if (aniversariantes.Count > 0)
        {
            Console.WriteLine("Aniversariantes encontrados:");
            
            foreach (Pessoa pessoa in aniversariantes)
            {
                Console.WriteLine($"Nome: {pessoa.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum aniversariante encontrado.");
        }
    }

    private static void ListarAtendimentosEmAberto(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Atendimentos em Aberto");
        
        List<Atendimento> atendimentos = academia.AtendimentosEmAberto();
        
        if (atendimentos.Count > 0)
        {
            Console.WriteLine("Atendimentos encontrados:");
            
            foreach (Atendimento atendimento in atendimentos)
            {
                Console.WriteLine($"Médico: {atendimento.MedicoResponsavel.Nome}, Paciente: {atendimento.Paciente.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum atendimento em aberto encontrado.");
        }
    }

    private static void ListarMedicosPorQuantidadeAtendimentos(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Médicos por Quantidade de Atendimentos");
        
        List<Medico> medicos = academia.MedicosPorQuantidadeAtendimentos();
        
        if (medicos.Count > 0)
        {
            Console.WriteLine("Médicos encontrados:");
            
            foreach (Medico medico in medicos)
            {
                Console.WriteLine($"Nome: {medico.Nome}, Quantidade de Atendimentos: {academia.Atendimentos.Count(a => a.MedicoResponsavel == medico)}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum médico encontrado.");
        }
    }

    private static void ListarAtendimentosComPalavra(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Atendimentos com Palavra");
        
        Console.Write("Digite a palavra: ");
        string palavra = Console.ReadLine();
        
        List<Atendimento> atendimentos = academia.AtendimentosComPalavra(palavra);
        
        if (atendimentos.Count > 0)
        {
            Console.WriteLine("Atendimentos encontrados:");
            
            foreach (Atendimento atendimento in atendimentos)
            {
                Console.WriteLine($"Médico: {atendimento.MedicoResponsavel.Nome}, Paciente: {atendimento.Paciente.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum atendimento encontrado.");
        }
    }

    private static void ListarTop10ExamesUtilizados(GerenciamentoAcademia academia)
    {
        Console.WriteLine("Listar Top 10 Exames Utilizados");
        
        List<Exame> exames = academia.Top10ExamesUtilizados();
        
        if (exames.Count > 0)
        {
            Console.WriteLine("Exames encontrados:");
            
            foreach (Exame exame in exames)
            {
                Console.WriteLine($"Nome: {exame.Nome}, Quantidade de Utilizações: {academia.Atendimentos.SelectMany(a => a.ExamesResultado).Count(er => er.Item1 == exame)}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum exame encontrado.");
        }
    }
    
}
