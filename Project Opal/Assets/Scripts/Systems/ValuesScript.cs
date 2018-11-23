using UnityEngine;


public class ValuesScript : MonoBehaviour
{
    public static ValuesScript singleton;

    public int love = 0;
    public int intellect = 0;
    public int charisma = 0;

    public int loveEXP = 0;
    public int intellectEXP = 0;
    public int charismaEXP = 0;

    public float seconds;
    public int minutes;
    public int hours;
    public int totalHours;
    public int days;
    public int weeks;

    public float energy = 100;
    public float energyDeplition = 0.08f;
    public float cash;
    public Vector3 playerPosition;
    public string currentMap;
    

    public bool ding = false;  // Used in SkillManager and Gainz scripts to trigger level up ComeOut() method.


    private void Awake()
    {
        singleton = this;
    }

}
