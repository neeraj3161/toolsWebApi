using toolsWebApi.IServices;

namespace toolsWebApi.Services
{
    public class GenerateOtp : IGenerateOtp
    {
        int IGenerateOtp.GenerateOtp(int length)
        {
            string start = "1";
            string end = "9";
            for (int i = 1; i < length; i++) 
            {
                string startValue = "1";
                string endValue = "9";
                start = start+startValue;  
                end = end+endValue;
            }

            Random random = new Random();

            int otp= random.Next(Int32.Parse(start),Int32.Parse(end));


            return otp;
        }
    }
}
