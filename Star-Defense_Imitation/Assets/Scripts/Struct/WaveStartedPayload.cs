public struct WaveStartedPayload
{
    public int currentWave;
    public int totalWave;

    public WaveStartedPayload(int current, int total)
    {
        currentWave = current;
        totalWave = total;
    }
}
