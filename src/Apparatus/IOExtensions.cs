using System.Text;

namespace Apparatus
{
    /// <summary>
    /// Safe helpers for creating and deleting folders. They check first, so they never fail just because the folder is (or is not) already there.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a folder (and any missing parent folders) only if it does not exist yet.
        /// </summary>
        /// <param name="directory">Full or relative path of the folder.</param>
        /// <example>
        /// <code>
        /// DirectoryHelper.CreateIfNotExists(@"C:\temp\reports");
        /// </code>
        /// </example>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Deletes a folder if it exists. The folder must be empty; use the overload with <c>recursive</c> to delete its content too.
        /// </summary>
        /// <param name="directory">Full or relative path of the folder.</param>
        /// <exception cref="IOException">The folder exists but is not empty.</exception>
        /// <example>
        /// <code>
        /// DirectoryHelper.DeleteIfExists(@"C:\temp\empty-folder");
        /// </code>
        /// </example>
        public static void DeleteIfExists(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory);
            }
        }

        /// <summary>
        /// Deletes a folder if it exists, and optionally everything inside it.
        /// </summary>
        /// <param name="directory">Full or relative path of the folder.</param>
        /// <param name="recursive"><c>true</c> to also delete all files and sub-folders; <c>false</c> to delete only an empty folder.</param>
        /// <exception cref="IOException"><paramref name="recursive"/> is <c>false</c> and the folder is not empty.</exception>
        /// <example>
        /// <code>
        /// DirectoryHelper.DeleteIfExists(@"C:\temp\reports", true);
        /// </code>
        /// </example>
        public static void DeleteIfExists(string directory, bool recursive)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive);
            }
        }

        /// <summary>
        /// Creates the folder described by a <see cref="DirectoryInfo"/> only if it does not exist yet.
        /// </summary>
        /// <param name="directory">The folder to create.</param>
        /// <example>
        /// <code>
        /// DirectoryHelper.CreateIfNotExists(new DirectoryInfo(@"C:\temp\reports"));
        /// </code>
        /// </example>
        public static void CreateIfNotExists(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                directory.Create();
            }
        }
    }

    /// <summary>
    /// Helpers for common file tasks: safe delete, file extension and async reading.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Deletes a file if it exists.
        /// </summary>
        /// <param name="filePath">Path of the file.</param>
        /// <returns><c>true</c> if the file existed and was deleted; <c>false</c> if there was nothing to delete.</returns>
        /// <example>
        /// <code>
        /// bool deleted = FileHelper.DeleteIfExists(@"C:\temp\old.log");
        /// </code>
        /// </example>
        public static bool DeleteIfExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            File.Delete(filePath);
            return true;
        }

        /// <summary>
        /// Gets the extension of a file name, without the dot.
        /// </summary>
        /// <param name="fileNameWithExtension">File name or path, for example <c>"report.pdf"</c>. Must not be <c>null</c> or empty.</param>
        /// <returns>
        /// The text after the last dot, for example <c>"pdf"</c>. Returns an empty string if the name ends with a dot.
        /// Returns <c>null</c> if the name has no dot.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileNameWithExtension"/> is <c>null</c> or empty.</exception>
        /// <example>
        /// <code>
        /// FileHelper.GetExtension("archive.tar.gz"); // "gz"
        /// FileHelper.GetExtension("README");         // null
        /// </code>
        /// </example>
        public static string GetExtension(string fileNameWithExtension)
        {
            fileNameWithExtension.ThrowIfNullOrEmpty();

            var lastDotIndex = fileNameWithExtension.LastIndexOf('.');
            if (lastDotIndex < 0)
            {
                return null;
            }

            return fileNameWithExtension.Substring(lastDotIndex + 1);
        }

        /// <summary>
        /// Reads a whole text file into one string, without blocking the calling thread.
        /// </summary>
        /// <param name="path">Path of the file to read.</param>
        /// <returns>The full text of the file.</returns>
        /// <exception cref="FileNotFoundException">The file does not exist.</exception>
        /// <example>
        /// <code>
        /// string text = await FileHelper.ReadAllTextAsync("notes.txt");
        /// </code>
        /// </example>
        public static async Task<string> ReadAllTextAsync(string path)
        {
            using (var reader = File.OpenText(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Reads a whole file into a byte array, without blocking the calling thread.
        /// </summary>
        /// <param name="path">Path of the file to read.</param>
        /// <returns>All bytes of the file.</returns>
        /// <exception cref="FileNotFoundException">The file does not exist.</exception>
        /// <example>
        /// <code>
        /// byte[] data = await FileHelper.ReadAllBytesAsync("image.png");
        /// </code>
        /// </example>
        public static async Task<byte[]> ReadAllBytesAsync(string path)
        {
            using (var stream = File.Open(path, FileMode.Open))
            {
                var result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length);
                return result;
            }
        }

        /// <summary>
        /// Reads a text file line by line, without blocking the calling thread, and returns the lines as an array.
        /// </summary>
        /// <param name="path">Path of the file to read.</param>
        /// <param name="encoding">Text encoding of the file. Default is UTF-8.</param>
        /// <param name="fileMode">How the operating system should open the file. Default is <see cref="FileMode.Open"/>.</param>
        /// <param name="fileAccess">Read or write access. Default is <see cref="FileAccess.Read"/>.</param>
        /// <param name="fileShare">What other readers or writers may do with the file while it is open. Default is <see cref="FileShare.Read"/>.</param>
        /// <param name="bufferSize">Size of the read buffer in bytes. Default is 4096.</param>
        /// <param name="fileOptions">Extra file options. Default is <see cref="FileOptions.Asynchronous"/> and <see cref="FileOptions.SequentialScan"/>.</param>
        /// <returns>One array item per line. Line break characters are not included.</returns>
        /// <exception cref="FileNotFoundException">The file does not exist.</exception>
        /// <example>
        /// <code>
        /// string[] lines = await FileHelper.ReadAllLinesAsync("data.csv");
        /// </code>
        /// </example>
        public static async Task<string[]> ReadAllLinesAsync(string path,
            Encoding encoding = null,
            FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = FileShare.Read,
            int bufferSize = 4096,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var lines = new List<string>();

            using (var stream = new FileStream(
                path,
                fileMode,
                fileAccess,
                fileShare,
                bufferSize,
                fileOptions))
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines.ToArray();
        }

    }
}
