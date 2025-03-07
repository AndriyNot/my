﻿namespace ExamProject.API.Models.Response
{
    public class PaginatedProductsResponse<T>
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public long Count { get; init; }

        public IEnumerable<T> Data { get; init; } = null!;
    }
}
