using Experian.CurrencyChange.Business;
using Experian.CurrencyChange.Interfaces;

namespace CurrencyChange.Tests
{
    public class UKCurrencyOperationsTests
    {
       
        [Fact]
        public async Task UKCurrencyOperations_Returns_Success()
        {
            // Arrange
            ICurrencyOperations _currencyOperations = new UKCurrencyOperations(5.5m, 20m);
            var expectedRes = new Dictionary<string, decimal>
            {
                { "10", 1m },
                { "2", 2m },
                { "0.5", 1m }
            };


            // Act
            var res = await _currencyOperations.GetChange(); 

            // Assert
            Assert.NotNull(res);
            Assert.Equal(expectedRes, res);
        }
        [Fact]
        public async Task UKCurrencyOperations_NoChange_Returns_Success()
        {
            // Arrange
            ICurrencyOperations _currencyOperations = new UKCurrencyOperations(20m, 20m);
            var expectedRes = new Dictionary<string, decimal>
            {
                { "0", 0m }
                
            };


            // Act
            var res = await _currencyOperations.GetChange();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(expectedRes, res);
        }
        [Fact]
        public void UKCurrencyOperations_PrintOutPut_Success()
        {
            try
            {
                ICurrencyOperations _currencyOperations = new UKCurrencyOperations(5.5m, 20m);
                var expectedRes = new Dictionary<string, decimal>
                    {
                        { "10", 1m },
                        { "2", 2m },
                        { "0.5", 1m }
                    };
                _currencyOperations.PrintOutPut(expectedRes);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}