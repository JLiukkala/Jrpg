public class ActionObject
{

    private int origin;
    private string action;
    private int target;

    public ActionObject() { }

    public ActionObject(int originIn, string actionIn, int targetIn)
    {
        origin = originIn;
        action = actionIn;
        target = targetIn;
    }

    public int Origin
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

    public int Target
    {
        get {
            return target;
        }

        set {
            target = value;
        }
    }
}
