namespace Vaeyori.Randomization.UnitTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Vaeyori.Randomization.Abstractions;

    using Xunit;
    using Xunit.Abstractions;

    public class DefaultRandomNumberGeneratorUnitTests
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly ITestOutputHelper _testOutputHelper;

        public DefaultRandomNumberGeneratorUnitTests(ITestOutputHelper testOutputHelper)
        {
            _randomNumberGenerator = new DefaultRandomNumberGenerator();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public Task Constructor_Successful()
        {
            var defaultRandomNumberGenerator = new DefaultRandomNumberGenerator();

            Assert.NotNull(defaultRandomNumberGenerator);

            return Task.CompletedTask;
        }

        [Theory]
        [InlineData(100, 0.0, 1.0, 0.9687746888812514)]
        [InlineData(1000, 0.0, 1.0, 0.15155745910087481)]
        [InlineData(5000, 0.0, 1.0, 0.8528142156325347)]
        [InlineData(10000, 0.0, 1.0, 0.9793851612971095)]
        [InlineData(50000, 0.0, 1.0, 0.9919527266137081)]
        [InlineData(100000, 0.0, 1.0, 0.2576621832594565)]
        [InlineData(500000, 0.0, 1.0, 0.3833378364254431)]
        public async Task GetValueAsync_Double_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum,
            double expectedResult)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(100, 1, 50, 48)]
        [InlineData(500, 1, 50, 47)]
        [InlineData(1000, 1, 50, 8)]
        [InlineData(5000, 1, 50, 42)]
        [InlineData(100, 1, 10, 9)]
        [InlineData(500, 10, 500, 469)]
        public async Task GetValueAsync_MaximumOnly_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum,
            double expectedResult)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, (int)expectedMaximum, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}, >= {expectedMinimum}: {result >= expectedMinimum}, <= {expectedMaximum}: {result <= expectedMaximum}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(100, 1, 50, 48)]
        [InlineData(500, 1, 50, 47)]
        [InlineData(1000, 1, 50, 8)]
        [InlineData(5000, 1, 50, 42)]
        [InlineData(100, 1, 10, 9)]
        [InlineData(500, 10, 500, 470)]
        public async Task GetValueAsync_MinimumAndMaximum_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum,
            double expectedResult)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, (int)expectedMinimum, (int)expectedMaximum, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}, >= {expectedMinimum}: {result >= expectedMinimum}, <= {expectedMaximum}: {result <= expectedMaximum}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
            Assert.Equal(expectedResult, result);
        }


        [Theory]
        [InlineData(100, 51, 50)]
        public async Task GetValueAsync_MinimumAndMaximum_ThrowsArugmentOutOfRange(
            int seed,
            double expectedMinimum,
            double expectedMaximum)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            _ = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _randomNumberGenerator.GetValueAsync(seed, (int)expectedMinimum, (int)expectedMaximum, cancellationTokenSource.Token));
        }
    }
}
