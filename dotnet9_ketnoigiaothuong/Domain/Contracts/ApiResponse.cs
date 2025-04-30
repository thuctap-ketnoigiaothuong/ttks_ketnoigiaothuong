namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }
}
