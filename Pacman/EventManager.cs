namespace Pacman;

public delegate void ValueChangedEvent(Scene scene, int value);

public class EventManager
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent CandyEaten;
    public event ValueChangedEvent ResetEvent;
    
    private int scoreGained;
    private int loseHealth;
    private int candyEaten;
    private int timerValue;
    
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLoseHealth(int amount) => loseHealth += amount;
    public void PublishCandyEaten(int amount) => candyEaten += amount;
    public void PublishResetEvent(int amount) => timerValue += amount;

    public void UpdateEvents(Scene scene)
    {
        if (scoreGained != 0)
        {
            GainScore?.Invoke(scene, scoreGained);
            scoreGained = 0;
        }

        if (loseHealth != 0)
        {
            LoseHealth?.Invoke(scene, loseHealth);
            loseHealth = 0;
        }

        if (candyEaten != 0)
        {
            CandyEaten?.Invoke(scene, candyEaten);
            candyEaten = 0;
        }

        if (timerValue != 0)
        {
            ResetEvent?.Invoke(scene, timerValue);
            timerValue = 0;
        }
    }
}
