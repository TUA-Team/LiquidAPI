namespace LiquidAPI.Data
{
    public struct Bit
    {
        internal byte data;

        public bool this[int index]
        {
            get
            {
                return (data & (1 << index)) != 0;
            }
            set
            {
                data = (byte)((data & ~(1 << index)) | (value ? (1 << index) : 0));
            }
        }

        public Bit(byte b)
        {
            data = b;
        }

        public static implicit operator Bit(byte b)
        {
            return new Bit(b);
        }

        public static implicit operator byte(Bit b)
        {
            return b.data;
        }

        public static byte operator ~(Bit b1)
        {
            return (byte)(~b1.data);
        }

        public static byte operator +(Bit b1, Bit b2)
        {
            return (byte)(b1.data + b2.data);
        }

        public static byte operator -(Bit b1, Bit b2)
        {
            return (byte)(b1.data - b2.data);
        }

        public static byte operator *(Bit b1, Bit b2)
        {
            return (byte)(b1.data * b2.data);
        }

        public static byte operator /(Bit b1, Bit b2)
        {
            return (byte)(b1.data / b2.data);
        }

        public static byte operator %(Bit b1, Bit b2)
        {
            return (byte)(b1.data % b2.data);
        }

        public static byte operator &(Bit b1, Bit b2)
        {
            return (byte)(b1.data & b2.data);
        }

        public static byte operator |(Bit b1, Bit b2)
        {
            return (byte)(b1.data | b2.data);
        }

        public static bool operator ==(Bit b1, Bit b2)
        {
            return b1.data == b2.data;
        }

        public static bool operator !=(Bit b1, Bit b2)
        {
            return b1.data != b2.data;
        }

        public static bool operator <(Bit b1, Bit b2)
        {
            return b1.data < b2.data;
        }

        public static bool operator >(Bit b1, Bit b2)
        {
            return b1.data > b2.data;
        }

        public static bool operator <=(Bit b1, Bit b2)
        {
            return b1.data <= b2.data;
        }

        public static bool operator >=(Bit b1, Bit b2)
        {
            return b1.data >= b2.data;
        }

        public override bool Equals(object obj)
        {
            return data == ((Bit)obj).data;
        }

        public override int GetHashCode()
        {
            return data;
        }
    }
}
