namespace Interação_Medicamentosa.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Idade { get; set; }
        public float Peso { get; set; }
        public DateTime UltimaVisita { get; set; }

        public List<string> CondicoesMedicas { get; set; } = new List<string>();
        public ICollection<PrescriçãoModel>? Prescrições { get; set; } = new List<PrescriçãoModel>();
    }
}
