﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.R.Actions.Utility {
    /// <summary>
    /// Verifies that R is installed in the folder
    /// specified in settings. If nothing is specified
    /// settings try and find highest version.
    /// </summary>
    public static class RInstallation {
        private static string[] rFolders = new string[] { "MRO", "RRO", "R" };

        public static RInstallData GetInstallationData(string basePath, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion, bool useRegistry = true) {
            string path = string.Empty;
            if (useRegistry) {
                path = RInstallation.GetRInstallPath(basePath);
            }

            if (string.IsNullOrEmpty(path)) {
                foreach (var f in rFolders) {
                    path = TryFindRInProgramFiles(f, minMajorVersion, minMinorVersion, maxMajorVersion, maxMinorVersion);
                    if (!string.IsNullOrEmpty(path)) {
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(path)) {
                return new RInstallData() { Status = RInstallStatus.PathNotSpecified };
            }

            RInstallData data = new RInstallData() { Status = RInstallStatus.OK, Path = path };

            try {
                string rDirectory = Path.Combine(path, @"bin\x64");
                string rDllPath = Path.Combine(rDirectory, "R.dll");
                if (File.Exists(rDllPath)) {
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(rDllPath);
                    int minor, revision;

                    GetRVersionPartsFromFileMinorVersion(fvi.FileMinorPart, out minor, out revision);
                    data.Version = new Version(fvi.FileMajorPart, minor, revision);

                    if (fvi.FileMajorPart < minMajorVersion || fvi.FileMajorPart > maxMajorVersion) {
                        data.Status = RInstallStatus.UnsupportedVersion;
                    } else if (minor < minMinorVersion || minor > maxMinorVersion) {
                        data.Status = RInstallStatus.UnsupportedVersion;
                    }
                } else {
                    data.Status = RInstallStatus.NoRBinaries;
                }
            } catch (ArgumentException aex) {
                data.Status = RInstallStatus.ExceptionAccessingPath;
                data.Exception = aex;
            } catch (IOException ioex) {
                data.Status = RInstallStatus.ExceptionAccessingPath;
                data.Exception = ioex;
            }

            return data;
        }

        public static void GoToRInstallPage() {
            Process.Start("https://mran.revolutionanalytics.com/download/#download");
        }

        /// <summary>
        /// Retrieves path to the installed R engine root folder.
        /// First tries user settings, then 64-bit registry.
        /// </summary>
        public static string GetRInstallPath(string basePath) {
            if (string.IsNullOrEmpty(basePath) || !Directory.Exists(basePath)) {
                basePath = RInstallation.GetLatestEnginePathFromRegistry();
            }
            return basePath;
        }

        /// <summary>
        /// Retrieves path to the installed R engine binaries folder.
        /// R version is retrieved from settings or, af none is set,
        /// highest version is retrieved from registry.
        /// </summary>
        public static string GetBinariesFolder(string basePath) {
            string binFolder = null;
            string installPath = RInstallation.GetRInstallPath(basePath);

            if (!string.IsNullOrEmpty(installPath)) {
                binFolder = Path.Combine(installPath, @"bin\x64");
            }

            return binFolder;
        }

        /// <summary>
        /// Given R minor file version like 10 converts it to R engine minor version.
        /// For example, file may have version 3.10 which means R 3.1.0. In turn,
        /// file version 2.125 means R engine version is 2.12.5.
        /// </summary>
        /// <param name="minorVersion"></param>
        /// <param name="minor"></param>
        /// <param name="revision"></param>
        private static void GetRVersionPartsFromFileMinorVersion(int minorVersion, out int minor, out int revision) {
            revision = minorVersion % 10;
            if (minorVersion < 100) {
                minor = minorVersion / 10;
            } else {
                minor = minorVersion / 100;
            }
        }

        /// <summary>
        /// Retrieves path to the latest (highest version) R installation
        /// from registry. Typically in the form 'Program Files\R\R-3.2.1'
        /// </summary>
        public static string GetLatestEnginePathFromRegistry() {
            string[] installedEngines = GetInstalledEngineVersionsFromRegistry();
            string highestVersionName = string.Empty;
            Version highest = null;

            foreach (string name in installedEngines) {
                Version v = new Version(name);
                if (highest != null) {
                    if (v > highest) {
                        highest = v;
                        highestVersionName = name;
                    }
                } else {
                    highest = v;
                    highestVersionName = name;
                }
            }

            return GetRVersionInstallPathFromRegistry(highestVersionName);
        }

        /// <summary>
        /// Retrieves installed R versions. Returns array of strings
        /// that typically look like 'R-3.2.1' and typically are
        /// subfolders of 'Program Files\R'
        /// </summary>
        public static string[] GetInstalledEngineVersionsFromRegistry() {
            List<string> enginePaths = new List<string>();

            // HKEY_LOCAL_MACHINE\SOFTWARE\R-core
            // HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\R-core
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)) {
                RegistryKey rKey = null;

                try {
                    rKey = hklm.OpenSubKey(@"SOFTWARE\R-core\R");
                    if (rKey == null) {
                        // Possibly 64-bit machine with only 32-bit R installed
                        // This is not supported as we require 64-bit R.
                        // rKey = hklm.OpenSubKey(@"SOFTWARE\Wow6432Node\R-core\R");
                    }
                    if (rKey != null) {
                        return rKey.GetSubKeyNames();
                    }
                } catch (Exception) { } finally {
                    if (rKey != null) {
                        rKey.Dispose();
                    }
                }
            }

            return new string[0];
        }

        private static string GetRVersionInstallPathFromRegistry(string version) {
            // HKEY_LOCAL_MACHINE\SOFTWARE\R-core
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)) {
                using (var rKey = hklm.OpenSubKey(@"SOFTWARE\R-core\R\" + version)) {
                    if (rKey != null) {
                        return rKey.GetValue("InstallPath") as string;
                    }
                }
            }

            return string.Empty;
        }

        public static Version GetRVersionFromFolderName(string folderName) {
            if (folderName.StartsWith("R-")) {
                try {
                    return new Version(folderName.Substring(2));
                } catch (Exception) { }
            }
            return new Version(0, 0);
        }

        private static string TryFindRInProgramFiles(string folder, int minMajorVersion, int minMinorVersion, int maxMajorVersion, int maxMinorVersion) {
            string root = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            string baseRFolder = Path.Combine(root + @"Program Files\", folder);
            List<Version> versions = new List<Version>();
            try {
                foreach (string dir in Directory.EnumerateDirectories(baseRFolder)) {
                    string subFolderName = dir.Substring(baseRFolder.Length + 1);
                    Version v = GetRVersionFromFolderName(subFolderName);
                    if (v.Major >= minMajorVersion &&
                        v.Minor >= minMinorVersion &&
                        v.Major <= maxMajorVersion &&
                        v.Minor <= maxMinorVersion) {
                        versions.Add(v);
                    }
                }
            } catch (IOException) {
                // Don't do anything if there is no RRO installed
            }

            if (versions.Count > 0) {
                versions.Sort();
                Version highest = versions[versions.Count - 1];
                return Path.Combine(baseRFolder, string.Format(CultureInfo.InvariantCulture, "R-{0}.{1}.{2}", highest.Major, highest.Minor, highest.Build));
            }

            return string.Empty;
        }
    }
}
