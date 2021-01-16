using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    public static float Speed => Instance.speed;

    [SerializeField]
    private float aggroRadius = 4f;

    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField]
    private float attackRange = 3f;

    public static float AttackRange => Instance.attackRange;

    [SerializeField]
    private GameObject AIprojectialPrefab;


    [SerializeField]
    private bool timerDone;

    public static bool TimerDone => Instance.timerDone;


    [SerializeField]
    private bool timerStart;

    public static bool TimerStart => Instance.timerDone;
    [SerializeField]
    private float timer = 3f;

    [SerializeField]
    private int unitLimit = 80;

    public int UnitLimit => Instance.unitLimit;

    public static float Timer => Instance.timer;

    public static GameObject AIProjectialPrefab => Instance.AIprojectialPrefab;

    public static GameSettings Instance { get; private set; }






    //so if there's more than 1 instance of this obj
    //it'll destroy the other version of this obj
    //also of corse this is a singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    } 
}
