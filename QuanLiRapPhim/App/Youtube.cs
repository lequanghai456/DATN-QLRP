using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Upload;
//using Google.Apis.Util.Store;
//using Google.Apis.YouTube.v3;
//using Google.Apis.YouTube.v3.Data;
//using QuanLiRapPhim.Areas.Admin.Models;

namespace QuanLiRapPhim.App
{
    public class Youtube
    {
        //public static async Task<bool> Run(Movie movie)
        //{
        //    //Chứng chỉ
        //    UserCredential credential;
        //    using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        //    {
        //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            // This OAuth 2.0 access scope allows an application to upload files to the
        //            // authenticated user's YouTube channel, but doesn't allow other types of access.
        //            new[] { YouTubeService.Scope.YoutubeUpload },
        //            "user",
        //            CancellationToken.None
        //        );
        //    }

        //    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
        //    });

        //    var video = new Video();
        //    video.Snippet = new VideoSnippet();
        //    video.Snippet.Title = movie.Title;
        //    video.Snippet.Description = movie.Describe;
        //    video.Snippet.Tags = new string[] { "Trailer" };
        //    video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
        //    video.Status = new VideoStatus();
        //    video.Status.PrivacyStatus = "unlisted"; // or "private" or "public or unlisted"
        //    var filePath = "wwwroot/admin/img/Trailer/" + movie.Trailer; // Replace with path to actual movie file.
        //    try
        //    {
        //        using (var fileStream = new FileStream(filePath, FileMode.Open))
        //        {
        //            var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
        //            await videosInsertRequest.UploadAsync();
        //        }
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        return false;
        //    }

        //}
    }
}
