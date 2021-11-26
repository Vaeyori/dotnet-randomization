namespace Vaeyori.Randomization
{
    using Vaeyori.Randomization.Abstractions;

    public class DefaultRandomNumberGenerator : IRandomNumberGenerator
    {
        public ValueTask<double> GetValueAsync(int seed, CancellationToken cancellationToken)
        {
            cancellationToken!.ThrowIfCancellationRequested();

            var random = new Random(seed);

            var result = random.NextDouble();

            return ValueTask.FromResult(result);
        }

        public ValueTask<int> GetValueAsync(int seed, int maximum, CancellationToken cancellationToken)
        {
            return GetValueAsync(seed, 1, maximum, cancellationToken);
        }

        public ValueTask<int> GetValueAsync(int seed, int minimum, int maximum, CancellationToken cancellationToken)
        {
            if (minimum > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(minimum), "Minimum value cannot be greater than maximum value.");
            }

            cancellationToken!.ThrowIfCancellationRequested();

            var random = new Random(seed);

            var result = random.Next(minimum, maximum);

            return ValueTask.FromResult(result);
        }
    }
}
