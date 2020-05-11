using System;

namespace MarsRover.Core
{
    public class MarsRover
    {
        private int _x;
        private int _y;
        private string _headingTo;
        public const string INVALID_COMMAND = "Invalid command";

        public MarsRover(int x, int y, string headingTo)
        {
            _x = x;
            _y = y;
            _headingTo = headingTo;
        }

        public void Process(string commands)
        {
            foreach (var command in commands.ToCharArray())
            {
                if (command == 'f')
                {
                    if (_headingTo == "N")
                    {
                        _y++;
                    }
                    else if (_headingTo == "E")
                    {
                        _x++;
                    }
                }
                else if (command == 'b')
                {
                    if (_headingTo == "N")
                    {
                        _y--;
                    }
                    else if (_headingTo == "E")
                    {
                        _x--;
                    }
                }
                else if (command == 'l')
                {
                    if (_headingTo == "N")
                    {
                        _headingTo = "W";
                    }
                }
                else if (command == 'r')
                {
                    if (_headingTo == "N")
                    {
                        _headingTo = "E";
                    }
                    else if (_headingTo == "E")
                    {
                        _headingTo = "S";
                    }
                }
                else
                {
                    throw new ArgumentException(INVALID_COMMAND);
                }
            }
        }

        public bool IsAtHeadingTo(int x, int y, string headingTo)
        {
            return _x == x && _y == y && _headingTo == headingTo;
        }
    }
}
