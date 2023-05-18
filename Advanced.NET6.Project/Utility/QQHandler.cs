using Advanced.NET6.ExceptionService;
using Microsoft.AspNetCore.Authorization;

namespace Advanced.NET6.Project.Utility
{
    public class QQHandler : AuthorizationHandler<QQEmailRequirement>
    {

        private IUserService _UserService;

        public QQHandler(IUserService userService)
        {
            this._UserService = userService;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QQEmailRequirement requirement)
        {

            if (context.User.Claims.Count() == 0)
            {
                return Task.CompletedTask;
            }

              string userId = context.User.Claims.First(c => c.Type == "Userid").Value;
              string qq = context.User.Claims.First(c => c.Type == "QQ").Value;

            if (_UserService.Validata(userId, qq))
            {
                context.Succeed(requirement); //验证通过了
            }
            //在这里就可以做验证

            return Task.CompletedTask;
        }
    }
}
