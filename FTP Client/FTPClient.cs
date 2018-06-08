using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GamFacturacion.Common
{
    public class FTPClient
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Path { get; set; }
       

        public FTPClient(string username, string password, string path)
        {
            UserName = username;
            Password = password;
            Path = path;
         
        }

        public FtpStatusCode DownloadFile(string fileName, string destinationPath) //descargo fichero de FTP aquí para editar y añadir nuevos valores
        {
            try
            {
                var path = Path + fileName;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@path);
                request.KeepAlive = true;
                request.UsePassive = true;
                request.UseBinary = true;

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(UserName, Password);

                using (var reader = request.GetResponse().GetResponseStream())
                {
                    using (var fileContent = new StreamReader(reader))
                    {
                        using (var localFile = new StreamWriter(File.Create(destinationPath)))
                        {
                            localFile.Write(fileContent.ReadToEnd());
                            localFile.Flush();
                        }
                    }
                }

            }catch(Exception ex)
            {
                return FtpStatusCode.ServiceNotAvailable;
            }

            return FtpStatusCode.CommandOK;
        }

        public FtpStatusCode UploadFile(string localPath, string uploadFileName)
        {
            try
            {
                var path = Path + uploadFileName;

                using (var ftpClient = new WebClient())
                {
                    ftpClient.Credentials = new NetworkCredential(UserName, Password);
                    ftpClient.UploadFile(@path, WebRequestMethods.Ftp.UploadFile, localPath);
                }
            }
            catch
            {
                return FtpStatusCode.ServiceNotAvailable;
            }

            return FtpStatusCode.CommandOK;
        }

        private void DeleteFile(string ftpFilePath)
        {
            try
            {
                var path = ftpFilePath;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@path);
                request.KeepAlive = true;
                request.UsePassive = true;
                request.UseBinary = true;

                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(UserName, Password);

                var reader = request.GetResponse();       
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
