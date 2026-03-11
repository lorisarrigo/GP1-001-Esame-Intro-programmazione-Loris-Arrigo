public class PlayerBase : HealthManager
{
    //The class used to manage the health of the Player

    /*Singleton used in:
     * Enemy.cs: to check if it can despawn;
     * UIManager to Update the Player HelthBar.
    */

    public static PlayerBase Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    //set the health of the Player to the Max
    private void Start()
    {
        currentHP = maxHP;
    }

    //Opens the GameOver Screen
    public override void Despawn()
    {
        UIManager.Instance.LostGame();
    }
}