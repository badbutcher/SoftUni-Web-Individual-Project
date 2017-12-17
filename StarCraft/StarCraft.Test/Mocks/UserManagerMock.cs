namespace StarCraft.Test.Mocks
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using StarCraft.Data.Models;

    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}