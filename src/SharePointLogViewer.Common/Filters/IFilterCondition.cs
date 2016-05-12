namespace SharePointLogViewer.Common.Filters
{
    public interface IFilterCondition<TValue>
    {
        TValue Value { get; }

        bool IsMatch(TValue compareTo);
    }
}