using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Buffer
{
    public class RingBuffer<T>
    {
        T[] Buffer { get; }

        int ReadCursor { get; set; } = 0;

        int WriteCursor { get; set; } = 0;

        public int Capacity { get; }

        public RingBuffer(int capacity)
        {
            Capacity = capacity;
            Buffer = new T[Capacity];
        }

        public bool Write(T value)
        {
            if (!CanWrite())
                return false;

            WriteCursor = (WriteCursor + 1) % Capacity;
            Buffer[WriteCursor] = value;
            return true;
        }

        public bool Write(IEnumerable<T> values)
        {
            if (!CanWrite(values.Count()))
                return false;

            foreach (var value in values)
            {
                WriteCursor = (WriteCursor + 1) % Capacity;
                Buffer[WriteCursor] = value;
            }

            return true;
        }

        public T Read()
        {
            if (!CanRead())
                throw new InvalidOperationException();

            ReadCursor = (ReadCursor + 1) % Capacity;
            return Buffer[ReadCursor];
        }

        public IEnumerable<T> Read(int count)
        {
            if (!CanRead(count))
                throw new InvalidOperationException();

            for (int i = 0; i < count; i++)
            {
                ReadCursor = (ReadCursor + 1) % Capacity;
                yield return Buffer[ReadCursor];
            }

            yield break;
        }

        public T Seek()
        {
            if (!CanRead())
                return default;

            return Buffer[ReadCursor];
        }

        private bool CanWrite(int howMuch = 1)
        {
            var counter = 1;
            while((WriteCursor + counter) % Capacity != ReadCursor) counter++;
            return counter == howMuch;
        }

        private bool CanRead(int howMuch = 1)
        {
            var counter = 1;
            while ((ReadCursor + counter) % Capacity != WriteCursor) counter++;
            return counter == howMuch;
        }
    }
}
