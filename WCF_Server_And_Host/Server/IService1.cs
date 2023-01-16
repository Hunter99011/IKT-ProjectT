using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace Server
{

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/CarListDB/"
            )]
        List<Car> CarListDB();

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/CarListDBSpec?Name={Name}"
            )]
        List<Car> CarListDBSpec(string name);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/CarPostDB/"
            )]
        string CarPostDB(Car car);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarData/"
            )]
        Car OneCarGet();

        [OperationContract]

        Car OneCarGetCS();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarPost/"
            )]
        Car OneCarPost();

        [OperationContract]

        Car OneCarPostCS();

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/Cars/"
            )]
        List<Car> CarList();

        [OperationContract]

        List<Car> CarListCS();

        [OperationContract]

        string OneCarAddCS(Car car);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarAdd"
            )]

        string OneCarAdd(Car car);

        [OperationContract]

        string OneCarPutCS(Car car);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarModify"
            )]

        string OneCarPut(Car car);

        [OperationContract]

        string OneCarPatchCS(Car car);

        [OperationContract]
        [WebInvoke(Method = "PATCH",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarModify2"
            )]

        string OneCarPatch(Car car);

        [OperationContract]

        string OneCarDeleteCS(int ID);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarDelete"
            )]

        string CarDeleteDB(int ID);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/OneCarDelete?ID={ID}"
            )]

        string CarDeleteDBID(int ID);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/CarPutDB/"
            )]
        string CarPutDB(Car car);

        [OperationContract]

        string CarPutDBCS(Car car);

        [OperationContract]
        string CarPostDBCS(Car car);
    }

    [DataContract]

    public class Record
    {
        [DataMember]

        public int? ID { get; set; }
    }

    [DataContract]

    public class Car : Record
    {
        [DataMember(IsRequired = true)]

        public string Make { get; set; }

        [DataMember(IsRequired = true)]

        public string Model { get; set; }

        [DataMember(IsRequired = true)]

        public int Year { get; set; }

        [DataMember(IsRequired = true)]

        public string Color { get; set; }

        [DataMember(IsRequired = true)]

        public string Vin { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return $"ID: {ID}\n Gyártó: {Make}\n Model neve: {Model}\n Év: {Year}\n Szín: {Color}\n Alvázszám: {Vin}";
        }

    }
}
