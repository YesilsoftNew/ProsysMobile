using System;
using System.IO;
using System.IO.Compression;
using WiseDynamicMobile.Helper;

namespace WiseMobile.Helper
{
    public static class ZipFile
    {
        public static String[] QuickZip(string filePath, string fileName, string newFileName)
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                string destinationZipFullPath = filePath + newFileName;

                using (ZipArchive zip = System.IO.Compression.ZipFile.Open(destinationZipFullPath, ZipArchiveMode.Create))
                {
                    FileInfo fiDB = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName));

                    if (fiDB != null)
                    {
                        decimal xDBFileLengtMB = ((TOOLS.ToDecimal(fiDB.Length) / TOOLS.ToDecimal(1024.0)) / TOOLS.ToDecimal(1024.0));

                        if (xDBFileLengtMB < 400)
                            zip.CreateEntryFromFile(fiDB.FullName, fileName, CompressionLevel.Optimal);
                    } 
                }

                Byte[] bytes = File.ReadAllBytes(destinationZipFullPath);
                String file = Convert.ToBase64String(bytes);

                int retval = file.Length / 20;
                int fileLength = file.Length;
                int usedLength = 0;
                String[] arrFile = new String[20];

                for (int i = 0; i < 20; i++)
                {
                    if (i == 19)
                    {
                        arrFile[i] = file.Substring(usedLength, fileLength - usedLength);
                        break;
                    }

                    arrFile[i] = file.Substring(usedLength, retval);

                    usedLength += retval;
                }

                return arrFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
