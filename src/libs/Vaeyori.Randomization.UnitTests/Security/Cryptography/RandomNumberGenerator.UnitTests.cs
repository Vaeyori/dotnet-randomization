namespace Vaeyori.Randomization.Security.Cryptography.UnitTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Vaeyori.Randomization.Abstractions;

    using Xunit;
    using Xunit.Abstractions;

    public class RandomNumberGeneratorUnitTests
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly ITestOutputHelper _testOutputHelper;

        public RandomNumberGeneratorUnitTests(ITestOutputHelper testOutputHelper)
        {
            _randomNumberGenerator = new RandomNumberGenerator();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public Task Constructor_Successful()
        {
            var randomNumberGenerator = new RandomNumberGenerator();

            Assert.NotNull(randomNumberGenerator);

            return Task.CompletedTask;
        }


        [Theory]
        [InlineData(100, 0.0, 1.0)]
        [InlineData(1000, 0.0, 1.0)]
        [InlineData(5000, 0.0, 1.0)]
        [InlineData(10000, 0.0, 1.0)]
        [InlineData(50000, 0.0, 1.0)]
        [InlineData(100000, 0.0, 1.0)]
        [InlineData(500000, 0.0, 1.0)]
        public async Task GetValueAsync_Double_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
        }

        [Theory]
        [InlineData(100, 0, 50)]
        [InlineData(500, 0, 50)]
        [InlineData(1000, 0, 50)]
        [InlineData(5000, 0, 50)]
        [InlineData(100, 0, 10)]
        [InlineData(0, 0, 500)]
        public async Task GetValueAsync_MaximumOnly_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, (int)expectedMaximum, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}, >= {expectedMinimum}: {result >= expectedMinimum}, <= {expectedMaximum}: {result <= expectedMaximum}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
        }

        [Theory]
        [InlineData(100, 1, 50)]
        [InlineData(500, 1, 50)]
        [InlineData(1000, 1, 50)]
        [InlineData(5000, 1, 50)]
        [InlineData(100, 1, 10)]
        [InlineData(500, 10, 500)]
        [InlineData(0, 0, 500)]
        public async Task GetValueAsync_MinimumAndMaximum_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await _randomNumberGenerator.GetValueAsync(seed, (int)expectedMinimum, (int)expectedMaximum, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}, >= {expectedMinimum}: {result >= expectedMinimum}, <= {expectedMaximum}: {result <= expectedMaximum}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        public async Task GetValueInternalAsync_MinimumAndMaximum_ReturnsValueBetweenRangeSuccessfully(
            int seed,
            double expectedMinimum,
            double expectedMaximum)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var result = await RandomNumberGenerator.GetValueInternalAsync(seed, (int)expectedMinimum, (int)expectedMaximum, cancellationTokenSource.Token);

            _testOutputHelper.WriteLine($"Seed: {seed}, Result: {result}, >= {expectedMinimum}: {result >= expectedMinimum}, <= {expectedMaximum}: {result <= expectedMaximum}");

            Assert.True(result >= expectedMinimum);
            Assert.True(result <= expectedMaximum);
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
