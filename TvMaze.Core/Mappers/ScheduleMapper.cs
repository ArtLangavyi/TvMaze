using TvMaze.Core.Models.Schedule;
using TvMaze.Domain;

namespace TvMaze.Core.Mappers
{
    public static class ScheduleMapper
    {
        public static ShowLink ToShowLinkEntity(this ScheduleShowLink model)
        {
            var entity = new ShowLink();

            if (model == null)
                return entity;

            entity.Url = model.href;


            return entity;
        }
    }
}
