using Portfolio_AppRepo_UI.Models;

namespace Portfolio_AppRepo_UI.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<int> Create(AddApplicationModel model);
        Task<AddApplicationModel> GetApplicationById(int Id);
        Task<List<AddApplicationModel>> GetAllApplications();
        Task<bool> Update(AddApplicationModel model);
        Task<bool> Delete(int Id, string User);
        Task<List<ApplicationListModel>> GetApplicationExistanceCheck(string Name);
    }
}
