using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleGo.OutAPI
{
    static class Program
    {

        [STAThread]
        static int Main()
        {
            var v = new QCloudCOS("1251500567", "ap-chengdu", "AKIDnLFlgDn6iPImLsMCGQ15EKJDxkzontI7", "eSpwKHTRIYTRzz2N7Lz6jtyGJYKVt4Ck", "test001-1251500567");
            v.Upload(@"E:\tmp\u135.png", "/v1/u135.png");
            return 0;
        }
    }
    public class QCloudCOS
    {
        private CosXmlConfig config = null;
        //初始化 QCloudCredentialProvider ，SDK中提供了3种方式：永久密钥 、 临时密钥  、 自定义 
        private QCloudCredentialProvider cosCredentialProvider = null; CosXmlServer cosXml = null;
        private string _bucket = null;
        public QCloudCOS(string appid, string region, string secretId, string secretKey, string bucket)
        {
            _bucket = bucket;
            //初始化 CosXmlConfig 
            //string appid = "1258572948";//设置腾讯云账户的账户标识 APPID
            //string region = "ap-chengdu"; //设置一个默认的存储桶地域
            // string secretId = "AKIDKi1vlbNKDvSqOtPONBsPkfdOyNdhGWxS"; //"云 API 密钥 SecretId";
            // string secretKey = "Vq14wcIe1wt3HNK1ksCMj4LVBlEcgls3"; //"云 API 密钥 SecretKey";

            config = new CosXmlConfig.Builder()
                .SetConnectionTimeoutMs(5000)  //设置连接超时时间，单位 毫秒 ，默认 45000ms
                .SetReadWriteTimeoutMs(5000)  //设置读写超时时间，单位 毫秒 ，默认 45000ms
                .IsHttps(true)  //设置默认 https 请求
                .SetAppid(appid)  //设置腾讯云账户的账户标识 APPID
                .SetRegion(region)  //设置一个默认的存储桶地域
                .SetDebugLog(true)  //显示日志
                .Build();  //创建 CosXmlConfig 对象

            //方式1， 永久密钥
            long durationSecond = 600;  //secretKey 有效时长,单位为 秒
            cosCredentialProvider = new DefaultQCloudCredentialProvider(secretId, secretKey, durationSecond);

            //初始化 CosXmlServer
            cosXml = new CosXmlServer(config, cosCredentialProvider);
        }

        public int Upload(string src, string key)
        {
            try
            {
                string bucket = _bucket;// "examplebucket-1250000000"; //存储桶，格式：BucketName-APPID
                                        // string key = "exampleobject"; //对象在存储桶中的位置，即称对象键.
                string srcPath = src;// @"F:\exampleobject";//本地文件绝对路径
                PutObjectRequest request = new PutObjectRequest(bucket, key, srcPath);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //请求成功
                string r = result.GetResultInfo();
                Console.WriteLine(r);
                return 0;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx.Message);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
            return 1;
        }

    }
}
