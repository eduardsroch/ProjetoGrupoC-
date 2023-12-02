using System;

namespace ProjetoSolo;

using System;
using System.Collections.Generic;

class Program
{
    static GerenciamentoAcademia academia = new GerenciamentoAcademia();

    static void Main(string[] args)
    {
        bool sair = false;
        while (!sair)
        {
            ExibirMenu();

            int opcao = ObterOpcao();

            switch (opcao)
            {
                case 1:
                    InserirMédico();
                    break;
                case 2:
                    RemoverMédico();
                    break;
                case 3:
                    InserirPaciente();
                    break;
                case 4:
                    RemoverPaciente();
                    break;
                case 0:
                    sair = true;
                    Console.WriteLine("Saindo do programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha novamente.");
                    break;
            }
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("Academia Tech Med");
        Console.WriteLine("1. Inserir Médico");
        Console.WriteLine("2. Remover Médico");
        Console.WriteLine("3. Inserir Paciente");
        Console.WriteLine("4. Remover Paciente");
        Console.WriteLine("0. Sair");
    }

    static int ObterOpcao()
    {
        Console.Write("Escolha uma opção: ");
        int opcao;
        while (!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Por favor, digite um número válido.");
            Console.Write("Escolha uma opção: ");
        }
        return opcao;
    }

    static void InserirMédico()
    {
        Console.WriteLine("Inserindo novo médico...");
        Console.Write("Digite o nome do médico: ");
        string nome = Console.ReadLine();

        Console.Write("Digite a data de nascimento do médico (Formato: dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
        {
            Console.Write("Digite o CPF do médico: ");
            string cpf = Console.ReadLine();

            Console.Write("Digite o CRM do médico: ");
            string crm = Console.ReadLine();

            Medico novoMédico = new Medico
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                CPF = cpf,
                CRM = crm
            };

            academia.InserirMedico(novoMédico);
            Console.WriteLine("Médico inserido com sucesso!");
        }
        else
        {
            Console.WriteLine("Data de nascimento inválida.");
        }
    }

    static void RemoverMédico()
    {
        Console.WriteLine("Removendo médico...");
        Console.Write("Digite o nome do médico a ser removido: ");
        string nomeMédico = Console.ReadLine();

        Medico médicoParaRemover = academia.ObterMedicoPorNome(nomeMédico);
        
        if (médicoParaRemover != null)
        {
            academia.RemoverMedico(médicoParaRemover);
            Console.WriteLine("Médico removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Médico não encontrado.");
        }
    }

    static void InserirPaciente()
    {
        Console.WriteLine("Inserindo novo paciente...");
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine();

        Console.Write("Digite a data de nascimento do paciente (Formato: dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
        {
            Console.Write("Digite o CPF do paciente: ");
            string cpf = Console.ReadLine();

            Console.Write("Digite o sexo do paciente: ");
            string sexo = Console.ReadLine();

            Paciente novoPaciente = new Paciente
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                CPF = cpf,
                Sexo = sexo
            };

            academia.InserirPaciente(novoPaciente);
            Console.WriteLine("Paciente inserido com sucesso!");
        }
        else
        {
            Console.WriteLine("Data de nascimento inválida.");
        }
    }

    static void RemoverPaciente()
    {
        Console.WriteLine("Removendo paciente...");
        Console.Write("Digite o nome do paciente a ser removido: ");
        string nomePaciente = Console.ReadLine();

        Paciente pacienteParaRemover = academia.ObterPacientePorNome(nomePaciente);
        
        if (pacienteParaRemover != null)
        {
            academia.RemoverPaciente(pacienteParaRemover);
            Console.WriteLine("Paciente removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Paciente não encontrado.");
        }
    }
}
