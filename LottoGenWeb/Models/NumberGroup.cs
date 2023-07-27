namespace LottoGenWeb.Models;

public class NumberGroup
{
    public NumberGroup(int groupId, bool enabled, int minValue, int maxValue, int numbersPerGroup, int divergence, bool checkSumEnabled, bool checkOEEnabled)
    {
        GroupId = groupId;
        Enabled = enabled;
        MinValue = minValue;
        MaxValue = maxValue;
        NumbersPerGroup = numbersPerGroup;
        Divergence = divergence;
        CheckOEEnabled = checkOEEnabled;
        CheckSumEnabled = checkSumEnabled;
    }

    public int GroupId { get; set; }
    public bool Enabled { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int NumbersPerGroup { get; set; }
    public int Divergence { get; set; }
    public bool CheckSumEnabled { get; set; }
    public bool CheckOEEnabled { get; set; }

}