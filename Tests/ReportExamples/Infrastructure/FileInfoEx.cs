namespace System.Diagnostics;

internal static class FileInfoEx
{
    public static Process? Execute(this FileInfo file, bool UseShellExecute = true)
    {
        var info = new ProcessStartInfo(file.FullName) { UseShellExecute = UseShellExecute };
        return Process.Start(info);
    }
}
