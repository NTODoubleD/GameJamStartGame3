namespace DoubleDCore.OS
{
    public interface IFileDialogWindow
    {
        public string[] OpenFile(string title, string directory, bool isMultiselect = false,
            params string[] extensions);

        public string[] OpenFolder(string title, string directory, bool isMultiselect = false);

        public string SaveFile(string title, string directory, string fileName, string extension);

        // public UniTask<string[]> OpenFileAsync(string title, string directory, bool isMultiselect = false,
        //     params string[] extensions);
        //
        // public UniTask<string[]> OpenFolderAsync(string title, string directory, bool isMultiselect = false);
        //
        // public UniTask<string> SaveFileAsync(string title, string directory, string defaultName, string extension);
    }
}