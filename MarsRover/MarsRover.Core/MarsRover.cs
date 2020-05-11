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
                if (_headingTo == "N")
                {
                    if (command == 'f')
                    {
                        _y++;
                    }
                    else if (command == 'b')
                    {
                        _y--;
                    }
                    else if (command == 'l')
                    {
                        _headingTo = "W";
                    }
                    else if (command == 'r')
                    {
                        _headingTo = "E";
                    }
                    else
                    {
                        throw new ArgumentException(INVALID_COMMAND);
                    }
                }

                else if (_headingTo == "E")
                {
                    if (command == 'f')
                    {
                        _x++;
                    }
                    else if (command == 'b')
                    {
                        _x--;
                    }
                    else if (command == 'l')
                    {
                            _headingTo = "N";
                    }
                    else if (command == 'r')
                    {
                        _headingTo = "S";
                    }
                    else
                    {
                        throw new ArgumentException(INVALID_COMMAND);
                    }
                }
            }
        }

        public bool IsAtHeadingTo(int x, int y, string headingTo)
        {
            return _x == x && _y == y && _headingTo == headingTo;
        }
    }
}
