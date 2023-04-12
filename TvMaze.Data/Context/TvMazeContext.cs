using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Data.Context
{
    public partial class TvMazeContext : DbContext
    {
        public TvMazeContext()
        {
            AttachEventHandlers();
        }

        public TvMazeContext(DbContextOptions<TvMazeContext> options)
            : base(options)
        {
            AttachEventHandlers();
        }

        partial void OnEntityTrackedPartial(object sender, EntityTrackedEventArgs e);

        partial void OnEntityStateChangedPartial(object sender, EntityStateChangedEventArgs e);
    }
}
