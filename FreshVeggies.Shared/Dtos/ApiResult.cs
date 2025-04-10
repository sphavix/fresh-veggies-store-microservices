﻿namespace FreshVeggies.Shared.Dtos
{
    public record ApiResult(bool IsSuccess, string? Error)
    {
        public static ApiResult Success() => new(true, null);
        public static ApiResult Failure(string errorMessage) => new(false, errorMessage);
    }

    public record ApiResult<TData>(bool IsSuccess, TData data, string? Error)
    {
        public static ApiResult<TData> Success(TData data) => new(true, data, null);
        public static ApiResult<TData> Failure(string errorMessage) => new(false, default!, errorMessage);
    }
}
