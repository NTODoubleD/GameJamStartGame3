using UnityEngine;

namespace DoubleDCore.OS
{
    public class MockFileDownloader : IFileDownloader
    {
        public void DownloadFile(string filename, string content)
        {
            Debug.Log($"File {filename} downloaded");
        }
    }
}