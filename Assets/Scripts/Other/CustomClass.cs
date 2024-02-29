public class CMDData
{
    public string Name;
    public object Value;
    public CMDOperate OperateID;
    public CMDData(string name, object value, CMDOperate id)
    {
        Name = name;
        Value = value;
        OperateID = id;
    }
}
public enum CMDOperate
{
    Active,
    Text,
    ChangeValue,
}
