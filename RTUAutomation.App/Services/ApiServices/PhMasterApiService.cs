namespace RTUAutomation.App.Services.ApiServices
{
    [AutoRegisterService]
    internal sealed class PhMasterApiService(BaseApiService api)
    {
        private const string BaseUrl = "PhMaster";

        public Task<ResponseResult> AddPhAsync(PhMasterModel request) =>
            api.PostAsync<ResponseResult>($"{BaseUrl}/AddPh", request);

        public Task<ResponseResult> UpdatePhAsync(PhMasterModel request) =>
            api.PutAsync<PhMasterModel, ResponseResult>($"{BaseUrl}/UpdatePh", request);

        public Task<PhMasterModelRoot> GetPhAsync(int phId) =>
            api.GetAsync<PhMasterModelRoot>($"{BaseUrl}/GetPh/{phId}");

        public Task<List<PhMasterModelRoot>> GetAllPhsAsync() =>
            api.GetAsync<List<PhMasterModelRoot>>($"{BaseUrl}/GetAllPhs");

        public Task<ResponseResult> DeletePhAsync(int phId) =>
            api.DeleteAsync<ResponseResult>($"{BaseUrl}/DeletePh/{phId}");

        public Task<ResponseResult> DeletePhsAsync(List<int> phIds) =>
            api.PostAsync<ResponseResult>($"{BaseUrl}/DeletePhs", phIds);
    }
}