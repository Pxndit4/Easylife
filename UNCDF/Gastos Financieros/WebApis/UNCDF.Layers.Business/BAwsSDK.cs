using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UNCDF.Layers.Models;
using UNCDF.Utilities;

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


    }
}
