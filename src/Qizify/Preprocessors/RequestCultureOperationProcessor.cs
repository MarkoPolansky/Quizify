﻿using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Quizify.Api.App.Preprocessors
{
    public class RequestCultureOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "culture",
                Kind = OpenApiParameterKind.Query,
                Type = JsonObjectType.String,
                IsRequired = false
            });

            return true;
        }
    }
}