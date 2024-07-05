namespace LottoGenApi.Models;

public sealed class NumberGroup
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int NumbersPerGroup { get; set; }
    public decimal Divergence { get; set; }
    public bool SumCheck { get; set; }
    public bool OeCheck { get; set; }
}

public sealed class SetsRequest(List<NumberGroup> numberSet, int sets)
{
    public List<NumberGroup> NumberSet { get; set; } = numberSet ?? throw new ArgumentNullException(nameof(numberSet));
    public int Sets { get; set; } = sets;
}
