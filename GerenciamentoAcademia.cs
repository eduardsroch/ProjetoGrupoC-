using System;

namespace ProjetoSolo;

    public class GerenciamentoAcademia
    {
        private List<Medico> medicos;
        private List<Paciente> pacientes;
        private List<Atendimento> atendimentos;

        public GerenciamentoAcademia()
        {
            medicos = new List<Medico>();
            pacientes = new List<Paciente>();
            atendimentos = new List<Atendimento>();
        }

        public void IniciarAtendimento(Medico medico, Paciente paciente, string suspeita)
        {
            Atendimento novoAtendimento = new Atendimento(medico, paciente, suspeita);
            atendimentos.Add(novoAtendimento);
        }

        public void FinalizarAtendimento(Medico medico, Paciente paciente, string diagnostico)
        {
            Atendimento atendimento = atendimentos.FirstOrDefault(a =>
                a.MedicoResponsavel == medico && a.PacienteAtendido == paciente && a.Finalizado == false);

            if (atendimento != null)
            {
                atendimento.FinalizarAtendimento(diagnostico);
            }
            else
            {
                Console.WriteLine("Atendimento não encontrado ou já finalizado para este médico e paciente.");
            }
        }

        public List<Medico> MedicosComIdadeEntre(int idadeMinima, int idadeMaxima)
        {
            DateTime hoje = DateTime.Now;
            return medicos.Where(m => (hoje.Year - m.DataNascimento.Year) >= idadeMinima &&
                                      (hoje.Year - m.DataNascimento.Year) <= idadeMaxima).ToList();
        }

        public void ExibirMedicosComIdadeEntre(int idadeMinima, int idadeMaxima)
        {
            List<Medico> medicosFiltrados = MedicosComIdadeEntre(idadeMinima, idadeMaxima);
            if (medicosFiltrados.Any())
            {
                Console.WriteLine("Médicos com idade entre " + idadeMinima + " e " + idadeMaxima + " anos:");
                foreach (var medico in medicosFiltrados)
                {
                    Console.WriteLine($"Nome: {medico.Nome}, Idade: {DateTime.Now.Year - medico.DataNascimento.Year}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum médico encontrado com a idade especificada.");
            }
        }

}
