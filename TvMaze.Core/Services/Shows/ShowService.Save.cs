using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml;
using TvMaze.Core.Clients.TvMaze.Models.Cast;
using TvMaze.Core.Clients.TvMaze.Models.Schedule;
using TvMaze.Core.Clients.TvMaze.Models.Show;
using TvMaze.Domain;

namespace TvMaze.Core.Services.Shows
{
    public partial class ShowService
    {
        public async Task SaveShowUrlsAsync(List<ScheduleOverviewResponse> scheduleOverviewList)
        {
            if (scheduleOverviewList == null)
            {
                return;
            }

            var showLinks = scheduleOverviewList.Select(s => s._links.show.href).Distinct().ToList();

            var oldLinks = await _context.ShowLinks.Where(l => !showLinks.Contains(l.Url)).ToListAsync();
            if (oldLinks.Any())
            {
                _context.ShowLinks.RemoveRange(oldLinks);
                await _context.SaveChangesAsync();
            }

            var existingLinks = await _context.ShowLinks.Select(l => l.Url).AsNoTracking().ToListAsync();
            showLinks.RemoveAll(link => existingLinks.Contains(link));

            if (showLinks.Any())
            {
                var newLinks = showLinks.Select(link => new ShowLink() { Url = link }).ToList();

                await _context.ShowLinks.AddRangeAsync(newLinks);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveShowWithCastAsync(List<ShowDetailResponse> showDetailResponseList)
        {
            await SaveAllShowsAsync(showDetailResponseList);

            await SaveAllCastPersonesAsync(showDetailResponseList);

            await SaveShowCastPersonesRelationAsync(showDetailResponseList);

        }

        private async Task SaveAllShowsAsync(List<ShowDetailResponse> showDetailResponseList)
        {
            if (showDetailResponseList.Any())
            {
                var showIDs = showDetailResponseList.Select(s => s.Id).ToList();

                var entities = await _context.Shows.Where(s => showIDs.Contains(s.ShowId)).ToListAsync();

                if (entities.Any())
                {
                    entities.ForEach((entity) =>
                    {
                        _context.Shows.Attach(entity);
                        _context.Entry(entity).State = EntityState.Modified;

                    });
                }

                var existingShowEntities = entities.Select(e => e.ShowId).ToList();

                var newShows = showDetailResponseList.Select(s => new Show()
                {
                    ShowId = s.Id,
                    Name = s.Name,
                }).Where(s => !existingShowEntities.Contains(s.ShowId)).ToList();

                if (newShows.Any())
                    newShows.ForEach((entity) =>
                    {
                        _context.Shows.Attach(entity);
                        _context.Entry(entity).State = EntityState.Added;
                    });

                await _context.SaveChangesAsync();
            }
        }

        private async Task SaveAllCastPersonesAsync(List<ShowDetailResponse> showDetailResponseList)
        {
            var allCastPersones = showDetailResponseList.SelectMany(s => s._embedded.Cast.Select(c => c.Person)).Distinct().ToList();

            if (allCastPersones.Any())
            {

                var personIDs = allCastPersones.Select(s => s.Id).ToList();

                var entities = await _context.CastPersones.Where(s => personIDs.Contains(s.PersonId)).ToListAsync();

                if (entities.Any())
                {
                    entities.ForEach((entity) =>
                    {
                        _context.CastPersones.Attach(entity);
                        _context.Entry(entity).State = EntityState.Modified;

                    });
                }

                var existingPersoneEntities = entities.Select(e => e.PersonId).ToList();

                var newCastPersones = allCastPersones.Select(s => new CastPersone()
                {
                    PersonId = s.Id,
                    Name = s.Name,
                    Birthday = s.Birthday,
                }).Where(s => !existingPersoneEntities.Contains(s.PersonId)).ToList();

                if (newCastPersones.Any())
                    newCastPersones.ForEach((entity) =>
                    {
                        _context.CastPersones.Attach(entity);
                        _context.Entry(entity).State = EntityState.Added;
                    });

                await _context.SaveChangesAsync();
            }
        }

        private async Task SaveShowCastPersonesRelationAsync(List<ShowDetailResponse> showDetailResponseList)
        {
            if (showDetailResponseList.Any())
            {
                var allShowCastPersones = showDetailResponseList.SelectMany(s => s._embedded.Cast.Select(c => new { ShowId = s.Id, CastPersoneId = c.Person.Id })).Distinct().ToList();

                //var obsoleteEntities = await _context.ShowCastRelation.Where(s => !showDetailResponseList.Any(r=>r.Id == s.ShowId && !r._embedded.Cast.Any(c=>c.Person.Id == s.CastPersoneId))).ToListAsync();
                var showIDs = allShowCastPersones.Select(c=> c.ShowId).Distinct().ToList();
                var personeIDs = allShowCastPersones.Select(c => c.CastPersoneId).Distinct().ToList();

                var obsoleteEntitiesByShows = await _context.ShowCastPersoneRelation.Where(s => !showIDs.Contains(s.ShowId)).ToListAsync();
                var obsoleteEntitiesByPersone = obsoleteEntitiesByShows.Where(s => !personeIDs.Contains(s.CastPersoneId)).ToList();


                //var obsoleteEntities = await _context.ShowCastPersoneRelation.Where(s => !allShowCastPersones.Any(r => r.ShowId == s.ShowId && r.CastPersoneId == s.CastPersoneId)).ToListAsync();

                if (obsoleteEntitiesByPersone.Any())
                {
                    _context.ShowCastPersoneRelation.RemoveRange(obsoleteEntitiesByPersone);
                    await _context.SaveChangesAsync();
                }

                var entities = await _context.ShowCastPersoneRelation.AsNoTracking().ToListAsync();

                //                var newEntities = allShowCastPersones.Where(s => !entities.Any(e => e.ShowId == s.ShowId && e.CastPersoneId == s.CastPersoneId)).Select(r => new ShowCastPersoneRelation(r.ShowId, r.CastPersoneId)).ToList();

                var shows = _context.Shows.Where(s=> showIDs.Contains(s.ShowId)).AsNoTracking().ToList();
                var persones = _context.CastPersones.Where(s => personeIDs.Contains(s.PersonId)).AsNoTracking().ToList();

                var newEntities = allShowCastPersones.Where(s => !entities
                                                            .Any(e => e.ShowId == s.ShowId && e.CastPersoneId == s.CastPersoneId))
                                .Select(r => new ShowCastPersoneRelation(shows.First(s=>s.ShowId == r.ShowId).Id, persones.First(p=>p.PersonId == r.CastPersoneId).Id)).ToList();

                if (newEntities.Any())
                {
                    await _context.ShowCastPersoneRelation.AddRangeAsync(newEntities);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}