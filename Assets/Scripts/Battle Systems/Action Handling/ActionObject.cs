/********************************************
 * ActionObject class
 * 
 * This is stricly an object for passing around data 
 * 
 * Contains an action string thats the name of a skill
 * the battle entity that is the origin of the skill
 * and the target of said skill
 * 
 */
public class ActionObject
{
    //Variables we are tracking
    private BattleEntity origin;
    private string action;
    private BattleEntity target;

    public ActionObject() { }

    //Initializer when a new instance of actionObject is made the option to pass in parameters for all the variables is here
    public ActionObject(BattleEntity originIn, string actionIn, BattleEntity targetIn)
    {
        origin = originIn;
        action = actionIn;
        target = targetIn;
    }

    //Getters and setters for all three variables
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
