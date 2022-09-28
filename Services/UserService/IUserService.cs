using Portfolio_AppRepo_UI.Models;

namespace Portfolio_AppRepo_UI.Services.UserService
{
    public interface IUserService
    {
        Task<bool> Create(UserModel model);
        Task<bool> Update(UserModel model);
        Task<bool> Delete(UserModel model);
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(int Id);
    }
}
