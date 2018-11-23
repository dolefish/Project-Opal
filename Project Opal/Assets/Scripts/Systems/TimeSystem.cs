using UnityEngine;
using UnityEngine.UI;


public class TimeSystem : MonoBehaviour {

    private UIObjects ui;
    private PlayerStats pStats;

    [Header("Time")]
    public GameTime gameTime;

    [Header("StartTime")]
    public GameTime startTime;


    void Start ()
    {
        ui = UIObjects.singleton;
        pStats = PlayerStats.singleton;

        SetTime(startTime);
        CheckSunMoon();
	}

	void Update ()
    {
        gameTime.seconds += Time.deltaTime;

        if (gameTime.seconds >= 1.25f)
        {
            pStats.DepleteEnergy();

            gameTime.minutes++;
            gameTime.seconds = 0;

            if (gameTime.minutes >= 60)
            {  
                gameTime.hours++;
                gameTime.totalHours++;
                gameTime.minutes = 0;

                if (gameTime.hours == 12 && gameTime.isAM == true)
                {
                    gameTime.isAM = false;
                    gameTime.hours = 0;
                }
                else if (gameTime.hours == 12 && gameTime.isAM == false)
                {
                    gameTime.isAM = true;
                    gameTime.hours = 0;
                }

                CheckSunMoon();

            }
        }

        if (NextDay())
        {
            gameTime.totalHours = 0;
            gameTime.day++;
            gameTime.totalDays++;
        }

        if (NextMonth())
        {
            gameTime.totalDays = 1;
            gameTime.month++;
        }

        if (NextYear())
        {
            gameTime.year++;
            gameTime.month = 0;
        }
        
        ui.timeObjects.UpdateTime(gameTime);
    }

    public void CheckSunMoon()
    {

        if (gameTime.totalHours > 5 && gameTime.totalHours < 19)
        {
            ui.timeObjects.UpdateSunMoon(true);
        }
        else
        {
            ui.timeObjects.UpdateSunMoon(false);
        }
        
    }


    public bool NextDay()
    {
        if (gameTime.totalHours >= 24)
        {
            return true;
        }
        return false;
    }

    public bool NextMonth()
    {
        if (gameTime.totalDays >= 28)
        {
            return true;
        }
        return false;
    }

    public bool NextYear()
    {
        if ((int)gameTime.month > 11)
        {
            return true;
        }
        return false;
    }

    public void SetTime(GameTime timeStamp)
    {
        gameTime.minutes = timeStamp.minutes;
        gameTime.hours = timeStamp.hours;
        gameTime.day = timeStamp.day;
        gameTime.totalDays = timeStamp.totalDays;
        gameTime.month = timeStamp.month;
        gameTime.year = timeStamp.year;

        if (timeStamp.isAM)
        { gameTime.isAM = true; gameTime.totalHours = timeStamp.hours; }
        else if (!timeStamp.isAM && timeStamp.hours == 12)
        { gameTime.isAM = false; gameTime.totalHours = 12; }
        else
        { gameTime.isAM = false; gameTime.totalHours = timeStamp.hours + 12; }

    }
}

[System.Serializable]
public class GameTime
{
    [Header("Time")]
    public bool isAM;
    public float seconds;
    public int minutes;
    public int hours;
    public int totalHours;
  

    [Header("MetaTime")]
    public Day day;
    public int totalDays;
    public Month month;
    public int year = 1994;

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Month
    {
        January,
        Febuary,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    
}
