namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ServiceBaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T ResponseData { get; set; }
        public string ExceptionMessage { get; set; }
    }
}