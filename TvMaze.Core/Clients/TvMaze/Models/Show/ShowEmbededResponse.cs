using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Core.Clients.TvMaze.Models.Cast;

namespace TvMaze.Core.Clients.TvMaze.Models.Show
{
    public class ShowEmbededResponse
    {
        public List<CastResponse> Cast { get; set; }
    }
}
