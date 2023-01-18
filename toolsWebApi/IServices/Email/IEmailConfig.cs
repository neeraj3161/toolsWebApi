namespace toolsWebApi.IServices.Email
{
    public interface IEmailConfig
    {

        void SendOtpVerificationEmail(string emailId, string subject, string body);

        string OtpTemplate(int otp);
    }
}
