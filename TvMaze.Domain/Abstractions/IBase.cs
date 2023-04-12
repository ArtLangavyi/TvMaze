namespace TvMaze.Domain.Abstractions
{
    public interface IBase<IdType>
    {
        IdType Id { get; set; }

        string CreatedBy { get; set; }

        string UpdatedBy { get; set; }
    }
}
