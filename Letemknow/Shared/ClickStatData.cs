namespace Letemknow.Shared;

public sealed class ClickStatData
{
    public int TotalClicks { get; set; }
    public IEnumerable<TargetClickCount> TargetClicks { get; set; }
}

public record class TargetClickCount(ClickTarget Target, int Clicks);
