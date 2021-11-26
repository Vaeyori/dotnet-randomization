namespace Vaeyori.Randomization.Security.Cryptography
{
    using Vaeyori.Randomization.Abstractions;

    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public ValueTask<double> GetValueAsync(int seed, CancellationToken cancellationToken)
        {
            return GetValueAsync(seed, 0.0, 1.0, cancellationToken);
        }

        public ValueTask<int> GetValueAsync(int seed, int maximum, CancellationToken cancellationToken)
        {
            return GetValueAsync(seed, 0, maximum, cancellationToken);
        }

        public ValueTask<int> GetValueAsync(int seed, int minimum, int maximum, CancellationToken cancellationToken)
        {
            if (minimum > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(minimum), "Minimum value cannot be greater than maximum value.");
            }

            return GetValueInternalAsync(seed, minimum, maximum, cancellationToken);
        }

        internal static async ValueTask<int> GetValueInternalAsync(int seed, int minimum, int maximum, CancellationToken cancellationToken = default)
        {
            var result = await GetValueAsync(seed, 0.0, 1.0, cancellationToken).ConfigureAwait(false);

            result *= maximum;

            if (result <= minimum)
            {
                result = minimum;
            }

            return (int)result;
        }

        private static ValueTask<double> GetValueAsync(int seed, double minimum, double maximum, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var random = System.Security.Cryptography.RandomNumberGenerator.Create();
            var buffer = new byte[4];

            random!.GetBytes(buffer);

            var numberGenerated = BitConverter.ToInt32(buffer, 0);
            var result = numberGenerated / (minimum + int.MaxValue);

            var absoluteResult = Math.Abs(result + (maximum * seed));

            var wholeNumber = (int)absoluteResult;
            absoluteResult = Math.Abs((absoluteResult - wholeNumber) + minimum);

            return ValueTask.FromResult(absoluteResult);
        }
    }
}
