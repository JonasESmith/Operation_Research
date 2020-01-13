using System.Collections.Generic;

namespace Project_1.classes
{
    class TestCases
    {
        public List<List<variable>> cases = new List<List<variable>>()
        {
            new List<variable>() {
                new variable('a', 2 ),
                new variable('b', 3 ),
                new variable('c', 5 ),
                new variable('d', -4),
                new variable('r', 8 ),
                new variable('s', 9 ),
            },

            new List<variable>() {
                new variable('a', 2 ),
                new variable('b', -5 ),
                new variable('c', -4 ),
                new variable('d', 10),
                new variable('r', 8 ),
                new variable('s', 9 ),
            },

            new List<variable>() {
                new variable('a', 2 ),
                new variable('b', -5 ),
                new variable('c', -4 ),
                new variable('d', 10),
                new variable('r', 8 ),
                new variable('s', -16 ),
            },

            new List<variable>() {
                new variable('a', 2 ),
                new variable('b', 3 ),
                new variable('c', 5 ),
                new variable('d', 0 ),
                new variable('r', 8 ),
                new variable('s', 12 ),
            },

            new List<variable>() {
                new variable('a', 2),
                new variable('b', 3),
                new variable('c', 0),
                new variable('d', 5),
                new variable('r', 8),
                new variable('s', 12),
            },

            new List<variable>() {
                new variable('a', 0),
                new variable('b', 0),
                new variable('c', 2),
                new variable('d', -3),
                new variable('r', 0),
                new variable('s', 5),
            },

            new List<variable>() {
                new variable('a', 4),
                new variable('b', 5),
                new variable('c', 0),
                new variable('d', 0),
                new variable('r', 8),
                new variable('s', 3),
            },

        };
    }
}
