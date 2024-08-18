namespace LottoGenWeb.Models;

public class NumberGroupRequest
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int NumbersPerGroup { get; set; }
    public int Divergence { get; set; }
    public bool SumCheck { get; set; }
    public bool OeCheck { get; set; }
}

public class Root
{
    public List<NumberGroupRequest> NumberSet { get; set; } = [];
    public int Sets { get; set; }
}