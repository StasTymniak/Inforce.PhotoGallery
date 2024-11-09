namespace Inforce.PhotoGallery.Api.Context.Models
{
    public class Result
    {
        public bool IsSuccessful => ErrorMessage == null;
        public bool IsFailed => !IsSuccessful;
        public string? ErrorMessage { get; set; }
        public Exception? Exception { get; set; }
    }
    public class Result<T> : Result
    {
        public T? Data { get; set; }
    }
}
