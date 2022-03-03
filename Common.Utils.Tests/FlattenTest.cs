using Xunit;
using System;
using Utils.ExtensionMethods.IEnumerable;
using System.Linq;

namespace Common.Utils.Tests
{
    public class FlattenTest
    {
        [Fact]
        public void Test_Flatten_Array_With_Fixed_Input()
        {
            // Arrange
            var toBeFlatted = new object[] 
            { 
                1,
                2,
                new object[] 
                { 
                    3, 
                    4, 
                    new int[]
                    { 
                        5, 
                        6 
                    } 
                },
                new int[] 
                { 
                    9, 
                    10 
                },
                new object[]
                {
                    new object[]
                    {
                        new object[]
                        {
                            new object[]
                            {
                                12
                            }
                        }
                    }
                }
            };

            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Equal(9, flatted.Count());
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 9, 10, 12 };
            Assert.True(expected.SequenceEqual(flatted));
        }

        [Fact]
        public void Test_Flatten_Simple_Array_No_Nesting_Input()
        {
            // Arrange
            var toBeFlatted = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Equal(toBeFlatted.Length, flatted.Count());
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.True(expected.SequenceEqual(flatted));
        }

        [Fact]
        public void Test_Flatten_Simple_Array_With_Mixed_Input()
        {
            // Arrange
            var toBeFlatted = new object[] { 1, 2, 3, 4, "d", "e", 7, 8, "x", 10 };

            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Equal(7, flatted.Count());
            var expected = new int[] { 1, 2, 3, 4, 7, 8, 10 };
            Assert.True(expected.SequenceEqual(flatted));
        }

        [Fact]
        public void Test_Flatten_Simple_Array_With_No_Requested_Input()
        {
            // Arrange
            var toBeFlatted = new object[] 
            { 
                "d", 
                "e", 
                "x", 
                new object[] 
                { 
                    "eee", 
                    DateTime.Now, 
                    new object[] 
                    { 
                        new object[] { "hello" } 
                    } 
                } 
            };

            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Empty(flatted);
        }

        [Fact]
        public void Test_Flatten_Array_Single_Element_Input()
        {
            // Arrange
            var toBeFlatted = new object[] { 1 };

            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Equal(toBeFlatted.Length, flatted.Count());
        }

        [Theory]
        [InlineData(20, 3, 3)]
        [InlineData(50, 5, 3)]
        [InlineData(150, 5, 5)]
        [InlineData(500, 5, 5)]
        [InlineData(1000, 18, 9)]
        [InlineData(10000, 100, 12)]
        [InlineData(100000, 1000, 100)]
        public void Flatten_Random_Input(int length, int maxNestedLenght, int maxDeep)
        {
            // Arrange
            var (toBeFlatted, expected) = UnitTestHelpers.GenerateRandomNestedInput(length, maxNestedLenght, maxDeep);
            
            // Act
            var flatted = toBeFlatted.Flatten<int>();

            // Assert
            Assert.NotNull(flatted);
            Assert.Equal(length, flatted.Count());
            Assert.True(expected.SequenceEqual(flatted));
        }
    }
}
