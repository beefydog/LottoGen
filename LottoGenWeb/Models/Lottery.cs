namespace LottoGenWeb.Models;

public sealed class Lottery
{
    public string? Location { get; set; }
    public string? Name { get; set; }
    public int? Min1 { get; set; }
    public int? Max1 { get; set; }
    public int? NumbersPerGroup1 { get; set; }
    public int? Min2 { get; set; }
    public int? Max2 { get; set; }
    public int? NumbersPerGroup2 { get; set; }
    public int? Min3 { get; set; }
    public int? Max3 { get; set; }
    public int? NumbersPerGroup3 { get; set; }
    public string? Dups { get; set; }
    public string? Ordered { get; set; }
}