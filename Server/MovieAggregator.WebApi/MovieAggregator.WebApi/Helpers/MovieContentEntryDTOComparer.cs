using MovieAggregator.DTOs;
using System;
using System.Collections.Generic;

namespace MovieAggregator.WebApi.Helpers
{
    public class MovieContentEntryDTOComparer : IComparer<MovieContentEntryDTO>
    {
        public int Compare(MovieContentEntryDTO x, MovieContentEntryDTO y)
        {
            var xInfo = x.Info;
            var yInfo = y.Info;

            if (xInfo.Released.HasValue && yInfo.Released.HasValue)
            {
                return -Math.Sign((xInfo.Released.Value - yInfo.Released.Value).Days);
            }

            if (xInfo.Released.HasValue && yInfo.Released.HasValue == false)
            {
                return -1;
            }

            if (xInfo.Released.HasValue == false && yInfo.Released.HasValue)
            {
                return 1;
            }

            return 0;
        }
    }
}