using System;
using System.Collections.Generic;
using System.Text;

namespace UNCDF.Layers.Business
{
    public class Constant
    {
        public const string S3Server = "https://uncdfbucket.s3.us-east-2.amazonaws.com/";
    }

    public class Messages
    {
        public const string ApplicationTokenNoAutorize = "The application is not authorized";
        public const string Success = "Success";
        public const string ErrorPayment = "A problem occurred requesting payment";
        public const string ErrorObtainingReults = "Error when obtaining {0}";
        public const string NotReults = "There is no information on {0}";
        public const string NoExistsSelect = "The requested {0} does not exist";
        public const string ErrorInsert = "Error when inserting the {0}";
        public const string ErrorDelete = "Error when deleting the {0}";
        public const string ErrorUpdate = "Error when updating the {0}";
        public const string ErrorSelect = "Error when obtaining the {0}";
        public const string ErrorLoadPhoto = "An error occurred while uploading the {0} photo";
        public const string ErrorLoadVideo = "An error occurred while uploading the {0} Video";

    }
}
