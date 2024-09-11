using System;
using System.Collections.Generic;
using DoubleDCore.OS;
using UnityEditor;

namespace DoubleDEditor.OS.FileDialog
{
    public class EditorFileDialogWindow : IFileDialogWindow
    {
        public string[] OpenFile(string title, string directory, bool isMultiselect = false, params string[] extensions)
        {
            ProcessExtensions(extensions);

            var path = extensions == null
                ? EditorUtility.OpenFilePanel(title, directory, "")
                : EditorUtility.OpenFilePanelWithFilters(title, directory, extensions);

            return string.IsNullOrEmpty(path) ? Array.Empty<string>() : new[] { path };
        }

        public string[] OpenFolder(string title, string directory, bool isMultiselect = false)
        {
            var path = EditorUtility.OpenFolderPanel(title, directory, "");
            return string.IsNullOrEmpty(path) ? Array.Empty<string>() : new[] { path };
        }

        public string SaveFile(string title, string directory, string fileName, string extension)
        {
            extension = GetProcessExtension(extension);
            return EditorUtility.SaveFilePanel(title, directory, fileName, extension);
        }

        private void ProcessExtensions(IList<string> extensions)
        {
            if (extensions is not { Count: > 0 })
                return;

            for (var i = 0; i < extensions.Count; i++)
                extensions[i] = GetProcessExtension(extensions[i]);
        }

        private string GetProcessExtension(string extensions)
            => extensions.Trim().Trim('.');
    }
}