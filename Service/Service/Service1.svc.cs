using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 Service1.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class Service1 : IService1
    {
        Project_DB db = Project_DB.GetInstance();


        public bool OverlapID(string id)
        {
            bool re = db.OverlapID(id);
            return re;
        }
        public bool OverlapPhoneNum(string pnum)
        {
            bool re = db.OverlapPhoneNum(pnum) ;
            return re;
        }
        public bool JoinMember(string id, string pw, string name, string email, string pnum)
        {
            bool re = db.JoinMember(id,pw,name,email,pnum,pnum);
            return re;
        }
        public bool LoginIDCheck(string id)
        {
            bool re = db.LoginIDCheck(id);
            return re;
        }
        public bool LoginPWCheck(string pw)
        {
            bool re = db.LoginPWCheck(pw);
            return re;
        }
        public bool LoginIDPWCheck(string id, string pw)
        {
            bool re = db.LoginIDPWCheck(id,pw);
            return re;
        }
    }
}
