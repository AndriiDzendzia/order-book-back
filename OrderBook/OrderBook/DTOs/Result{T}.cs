// <copyright file="Result{T}.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

namespace OrderBook.DTOs
{
    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T? data = default) =>
            new ()
            {
                Data = data,
            };

        public static new Result<T> Failure(string? errorMessage = null, string? errorCode = null) =>
            new ()
            {
                ErrorMessage = errorMessage,
                ErrorCode = errorCode,
                IsSuccess = false,
            };
    }
}
