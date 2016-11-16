using System;
using System.Net.Mail;
using System.Text;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// 메일인증을 위한 클래스
    /// </summary>
    class SendMail
    {
        String SMTPAddress = "smtp.gmail.com";//STMP서버주소
        String SMTPid = "top1432@gmail.com";//계정 아이디
        String SMTPpassword = "1q2w3e4r!@#$";//계정 비밀번호
        String Tmail = null;//받는사람 메일

        String senderID = "USB_Lock";
        String SenderName = "메일인증";
        String Tsub = "인증메일 입니다.";
        public String Tbody { get; set; }


        /// <summary>
        /// 메일인증 생성자
        /// </summary>
        /// <param name="tmail">받는사람 메일주소</param>
        public SendMail(String tmail)
        {
            Tmail = tmail;
            Tbody = RandomNum();
        }

        /// <summary>
        /// 메일 보내기
        /// </summary>
        /// <returns>성공과 에러 반환</returns>
        public String SendGmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTPAddress, 587);
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.From = new MailAddress(senderID, SenderName, System.Text.Encoding.UTF8);
                mail.To.Add(Tmail);//보내는 사람 메일
                mail.Subject = Tsub;// 메일 제목
                mail.Body = Tbody;// 메일 내용

                //한글화
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPid, SMTPpassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                mail.Dispose();
                return "메일발송을 하였습니다.";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        /// <summary>
        /// 인증번호 생성
        /// </summary>
        /// <returns>인증번호</returns>
        private String RandomNum()
        {
            StringBuilder ran = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                int n = (int)r.Next(1, 10);
                ran.Append(n.ToString());
            }
            return ran.ToString();
        }
      

    }
}
