using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamer.Infrastructure.EntityFrameworkProvider.Scripts
{
    public static class Scripts
    {
        private static string InitialPath = @"Scripts";
        public static string InsertMusicasBandas => $"{InitialPath}\\InsertMusicasBandas.sql";
    }
}
