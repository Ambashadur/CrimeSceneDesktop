﻿namespace CS.Contracts;

public class PageResult<TData>
{
    public int Page { get; set; }

    public int Count { get; set; }

    public IEnumerable<TData> Data { get; set; } = Enumerable.Empty<TData>();
}