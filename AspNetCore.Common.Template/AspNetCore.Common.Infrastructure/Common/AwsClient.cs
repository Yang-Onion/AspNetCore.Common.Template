using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
//using Amazon;
//using Amazon.S3;
//using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace AspNetCore.Common.Infrastructure.Common
{
    public class AWSConfig
    {
    //    public AWSConfig(IConfiguration config) {
    //        var configSection = config.GetSection("AWSConfig");
    //        AppKey = configSection["AWSAccessKey"];
    //        AppSecret = configSection["AWSSecretKey"];
    //        Endpoint = RegionEndpoint.CNNorth1;
    //        FileBucketName = configSection["FileBucketName"];
    //        ProtectionKeysBucketName = configSection["ProtectionKeysBucketName"];
    //        FileHostUrl = configSection["S3FileHostUrl"];
    //    }

    //    public string AppKey { get; set; }

    //    public string AppSecret { get; set; }

    //    public RegionEndpoint Endpoint { get; set; }

    //    public string FileBucketName { get; set; }

    //    public string ProtectionKeysBucketName { get; set; }

    //    public string FileHostUrl { get; set; }
    //}

    //public class AwsClient
    //{
    //    #region ctor

    //    public AwsClient(IConfiguration config) {
    //        AWSConfig = new AWSConfig(config);
    //    }

    //    #endregion ctor

    //    public AWSConfig AWSConfig { get; set; }

    //    #region public methods

    //    public async Task<bool> PutObject(string guidKey, Stream stream) {
    //        var request = InitPutRequest(guidKey, stream);
    //        return await Invoke(request);
    //    }

    //    public void PutObjectAsync(string guidKey, Stream stream) {
    //        var request = InitPutRequest(guidKey, stream);
    //        InvokeAsync(request);
    //    }

    //    public string GetObjectUrl(string guidkey) {
    //        var request = InitUrlRequest(guidkey);
    //        using (var client = new AmazonS3Client(AWSConfig.AppKey, AWSConfig.AppSecret, AWSConfig.Endpoint)) {
    //            return client.GetPreSignedURL(request);
    //        }
    //    }

    //    public async Task<Stream> GetObjectStream(string guidkey) {
    //        var request = InitGetRequest(guidkey);
    //        using (var client = new AmazonS3Client(AWSConfig.AppKey, AWSConfig.AppSecret, AWSConfig.Endpoint)) {
    //            var res = await client.GetObjectAsync(request);
    //            return res.ResponseStream;
    //        }
    //    }

    //    public async Task RemoveObject(string guidKey) {
    //        var request = InitDeleteRequest(guidKey);
    //        using (var client = new AmazonS3Client(AWSConfig.AppKey, AWSConfig.AppSecret, AWSConfig.Endpoint)) {
    //            await client.DeleteObjectAsync(request);
    //        }
    //    }

    //    #endregion public methods

    //    #region private methods

    //    private PutObjectRequest InitPutRequest(string key, Stream stream) {
    //        var request = new PutObjectRequest();
    //        request.BucketName = AWSConfig.FileBucketName;
    //        request.CannedACL = S3CannedACL.PublicRead;
    //        request.Key = key;
    //        request.InputStream = stream;
    //        return request;
    //    }

    //    private GetObjectRequest InitGetRequest(string key) {
    //        var request = new GetObjectRequest();
    //        request.BucketName = AWSConfig.FileBucketName;
    //        request.Key = key;
    //        return request;
    //    }

    //    private DeleteObjectRequest InitDeleteRequest(string key) {
    //        var request = new DeleteObjectRequest();
    //        request.BucketName = AWSConfig.FileBucketName;
    //        request.Key = key;
    //        return request;
    //    }

    //    private GetPreSignedUrlRequest InitUrlRequest(string key) {
    //        var request = new GetPreSignedUrlRequest();
    //        request.BucketName = AWSConfig.FileBucketName;
    //        request.Expires = DateTime.Now.AddMinutes(10);
    //        request.Key = key;
    //        return request;
    //    }

    //    private async Task<bool> Invoke(PutObjectRequest request) {
    //        using (var client = new AmazonS3Client(AWSConfig.AppKey, AWSConfig.AppSecret, AWSConfig.Endpoint)) {
    //            var res = await client.PutObjectAsync(request);
    //            return res.HttpStatusCode == HttpStatusCode.OK;
    //        }
    //    }

    //    private void InvokeAsync(PutObjectRequest request) {
    //        using (var client = new AmazonS3Client(AWSConfig.AppKey, AWSConfig.AppSecret, AWSConfig.Endpoint)) {
    //            try {
    //                client.PutObjectAsync(request);
    //            }
    //            catch (Exception ex) {
    //                throw ex;
    //            }
    //        }
    //    }

    //    #endregion private methods
    }
}