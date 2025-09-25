namespace HospitalManagment.Application.Common;

public class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message)
    {
    }
}