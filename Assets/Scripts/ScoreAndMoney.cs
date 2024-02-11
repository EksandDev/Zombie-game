public class ScoreAndMoney
{
    public static int Score 
    {  
        get => _score; 
        set
        {
            _score = value;

            if (_score < 0)
                _score = 0;
        }
    }
    public static int Money 
    { 
        get => _money;
        set
        {
            _money = value;

            if (_money < 0)
                _money = 0;
        }
    }

    private static int _score = 0;
    private static int _money = 0;
}
