namespace TvMaze.Core.Models
{
    public class ServiceModelResult<T> : ServiceResult where T : class, new()
    {
        public T? Model { get; set; }

        public override bool IsError
        {
            get
            {
                if (Model != null)
                {
                    return base.IsError;
                }

                return true;
            }
        }
    }
}
