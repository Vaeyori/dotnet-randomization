namespace Vaeyori.Randomization.Abstractions
{
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Returns a floating-point number between 0.0 and 1.0.
        /// </summary>
        /// <param name="seed">An integer that will be used to seed the random number generator.</param>
        /// <param name="cancellationToken">The cancellation token used for the request</param>
        /// <returns></returns>
        ValueTask<double> GetValueAsync(int seed, CancellationToken cancellationToken);

        /// <summary>
        /// Returns an integer between 1 and the value specified as the maximum parameter.
        /// </summary>
        /// <param name="seed">An integer that will be used to seed the random number generator.</param>
        /// <param name="maximum">The maximum value in which to select when randomizing the generation of a number.</param>
        /// <param name="cancellationToken">The cancellation token used for the request</param>
        /// <returns></returns>
        ValueTask<int> GetValueAsync(int seed, int maximum, CancellationToken cancellationToken);


        /// <summary>
        /// Returns an integer between the range specified.
        /// </summary>
        /// <param name="seed">An integer that will be used to seed the random number generator.</param>
        /// <param name="minimum">The minimum value in which to select when randomizing the generation of a number.</param>
        /// <param name="maximum">The maximum value in which to select when randomizing the generation of a number.</param>
        /// <param name="cancellationToken">The cancellation token used for the request</param>
        /// <returns></returns>
        ValueTask<int> GetValueAsync(int seed, int minimum, int maximum, CancellationToken cancellationToken);
    }
}
