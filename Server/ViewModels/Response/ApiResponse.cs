namespace Server.ViewModels.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public required T Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
