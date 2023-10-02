namespace Domain.Models.Response;

public class ListResponse<T>
{
    public int TotalCount { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }

    public bool IsFirst { get; set; }

    public bool IsLast { get; set; }

    public List<T> Data { get; set; }

    public ListResponse()
    {
        TotalCount = PageNumber = PageSize = 0;
        IsFirst = IsLast = true;
        Data = new List<T>();
    }

    public ListResponse(List<T> data)
    {
        IsFirst = IsLast = true;
        Data = data;
        TotalCount = data.Count;
        PageNumber = PageSize = 0;
    }

    public ListResponse(List<T> data, int allCount, int pageNumber, int pageSize)
    {
        IsFirst = pageNumber == 1;
        Data = data;
        TotalCount = allCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsLast = pageNumber * pageSize >= allCount;
    }

    public ListResponse(int totalCount, int pageNumber, int pageSize, bool isFirst, bool isLast, List<T> data)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsFirst = isFirst;
        IsLast = isLast;
        Data = data;
    }

}