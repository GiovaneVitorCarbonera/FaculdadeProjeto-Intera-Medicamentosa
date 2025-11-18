namespace Interação_Medicamentosa.Models
{
    public class MedicamentoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;
        public string? Fabricante { get; set; }

        public decimal PrecoMin { get; set; }
        public decimal PrecoMax { get; set; }

        public string? FormaFarmaceutica { get; set; }
        public string? ClasseTerapeutica { get; set; }
        public string? Indicacoes { get; set; }
        public string? Armazenamento { get; set; }
    }
}
