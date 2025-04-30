namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class ErrorResponse
    {
        public string Title { get; set; } = null!;
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
    }
}
