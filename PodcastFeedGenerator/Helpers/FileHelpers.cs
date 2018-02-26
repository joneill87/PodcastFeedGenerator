using NAudio.Wave;
using NLayer.NAudioSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastFeedGenerator.Helpers
{
    public class FileHelpers
    {
        public class Mp3Meta
        {
            public int LengthInSeconds { get; set; }
            public int FileSizeInBytes { get; set; }
            public string MimeType { get; set; }
        }

        private static TimeSpan GetAudioFileLength(HttpPostedFileBase fileBase)
        {
            var builder = new Mp3FileReader.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            using (var mp3Reader = new Mp3FileReader(fileBase.InputStream, builder))
            {
                return mp3Reader.TotalTime;
            }
        }

        public static Mp3Meta GetMp3Meta(HttpPostedFileBase file)
        {
            //item.FileSizeInBytes = model.File.ContentLength;
            //item.LengthInSeconds = Convert.ToInt32(GetAudioFileLength(model.File).TotalSeconds);
            //item.MimeType = model.File.ContentType;
            //item.FileURL = new FTPClient().UploadAudioFile(model.File);
            return new Mp3Meta()
            {
                FileSizeInBytes = file.ContentLength,
                LengthInSeconds = System.Convert.ToInt32(GetAudioFileLength(file).TotalSeconds),
                MimeType = file.ContentType
            };
        }
    }
}