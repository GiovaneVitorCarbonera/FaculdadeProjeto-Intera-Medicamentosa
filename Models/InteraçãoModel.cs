namespace Interação_Medicamentosa.Models
{
    public class InteraçãoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descrição { get; set; } = null!;
        public string? Substancia { get; set; } = null!;
        public string? Risco { get; set; } = null!;
        public int MedicamentoId_1 { get; set; }
        public int MedicamentoId_2 { get; set; }
        public MedicamentoModel? Medicamento_1 { get; set; } = null!;
        public MedicamentoModel? Medicamento_2 { get; set; } = null!;
    }
}
