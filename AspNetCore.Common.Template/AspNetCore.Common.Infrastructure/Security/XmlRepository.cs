using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace AspNetCore.Common.Infrastructure.Security
{
    //public class XmlRepository : IXmlRepository
    //{
    //    private readonly AWSConfig _config;

    //    public XmlRepository(IConfiguration config) {
    //        _config = new AWSConfig(config);
    //    }

    //    public IReadOnlyCollection<XElement> GetAllElements() {
    //        return GetAllElementsCore().Result;
    //    }

    //    public void StoreElement(XElement element, string friendlyName) {
    //        if (element == null) {
    //            throw new ArgumentNullException(nameof(element));
    //        }
    //        StoreElementCore(element, friendlyName);
    //    }

    //    private async Task<List<XElement>> GetAllElementsCore() {
    //        var xEleList = new List<XElement>();
    //        using (var client = new AmazonS3Client(_config.AppKey, _config.AppSecret, _config.Endpoint)) {
    //            var objects = await client.ListObjectsAsync(_config.ProtectionKeysBucketName);
    //            foreach (var obj in objects.S3Objects) {
    //                var s3Object = await client.GetObjectAsync(_config.ProtectionKeysBucketName, obj.Key);
    //                xEleList.Add(XElement.Load(s3Object.ResponseStream));
    //                s3Object.Dispose();
    //            }
    //        }
    //        return xEleList;
    //    }

    //    private async void StoreElementCore(XElement element, string filename) {
    //        using (var client = new AmazonS3Client(_config.AppKey, _config.AppSecret, _config.Endpoint)) {
    //            var putrequest = new PutObjectRequest();
    //            putrequest.BucketName = _config.ProtectionKeysBucketName;
    //            putrequest.Key = filename;
    //            using (var stream = new MemoryStream()) {
    //                element.Save(stream);
    //                putrequest.InputStream = stream;
    //                await client.PutObjectAsync(putrequest);
    //            }
    //        }
    //    }
    //}
}