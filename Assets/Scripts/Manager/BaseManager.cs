public class BaseManager<T> where T : new()
{
    protected BaseManager()
    {
        OnInit();
    }
    private static T baseManager;
    protected virtual void OnInit() { }
    public static T GetInstance
    {
        get
        {
            baseManager = baseManager ?? new T();
            return baseManager;
        }
    }
    public virtual void OnCreat() { }
    public virtual void OnDestroy() { }
    public virtual void Update() { }
}
