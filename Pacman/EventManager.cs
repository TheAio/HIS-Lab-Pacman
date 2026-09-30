namespace Pacman;

public delegate void ValueChangedEvent(Scene scene, int value);

public class EventManager
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent CandyEaten;
    
    private int scoreGained;
    private int loseHealth;
    private int candyEaten;
    
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLoseHealth(int amount) => loseHealth += amount;
    public void PublishCandyEaten(int amount) => candyEaten += amount;

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
    }
}
