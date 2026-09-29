// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-23
//   by Toshal Infotech
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sub-license, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//   of the Software.
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//   DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Apparatus
{
    /// <summary>
    /// Helpers for reading a whole <see cref="Stream"/> into memory or copying it.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads the whole stream from the beginning and returns all bytes.
        /// The stream position is reset to the start before reading, so the stream must support seeking.
        /// </summary>
        /// <param name="stream">A seekable, readable stream.</param>
        /// <returns>All bytes in the stream. Empty array for an empty stream.</returns>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        /// <example>
        /// <code>
        /// using var stream = File.OpenRead("data.bin");
        /// byte[] bytes = stream.GetAllBytes();
        /// </code>
        /// </example>
        public static byte[] GetAllBytes([NotNull] this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Reads the whole stream from the beginning and returns all bytes, without blocking the calling thread.
        /// The stream position is reset to the start before reading, so the stream must support seeking.
        /// </summary>
        /// <param name="stream">A seekable, readable stream.</param>
        /// <param name="cancellationToken">Token to cancel the copy.</param>
        /// <returns>A task that returns all bytes in the stream.</returns>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        /// <exception cref="OperationCanceledException">The token was cancelled.</exception>
        /// <example>
        /// <code>
        /// byte[] bytes = await stream.GetAllBytesAsync();
        /// </code>
        /// </example>
        public static async Task<byte[]> GetAllBytesAsync([NotNull] this Stream stream, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                await stream.CopyToAsync(memoryStream, cancellationToken);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Copies the whole stream, from the beginning, to another stream.
        /// The source position is reset to the start before copying. Uses the default 81920 byte buffer.
        /// </summary>
        /// <param name="stream">The source stream. Must support seeking.</param>
        /// <param name="destination">The stream to write to.</param>
        /// <param name="cancellationToken">Token to cancel the copy.</param>
        /// <returns>A task that completes when the copy is done.</returns>
        /// <remarks>
        /// The framework already has <c>Stream.CopyToAsync(Stream, CancellationToken)</c>, and the compiler picks it over this extension
        /// when you write <c>source.CopyToAsync(destination, token)</c>. To use this version (which rewinds the source first),
        /// call it as <c>StreamExtensions.CopyToAsync(source, destination, token)</c>.
        /// </remarks>
        /// <exception cref="NotSupportedException">The source stream does not support seeking.</exception>
        /// <example>
        /// <code>
        /// await StreamExtensions.CopyToAsync(source, destination, CancellationToken.None);
        /// </code>
        /// </example>
        public static Task CopyToAsync(this Stream stream, Stream destination, CancellationToken cancellationToken)
        {
            stream.Position = 0;
            return stream.CopyToAsync(
                destination,
                81920, //this is already the default value, but needed to set to be able to pass the cancellationToken
                cancellationToken
            );
        }

    }
}
