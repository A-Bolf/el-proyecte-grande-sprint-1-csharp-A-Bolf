namespace SitRep.Core.Domain;

public class Guard
{
    public static void Require(bool precondition, string exceptionMessage)
    {
        if (!precondition)
        {
            throw new ArgumentException(exceptionMessage);
        }
    }
}