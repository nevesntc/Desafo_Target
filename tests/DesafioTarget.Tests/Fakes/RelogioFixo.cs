namespace DesafioTarget.Tests.Fakes;

internal sealed class RelogioFixo : TimeProvider
{
    private readonly DateTimeOffset _agora;

    public RelogioFixo(DateTime agora)
    {
        _agora = new DateTimeOffset(agora, TimeSpan.Zero);
    }

    public override DateTimeOffset GetUtcNow() => _agora;

    public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
}
