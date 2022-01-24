namespace CSharpIdCardCheck
{
    public class IdCardCheckTool
    {

        public const int IdCardLength = 18;

        public static int CharToInt(char c)
        {
            if (c >= '0' && c <= '9')
                return int.Parse(c.ToString());
            else if (c == 'X')
                return 10;
            else
                throw new ArgumentOutOfRangeException(nameof(c));
        }

        private static List<char> CheckCodeList{get;set;} = "10X98765432".ToList();

        public char IntToCheckCode(int n)
        {
            if (n > 10 || n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));
            return CheckCodeList[n];
        }

        public bool CheckIdCard(string idCard)
        {
            int count = idCard.Length;

            if (count != IdCardLength)
                throw new ArgumentException("应传递十八位身份", nameof(idCard));

            string s1 = idCard.Substring(0, count - 1);
            string s2 = idCard.Substring(count - 1, 1);

            string checkNO = GetIdCardCheckNO(s1).ToString();

            if (checkNO == s2)
                return true;
            else
                return false;
        }

        public char GetIdCardCheckNO(string idCardWithoutCheckCode)
        {
            int count = idCardWithoutCheckCode.Length;
            if (count != IdCardLength - 1)
                throw new ArgumentException("应传递身份证前十七位", nameof(idCardWithoutCheckCode));

            int sumNum = 0;
            for (int i = 0; i < count; i++)
            {
                var c = idCardWithoutCheckCode[i];
                int n = CharToInt(c);
                var pow = ((int)Math.Pow(2, count - i)) % 11;
                sumNum += n * pow;
            }

            var check_code = IntToCheckCode(sumNum % 11);
            return check_code;
        }
    }

}