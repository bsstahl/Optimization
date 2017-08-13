using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Entities
{
    public class GridCell
    {
        byte _x;
        byte _y;
        byte _value;

        public byte X
        {
            get { return _x; }
            set
            {
                if (value > 8)
                    throw new ArgumentOutOfRangeException();
                _x = value;
            }
        }

        public byte Y
        {
            get { return _y; }
            set
            {
                if (value > 8)
                    throw new ArgumentOutOfRangeException();
                _y = value;
            }
        }

        public byte Value
        {
            get { return _value; }
            set
            {
                if ((value > 9) || (value < 1))
                    throw new ArgumentOutOfRangeException();
                _value = value;
            }
        }

        public static GridCell Create(byte x, byte y, byte value)
        {
            return new GridCell()
            {
                X = x,
                Y = y,
                Value = value
            };
        }
    }
}
