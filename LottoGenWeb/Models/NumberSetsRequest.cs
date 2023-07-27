namespace LottoGenWeb.Models;

public class NumberGroupRequest
{
    public int min { get; set; }
    public int max { get; set; }
    public int numbersPerGroup { get; set; }
    public int divergence { get; set; }
    public bool sumCheck { get; set; }
    public bool oeCheck { get; set; }
}

public class Root
{
    public List<NumberGroupRequest> numberSet { get; set; } = new List<NumberGroupRequest>();
    public int sets { get; set; }
}