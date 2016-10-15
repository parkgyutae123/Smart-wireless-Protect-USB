using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름 "IService1"을 변경할 수 있습니다.
    [ServiceContract]   //지시자는 아라에 인터페이스가 계약에 사용된다는 것을 알린다.
    public interface IService1
    {
        [OperationContract]
        bool OverlapID(string id);

        [OperationContract]
        bool OverlapPhoneNum(string pnum);

        [OperationContract]
        bool JoinMember(string id, string pw, string name, string email, string pnum);

        [OperationContract]
        bool LoginIDCheck(string id);

        [OperationContract]
        bool LoginPWCheck(string pw);

        [OperationContract]
        bool LoginIDPWCheck(string id, string pw);
    }
}
