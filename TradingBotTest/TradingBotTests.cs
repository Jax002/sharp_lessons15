using Moq;
using TradingBot;

namespace TradingBotTest
{
    [TestClass]
    public class TradingBotTests
    {
        private Mock<IExchangeService> _mockExchange = null!;
        private TradingBot.TradingBot _bot = null!;
        private const string Symbol = "BTCUSDT";

        [TestInitialize]
        public void Setup()
        {
            _mockExchange = new Mock<IExchangeService>();
            _bot = new TradingBot.TradingBot(_mockExchange.Object);
        }

        [TestMethod]
        public void ExecuteStrategy_WhenPriceIs15PercentBelowAverage_ShouldReturnBuy()
        {
            decimal averagePrice = 100m;
            decimal currentPrice = 85m;

            _mockExchange.Setup(x => x.GetCurrentPrice(Symbol))
                         .Returns(currentPrice);
            string result = _bot.ExecuteStrategy(Symbol, averagePrice);

            Assert.AreEqual("Buy", result);
            _mockExchange.Verify(x => x.GetCurrentPrice(Symbol), Times.Once);
        }

        [TestMethod]
        public void ExecuteStrategy_WhenPriceIs15PercentAboveAverage_ShouldReturnSell()
        {
            decimal averagePrice = 100m;
            decimal currentPrice = 115m;

            _mockExchange.Setup(x => x.GetCurrentPrice(Symbol))
                         .Returns(currentPrice);

            string result = _bot.ExecuteStrategy(Symbol, averagePrice);

            Assert.AreEqual("Sell", result);
            _mockExchange.Verify(x => x.GetCurrentPrice(Symbol), Times.Once);
        }

        [TestMethod]
        public void ExecuteStrategy_WhenPriceIsWithin5PercentOfAverage_ShouldReturnHold()
        {
            decimal averagePrice = 100m;
            decimal currentPrice = 103m;

            _mockExchange.Setup(x => x.GetCurrentPrice(Symbol))
                         .Returns(currentPrice);

            string result = _bot.ExecuteStrategy(Symbol, averagePrice);

            Assert.AreEqual("Hold", result);
            _mockExchange.Verify(x => x.GetCurrentPrice(Symbol), Times.Once);
        }
    }
}
