using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace LosBarriosEvents.Authorization
{
    public static class LosBarriosEventsOperations
    {
        //Operations that control what users can edit/see on the page based on role will go here 
    }

    public class UserRoles{
        public static readonly string LosBarriosEventsAdministratorRole = "Administrator";
        public static readonly string LosBarriosEventsStudentRole = "Student";
        public static readonly string LosBarriosEventsVolunteerRole = "Volunteer";
        public static readonly string LosBarriosEventsSpeakerRole = "Speaker";
    }
}