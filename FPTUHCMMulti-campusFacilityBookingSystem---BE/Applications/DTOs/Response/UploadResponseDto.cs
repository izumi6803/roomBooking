namespace Applications.DTOs.Response
{
    public class UploadResponseDto
    {
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long Size { get; set; }
    }

    public class MultiUploadResponseDto
    {
        public List<string> Urls { get; set; } = new();
        public int TotalFiles { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
    }
}
