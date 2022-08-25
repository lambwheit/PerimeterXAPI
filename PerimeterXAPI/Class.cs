using System.Text;

namespace PerimeterXAPI
{
    public struct Uuid : IEquatable<Uuid>
    {
        private readonly long leastSignificantBits;
        private readonly long mostSignificantBits;


        public Uuid(long mostSignificantBits, long leastSignificantBits)
        {
            this.mostSignificantBits = mostSignificantBits;
            this.leastSignificantBits = leastSignificantBits;
        }

        public long LeastSignificantBits
        {
            get { return leastSignificantBits; }
        }

        public long MostSignificantBits
        {
            get { return mostSignificantBits; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Uuid))
            {
                return false;
            }

            Uuid uuid = (Uuid)obj;

            return Equals(uuid);
        }

        public bool Equals(Uuid uuid)
        {
            return this.mostSignificantBits == uuid.mostSignificantBits && this.leastSignificantBits == uuid.leastSignificantBits;
        }

        public override int GetHashCode()
        {
            return ((Guid)this).GetHashCode();
        }

        public override string ToString()
        {
            return ((Guid)this).ToString();
        }


        public static bool operator ==(Uuid a, Uuid b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Uuid a, Uuid b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Guid(Uuid uuid)
        {
            if (uuid == default(Uuid))
            {
                return default(Guid);
            }

            byte[] uuidMostSignificantBytes = BitConverter.GetBytes(uuid.mostSignificantBits);
            byte[] uuidLeastSignificantBytes = BitConverter.GetBytes(uuid.leastSignificantBits);
            byte[] guidBytes = new byte[16] {
                uuidMostSignificantBytes[4],
                uuidMostSignificantBytes[5],
                uuidMostSignificantBytes[6],
                uuidMostSignificantBytes[7],
                uuidMostSignificantBytes[2],
                uuidMostSignificantBytes[3],
                uuidMostSignificantBytes[0],
                uuidMostSignificantBytes[1],
                uuidLeastSignificantBytes[7],
                uuidLeastSignificantBytes[6],
                uuidLeastSignificantBytes[5],
                uuidLeastSignificantBytes[4],
                uuidLeastSignificantBytes[3],
                uuidLeastSignificantBytes[2],
                uuidLeastSignificantBytes[1],
                uuidLeastSignificantBytes[0]
            };

            return new Guid(guidBytes);
        }

        public static implicit operator Uuid(Guid value)
        {
            if (value == default(Guid))
            {
                return default(Uuid);
            }

            byte[] guidBytes = value.ToByteArray();
            byte[] uuidBytes = new byte[16] {
                guidBytes[6],
                guidBytes[7],
                guidBytes[4],
                guidBytes[5],
                guidBytes[0],
                guidBytes[1],
                guidBytes[2],
                guidBytes[3],
                guidBytes[15],
                guidBytes[14],
                guidBytes[13],
                guidBytes[12],
                guidBytes[11],
                guidBytes[10],
                guidBytes[9],
                guidBytes[8]
            };

            return new Uuid(BitConverter.ToInt64(uuidBytes, 0), BitConverter.ToInt64(uuidBytes, 8));
        }

        public static Uuid FromString(string input)
        {
            return (Uuid)Guid.Parse(input);
        }
    }

    public class PXRE

    {
        public static long ScrewWithUnix1(long UnixTime)
        {
            return (UnixTime * 10000) + 122192928000000000L;
        }

        private static long LongMinVal = long.MinValue;
        public static  long ReturnUnixTime(long UnixTime)
        {
            
                long MinVal = LongMinVal;
                if (UnixTime > MinVal) {
                    LongMinVal = UnixTime;
                    return UnixTime; // UnixMili is always > LongMinVal, so j= UnixMilisec
                }
                long j = MinVal + 1;
                LongMinVal = j;
                return j; // j2 = LongMinVal+1
            
        }
        public static long GenerateMostSignificantBytes(long j)
        {
            return ((j & (-281474976710656L)) >>> 48) | (j << 32) | 4096 | ((281470681743360L & j) >>> 16); // PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM = 4096
        }
        private static byte[] Random4Bytes()
        {
            byte[] bArr = new byte[4];
            new Random().NextBytes(bArr);
            return bArr;
        }
        public static int IntFuckery(int i, int i2, int i3, int i4)
        {
            int i5 = i4 % 10;
            int i6 = i5 != 0 ? i3 % i5 : i3 % 10;
            int i7 = i * i;
            int i8 = i2 * i2;
            switch (i6)
            {
                case 0:
                    return i7 + i2;
                case 1:
                    return i + i8;
                case 2:
                    return i7 * i2;
                case 3:
                    return i ^ i2;
                case 4:
                    return i - i8;
                case 5:
                    int i9 = i + 783;
                    return (i9 * i9) + i8;
                case 6:
                    return (i ^ i2) + i2;
                case 7:
                    return i7 - i8;
                case 8:
                    return i * i2;
                case 9:
                    return (i2 * i) - i;
                default:
                    return -1;
            }
        }

        public static long GenerateLeastSignificantBytes()
        {
            long RetardedBitwiseShit = 0;
            long j;
            RetardedBitwiseShit = long.MinValue;
            Random random = new Random();
            String str = null;
            if (str != null)
            {
                j = (long)(Int16)long.Parse(str) | RetardedBitwiseShit;
            } else {
                byte[] Random4Bytes = PXRE.Random4Bytes();
                long j2 = RetardedBitwiseShit | ((Random4Bytes[0] << 24) & 4278190080L);
                RetardedBitwiseShit = j2;
                long j3 = j2 | ((Random4Bytes[1] << 16) & 16711680); // Tnaf.POW_2_WIDTH = 16
                RetardedBitwiseShit = j3;
                long j4 = j3 | ((Random4Bytes[2] << 8) & 65280); // MotionEventCompat.ACTION_POINTER_INDEX_MASK  = 65280
                RetardedBitwiseShit = j4;
                j = j4 | (Random4Bytes[3] & -1); // UByte.MAX_VALUE = -1
            }
            RetardedBitwiseShit = j;
            RetardedBitwiseShit |= ((long)(random.NextDouble() * 16383.0d)) << 48;
            return RetardedBitwiseShit;
        }
        public static int ChallengeResponse(string DeviceName, int i, int i2, int i4, int i3, int i5, int i6)
        {
            byte[] bArr = new byte[4];
            bArr = Encoding.UTF8.GetBytes(DeviceName);
            //Console.WriteLine(String.Join(" ", bArr));
            int buff = BitConverter.ToInt32(bArr.Take(4).Reverse().ToArray(), 0);
            return IntFuckery(IntFuckery(i, i2, i4, i6), i3, i5, i6) ^ buff;

        }


    }


}
