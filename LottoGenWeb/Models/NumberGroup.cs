namespace LottoGenWeb.Models;

public class NumberGroup(int groupId, bool enabled, int minValue, int maxValue, int numbersPerGroup, int divergence, bool checkSumEnabled, bool checkOEEnabled)
{
    public int GroupId { get; set; } = groupId;
    public bool Enabled { get; set; } = enabled;
    public int MinValue { get; set; } = minValue;
    public int MaxValue { get; set; } = maxValue;
    public int NumbersPerGroup { get; set; } = numbersPerGroup;
    public int Divergence { get; set; } = divergence;
    public bool CheckSumEnabled { get; set; } = checkSumEnabled;
    public bool CheckOEEnabled { get; set; } = checkOEEnabled;

}