using TestingPlatform.Constans;

namespace TestingPlatform.Extensions
{
    public static class HttpContextExtension
    {
        public static int TryGetUserId(this HttpContext context)
        {
            var studentIdValue = context.User.Claims
             .FirstOrDefault(c => c.Type == TestingPlatformClaimType.StudentId)?.Value;

            if (!int.TryParse(studentIdValue, out var studentId))
                throw new InvalidOperationException("User is not authorized as a student.");

            return studentId;
        }
    }
}
