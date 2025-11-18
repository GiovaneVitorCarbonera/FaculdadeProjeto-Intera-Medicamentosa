using Newtonsoft.Json;

namespace Interação_Medicamentosa.Models
{
    public class PrescriçãoModel
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedicamento { get; set; }

        public MedicamentoModel? Medicamento { get; set; } = null;

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int HorariodoRemedio { get; set; }
        public int Dosagem { get; set; }
        public int frequencia { get; set; }
        public int Duracao { get; set; }
        public string Observacao { get; set; }
    }
}
