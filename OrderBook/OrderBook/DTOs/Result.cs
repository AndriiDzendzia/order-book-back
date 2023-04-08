// <copyright file="Result.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

namespace OrderBook.DTOs
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;

        public bool IsFailure => !IsSuccess;

        public string? ErrorMessage { get; set; }

        public string? ErrorCode { get; set; }

        public static Result Success() =>
            new ()
            {
            };

        public static Result<T> Success<T>(T data) => Result<T>.Success(data);

        public static Result Failure(string? errorMessage = null, string? errorCode = null) =>
            new ()
            {
                ErrorMessage = errorMessage,
                ErrorCode = errorCode,
                IsSuccess = false,
            };
    }
}
