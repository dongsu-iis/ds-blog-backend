namespace Api.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "リクエストに問題あります",
                401 => "認可されてません",
                404 => "リソースが見つかりません",
                500 => "エラーが発生しました",
                _ => null
            };
        }
    }
}
