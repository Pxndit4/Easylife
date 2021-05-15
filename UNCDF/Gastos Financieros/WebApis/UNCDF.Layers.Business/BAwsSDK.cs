using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Models;
using UNCDF.Utilities;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Runtime;

namespace UNCDF.Layers.Business
{
    public static class BAwsSDK
    {
        public static async void SendEmailAsync(MAwsEmail mAwsEmail)
        {  
            try
            {
                using (var clients = new AmazonSimpleEmailServiceClient(UEncrypt.Decrypt(mAwsEmail.AccessKey), UEncrypt.Decrypt(mAwsEmail.SecretKey), RegionEndpoint.USWest2))
                {
                    var sendRequest = new SendEmailRequest
                    {
                        Source = mAwsEmail.VerifiedFromEmail,
                        Destination = new Destination
                        {
                            ToAddresses = new List<string> { mAwsEmail.ToEmail }
                        },
                        Message = new Message
                        {
                            Subject = new Content(mAwsEmail.Subject),
                            Body = new Body
                            {
                                Html = new Content(mAwsEmail.Message)
                            }
                        },
                    };

                    try
                    {
                        var response = await clients.SendEmailAsync(sendRequest);
                    }
                    catch (Exception)
                    {
                       
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public static bool UploadS3(MAwsS3 mAwsS3, string localFilePath, string subDirectoryInBucket, string fileNameInS3)
        {
            try
            {
                AWSCredentials credentials;
                credentials = new BasicAWSCredentials(UEncrypt.Decrypt(mAwsS3.AccessKey.Trim()), UEncrypt.Decrypt(mAwsS3.SecretKey.Trim()));
                IAmazonS3 client = new AmazonS3Client(credentials, RegionEndpoint.USEast2);

                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    request.BucketName = UEncrypt.Decrypt(mAwsS3.BucketName); //no subdirectory just bucket name 
                }
                else
                { // subdirectory and bucket name 
                    request.BucketName = UEncrypt.Decrypt(mAwsS3.BucketName) + @"/" + subDirectoryInBucket;
                }

                request.Key = fileNameInS3; //file name up in S3 
                request.FilePath = localFilePath; //local file name
                request.CannedACL = S3CannedACL.PublicRead;
                utility.Upload(request); //commensing the transfer 

                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }


    }
}
