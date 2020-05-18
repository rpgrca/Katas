using System;
using System.Collections.Generic;

namespace MarsRover.Core
{
    public class MarsRover
    {
        public const string INVALID_COMMAND = "Invalid command";

        private int _x;
        private int _y;
        private int _headingTo;
        private readonly string[] _directions = { "N", "E", "S", "W"};
        private readonly List<Dictionary<char, (int, int)>> _movements = new List<Dictionary<char, (int, int)>>
        {
            { new Dictionary<char, (int, int)> { { 'f', (0, 1) }, { 'b', (0, -1) } } },
            { new Dictionary<char, (int, int)> { { 'f', (1, 0) }, { 'b', (-1, 0) } } },
            { new Dictionary<char, (int, int)> { { 'f', (0, -1) }, { 'b', (0, 1) } } },
            { new Dictionary<char, (int, int)> { { 'f', (-1, 0) }, { 'b', (1, 0) } } }
        };

        public MarsRover(int x, int y, string headingTo)
        {
            _x = x;
            _y = y;
            _headingTo = Array.IndexOf(_directions, headingTo);
        }

        private void TurnAround(char command) =>
            _headingTo = (_headingTo + (command == 'r' ? 1 : 3)) % 4;

        private (int, int) GetMovementOffset(char command) =>
            _movements[_headingTo][command];

        public void Process(string commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'l':
                    case 'r':
                        TurnAround(command);
                        break;
                    case 'f':
                    case 'b':
                        var (offsetX, offsetY) = GetMovementOffset(command);
                        _x += offsetX;
                        _y += offsetY;
                        break;
                    default:
                        throw new ArgumentException(INVALID_COMMAND);
                }
            }
        }

        public bool IsAtHeadingTo(int x, int y, string headingTo) =>
            _x == x && _y == y && _directions[_headingTo] == headingTo;
    }
}
