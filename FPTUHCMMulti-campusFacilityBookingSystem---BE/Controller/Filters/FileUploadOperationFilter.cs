using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Controller.Filters
{
    /// <summary>
    /// Operation filter để xử lý file uploads trong Swagger UI
    /// </summary>
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Lấy tất cả parameters từ form
            var formFileParams = context.ApiDescription.ParameterDescriptions
                .Where(p => p.Source?.Id == "Form")
                .ToList();

            if (formFileParams.Count == 0)
                return;

            // Kiểm tra xem có IFormFile hoặc List<IFormFile> không
            var hasFormFile = formFileParams.Any(p =>
            {
                var paramType = p.Type;
                // Check for nullable types
                if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    paramType = Nullable.GetUnderlyingType(paramType)!;
                }
                
                return paramType == typeof(IFormFile) ||
                       paramType == typeof(IFormFile[]) ||
                       paramType == typeof(List<IFormFile>);
            });

            if (!hasFormFile)
                return;

            // Tạo schema cho multipart/form-data
            var properties = new Dictionary<string, OpenApiSchema>();
            var requiredFields = new HashSet<string>();

            foreach (var param in formFileParams)
            {
                var paramType = param.Type;
                var isNullable = false;

                // Check for nullable types
                if (paramType.IsGenericType && paramType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    paramType = Nullable.GetUnderlyingType(paramType)!;
                    isNullable = true;
                }

                OpenApiSchema schema;

                if (paramType == typeof(IFormFile))
                {
                    schema = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary",
                        Nullable = isNullable
                    };
                }
                else if (paramType == typeof(IFormFile[]) || paramType == typeof(List<IFormFile>))
                {
                    schema = new OpenApiSchema
                    {
                        Type = "array",
                        Items = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary"
                        },
                        Nullable = isNullable
                    };
                }
                else
                {
                    schema = new OpenApiSchema
                    {
                        Type = "string",
                        Nullable = !param.IsRequired
                    };
                }

                properties[param.Name] = schema;

                if (param.IsRequired)
                {
                    requiredFields.Add(param.Name);
                }
            }

            // Tạo requestBody mới
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = properties,
                            Required = requiredFields
                        }
                    }
                }
            };

            // Xóa các parameters khỏi query/path vì đã đưa vào requestBody
            operation.Parameters?.Clear();
        }
    }
}
