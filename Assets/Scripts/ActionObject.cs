public class ActionObject
{

    private BattleEntity origin;
    private string action;
    private BattleEntity target;

    public ActionObject() { }

    public ActionObject(BattleEntity originIn, string actionIn, BattleEntity targetIn)
    {
        origin = originIn;
        action = actionIn;
        target = targetIn;
    }

    public BattleEntity Origin
    {
        get {
            return origin;
        }

        set {
            origin = value;
        }
    }

    public string Action
    {
        get {
            return action;
        }

        set {
            action = value;
        }
    }

    public BattleEntity Target
    {
        get {
            return target;
        }

        set {
            target = value;
        }
    }
}
