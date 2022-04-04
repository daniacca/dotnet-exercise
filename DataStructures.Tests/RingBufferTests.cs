using DataStructure.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructure.Tests
{
    public class RingBufferTests
    {
        [Fact]
        public void Simple_Write_Read_Buffer()
        {
            var buffer = new RingBuffer<int>(50);

            Assert.True(buffer.Write(new int[] { 1, 2, 3, 4, 5 }));
            Assert.Equal(1, buffer.Seek());
        }
    }
}
