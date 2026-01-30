namespace Applications.DTOs.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public ErrorResponse? Error { get; set; }

        public static ApiResponse Ok()
        {
            return new ApiResponse { Success = true };
        }

        public static ApiResponse Fail(int code, string message)
        {
            return new ApiResponse
            {
                Success = false,
                Error = new ErrorResponse { Code = code, Message = message }
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        public static new ApiResponse<T> Fail(int code, string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Error = new ErrorResponse { Code = code, Message = message }
            };
        }
    }

    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class PaginationResponse
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }

    public class ApiResponseWithPagination<T> : ApiResponse<T>
    {
        public PaginationResponse? Pagination { get; set; }

        public static ApiResponseWithPagination<T> Ok(T data, int page, int limit, int total)
        {
            return new ApiResponseWithPagination<T>
            {
                Success = true,
                Data = data,
                Pagination = new PaginationResponse
                {
                    Page = page,
                    Limit = limit,
                    Total = total
                }
            };
        }
    }
}


