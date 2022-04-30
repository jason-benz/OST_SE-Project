using System;
using System.Collections.Generic;
using MediaHub.Data.Model;

namespace MediaHub.Test.MediaSearchTest;

public static class MockMovie
{
    public static Movie GetMovie()
    {
        return new Movie(12,
            "MockMovie" + DateTime.Now.Year,
            "mockmovie.ch",
            new List<string>() { "Mock", "NotReal" },
            10, 
            "Mocked movie story", 
            "120", 
            "2022-04-04");
    }

    public static Movie GetAvengersEndgameMovie()
    {
        return new Movie(
            299534,
            "Avengers: Endgame",
            "https://image.tmdb.org/t/p/original/or06FN3Dka5tukK1e9sl16pB3iy.jpg",
            new List<string>() { "Action", "Adventure", "Science Fiction" },
            8,
            "After the devastating events of Avengers: Infinity War, the universe is in ruins due to the efforts of the Mad Titan, Thanos. With the help of remaining allies, the Avengers must assemble once more in order to undo Thanos' actions and restore order to the universe once and for all, no matter what consequences may be in store.",
            "181",
            "2019-04-24");
    }
}