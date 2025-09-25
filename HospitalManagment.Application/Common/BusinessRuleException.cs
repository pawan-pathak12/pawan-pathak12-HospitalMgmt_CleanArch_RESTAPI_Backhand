namespace HospitalManagment.Application.Common;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string messsage) : base(messsage)
    {
    }
}